using System;
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
        /// <summary>
        /// we add the station that is got in the parametr
        /// </summary>
        /// <param name="sta"></param>
        public void AddStation(Station sta);
        public void AddDrone(Drone dro);
        /// <summary>
        ///  adding the customer with this Id
        /// </summary>
        /// <param name="cst"></param>
        public void AddCustomer(Customer cst);
        /// <summary>
        /// addina a parcel by the customer
        /// </summary>
        /// <param name="prc"></param>
        public void AddSParcel(Parcel prc);
        /// <summary>
        /// updats the drone Id in a parcel....
        /// </summary>
        /// <param name="prcId"></param>
        /// <param name="drnId"></param>
        public void DroneIdOfPArcel(int prcId, int drnId);
        public void UpdateDrone(int drnId,string Name="");
        /// <summary>
        /// the func gets the id of the customer and a new name or phone \
        /// and it with a defult empty and we use the replace func and null or empty..
        /// </summary>
        /// <param name="CusId"></param>
        /// <param name="Name"></param>
        /// <param name="phone"></param>
        public void UpdateCustomer(int CusId, string Name ="", string phone = "");
        /// <summary>
        /// the func gets the id of the statoin and the name and number of cargin slots
        /// it is with a dfult empty so we can chang only one of the field
        /// </summary>
        /// <param name="StationId"></param>
        /// <param name="Name"></param>
        /// <param name="NumOfCarg"></param>
        public void UpdatStation(int StationId, string Name = "", int NumOfCarg = -1);
        /// <summary>
        /// gets the parcel id and deletes the parcel
        /// </summary>
        /// <param name="id"></param>
        public void DeleteParcel(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parclId"></param>
        /// <param name="SenderId"></param>
        /// <param name="TargetId"></param>
        /// <param name="DroneId"></param>
        /// <param name="whihgt"></param>
        /// <param name="priorty"></param>
        /// <param name="Updatereqwested"></param>
        /// <param name="UpdatSchedueld"></param>
        /// <param name="UpdatPicedup"></param>
        /// <param name="UpdateDeleverd"></param>
        public void UpdatParcel(int parclId, int SenderId = 0, int TargetId = 0,int DroneId=0, Weight whihgt = 0, Priority priorty = 0, int Updatereqwested = 0, int UpdatSchedueld = 0, int UpdatPicedup = 0, int UpdateDeleverd = 0);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="staId"></param>
        /// <param name="drnId"></param>
        public void CreateANewDroneCharge(int staId, int drnId);
        public void ClearDroneCharge(int drnId);
        /// <summary>
        /// gets the Id and returns the station.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Station DisplayStation(int id);
        public Drone DisplayDrone(int id);
        /// <summary>
        /// disply customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer PrintCustomer(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Parcel PrintParcel(int id);
        /// 
        /// <summary>
        /// we finde the place with the station and the dronr we need 
        /// </summary>
        /// <param name="DroneId"></param>
        /// <param name="StationId"></param>
        /// <returns></returns>
        /// 
        public DroneCharge DisplayDroneCharge(int DroneId = 0);
        /// <summary>
        /// returns a Ienumrable that is with a predicet
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Station> PrintStationList(Predicate<Station> predicate = null);
        public IEnumerable<Drone> DisplayDronesList(Predicate<Drone> predicate = null);
        /// <summary>
        /// disply customer list with the pridecet
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Customer> PrintCustomerList(Predicate<Customer> predicate = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Parcel> PrintParcelList(Predicate<Parcel> predicate = null);      
        /// <summary>
        /// displaying the drobe charg
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DroneCharge> DisplayDronesInCharging(Predicate<DroneCharge> predicate = null);
        public double[] Consumption();
        /// <summary>
        /// returns the runing numbner
        /// </summary>
        /// <returns></returns>
        public int GetRuningNumber();
    }
}
