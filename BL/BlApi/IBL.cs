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
        /// <summary>
        /// gets a station of BL and adds a station to the data
        /// </summary>
        /// <param name="sta">the new station</param>   
        public void AddStation(Station sta);
        /// <summary>
        /// gets a drone of BL and ID of a station where it charges in the begining and adds a drone to the data
        /// </summary>
        /// <param name="dro">the new drone</param>
        /// <param name="IDOfStation">ID of station for first charging</param>
        /// 
        public void AddDrone(Drone dro, int IDOfStation);
        /// <summary>
        /// gets a customer of BL and adds him to the data
        /// </summary>
        /// <param name="cus">the new customer</param>
        /// 
        public void AddCustomer(Customer cus);
        /// <summary>
        /// gets a parcel of BL and adds it to the data
        /// </summary>
        /// <param name="par">the new parcel</param>
        /// 
        public void AddParcel(Parcel par);
        /// <summary>
        /// returns the station with this id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Do.station</returns>
        public DO.Station DisplayStation(int ID);
        /// <summary>
        /// returns the Drone to list with this id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>dron to list</returns>
        public DroneToList DisplayDrone(int ID);
        /// <summary>
        /// returns the customer with this id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Do.customer</returns>
        public DO.Customer DisplayCustomer(int ID);
        /// <summary>
        /// returns the parcel with this id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Do.parcel</returns>
        public DO.Parcel DisplayParcel(int ID);
       /// <summary>
       /// the func gets predicet of the parcel (is itialized to null)
       /// </summary>
       /// <param name="predicate"></param>
       /// <returns> ienumrable of parcel</returns>
        public IEnumerable<BO.Parcel> DisplayParcelLists(Predicate<BO.Parcel> predicate = null);
        /// <summary>
        /// the func gets predicet of the station (is itialized to null)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>  list of statoion</returns>
        public List<StationToList> DisplayStationList(Predicate<StationToList> predicate = null);
        /// <summary>
        /// the func gets predicet of the sdrones (is itialized to null)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>  list of sdron to listn</returns>
        public List<DroneToList> DisplayDroneList(Predicate<DroneToList> predicate = null);
        /// <summary>
        /// the func gets predicet of the customerto list (is itialized to null)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>  list of customer to list</returns>
        public List<CustomerToList> DisplayCustomerList(Predicate<CustomerToList> predicate = null);
        /// <summary>
        /// the func gets predicet of the parcel (is itialized to null)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns> ienumrable of parcel</returns>
        public IEnumerable<ParcelToList> DisplayParcelList(Predicate<ParcelToList> predicate = null);
        /// <summary>
        /// the func gets predicet of the drone charging (is itialized to null)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns> ienumrable of drone in charging</returns>
        public IEnumerable<DroneInCharging> DisplayDronesInCharging(Predicate<DroneInCharging> predicate = null);
        /// <summary>
        /// return the ID of the closest station with ready stands to the location
        /// </summary>
        /// <param name="a">the location we want to get it closest station</param>
        /// <returns>ID of the closest station</returns>
        /// 
        public int GetClosestStation(Location a, IEnumerable<DO.Station> stations=null);
        /// <summary>
        /// the func hase  a option
        /// to get the distenc with difrrentn pararmeters
        /// </summary>
        /// <param name="a">if its jest a location to a location</param>
        /// <param name="b"></param>
        /// <param name="longA">if we wont to mack the disenc with a long and lat</param>
        /// <param name="latA"></param>
        /// <param name="longB"></param>
        /// <param name="latB"></param>
        /// <returns></returns>     
        public double GetDistance(Location a, Location b,double longA= 0, double latA = 0, double longB = 0, double latB = 0);
        /// <summary>
        /// get ID of a station and return it location
        /// </summary>
        /// <param name="ID">ID of station</param>
        /// <returns>location of station</returns>
        /// 
        public Location GetLocationOfStation(int ID);
        /// <summary>
        /// the func gets adrone and checks if the root to a parcel is feasible
        /// </summary>
        /// <param name="dro"></param>
        /// <param name="prc"></param>
        /// <returns>bool if yes or no</returns>
        public bool GetBatteryUseAndRootFeasibility(BO.DroneToList dro,DO.Parcel prc);
        /// <summary>
        /// Turn a DAL station into a BL station
        /// </summary>
        /// <param name="s">DAL station</param>
        /// <returns>BL station</returns>
        /// 
        public Station BLStation(int id); 
        public Station BLStation();
        /// <summary>
        /// Turn a DroneToList drone into a BL Drone
        /// </summary>
        /// <param name="d">DroneToList drone</param>
        /// <returns>BL Drone</returns>
        /// 
        public Drone BLDrone(DroneToList d);
        /// <summary>
        /// Turn a DAL customer into a BL customer
        /// gets int of the id
        /// </summary>
        /// <param name="Id">DAL customer </param>
        /// <returns>BL customer</returns>
        /// 
        public Customer BLCustomer(int Id);

        public Customer BLCustomer();
        /// <summary>
        /// Turn a DAL parcel into a BL parcel 
        /// </summary>
        /// <param name="p">DAL parcel </param>
        /// <returns>BL parcel</returns>
        /// 
        public Parcel BLParcel(DO.Parcel p);
        /// <summary>
        /// Turn a DroneToList drone into a DroneInParcel
        /// </summary>
        /// <param name="d">DroneToList</param>
        /// <returns>DroneInParcel</returns>
        /// 
        public DroneInParcel BLDroneInParcel(DroneToList d);
        /// <summary>
        /// Turn a DAL parcel into a BL ParcelInTransfer
        /// </summary>
        /// <param name="p">DAL parcel</param>
        /// <returns>BL ParcelInTransfer</returns>
        /// 
        public ParcelInTransfer BLParcelInTransfer(DO.Parcel p);
        /// <summary>
        /// Turn a DAL parcel into a BL ParcelAtCustomer
        /// </summary>
        /// <param name="p">DAL parcel</param>
        /// <param name="sender">a flag if the customer of ParcelAtCustomer is the sender or the recipient</param>
        /// <returns>BL ParcelAtCustomer</returns>
        /// 
        public ParcelAtCustomer BLParcelAtCustomer(DO.Parcel p, bool sender);
        /// <summary>
        /// Turn a DAL customer into a BL CustomerInParcel
        /// </summary>
        /// <param name="DalCus">DAL customer</param>
        /// <returns>BL CustomerInParcel</returns>
        /// 
        public CustomerInParcel BLCustomerInParcel(DO.Customer DalCus);
        /// <summary>
        /// Turn a DroneToList into a BL DroneInCharging
        /// </summary>
        /// <param name="d">DroneToList</param>
        /// <returns>DroneInCharging</returns>
        /// 
        public DroneInCharging BLDroneInCharging(DroneToList d);
        /// <summary>
        /// makes a dron in charging from a id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public DroneInCharging BLDroneInCharging1(int id);
        /// <summary>
        /// Turn a DAL station into a BL StationToList
        /// </summary>
        /// <param name="s">DAL station</param>
        /// <param name="s">DAL station</param>
        /// <returns>StationToList</returns>
        /// 
        public StationToList BLStationToList(DO.Station s);
        /// <summary>
        /// Turn a BL Drone into a DroneToList
        /// </summary>
        /// <param name="d"> BL Drone</param>
        /// <returns>DroneToList</returns>
        /// 
        public DroneToList BLDroneToList(Drone d);
        /// <summary>
        /// Turn a DAL customer into a CustomerToList
        /// </summary>
        /// <param name="c">DAL customer</param>
        /// <returns>CustomerToList</returns>
        /// 
        public CustomerToList BLCustomerToList(DO.Customer c);
        /// <summary>
        /// Turn a DAL parcel into a ParcelToList
        /// </summary>
        /// <param name="c">DAL parcel</param>
        /// <returns>ParcelToList</returns>
        /// 
        public ParcelToList BLParcelToList(DO.Parcel c);
        public ParcelToList BLParcelToList(BO.Parcel p);
        /// <summary>
        /// create a Location
        /// </summary>
        /// <param name="lon">longitude point value</param>
        /// <param name="lat">latitude point value</param>
        /// <returns>Location</returns>
        /// 
        public Location Location(double lon, double lat);
        /// <summary>
        /// the func returns location of the rtraget
        /// </summary>
        /// <param name="pr"></param>
        /// <returns></returns>
        /// 
        public Location GetReceiverLo(DO.Parcel pr);
        /// <summary>
        /// the func returns location of thr sender 
        /// </summary>
        /// <param name="pr"></param>
        /// <returns></returns>
        /// 
        public Location GetSenderLo(DO.Parcel pr);
        public Location GetLocationOfStation(StationToList s);
        /// <summary>
        /// turn DroneList into IEnumerable DroneList
        /// </summary>
        /// <returns></returns>  
        public IEnumerable<DroneToList> BLDrones(Predicate<DroneToList> predicate = null);
        /// <summary>
        /// the func makes sure that the drone exists and 
        /// if yes send the parameters to the update func in dal objects
        /// these are the only two places 
        /// </summary>
        /// <param name="DroneId"></param>
        /// <param name="Name"></param>
        /// 
        public void UpdatDroneName(int DroneId, string Name);
        /// <summary>
        /// the func gets the id and finds if it exsits (we assume that the parameters are valid and not a problem (we chack that on the console level) )
        /// ane use the update func
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// 
        public void UpdateCosomerInfo(int Id, string Name, string Phone);

        /// <summary>
        /// the func gets the id, finds if it exists and the changes 
        /// are with the pararmtets
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="numofCha"></param>
        /// 
        public void UpdateStation(int Id, string Name, int numofCha);
        /// <summary>
        /// we make 4 initial checks 1. make sure that the drone exists
        /// 2. checking if the drone is available 
        /// 3. checking if the station is available
        /// 4. checking if we have enough battery
        /// then we update the drone in the drone to-list
        /// </summary>
        /// <param name="DronId"></param>
        public void SendDroneToCarge(int DronId);
        /// <summary>
        /// the func gets the drone id
        /// we mack 2 inital chacks
        /// 1. that we have such a drone 
        /// 2. that its in charging
        /// and then we update everything 2 in drone to-list an 1 in the data source
        /// </summary>
        /// <param name="DroneId"></param>
        /// <param name="Time"></param>
        ///
        public void ReturnDroneFromeCharging(int DroneId);
        /// <summary>
        /// the func in order to deside witch one to do 
        /// first removing all the not relevent from the list(to hevy,scheduled alredy,not enough battrey)
        /// the we sort the list first acording to ditence and then priyoritity
        /// </summary>
        /// <param name="DroneId"></param>
        /// 
        public void AssignDronToParcel(int DroneId);
        /// <summary>
        /// the func updats the pich up 
        /// so first we check that we have the dron and that the dron can pick up this parcel
        /// and then we judst updat the dton acourdin to 
        /// </summary>
        /// <param name="DroneId"></param>
        /// 
        public void PickUp(int DroneId);
        /// <summary>
        /// the func gets the sdrone id and updtes the suuply
        /// </summary>
        /// <param name="DroneId"></param>
        /// 
        public void Suuply(int DroneId);
        /// <summary>
        /// the func gets id of a parecel and delets
        /// </summary>
        /// <param name="Id"></param>
        public void DeletAParcel(int Id);

        public void RunSimulator(int droneId, Action simulatorProgress, Func<bool> cancelSimulator);
    }
}
