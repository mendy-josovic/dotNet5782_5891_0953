using System;
using System.Collections.Generic;
using System.Text;
using DalApi;
using DO;
using BO;
using System.Collections;
using BlApi;
namespace BL
{
    internal partial class BL :IBl
    {
        /// <summary>
        /// gets a station of BL and adds a station to the data
        /// </summary>
        /// <param name="sta">the new station</param>
        public void AddStation(BO.Station sta)
        {
            try
            {
                List<DO.Station> tempDataStations = new(Data.PrintStationList(w => w.Id == sta.Id));//if elredy exsits we want to stop
                if(tempDataStations.Count!=0)
                    throw new BlException("The station is already exists");
                DO.Station station = new();
                station.Id = sta.Id;
                station.Name = sta.Name;
                station.Longitude = sta.location.Longitude;
                station.Latitude = sta.location.Latitude;
                station.ReadyChargeStands = sta.ReadyStandsInStation;               
                Data.AddStation(station);     
            }
            catch(DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// gets a drone of BL and ID of a station where it charges in the begining and adds a drone to the data
        /// </summary>
        /// <param name="dro">the new drone</param>
        /// <param name="IDOfStation">ID of station for first charging</param>
        public void AddDrone(BO.Drone dro, int IDOfStation)
        {
            try
            {
                List<DO.Station> tempDataStations =new (Data.PrintStationList(w=>w.Id==IDOfStation));
                if (tempDataStations.Count==0)
                    throw new BlException("there is not a station with this ID");
                DO.Drone drone = new();
                drone.Id = dro.Id;
                drone.Model = dro.Model;
                drone.MaxWeight = (DO.WEIGHT)dro.MaxWeight;
                int i = DroneList.FindIndex(w => w.Id == dro.Id);
                    if(i>=0)
                        throw new BlException("The drone is already exists");     
                Data.AddDrone(drone);
                dro.Battery = r.Next(20, 40);
                dro.status = STATUS_OF_DRONE.IN_MAINTENANCE;
                dro.ThisLocation = GetLocationOfStation(IDOfStation);
                DroneToList d = BLDroneToList(dro);
                DroneList.Add(d);
            }
            catch (DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// gets a customer of BL and adds him to the data
        /// </summary>
        /// <param name="cus">the new customer</param>
        public void AddCustomer(BO.Customer cus)
        {
            try
            {
                List<DO.Customer> tempDataCustomers = new(Data.PrintCustomerList(w=>w.Id==cus.Id));
             if(tempDataCustomers.Count!=0)
                        throw new BlException("The station is already exists");               
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

        /// <summary>
        /// gets a parcel of BL and adds it to the data
        /// </summary>
        /// <param name="par">the new parcel</param>
        public void AddParcel(BO.Parcel par)
        {
            try
            {
                DO.Parcel parcel = new();
                parcel.Id = par.Id;
                parcel.SenderId = par.Sender.Id;
                parcel.TargetId = par.Recipient.Id;
                parcel.TargetId = par.Recipient.Id;
                parcel.Weigh = (DO.WEIGHT)par.Weight;
                parcel.Priority = (DO.PRIORITY)par.Priority;
                parcel.DroneId = 0;
                par.TimeOfCreation = DateTime.Now;
                par.Scheduled = DateTime.MinValue;
                par.PickedUp = DateTime.MinValue;
                par.Delivered = DateTime.MinValue;
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
