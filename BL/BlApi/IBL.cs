using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using BO;
namespace BlApi
{
    public interface IBl
    {
        public void AddStation(Station sta);
        public void AddDrone(Drone dro, int IDOfStation);
        public void AddCustomer(Customer cus);
        public void AddParcel(Parcel par);
        public DO.Station DisplayStation(int ID);
        public DroneToList DisplayDrone(int ID);
        public DO.Customer DisplayCustomer(int ID);
        public DO.Parcel DisplayParcel(int ID);
        public List<StationToList> DisplayStationList(Predicate<StationToList> predicate = null);
        public List<DroneToList> DisplayDroneList(Predicate<DroneToList> predicate = null);
        public List<CustomerToList> DisplayCustomerList(Predicate<CustomerToList> predicate = null);
        public List<ParcelToList> DisplayParcelList(Predicate<ParcelToList> predicate = null);
        public int GetClosestStation(Location a);
        public double GetDistance(Location a, Location b,double longA= 0, double latA = 0, double longB = 0, double latB = 0);
        public Location GetLocationOfStation(int ID);
        public bool GetBatteryUseAndRootFeasibility(BO.DroneToList dro,DO.Parcel prc);
        public Station BLStation(DO.Station s);
        public Drone BLDrone(DroneToList d);
        public Customer BLCustomer(DO.Customer c);
        public Parcel BLParcel(DO.Parcel p);
        public ParcelInTransfer BLParcelInTransfer(DO.Parcel p);
        public ParcelAtCustomer BLParcelAtCustomer(DO.Parcel p, bool sender);
        public CustomerInParcel BLCustomerInParcel(DO.Customer DalCus);
        public DroneInCharging BLDroneInCharging(DroneToList d);
        public StationToList BLStationToList(DO.Station s);
        public DroneToList BLDroneToList(Drone d);
        public CustomerToList BLCustomerToList(DO.Customer c);
        public ParcelToList BLParcelToList(DO.Parcel c);
        public Location Location(double lon, double lat);
        public Location GetLocationOfStation(StationToList s);
        public IEnumerable<DroneToList> BLDrones(Predicate<DroneToList> predicate = null);        
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
