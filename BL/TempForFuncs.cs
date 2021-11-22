using System;
using System.Collections.Generic;
using System.Text;
using IDAL;
using IBL.BO;
using System.Collections;
using IBL;
namespace BL
{
    public partial class BL : IBl
    {
        /// <summary>
        /// the func gets the status of the parcel and acourdingly returns the consumption
        /// </summary>
        /// <param name="a">location a</param>
        /// <param name="b">location b</param>
        /// <param name="mode">mode of the drone in moving: without a parcel, or with a parcel, and which mode of parcel</param>
        /// <returns>how much battery this moving need</returns>
        public double Consumption(Location a, Location b, MODE_OF_DRONE_IN_MOVING mode)
        {
            return GetDistance(a, b) * batteryConfig[(int)mode];
        }
        public (bool,double) GetBatteryUseAndRootFeasibility(IBL.BO.DroneToList dro,IDAL.DO.Parcel prc)
        {
            try
            {
                IDAL.DO.Customer sender = new IDAL.DO.Customer();
                sender = Data.PrintCustomer(prc.SenderId);
                IDAL.DO.Customer Receiver = new IDAL.DO.Customer();
                Receiver = Data.PrintCustomer(prc.TargetId);
                IDAL.DO.Station closeststation = new IDAL.DO.Station();
                Location startingPiont = dro.ThisLocation;
                Location StapingPiont = GetSenderLo(prc);
                Location FinishingPiont = GetReceiverLo(prc);
                closeststation = Data.PrintStation(GetClosestStation(FinishingPiont));
                Location ClosestCarging = new Location(closeststation.Longitude, closeststation.Latitude);
                double batteryUse = Consumption(startingPiont, StapingPiont, IBL.BO.MODE_OF_DRONE_IN_MOVING.AVAILABLE) + Consumption(StapingPiont, FinishingPiont, (IBL.BO.MODE_OF_DRONE_IN_MOVING)prc.Weigh);
                if (dro.Battery - batteryUse < 20)
                {
                    batteryUse += Consumption(FinishingPiont, ClosestCarging, IBL.BO.MODE_OF_DRONE_IN_MOVING.AVAILABLE);
                    if (dro.Battery - batteryUse < 0)
                        return (false, 0);
                }
                return (true, batteryUse);
            }
             catch (IDAL.DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }

        }
        /// <summary>
        /// the func returns location of thr sender 
        /// </summary>
        /// <param name="pr"></param>
        /// <returns></returns>
        public Location GetSenderLo(IDAL.DO.Parcel pr)
        {
            try
            {
                IDAL.DO.Customer cs = Data.PrintCustomer(pr.SenderId);
                Location newloc = new Location(cs.Longitude, cs.Latitude);
                return newloc;
            }
            catch (IDAL.DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }
        /// <summary>
        /// the func returns location of the rtraget
        /// </summary>
        /// <param name="pr"></param>
        /// <returns></returns>
        public Location GetReceiverLo(IDAL.DO.Parcel pr)
        {
            try
            {
                IDAL.DO.Customer cs = Data.PrintCustomer(pr.TargetId);
                Location newloc = new Location(cs.Longitude, cs.Latitude);
                return newloc;
            }
            catch (IDAL.DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public IEnumerable<DroneToList> BLDrones()
        {
            return DroneList;
        }

        /// <summary>
        /// the func hase  a option
        /// to get the distenc with difrrentn pararmeters
        /// </summary>
        /// <param name="a">if its jest a location to a location</param>
        /// <param name="b"></param>
        /// <param name="longA">if we wont to mack the disenc with a long and lat</param>
        /// <param name="latA"></param>
        /// <param name="longB"></param>
        /// <param name="latB"></param>
        /// <returns></returns>
        public double GetDistance(Location a, Location b, double longA = 0, double latA = 0, double longB = 0, double latB = 0)
        {
            if (a.Latitude == 0)
            {
                a.Latitude = latA;
                a.Longitude = longA;
            }
            if (b.Latitude == 0)
            {
                b.Latitude = latB;
                b.Longitude = longB;
            }
            return Math.Sqrt((Math.Pow(a.Longitude - b.Longitude, 2) + Math.Pow(a.Latitude - b.Latitude, 2)));
        }

        /// <summary>
        /// return the ID of the closest station with ready stands to the location
        /// </summary>
        /// <param name="a">the location we want to get it closest station</param>
        /// <returns>ID of the closest station</returns>
        public int GetClosestStation(Location a)
        {
            try
            {
                int i = 0;
                int closestID = 0;
                double minimum = 0;
                List<IDAL.DO.Station> tempDataStations = new(Data.PrintStationList());
                List<Station> stationsBL = new();
                foreach (IDAL.DO.Station station in tempDataStations)
                {
                    stationsBL.Add(new Station(station));
                }
                if (stationsBL.Count == 0)
                    return closestID;
                while (i != stationsBL.Count)
                {
                    if (stationsBL[i].ReadyStandsInStation > 0)
                    {
                        closestID = stationsBL[0].Id;
                        minimum = GetDistance(a, stationsBL[0].location);
                        break;
                    }
                }
                if (i == stationsBL.Count)
                    throw new BlException("There is no station to charge");
                for (; i < stationsBL.Count; i++)
                {
                    if (minimum > GetDistance(a, stationsBL[i].location) && stationsBL[i].ReadyStandsInStation > 0)
                    {
                        closestID = stationsBL[i].Id;
                        minimum = GetDistance(a, stationsBL[i].location);
                    }
                }
                return closestID;
            }
            catch (IDAL.DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// get ID of a station and return it location
        /// </summary>
        /// <param name="ID">ID of station</param>
        /// <returns>location of station</returns>
        public Location GetLocationOfStation(int ID)
        {
            try
            {
                List<IDAL.DO.Station> tempDataStations = new(Data.PrintStationList());
                int i = tempDataStations.FindIndex(w => w.Id == ID);
                Location loc = new(tempDataStations[i].Longitude, tempDataStations[i].Latitude);
                return loc;
            }
            catch (IDAL.DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public Station BLStation(IDAL.DO.Station s)
        {
            try
            {
                Station station = new();
                station.Id = s.Id;
                station.Name = s.Name;
                station.location.Longitude = s.Longitude;
                station.location.Latitude = s.Latitude;
                return station;
            }
            catch (IDAL.DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }
    }

}
