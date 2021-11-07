using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using IDAL.DO;
namespace IDAL
{
   public interface IDal
    {
        public void AddStation(IDAL.DO.Station sta);
        public void AddDrone(IDAL.DO.Drone dro);
        public void AddCustomer(IDAL.DO.Customer cst);
        public void AddSParcel(IDAL.DO.Parcel prc);
        public void DroneStatusDelivery(int drnId);
        public void ParcelScheduled(int prcId);
        public void DroneIdOfPArcel(int prcId, int drnId);
        public void PickUp(int prcId);
        public void UpdateTimeOfSupplied(int prcId);
        public void DroneStatusAvailable1(int prcId);
        public void DroneStatusMaintenanse(int drnId);
        public void UpdateReadyStandsInStation(int staId);
        public void CreateANewDroneCharge(int staId, int drnId);
        public void DroneStatusAvailable(int drnId);
        public void ClearDroneCharge(int drnId);
        public void UpdateDroneChargesIndex(int drnId);
        public IDAL.DO.Station PrintStation(int id);
        public IDAL.DO.Drone PrintDrone(int id);
        public IDAL.DO.Customer PrintCustomer(int id);
        public IDAL.DO.Parcel PrintParcel(int id);
        public IEnumeralble PrintStationList();
        public Drone[] PrintDroneList();
        public IDAL.DO.Customer[] PrintCustomerList();
        public IDAL.DO.Parcel[] PrintParcelList();
        public IDAL.DO.Parcel[] PrintUnassignedParcels();
        public IDAL.DO.Station[] PrintAvailableChargingStations();

    }
}
