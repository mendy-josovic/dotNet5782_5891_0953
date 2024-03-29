﻿using System;
using System.Collections.Generic;
using System.Text;
using DalApi;
using BO;
using System.Linq;
using System.Collections;
using BlApi;
using DO;
using System.Runtime;
using System.Runtime.CompilerServices;
namespace BL
{
    internal partial class BL : IBl
    {
       
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void UpdatDroneName(int DroneId, string Name)
        {
            lock (Data)
            {
                try
                {
                    if (!DroneList.Exists(w => w.Id == DroneId))
                        throw new BlException("Drone doesn't exist");
                    int i = DroneList.FindIndex(w => w.Id == DroneId);
                    DroneList[i].Model = Name;
                    Data.UpdateDrone(DroneId, Name);//using the func in dl of update
                }
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]

        public void UpdateCosomerInfo(int Id,string Name, string Phone)
        {
            lock (Data)
            {
                try
                {
                    List<DO.Customer> Tempcustomers = new(Data.PrintCustomerList(w => w.Id == Id));
                    if (Tempcustomers.Count == 0)
                        throw new BlException("Customer doesn't exist");
                    Data.UpdateCustomer(Id, Name, Phone);//using the func in dl of update
                }
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }
            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]

        public void UpdateStation(int Id, string Name,int numofCha)
        {
            lock (Data)
            {
                try
                {
                    List<DO.Station> Tempstation = new(Data.PrintStationList(w => w.Id == Id));
                    if (Tempstation.Count == 0)
                        throw new BlException("Customer doesn't exist");
                    Data.UpdatStation(Id, Name, numofCha);//using the func do of update
                }
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }
            }
        }



        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendDroneToCarge(int DronId)
        {
            lock (Data)
            {
                try
                {
                    int i = DroneList.FindIndex(w => w.Id == DronId);
                    if (i < 0)
                        throw new BlException("Drone doesn't exist");
                    IEnumerable<DO.Station> stations = Data.PrintStationList(x => x.ReadyChargeStands>0 && DroneList[i].Battery - (int)Consumption(DroneList[i].ThisLocation, Location(x.Longitude, x.Latitude), BO.ModeOfDroneInMoving.Available) > 0);
                    if (stations.Count() == 0)
                        throw new BlException("Charging Not Possible (Station Charging slots are full, Not Enough Battery)");
                    int j = GetClosestStation(DroneList[i].ThisLocation, stations);//geting the id of station that we need to charge 
                    DO.Station tempStation = Data.DisplayStation(j);//a temporary station (like the one to charg)   
                    DroneList[i].status = StatusOfDrone.InMaintenance;//updating the drone status
                    Location location = Location(tempStation.Longitude, tempStation.Latitude);
                    DroneList[i].Battery -= (int)Consumption(DroneList[i].ThisLocation, location, BO.ModeOfDroneInMoving.Available);//updating the battery for the way to the station
                    DroneList[i].ThisLocation = location;//updating the location
                    Data.UpdatStation(j, "", tempStation.ReadyChargeStands - 1); //updating the ready charging stands     
                    Data.CreateANewDroneCharge(j, DronId); //creating a new drone-charge
                }
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }
            }
        }

 
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReturnDroneFromeCharging(int DroneId)
        {
            lock (Data)
            {
                try
                {
                    int i = DroneList.FindIndex(w => w.Id == DroneId);
                    if (i < 0)
                        throw new BlException("Drone doesn't exist");
                    if (!(DroneList[i].status == BO.StatusOfDrone.InMaintenance))
                        throw new BlException("ERROR: Dron Not In Cargong Mode");
                    DroneCharge dronech = Data.DisplayDroneCharge(DroneId);
                    DateTime time1 = DateTime.Now;               
                    TimeSpan Time= time1.Subtract(dronech.EntryTimeForLoading);
                    DroneList[i].status = BO.StatusOfDrone.Available;
                    DroneList[i].Battery += Time.TotalSeconds* batteryConfig[4];
                    if (DroneList[i].Battery > 100)//stoping the recharging in 100%
                        DroneList[i].Battery = 100;
                    DO.DroneCharge droneCharge = Data.DisplayDroneCharge(DroneId);
                    DO.Station station = Data.DisplayStation(droneCharge.StationId);
                    Data.UpdatStation(station.Id, "", station.ReadyChargeStands + 1);
                    Data.ClearDroneCharge(DroneId);

                }
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AssignDronToParcel(int DroneId)
        {
            lock (Data)
            {
                try
                {
                    if (!(DroneList.Exists(w => w.Id == DroneId)))
                        throw new BlException("Drone dosen't exist");
                    int i = DroneList.FindIndex(w => w.Id == DroneId);

                    //get only the relevent for us
                    List<DO.Parcel> tempDataParcels = Data.PrintParcelList(w => (int)w.Weigh <= (int)DroneList[i].MaxWeight
                    && w.Scheduled == null && GetBatteryUseAndRootFeasibility(DroneList.Find(w => w.Id == DroneId), w) == true).ToList();

                    //remove all tht cant do the root (because of the battery consemption)
                    tempDataParcels.OrderByDescending(w => w.Priority).ThenByDescending(w => w.Weigh).
                        ThenBy(w => GetDistance(DroneList[i].ThisLocation, GetSenderLo(w)));

                    //sorting acourding to priyuorty
                    if (tempDataParcels.Count == 0)
                        throw new BlException("Assignment Not Possible");
                    DroneList.Find(w => w.Id == DroneId).status = BO.StatusOfDrone.Delivery;
                    DroneList.Find(w => w.Id == DroneId).ParcelId = tempDataParcels[0].Id;
                    Data.UpdatParcel(tempDataParcels[0].Id, 0, 0, DroneList.Find(w => w.Id == DroneId).Id, 0, 0, 0, 1);//we updating the first parcel in the list
                }
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]

        public void PickUp(int DroneId)
        {
            lock (Data)
            {
                try
                {
                    if (!(DroneList.Exists(w => w.Id == DroneId)))
                        throw new BlException("Drone dosennt exist");
                    int i = DroneList.FindIndex(w => w.Id == DroneId);
                    DO.Parcel parcel = Data.PrintParcel(DroneList[i].ParcelId);
                    if (parcel.PickedUp != null)
                        throw new BlException("Parcel Already Picked Up");
                    double batteryuse = Consumption(DroneList[i].ThisLocation, GetSenderLo(parcel), ModeOfDroneInMoving.Available);
                    DroneList[i].Battery -= batteryuse;//the battery usige
                    DroneList[i].ThisLocation = GetSenderLo(parcel);
                    Data.UpdatParcel(parcel.Id, 0, 0, 0, 0, 0, 0, 0, 1);//the func gets parametets according to whrer we need to update
                }
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }
            }
        }

  
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void Suuply(int DroneId)
        {
            lock (Data)
            {
                try
                {
                    if (!(DroneList.Exists(w => w.Id == DroneId)))
                        throw new BlException("Drone dosen't exist");
                    int i = DroneList.FindIndex(w => w.Id == DroneId);
                    DO.Parcel parcel = Data.PrintParcel(DroneList[i].ParcelId);
                    if (parcel.PickedUp == null || parcel.Delivered != null)
                        throw new BlException("cannot supply sumthig is wrong");
                    double batteryuse = Consumption(DroneList[i].ThisLocation, GetReceiverLo(parcel), (ModeOfDroneInMoving)parcel.Weigh);
                    DroneList[i].Battery -= batteryuse;
                    DroneList[i].ThisLocation = GetReceiverLo(parcel);
                    Data.UpdatParcel(parcel.Id, 0, 0, 0, 0, 0, 0, 0, 0, 1);//updating the parcel in the drond
                    DroneList[i].status = StatusOfDrone.Available;
                }
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }
            }
        }    
    }
}
