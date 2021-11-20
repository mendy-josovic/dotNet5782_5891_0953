using System;
using System.Collections.Generic;
using System.Text;
using IDAL;
using IBL.BO;
using System.Collections;
using IBL;
namespace BL
{
    public partial class BL :IBl
    {
        /// <summary>
        /// gets a station of BL and adds a station to the data
        /// </summary>
        /// <param name="sta">the new station</param>
        public void AddStation(Station sta)
        {
            IDAL.DO.Station station = new();
            station.Id = sta.Id;
            station.Name = sta.Name;
            station.Longitude = sta.location.Longitude;
            station.Latitude = sta.location.Latitude;
            station.ReadyChargeStands = sta.ReadyStandsInStation;
            List<IDAL.DO.Station> tempDataStations = new List<IDAL.DO.Station>(Data.PrintStationList());
            foreach(IDAL.DO.Station state in tempDataStations)
            {
                if (state.Id == station.Id)
                    throw "The station is already exists\n";
            }
            Data.AddStation(station);
            tempDataStations.Add(station);
        }

        /// <summary>
        /// gets a drone of BL and ID of a station where it charges in the begining and adds a drone to the data
        /// </summary>
        /// <param name="dro">the new drone</param>
        /// <param name="IDOfStation">ID of station for first charging</param>
        public void AddDrone(Drone dro, int IDOfStation)
        {
            if (tempDataStations.FindIndex(w => w.Id == IDOfStation) < 0)
                throw "there is not a station with this ID\n";
            IDAL.DO.Drone drone = new();
            drone.Id = dro.Id;
            drone.Model = dro.Model;
            drone.MaxWeight = (IDAL.DO.WEIGHT)dro.MaxWeight;
            foreach(DroneToList drn in DroneList)
            {
                if (dro.Id == drn.Id)
                    throw "The drone is already exists\n";
            }
            Data.AddDrone(drone);
            dro.Battery = r.Next(20, 40);
            dro.status = STATUS_OF_DRONE.IN_MAINTENANCE;           
            dro.ThisLocation = GetLocationOfStation(IDOfStation);
            DroneToList d = new(dro);
            DroneList.Add(d);
        }

        /// <summary>
        /// gets a customer of BL and adds him to the data
        /// </summary>
        /// <param name="cus">the new customer</param>
        public void AddCustomer(Customer cus)
        {            
            List<IDAL.DO.Customer> tempDataCustomers = new(Data.PrintCustomerList());
            foreach (IDAL.DO.Customer c in tempDataCustomers)
            {
                if (c.Id == cus.Id)
                    throw "The station is already exists\n";
            }
            IDAL.DO.Customer customer = new();
            customer.Id = cus.Id;
            customer.Name = cus.Name;
            customer.Phone = cus.Phone;
            customer.Longitude = cus.location.Longitude;
            customer.Latitude = cus.location.Latitude;
            Data.AddCustomer(customer);
            tempDataCustomers.Add(customer);
        }

        /// <summary>
        /// gets a parcel of BL and adds it to the data
        /// </summary>
        /// <param name="par">the new parcel</param>
        public void AddParcel(Parcel par)
        {
            IDAL.DO.Parcel parcel = new();
            parcel.Id = par.Id;
            parcel.SenderId = par.Sender.Id;
            parcel.TargetId = par.Recipient.Id;
            parcel.TargetId = par.Recipient.Id;
            parcel.Weigh = (IDAL.DO.WEIGHT)par.Weight;
            parcel.Priority = (IDAL.DO.PRIORITY)par.Priority;
            parcel.DroneId = 0;
            par.TimeOfCreation = DateTime.Now;
            par.Scheduled = new(0, 0, 0, 0, 0, 0);
            par.PickedUp = new(0, 0, 0, 0, 0, 0);
            par.Delivered = new(0, 0, 0, 0, 0, 0);
            par.Drone = null;
            parcel.Requested = par.TimeOfCreation;
            parcel.Scheduled = par.Scheduled;
            parcel.PickedUp = par.PickedUp;
            parcel.Delivered = par.Delivered;
                Data.AddSParcel(parcel);
        }
    }
}
