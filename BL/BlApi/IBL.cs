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
        public IEnumerable<BO.Parcel> DisplayParcelLists(Predicate<BO.Parcel> predicate = null); 
        public List<StationToList> DisplayStationList(Predicate<StationToList> predicate = null);
        public List<DroneToList> DisplayDroneList(Predicate<DroneToList> predicate = null);
        public List<CustomerToList> DisplayCustomerList(Predicate<CustomerToList> predicate = null);
        public IEnumerable<ParcelToList> DisplayParcelList(Predicate<ParcelToList> predicate = null);
        public IEnumerable<DroneInCharging> DisplayDronesInCharging(Predicate<DroneInCharging> predicate = null);
        public int GetClosestStation(Location a, IEnumerable<DO.Station> stations=null);
        public double GetDistance(Location a, Location b,double longA= 0, double latA = 0, double longB = 0, double latB = 0);
        public Location GetLocationOfStation(int ID);
        public bool GetBatteryUseAndRootFeasibility(BO.DroneToList dro,DO.Parcel prc);
        public Station BLStation(int id);
        public Station BLStation();
        public Drone BLDrone(DroneToList d);
        public Customer BLCustomer(int Id);
        public Parcel BLParcel(DO.Parcel p);
        public ParcelInTransfer BLParcelInTransfer(DO.Parcel p);
        public ParcelAtCustomer BLParcelAtCustomer(DO.Parcel p, bool sender);
        public CustomerInParcel BLCustomerInParcel(DO.Customer DalCus);
        public DroneInCharging BLDroneInCharging(DroneToList d);
        public DroneInCharging BLDroneInCharging1(int id);
        public StationToList BLStationToList(DO.Station s);
        public DroneToList BLDroneToList(Drone d);
        public CustomerToList BLCustomerToList(DO.Customer c);
        public ParcelToList BLParcelToList(DO.Parcel c);
        public Location Location(double lon, double lat);
        public Location GetLocationOfStation(StationToList s);
        public IEnumerable<DroneToList> BLDrones(Predicate<DroneToList> predicate = null);        
        public void UpdatDroneName(int DroneId, string Name);
        public void UpdateCosomerInfo(int Id, string Name, string Phone);
        public void UpdateStation(int Id, string Name, int numofCha);
        public void SendDroneToCarge(int DronId);
        public void ReturnDroneFromeCharging(int DroneId, int Time);
        public void AssignDronToParcel(int DroneId);
        public void PickUp(int DroneId);
        public void Suuply(int DroneId);
        /// <summary>
        /// the func gets id of a parecel and delets
        /// </summary>
        /// <param name="Id"></param>
        public void DeletAParcel(int Id);

        public void RunSimulator(int droneId, Action simulatorProgress, Func<bool> cancelSimulator);
    }
}
