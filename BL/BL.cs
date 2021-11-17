﻿using System;
using System.Collections.Generic;
using System.Text;
using IDAL;
using IBL.BO;
using System.Collections;

namespace BL
{
    public partial class BL: IBL
    {
        List<DroneToList> DroneList;
        List<Station> StationsList;
        IDal Data = new DalObject.DalObject();
        //for(iterator<List> it = DalObject1.)
        public static Random r = new Random();
        public static double[] batteryConfig = new double[] { };
        /// <summary>
        /// return the distance between two locations
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double GetDistance(Location a, Location b)
        {
            return Math.Sqrt((Math.Pow(a.Longitude - b.Longitude, 2) + Math.Pow(a.Latitude - b.Latitude, 2)));
        }
        /// <summary>
        /// return the location of the closest station to the drone
        /// </summary>
        /// <param name="a"></param> 
        /// <param name="stations"></param>
        /// <returns></returns>
        public  Location GetMinimumDistance(Location a, List<Station> stations)
        {
            Location loc;
            if (stations.Count == 0)
            {
                loc.Longitude = -1;
                loc.Latitude = -1;
                return loc;
            }
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
        public double DistanceOfRout(Location lo, List<Location> lislo)
        {
            double distance =GetDistance(lo,lislo[0]);
            Location tl = lislo[0];
            foreach(Location item in lislo)
            {
                distance += GetDistance(tl, item);
                tl = item;
            }
            return distance;
        }

        template<class T>
        public static Location GetMinimumDistance1(Location a, List<T> myList)
        {
            Location loc;
            if (myList.Count == 0)
            {
                loc.Longitude = -1;
                loc.Latitude = -1;
                return loc;
            }
            double minimum = GetDistance(a, myList[0].location);
            loc = myList[0].location;
            for (int i = 1; i < myList.Count; i++)
            {
                if (minimum < GetDistance(a, myList[i].location))
                {
                    minimum = GetDistance(a, myList[i].location);
                    loc = myList[i].location;
                }
                Math.Min(minimum, GetDistance(a, myList[i].location));
            }
            return loc;
        }


        BL()
        {
            batteryConfig = Data.Consumption();
            List<IDAL.DO.Drone> tempDataDrone = new List<IDAL.DO.Drone>(Data.PrintDroneList());
            List<IDAL.DO.Parcel> tempDataParcels = new List<IDAL.DO.Parcel>(Data.PrintParcelList());
            List<IDAL.DO.Station> tempDataStations = new List<IDAL.DO.Station>(Data.PrintStationList());
            for(int i=0;i<tempDataStations.Count;i++)
            {
                StationsList.Add(new Station(tempDataStations[i]));
            }
            for (int i = 0; i < tempDataDrone.Count; i++)
            {
                DroneList.Add(new DroneToList(tempDataDrone[i]));
            }
            foreach (IDAL.DO.Drone item in tempDataDrone)
            {
              
                 if (tempDataParcels.Exists(w => w.DroneId == (item.Id)&&w.Delivered<DateTime.MinValue))
                 {
                        int i = tempDataParcels.FindIndex(w => w.DroneId == (item.Id));
                        DroneToList dron = new DroneToList(item);
                        dron.status = IBL.BO.STATUS_OF_DRONE.DELIVERY;
                        if (tempDataParcels[i].PickedUp < DateTime.MinValue)
                        {
                            IDAL.DO.Customer sender = Data.PrintCustomer(tempDataParcels[i].SenderId);
                            Location corent = new Location(sender.Longitude, sender.Latitude);
                            Location Loca = GetMinimumDistance(corent, StationsList);
                            dron.ThisLocation = Loca;

                        }



                 }             
           
            }
 
            for (int i = 0; i < DroneList.Count; i++)
            {
           
                }
                switch ()

                {
                    case STATUS_OF_DRONE.IN_MAINTENANCE:
                        {
                            DroneList[i].ThisLocation.Longitude = r.Next(0, 30);
                            DroneList[i].ThisLocation.Latitude = r.Next(0, 30);
                            DroneList[i].Battery = r.Next(0, 20);
                            break;
                        }
                    case STATUS_OF_DRONE.AVAILABLE:
                        {
                            DroneList[i].ThisLocation = r.Next;

                            DroneList[i].Battery = r.Next(, 100);
                            break;
                        }
                }
            }
        }
    }
}