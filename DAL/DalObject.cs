﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
namespace DalObject
{
    public class DalObject
    {
        public DalObject() 
        {
            DataSource.Initialize();
        }

        public void AddStation(IDAL.DO.Station sta)
        {
            DataSource.Stations[DataSource.Config.StationsIndex] = sta;
            DataSource.Config.StationsIndex++;
        }
        public void AddDrone(IDAL.DO.Drone dro)
        {
            DataSource.Drones[DataSource.Config.DronesIndex] = dro;
            DataSource.Config.DronesIndex++;
        }

        public void AddCustomer(IDAL.DO.Customer cst)
        {
            DataSource.Customers[DataSource.Config.CustomersIndex] = cst;
            DataSource.Config.CustomersIndex++;
        }

        public void AddSParcel(IDAL.DO.Parcel prc)
        {
            DataSource.Parcels[DataSource.Config.ParcelsIndex] = prc;
            DataSource.Config.ParcelsIndex++;
        }

        public void AssignDrone(int prcId, int drnId)
        {
            int i = Array.FindIndex(DataSource.Drones, w => w.Id == drnId);
            DataSource.Drones[i].Status = IDAL.DO.STATUS.DELIVERY;
            i = Array.FindIndex(DataSource.Parcels, w => w.Id == prcId);
            DataSource.Parcels[i].Scheduled = DateTime.Now;
        }
        public void PickUp(int prcId)
        {
            int i = Array.FindIndex(DataSource.Parcels, w => w.Id == prcId);
            DataSource.Parcels[i].PickedUp = DateTime.Now;
        }

       public void Supplied(int prcId, int drnId)
        {
            int i = Array.FindIndex(DataSource.Parcels, w => w.Id == prcId);
            DataSource.Parcels[i].Delivered = DateTime.Now;
            i = Array.FindIndex(DataSource.Drones, w => w.Id == drnId);
            DataSource.Drones[i].Status = IDAL.DO.STATUS.AVAILABLE;
        }

        public void SendToCharge(int staId, int drnId)
        {
            int i = Array.FindIndex(DataSource.Drones, w => w.Id == drnId);
            DataSource.Drones[i].Status = IDAL.DO.STATUS.MAINTENANSE;
            i = Array.FindIndex(DataSource.Stations, w => w.Id == staId);
            DataSource.Stations[i].ReadyChargeStands--;
            DataSource.DroneCharges[DataSource.Config.DroneChargesIndex] = new IDAL.DO.DroneCharge(drnId, staId);
        }

        public void EndCharge(int drnId)
        {
            int i = Array.FindIndex(DataSource.Drones, w => w.Id == drnId);
            DataSource.Drones[i].Status = IDAL.DO.STATUS.AVAILABLE;
            i = Array.FindIndex(DataSource.DroneCharges, w => w.DroneId == drnId);
            DataSource.DroneCharges[i].DroneId = 0;
            DataSource.DroneCharges[i].StationId = 0;
            if (DataSource.Config.DroneChargesIndex > i)
                DataSource.Config.DroneChargesIndex = i;
        }


        public IDAL.DO.Station PrintStation(int Id)
        {
            int i = Array.FindIndex(DataSource.Stations, w => w.Id == Id);
            return (DataSource.Stations[i]);
        }
    

        public IDAL.DO.Drone PrintDrone(int Id)
        {
          int i = Array.FindIndex(DataSource.Drones, w => w.Id == Id);
          return (DataSource.Drones[i]);
        }

        public IDAL.DO.Customer PrintCustomer(int Id)
        {
            int i = Array.FindIndex(DataSource.Stations, w => w.Id == Id);
            return (DataSource.Customers[i]);
        }

        public IDAL.DO.Parcel PrintParcel(int Id)
        {
            int i = Array.FindIndex(DataSource.Stations, w => w.Id == Id);
            return (DataSource.Parcels[i]);
        }

        public IDAL.DO.Station[] PrintStationList()
        {
            Station[] arr = new Station[DataSource.Config.StationsIndex];
            Array.Copy(DataSource.Stations, arr, DataSource.Config.StationsIndex);
            return arr;
        }

        public Drone[] PrintDroneList()
        {
            Drone[] arr = new Drone[DataSource.Config.DronesIndex];
            Array.Copy(DataSource.Drones,arr, DataSource.Config.DronesIndex);
            return arr;
         }

        public IDAL.DO.Customer[] PrintCustomerList()
        {
            Customer[] arr = new Customer[DataSource.Config.CustomersIndex];
            Array.Copy(DataSource.Customers, arr, DataSource.Config.CustomersIndex);
            return arr;
        }

        public IDAL.DO.Parcel[] PrintParcelList()
        {
            Parcel[] arr = new Parcel[DataSource.Config.ParcelsIndex];
            Array.Copy(DataSource.Parcels, arr, DataSource.Config.ParcelsIndex);
            return arr;
        }

        public IDAL.DO.Parcel[] PrintUnassignedParcels()
        {
            Parcel[] arr = new Parcel[DataSource.Config.ParcelsIndex];
            arr = Array.FindAll(DataSource.Parcels, x => x.DroneId == 0&& x.Id!=0);//find all the parcels that are not assigned and are initlized.
            return arr;
        }
        public IDAL.DO.Station[] PrintAvailableChargingStations()
        {
            Station[] arr = new Station[DataSource.Config.StationsIndex];
            arr = Array.FindAll(DataSource.Stations, x => x.ReadyChargeStands > 0&& x.Id!=0);
            return arr;
        }
    }
}
