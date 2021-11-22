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
        public DroneToList DisplayDrone(int ID);
        public IDAL.DO.Customer DisplayCustomer(int ID);
        public IDAL.DO.Parcel DisplayParcel(int ID);
        public List<StationToList> DisplayStationList();
        public List<DroneToList> DisplayDroneList();
        public List<CustomerToList> DisplayCustomerList();
        public List<ParcelToList> DisplayParcelList();
        public int GetClosestStation(Location a);
        public double GetDistance(Location a, Location b,double longA= 0, double latA = 0, double longB = 0, double latB = 0);
        public Location GetLocationOfStation(int ID);
        public (bool, double) GetBatteryUseAndRootFeasibility(IBL.BO.DroneToList dro, IDAL.DO.Parcel prc);
        public Station BLStation(IDAL.DO.Station s);
        public Drone BLDrone(DroneToList d);
        public Customer BLCustomer(IDAL.DO.Customer c);
        public Parcel BLParcel(IDAL.DO.Parcel p);
        public ParcelInTransfer BLParcelInTransfer(IDAL.DO.Parcel p);
        public ParcelAtCustomer BLParcelAtCustomer(IDAL.DO.Parcel p, bool sender);
        public CustomerInParcel BLCustomerInParcel(IDAL.DO.Customer DalCus);
        public StationToList BLStationToList(IDAL.DO.Station s);
        public DroneToList BLDroneToList(Drone d);
        public CustomerToList BLCustomerToList(IDAL.DO.Customer c);
        public ParcelToList BLParcelToList(IDAL.DO.Parcel c);
        public Location Location(double lon, double lat);
        public IEnumerable<DroneToList> BLDrones();        
        public void UpdatDroneName(int DroneId, string Name);
        public void UpdateCosomerInfo(int Id, string Name, string Phone);
        public void UpdateStstion(int Id, string Name, int numofCha);
        public void SendDroneToCarge(int DronId);
        public void ReturnDroneFromeCharging(int DroneId, int Time);
        public void AssignDronToParcel(int DroneId);
        public void PickUp(int DroneId);
        public void Suuply(int DroneId);    
    }
}
