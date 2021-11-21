using System;
using System.Collections.Generic;
using System.Text;
using IDAL;
using IBL.BO;
using System.Collections;
using IBL;

namespace BL
{
    public partial class BL:IBl
    {
        public List<DroneToList> DroneList;
        IDal Data = new DalObject.DalObject();
        public static Random r = new Random();
        public static double[] batteryConfig = new double[] { };
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