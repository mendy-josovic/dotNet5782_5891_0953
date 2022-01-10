﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using DO;
namespace DalApi
{
    public interface IDal
    {
        public void AddStation(Station sta);
        public void AddDrone(Drone dro);
        public void AddCustomer(Customer cst);
        public void AddSParcel(Parcel prc);
        public void ParcelScheduled(int prcId);
        public void DroneIdOfPArcel(int prcId, int drnId);
        public void PickUp(int prcId);
        public void UpdateDrone(int drnId,string Name="");
        public void UpdateCustomer(int CusId, string Name ="", string phone = "");
        public void UpdatStation(int StationId, string Name = "", int NumOfCarg = -1);

        public void UpdatParcel(int parclId, int SenderId = 0, int TargetId = 0,int DroneId=0, Weight whihgt = 0, Priority priorty = 0, int Updatereqwested = 0, int UpdatSchedueld = 0, int UpdatPicedup = 0, int UpdateDeleverd = 0);
        public void UpdateTimeOfSupplied(int prcId);
        public void UpdateReadyStandsInStation(int staId);
        public void CreateANewDroneCharge(int staId, int drnId);
        public void ClearDroneCharge(int drnId);
        public Station PrintStation(int id);
        public Drone PrintDrone(int id);
        public Customer PrintCustomer(int id);
        public Parcel PrintParcel(int id);
        public DroneCharge PrintDronCarg(int DroneId = 0, int StationId = 0);
        public IEnumerable<Station> PrintStationList(Predicate<Station> predicate = null);
        public IEnumerable<Drone> PrintDroneList(Predicate<Drone> predicate = null);
        public IEnumerable<Customer> PrintCustomerList(Predicate<Customer> predicate = null);
        public IEnumerable<Parcel> PrintParcelList(Predicate<Parcel> predicate = null);
        public IEnumerable<Parcel> PrintUnassignedParcels();
        public IEnumerable<DroneCharge> DisplayDronesInCharging(Predicate<DroneCharge> predicate = null);
        public double[] Consumption();
    }
}
