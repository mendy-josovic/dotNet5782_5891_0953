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
        public void AddStation(Station sta);
        public void AddDrone(Drone dro);
        public void AddCustomer(Customer cst);
        public void AddSParcel(Parcel prc);
        public void ParcelScheduled(int prcId);
        public void DroneIdOfPArcel(int prcId, int drnId);
        public void PickUp(int prcId);
        public void UpdateDrone(int drnId,string Name="");
        public void UpdateCustomer(int CusId, string Name ="", string phone = "");
        public void UpdatStation(int StationId, string Name = "", int NumOfCarg = 0);

        public void UpdatParcel(int parclId, int SenderId = 0, int TargetId = 0,int DroneId=0, WEIGHT whihgt = 0, PRIORITY priorty = 0, int Updatereqwested = 0, int UpdatSchedueld = 0, int UpdatPicedup = 0, int UpdateDeleverd = 0);
        public void UpdateTimeOfSupplied(int prcId);
        public void UpdateReadyStandsInStation(int staId);
        public void CreateANewDroneCharge(int staId, int drnId);
        public void ClearDroneCharge(int drnId);
        public Station PrintStation(int id);
        public Drone PrintDrone(int id);
        public Customer PrintCustomer(int id);
        public Parcel PrintParcel(int id);
        public DroneCharge PrintDronCarg(int DroneId = 0, int StationId = 0);
        public IEnumerable<Station> PrintStationList();
        public IEnumerable<Drone> PrintDroneList();
        public IEnumerable<Customer> PrintCustomerList();
        public IEnumerable<Parcel> PrintParcelList();
        public IEnumerable<Parcel> PrintUnassignedParcels();
        public IEnumerable<Station> PrintAvailableChargingStations();
        public double[] Consumption();
    }
}
