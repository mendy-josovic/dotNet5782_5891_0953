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
        //List<IDAL.DO.Drone> tempDataDrones;
        //List<IDAL.DO.Parcel> tempDataParcels;
        //List<IDAL.DO.Station> tempDataStations;
        public List<DroneToList> DroneList;
        IDal Data = new DalObject.DalObject();
        public static Random r = new Random();
        public static double[] batteryConfig = new double[] { };

        /// <summary>
        /// return the distance between two locations
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public double GetDistance(Location a, Location b)
        {
            return Math.Sqrt((Math.Pow(a.Longitude - b.Longitude, 2) + Math.Pow(a.Latitude - b.Latitude, 2)));
        }

        /// <summary>
        /// return the ID of the closest station with ready stands to the location
        /// </summary>
        /// <param name="a">the location we want to get it closest station</param>
        /// <returns>ID of the closest station</returns>
        public int GetClosestStation(Location a)
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
                if(stationsBL[i].ReadyStandsInStation > 0)
                {
                    closestID = stationsBL[0].Id;
                    minimum = GetDistance(a, stationsBL[0].location);
                    break;
                }
            }
            if (i == stationsBL.Count)
                throw "There is no station to charge";
            for(; i < stationsBL.Count; i++)
            {
                if (minimum > GetDistance(a, stationsBL[i].location) && stationsBL[i].ReadyStandsInStation > 0)
                {
                    closestID = stationsBL[i].Id;
                    minimum = GetDistance(a, stationsBL[i].location);
                }
            }
            return closestID;
        }

        /// <summary>
        /// get ID of a station and return it location
        /// </summary>
        /// <param name="ID">ID of station</param>
        /// <returns>location of station</returns>
        public Location GetLocationOfStation(int ID)
        {
            List<IDAL.DO.Station> tempDataStations = new(Data.PrintStationList());
            int i = tempDataStations.FindIndex(w => w.Id == ID);
            Location loc = new(tempDataStations[i].Longitude, tempDataStations[i].Latitude);
            return loc;
        }

        public BL()
        {
            batteryConfig = Data.Consumption();
            List<IDAL.DO.Drone> tempDataDrones = new(Data.PrintDroneList());
            List<IDAL.DO.Parcel> tempDataParcels = new(Data.PrintParcelList());
            List<IDAL.DO.Station> tempDataStations = new(Data.PrintStationList());

            foreach (IDAL.DO.Drone item in tempDataDrones)
            {
                DroneToList drone = new DroneToList(item);
                if (tempDataParcels.Exists(w => w.DroneId == (item.Id) && w.Delivered < DateTime.MinValue))
                {
                    int i = tempDataParcels.FindIndex(w => w.DroneId == (item.Id));
                    drone.status = IBL.BO.STATUS_OF_DRONE.DELIVERY;
                    var sender = Data.PrintCustomer(tempDataParcels[i].SenderId);
                    Location locOfSender = new Location(sender.Longitude, sender.Latitude);
                    var target = Data.PrintCustomer(tempDataParcels[i].TargetId);
                    Location locOfTarget = new Location(target.Longitude, target.Latitude);
                    double minBattery = Consumption(drone.ThisLocation, locOfSender, MODE_OF_DRONE_IN_MOVING.AVAILABLE)
                        + Consumption(locOfSender, locOfTarget, (MODE_OF_DRONE_IN_MOVING)tempDataParcels[i].Weigh)
                        + Consumption(locOfTarget, GetLocationOfStation(GetClosestStation(locOfTarget)), MODE_OF_DRONE_IN_MOVING.AVAILABLE);
                    if (tempDataParcels[i].PickedUp < DateTime.MinValue)
                    {
                        Location locOfClosestStation = GetLocationOfStation(GetClosestStation(locOfSender));
                        drone.ThisLocation = locOfClosestStation;
                        drone.Battery = r.Next((int)(minBattery * 1000), 100 * 1000) / 1000;
                    }
                    else
                    {
                        drone.ThisLocation = locOfSender;
                        drone.Battery = r.Next((int)((minBattery - Consumption(drone.ThisLocation, locOfSender, MODE_OF_DRONE_IN_MOVING.AVAILABLE)) * 1000), 100 * 1000) / 1000;
                    }
                }
                else
                {
                    drone.status = (STATUS_OF_DRONE)r.Next(0, 1);
                    if (drone.status == STATUS_OF_DRONE.AVAILABLE)
                    {
                        List<IDAL.DO.Customer> customers = new List<IDAL.DO.Customer>();
                        foreach (IDAL.DO.Parcel par in tempDataParcels)
                        {
                            if (par.TargetId > 0 && par.Delivered > DateTime.MinValue)
                                customers.Add(Data.PrintCustomer(par.TargetId));
                        }
                        int i = r.Next(0, customers.Count);
                        Location loc = new Location(customers[i].Longitude, customers[i].Latitude);
                        drone.ThisLocation = loc;
                        double con = Consumption(drone.ThisLocation, GetLocationOfStation(GetClosestStation(drone.ThisLocation)), MODE_OF_DRONE_IN_MOVING.AVAILABLE);
                        drone.Battery = r.Next((int)(con * 1000), 100 * 1000) / 1000;
                    }
                    else
                    {
                        int i = r.Next(0, tempDataStations.Count);
                        drone.ThisLocation = new(tempDataStations[i].Longitude, tempDataStations[i].Latitude);
                        drone.Battery = r.Next(0, 20);
                    }
                }
                DroneList.Add(drone);
            }
        }
    }
}