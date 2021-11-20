using System;
using System.Collections.Generic;
using System.Text;
using IDAL;
using IBL.BO;
using System.Collections;
using IBL;

namespace BL
{
    public partial class BL: IBl
    {
        List<DroneToList> DroneList;
        List<Station> StationsList;
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
        /// return the location of the closest station to the drone
        /// </summary>
        /// <param name="a"></param> 
        /// <param name="stations"></param>
        /// <returns></returns>
        public Location GetMinimumDistance(Location a, List<Station> stations)
        {
            Location loc = new Location(-1, -1);
            if (stations.Count == 0)
                return loc;
            double minimum = GetDistance(a, stations[0].location);
            loc = stations[0].location;
            for (int i = 1; i < stations.Count; i++)
            {
                if (minimum < GetDistance(a, stations[i].location))
                {
                    minimum = GetDistance(a, stations[i].location);
                    loc = stations[i].location;
                }
                Math.Min(minimum, GetDistance(a, stations[i].location));
            }
            return loc;
        }

        public BL()
        {
            batteryConfig = Data.Consumption();
            List<IDAL.DO.Drone> tempDataDrones = new List<IDAL.DO.Drone>(Data.PrintDroneList());
            List<IDAL.DO.Parcel> tempDataParcels = new List<IDAL.DO.Parcel>(Data.PrintParcelList());
            List<IDAL.DO.Station> tempDataStations = new List<IDAL.DO.Station>(Data.PrintStationList());

            for (int i = 0; i < tempDataStations.Count; i++)
            {
                StationsList.Add(new Station(tempDataStations[i]));
            }

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
                        + Consumption(locOfTarget, GetMinimumDistance(locOfTarget, StationsList), MODE_OF_DRONE_IN_MOVING.AVAILABLE);
                    if (tempDataParcels[i].PickedUp < DateTime.MinValue)
                    {
                        Location locOfClosestStation = GetMinimumDistance(locOfSender, StationsList);
                        drone.ThisLocation = locOfClosestStation;
                        drone.Battery = r.Next((int)minBattery, 100);
                    }
                    else
                    {
                        drone.ThisLocation = locOfSender;
                        drone.Battery = r.Next((int)(minBattery - Consumption(drone.ThisLocation, locOfSender, MODE_OF_DRONE_IN_MOVING.AVAILABLE)), 100);
                    }
                }
                else
                {
                    drone.status = (STATUS_OF_DRONE)r.Next(0, 1);
                    if(drone.status == STATUS_OF_DRONE.AVAILABLE)
                    {
                        List<IDAL.DO.Customer> customers = new List<IDAL.DO.Customer>();
                        foreach(IDAL.DO.Parcel par in tempDataParcels)
                        {
                            if (par.TargetId > 0 && par.Delivered > DateTime.MinValue)
                                customers.Add(Data.PrintCustomer(par.TargetId));
                        }
                        int i = r.Next(0, customers.Count);
                        Location loc = new Location(customers[i].Longitude, customers[i].Latitude);
                        drone.ThisLocation = loc;
                        int con = Consumption(drone.ThisLocation, GetMinimumDistance(drone.ThisLocation, StationsList), MODE_OF_DRONE_IN_MOVING.AVAILABLE);
                        drone.Battery = r.Next(con, 100);
                    }
                    else
                    {
                        int i = r.Next(0, StationsList.Count);
                        drone.ThisLocation = StationsList[i].location;
                        drone.Battery = r.Next(0, 20);
                    }
                }
                DroneList.Add(drone);
            }
        }
    }
}