using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DalApi;
using BO;
using System.Collections;
using BlApi;
using DO;
using System.Runtime.CompilerServices;
namespace BL
{
   internal partial class BL: IBl
    {
        private List<DroneToList> DroneList;
        internal IDal Data;  //object of DAL
        public static Random r = new Random();
        public static double[] batteryConfig = new double[] { };
        internal static IBl instance { get; } = new BL();
        public static IBl Instance { get => instance; }
        /// <summary>
        /// constractor of BL
        /// </summary>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]

        internal BL()
        {
            Data = DalFactory.GetDal("DalXml");
            DroneList = new();
            batteryConfig = Data.Consumption();
            //Copies the lists from DAL
            List<DO.Drone> tempDataDrones = new(Data.DisplayDronesList());
            List<DO.Parcel> tempDataParcels = new(Data.PrintParcelList());
            List<DO.Station> tempDataStations = new(Data.PrintStationList());

            //Turns a DAL Drone into a BL Drone
            foreach (DO.Drone item in tempDataDrones)
            {
                DroneToList drone = new();
                drone.Id = item.Id;
                drone.Model = item.Model;
                drone.MaxWeight = (BO.Weight)item.MaxWeight;
                //for all parcels that didn't delivered but associated to a drone
                if (tempDataParcels.Exists(w => w.DroneId == (item.Id) && w.Delivered == null))
                {
                    int i = tempDataParcels.FindIndex(w => w.DroneId == (item.Id));
                    drone.status = BO.StatusOfDrone.Delivery;
                    var sender = Data.PrintCustomer(tempDataParcels[i].SenderId);
                    Location locOfSender = Location(sender.Longitude, sender.Latitude);
                    var target = Data.PrintCustomer(tempDataParcels[i].TargetId);
                    Location locOfTarget = Location(target.Longitude, target.Latitude);
                    double minBattery = Consumption(drone.ThisLocation, locOfSender, ModeOfDroneInMoving.Available)
                        + Consumption(locOfSender, locOfTarget, (ModeOfDroneInMoving)tempDataParcels[i].Weigh)
                        + Consumption(locOfTarget, GetLocationOfStation(GetClosestStation(locOfTarget)), ModeOfDroneInMoving.Available);
                    if (tempDataParcels[i].PickedUp < DateTime.MinValue) //if parcel didn't pick up
                    {
                        Location locOfClosestStation = GetLocationOfStation(GetClosestStation(locOfSender));
                        drone.ThisLocation = locOfClosestStation;
                        drone.Battery = r.Next((int)(minBattery * 1000), 100 * 1000) / 1000;
                    }
                    else  //if parcel picked up but didn't delivered
                    {
                        drone.ThisLocation = locOfSender;
                        drone.Battery = r.Next((int)((minBattery - Consumption(drone.ThisLocation, locOfSender, ModeOfDroneInMoving.Available)) * 1000), 100 * 1000) / 1000;
                    }
                }
                else
                {
                    drone.status = (StatusOfDrone)r.Next(0, 1);
                    if (drone.status == StatusOfDrone.Available)
                    {
                        List<DO.Customer> customers = new();
                        foreach (DO.Parcel par in tempDataParcels)  //create a list of parcels that already provided
                        {
                            if (par.TargetId > 0 && par.Delivered > DateTime.MinValue)
                                customers.Add(Data.PrintCustomer(par.TargetId));
                        }
                        Location loc;
                        if (customers.Count != 0)  //the location of drone is randomize between parcels that delivered
                        {
                            int i = r.Next(0, customers.Count);
                            loc = Location(customers[i].Longitude, customers[i].Latitude);
                        }
                        else  //the location of drone is randomize between stations
                        {
                            int i = r.Next(0, tempDataStations.Count);
                            loc = Location(tempDataStations[i].Longitude, tempDataStations[i].Latitude);
                        }
                        drone.ThisLocation = loc;
                        double con = Consumption(drone.ThisLocation, GetLocationOfStation(GetClosestStation(drone.ThisLocation)), ModeOfDroneInMoving.Available);
                        drone.Battery = r.Next((int)(con * 1000), 100 * 1000) / 1000;
                    }
                    else
                    {
                        int i = r.Next(0, tempDataStations.Count);
                        drone.ThisLocation = Location(tempDataStations[i].Longitude, tempDataStations[i].Latitude);
                        drone.Battery = r.Next(0, 20);
                    }
                }
                DroneList.Add(drone);
            }
        }
    }
}