using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using IBL.BO;

namespace IBL
{
    public interface IBl
    {
        public void AddStation(Station sta);
        public void AddDrone(Drone dro, int IDOfStation);
        public void AddCustomer(Customer cus);
        public void AddParcel(Parcel par);
        public IDAL.DO.Station DisplayStation(int ID);
        public int GetClosestStation(Location a);
        public double GetDistance(Location a, Location b,double longA= 0, double latA = 0, double longB = 0, double latB = 0);
        public Location GetLocationOfStation(int ID);
        public (bool, double) GetBatteryUseAndRootFeasibility(IBL.BO.DroneToList dro, IDAL.DO.Parcel prc);
        public IEnumerable<DroneToList> BLDrones();
        public void UpdatDroneName(int DroneId, string Name);
        public void UpdateCosomerInfo(int Id, string Name, string Phone);
        public void UpdateStstion(int Id, string Name, int numofCha);
        public void SendDroneToCarge(int DronId);
        public void ReturnDroneFromeCharging(int DroneId, int Time);
        public void AssignDronToParcel(int DroneId);
        public void PickUp(int DroneId);
        public Station BLStation(IDAL.DO.Station s);     
    }
}
