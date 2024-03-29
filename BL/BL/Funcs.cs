﻿using BlApi;
using BO;
using System;
using System.Linq;
using System.Collections.Generic;
using DalApi;
using DO;
using System.Runtime.CompilerServices;

namespace BL
{
    internal partial class BL : IBl
    {
        /// <summary>
        /// the func gets the status of the parcel and acourdingly returns the consumption
        /// </summary>
        /// <param name="a">location a</param>
        /// <param name="b">location b</param>
        /// <param name="mode">mode of the drone in moving: without a parcel, or with a parcel, and which mode of parcel</param>
        /// <returns>how much battery this moving need</returns>
        /// 

        public double Consumption(Location a, Location b, ModeOfDroneInMoving mode)
        {
            return GetDistance(a, b) * batteryConfig[(int)mode];
        }


        [MethodImpl(MethodImplOptions.Synchronized)]

        public bool GetBatteryUseAndRootFeasibility(BO.DroneToList dro,DO.Parcel prc)
        {
            lock (Data)
            {
                try
                {
                    Location startingPiont = dro.ThisLocation;//strat
                    Location StapingPiont = GetSenderLo(prc);//stoping piont
                    Location FinishingPiont = GetReceiverLo(prc);
                    DO.Customer sender = Data.PrintCustomer(prc.SenderId);
                    DO.Customer Receiver = Data.PrintCustomer(prc.TargetId);
                    DO.Station closeststation = Data.DisplayStation(GetClosestStation(FinishingPiont));
                    Location ClosestCarging = Location(closeststation.Longitude, closeststation.Latitude);
                    double batteryUse = Consumption(startingPiont, StapingPiont, BO.ModeOfDroneInMoving.Available) + Consumption(StapingPiont, FinishingPiont, (BO.ModeOfDroneInMoving)prc.Weigh) + Consumption(FinishingPiont, ClosestCarging, BO.ModeOfDroneInMoving.Available);//geting how much baterry is used in a root
                    if (dro.Battery - batteryUse < 0)
                    {                   
                            return false;
                    }
                    return true;
                }
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }
            }

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Location GetSenderLo(DO.Parcel pr)
        {
            lock (Data)
            {
                try
                {
                    DO.Customer cs = Data.PrintCustomer(pr.SenderId);
                    Location newloc = Location(cs.Longitude, cs.Latitude);
                    return newloc;
                }
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Location GetReceiverLo(DO.Parcel pr)
        {
            lock (Data)
            {
                try
                {
                    DO.Customer cs = Data.PrintCustomer(pr.TargetId);
                    Location newloc = Location(cs.Longitude, cs.Latitude);
                    return newloc;
                }
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> BLDrones(Predicate<DroneToList> predicate = null)
        {
            lock (Data)
            {
                return DroneList.FindAll(x => predicate == null ? true : predicate(x));
            }
        }
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
                return Math.Sqrt((Math.Pow(a.Longitude - b.Longitude, 2) + Math.Pow(a.Latitude - b.Latitude, 2)));//using the ditance formula
            
        }


        [MethodImpl(MethodImplOptions.Synchronized)]

        public int GetClosestStation(Location a, IEnumerable<DO.Station> stations=null)
        {
            lock (Data)
            {
                try
                {
                    int i = 0;
                    int closestID = 0;
                    double minimum = 0;
                    IEnumerable<DO.Station> tempDataStations;
                    if (stations == null)
                        tempDataStations = Data.PrintStationList();//if its not the empty one
                    else
                        tempDataStations = stations;
                    List<BO.Station> stationsBL = new();
                    foreach (DO.Station station in tempDataStations)
                    {
                        stationsBL.Add(BLStation(station.Id));//convertint to bl station
                    }
                    if (stationsBL.Count == 0)
                        return closestID;
                    while (i != stationsBL.Count)
                    {
                        if (stationsBL[i].ReadyStandsInStation > 0)//only checking the ones with avelebale stands
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
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Location GetLocationOfStation(int ID)
        {
            lock (Data)
            {
                try
                {
                    DO.Station tempDataStations = Data.DisplayStation(ID);
                    Location loc = Location(tempDataStations.Longitude, tempDataStations.Latitude);
                    return loc;
                }
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Station BLStation(int id)
        {
            lock (Data)
            {
                DO.Station s = Data.DisplayStation(id);
                BO.Station station = new();
                station.Id = s.Id;
                station.Name = s.Name;
                station.location = new();
                station.location.Latitude = s.Latitude;
                station.location.Longitude = s.Longitude;
                station.ReadyStandsInStation = s.ReadyChargeStands;
                station.ListOfDrones = DisplayDronesInCharging((w => GetDistance(station.location, DisplayDrone(w.Id).ThisLocation) == 0)).ToList();//
                return station;
            }
        }

        public BO.Station BLStation()
        {
            BO.Station station = new();
            station.location = new();
            station.ListOfDrones = new();
            return station;
        }

   
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Drone BLDrone(DroneToList d)
        {
            lock (Data)
            {
                BO.Drone drone = new();
                drone.Id = d.Id;
                drone.Model = d.Model;
                drone.MaxWeight = d.MaxWeight;
                drone.Battery = d.Battery;
                drone.ThisLocation = d.ThisLocation;
                drone.status = d.status;
                if (drone.status == StatusOfDrone.Delivery)
                {
                    DO.Parcel p = Data.PrintParcel(d.ParcelId);
                    drone.parcel = BLParcelInTransfer(p);
                }
                return drone;
            }
        }

  
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Customer BLCustomer(int Id)
        {
            lock (Data)
            {
                DO.Customer c = new();
                c = DisplayCustomer(Id);
                List<DO.Parcel> tempDataParcels = new(Data.PrintParcelList());
                BO.Customer customer = new();
                customer.Id = c.Id;
                customer.Phone = c.Phone;
                customer.Name = c.Name;
                customer.location = Location(c.Longitude, c.Latitude);
                //From - a list okf parcels that sendered by this customer
                var From = tempDataParcels.FindAll(w => w.SenderId == customer.Id);
                customer.FromCustomer = new();
                foreach (DO.Parcel p in From)
                {
                    customer.FromCustomer.Add(BLParcelAtCustomer(p, true));
                }
                //To - a list okf parcels that sendered to this customer
                var To = tempDataParcels.FindAll(w => w.TargetId == customer.Id);
                customer.ToCustomer = new();
                foreach (DO.Parcel p in To)
                {
                    customer.ToCustomer.Add(BLParcelAtCustomer(p, false));
                }
                return customer;
            }
        }

        public BO.Customer BLCustomer()
        {
            BO.Customer customer = new();
            customer.location = new();
            return customer;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Parcel BLParcel(DO.Parcel p)
        {
            lock (Data)
            {
                List<DO.Customer> tempDataCustomers = new(Data.PrintCustomerList());
                BO.Parcel parcel = new();
                parcel.Id = p.Id;
                parcel.Weight = (BO.Weight)p.Weigh;
                parcel.Priority = (BO.Priority)p.Priority;
                parcel.TimeOfCreation = p.Requested;
                parcel.Scheduled = p.Scheduled;
                parcel.PickedUp = p.PickedUp;
                parcel.Delivered = p.Delivered;
                parcel.Sender = BLCustomerInParcel(tempDataCustomers.Find(w => w.Id == p.SenderId));
                parcel.Recipient = BLCustomerInParcel(tempDataCustomers.Find(w => w.Id == p.TargetId));
                if (p.Scheduled != null)
                {
                    parcel.Drone = BLDroneInParcel(DroneList.Find(w => w.Id == p.DroneId));
                }
                return parcel;
            }
        }


        public DroneInParcel BLDroneInParcel(DroneToList d)
        {
            DroneInParcel drone = new();
            drone.Id = d.Id;
            drone.Battery = d.Battery;
            drone.ThisLocation = d.ThisLocation;
            return drone;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]

        public ParcelAtCustomer BLParcelAtCustomer(DO.Parcel p, bool sender)
        {
            lock (Data)
            {
                List<DO.Customer> tempDataCustomers = new(Data.PrintCustomerList());
                ParcelAtCustomer par = new();
                par.Id = p.Id;
                par.Weight = (BO.Weight)p.Weigh;
                par.Priority = (BO.Priority)p.Priority;
                if (p.Scheduled == null)
                    par.Status = StatusOfParcel.Created;
                else if (p.PickedUp == null)
                    par.Status = StatusOfParcel.Associated;
                else if (p.Delivered == null)
                    par.Status = StatusOfParcel.PickedUp;
                else
                    par.Status = StatusOfParcel.Delivered;
                if (sender)
                    par.TheOther = BLCustomerInParcel(tempDataCustomers.Find(w => w.Id == p.TargetId));
                else
                    par.TheOther = BLCustomerInParcel(tempDataCustomers.Find(w => w.Id == p.SenderId));
                return par;
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]

        public ParcelInTransfer BLParcelInTransfer(DO.Parcel p)
        {
            lock (Data)
            {
                List<DO.Customer> tempDataCustomers = new(Data.PrintCustomerList());
                ParcelInTransfer par = new();
                par.Id = p.Id;
                par.PickedUp = p.PickedUp != null;
                par.Priority = (BO.Priority)p.Priority;
                par.Weight = (BO.Weight)p.Weigh;
                par.Sender = BLCustomerInParcel(tempDataCustomers.Find(w => w.Id == p.SenderId));
                par.Recipient = BLCustomerInParcel(tempDataCustomers.Find(w => w.Id == p.TargetId));
                par.LocationOfPickedUp = Location(tempDataCustomers.Find(w => w.Id == p.SenderId).Longitude, tempDataCustomers.Find(w => w.Id == p.SenderId).Latitude);
                par.LocationOfDestination = Location(tempDataCustomers.Find(w => w.Id == p.TargetId).Longitude, tempDataCustomers.Find(w => w.Id == p.TargetId).Latitude);
                par.Distance = GetDistance(par.LocationOfPickedUp, par.LocationOfDestination);
                return par;
            }
        }


        public CustomerInParcel BLCustomerInParcel(DO.Customer DalCus)
        {
            CustomerInParcel c = new();
            c.Id = DalCus.Id;
            c.Name = DalCus.Name;
            return c;
        }


        public DroneInCharging BLDroneInCharging(DroneToList d)
        {
            DroneInCharging drone = new();
            drone.Id = d.Id;
            drone.Battery = d.Battery;
            return drone;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneInCharging BLDroneInCharging1(int id)
        {
            lock (Data)
            {
                DroneInCharging drone = new();
                DO.DroneCharge d = Data.DisplayDroneCharge(id);
                drone.Id = d.DroneId;
                drone.Battery = DroneList.Find(w => w.Id == id).Battery;
                drone.EntryTimeForLoading = d.EntryTimeForLoading;
                return drone;
            }
        }


        public Location Location(double lon, double lat)
        {
            Location l = new();
            l.Longitude = lon;
            l.Latitude = lat;
            return l;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]

        public StationToList BLStationToList(DO.Station s)
        {
            lock (Data)
            {
                StationToList stationToList = new();
                BO.Station station = BLStation(s.Id);
                stationToList.Id = station.Id;
                stationToList.Name = station.Name;
                stationToList.ReadyStandsInStation = station.ReadyStandsInStation;
                stationToList.OccupiedStandsInStation = station.ListOfDrones.Count;
                return stationToList;
            }
        }

 
        [MethodImpl(MethodImplOptions.Synchronized)]

        public DroneToList BLDroneToList(BO.Drone d)
        {
            lock (Data)
            {
                DroneToList droneToList = new();
                droneToList.Id = d.Id;
                droneToList.Model = d.Model;
                droneToList.MaxWeight = d.MaxWeight;
                droneToList.Battery = d.Battery;
                droneToList.status = d.status;
                droneToList.ThisLocation = d.ThisLocation;
                if (d.parcel != null)
                    droneToList.ParcelId = d.parcel.Id;
                return droneToList;
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]

        public CustomerToList BLCustomerToList(DO.Customer c)
        {
            lock (Data)
            {
                CustomerToList customerToList = new();
                BO.Customer customer = BLCustomer(c.Id);
                customerToList.Id = customer.Id;
                customerToList.Name = customer.Name;
                customerToList.Phone = customer.Phone;
                var sumOfDelivered = customer.FromCustomer.FindAll(w => w.Status == StatusOfParcel.Delivered);
                customerToList.ParcelsSentAndDelivered = sumOfDelivered.Count;
                var sumOfSendered = customer.FromCustomer.FindAll(w => w.Status == StatusOfParcel.PickedUp);
                customerToList.ParcelsSentAndNotDelivered = sumOfSendered.Count;
                var sumOfgot = customer.ToCustomer.FindAll(w => w.Status == StatusOfParcel.Delivered);
                customerToList.ParcelsReceived = sumOfgot.Count;
                var sumOfOnWay = customer.ToCustomer.FindAll(w => w.Status == StatusOfParcel.PickedUp);
                customerToList.ParcelsOnWayToCustomer = sumOfOnWay.Count;
                return customerToList;
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]

        public ParcelToList BLParcelToList(DO.Parcel c)
        {
            lock (Data)
            {
                ParcelToList parcelToList = new();
                BO.Parcel parcel = BLParcel(c);
                parcelToList.Id = parcel.Id;
                parcelToList.Sender = parcel.Sender.Name;
                parcelToList.Recipient = parcel.Recipient.Name;
                parcelToList.Weight = parcel.Weight;
                parcelToList.Priority = parcel.Priority;
                if (parcel.Delivered != null && parcel.Delivered != DateTime.MinValue)
                    parcelToList.Status = StatusOfParcel.Delivered;
                else if (parcel.PickedUp != null && parcel.PickedUp != DateTime.MinValue)
                    parcelToList.Status = StatusOfParcel.PickedUp;
                else if (parcel.Scheduled != null && parcel.Scheduled != DateTime.MinValue)
                    parcelToList.Status = StatusOfParcel.Associated;
                else
                    parcelToList.Status = StatusOfParcel.Created;
                return parcelToList;
            }
        }

        public ParcelToList BLParcelToList(BO.Parcel p)
        {
            lock (Data)
            {
                ParcelToList parcelToList = new();
                parcelToList.Id = p.Id;
                parcelToList.Sender = p.Sender.Name;
                parcelToList.Recipient = p.Recipient.Name;
                parcelToList.Weight = p.Weight;
                parcelToList.Priority = p.Priority;
                if (p.Delivered != null && p.Delivered != DateTime.MinValue)
                    parcelToList.Status = StatusOfParcel.Delivered;
                else if (p.PickedUp != null && p.PickedUp != DateTime.MinValue)
                    parcelToList.Status = StatusOfParcel.PickedUp;
                else if (p.Scheduled != null && p.Scheduled != DateTime.MinValue)
                    parcelToList.Status = StatusOfParcel.Associated;
                else
                    parcelToList.Status = StatusOfParcel.Created;
                return parcelToList;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]

        public Location GetLocationOfStation(StationToList s)
        {
            lock (Data)
            {
                DO.Station st = Data.DisplayStation(s.Id);
                Location location = Location(st.Longitude, st.Latitude);
                return location;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]

        public void DeletAParcel(int Id)
        {
            lock (Data)
            {
                Data.DeleteParcel(Id);
            }
        }

        public void RunSimulator(int droneId, Action simulatorProgress, Func<bool> cancelSimulator)
        {
            Simulator simulator = new Simulator(this, droneId, simulatorProgress, cancelSimulator);
        }
    }
}