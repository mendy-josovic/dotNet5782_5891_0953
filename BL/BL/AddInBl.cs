using System;
using System.Collections.Generic;
using System.Text;
using DalApi;
using DO;
using BO;
using System.Collections;
using BlApi;
using System.Runtime.CompilerServices;
namespace BL
{
    internal partial class BL :IBl
    {
        /// <summary>
        /// gets a station of BL and adds a station to the data
        /// </summary>
        /// <param name="sta">the new station</param>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AddStation(BO.Station sta)
        {
            lock (Data)
            {
                try
                {
                    List<DO.Station> tempDataStations = new(Data.PrintStationList(w => w.Id == sta.Id));//if elredy exsits we want to stop
                    if (tempDataStations.Count != 0)
                        throw new BlException("The station already exists");
                    DO.Station station = new();
                    station.Id = sta.Id;
                    station.Name = sta.Name;
                    station.Longitude = sta.location.Longitude;
                    station.Latitude = sta.location.Latitude;
                    station.ReadyChargeStands = sta.ReadyStandsInStation;
                    Data.AddStation(station);
                }
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }
            }
        }

        /// <summary>
        /// gets a drone of BL and ID of a station where it charges in the begining and adds a drone to the data
        /// </summary>
        /// <param name="dro">the new drone</param>
        /// <param name="IDOfStation">ID of station for first charging</param>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AddDrone(BO.Drone dro, int IDOfStation)
        {
            lock (Data)
            {
                try
                {
                    List<DO.Station> tempDataStations = new(Data.PrintStationList(w => w.Id == IDOfStation));
                    if (tempDataStations.Count == 0)
                        throw new BlException("there is not a station with this ID");

                    DO.Drone drone = new();
                    drone.Id = dro.Id;

                    //Checks if the ID number does not already exist in the system
                    int i = DroneList.FindIndex(w => w.Id == dro.Id);
                    if (i >= 0)
                        throw new BlException("The drone already exists");

                    DO.Station station = Data.DisplayStation(IDOfStation);
                    if (station.ReadyChargeStands == 0)
                        throw new BlException("There are no ready stands at this station!");
                    else
                        UpdateStation(station.Id, station.Name, station.ReadyChargeStands - 1);

                    drone.Model = dro.Model;
                    drone.MaxWeight = (DO.Weight)dro.MaxWeight;
                    Data.AddDrone(drone);
                    dro.Battery = r.Next(20, 40);
                    dro.status = StatusOfDrone.InMaintenance;
                    dro.ThisLocation = GetLocationOfStation(IDOfStation);
                    DroneToList d = BLDroneToList(dro);
                    DroneList.Add(d);
                }
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }

            }
        }

        /// <summary>
        /// gets a customer of BL and adds him to the data
        /// </summary>
        /// <param name="cus">the new customer</param>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AddCustomer(BO.Customer cus)
        {
            lock (Data)
            {
                try
                {
                    List<DO.Customer> tempDataCustomers = new(Data.PrintCustomerList(w => w.Id == cus.Id));
                    if (tempDataCustomers.Count != 0)
                        throw new BlException("The Customer already exists");
                    DO.Customer customer = new();
                    customer.Id = cus.Id;
                    customer.Name = cus.Name;
                    customer.Phone = cus.Phone;
                    customer.Longitude = cus.location.Longitude;
                    customer.Latitude = cus.location.Latitude;
                    Data.AddCustomer(customer);
                }
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }
            }
        }

        /// <summary>
        /// gets a parcel of BL and adds it to the data
        /// </summary>
        /// <param name="par">the new parcel</param>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AddParcel(BO.Parcel par)
        {
            lock (Data)
            {
                try
                {
                    DO.Parcel parcel = new();
                    parcel.Id = Data.GetRuningNumber();
                    parcel.SenderId = par.Sender.Id;
                    parcel.TargetId = par.Recipient.Id;
                    parcel.Weigh = (DO.Weight)par.Weight;
                    parcel.Priority = (DO.Priority)par.Priority;
                    parcel.DroneId = 0;
                    par.TimeOfCreation = DateTime.Now;
                    par.Drone = null;
                    parcel.Requested = par.TimeOfCreation;
                    parcel.Scheduled = par.Scheduled;
                    parcel.PickedUp = par.PickedUp;
                    parcel.Delivered = par.Delivered;
                    Data.AddSParcel(parcel);
                }
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }
            }
        }
    }
}
