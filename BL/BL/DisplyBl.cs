using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DalApi;
using BO;
using System.Collections;
using BlApi;
using DO;
namespace BL
{
    internal partial class BL : IBl
    {
        public DO.Station DisplayStation(int id)
        {
            try
            {
                List<DO.Station> tempDataStations = new(Data.PrintStationList(w=>w.Id==id));
                if (tempDataStations.Count==0)
                    throw new BlException("Station does not exists");
                    return (Data.PrintStation(id));
            }
            catch (DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DroneToList DisplayDrone(int id)
        {
                if (!(DroneList.Exists(w => w.Id == id)))
                    throw new BlException("Drone does not exixt");
                return DroneList.Find(w => w.Id == id);
        }

        public DO.Customer DisplayCustomer(int id)
        {
            try
            {
                List<DO.Customer> tempDataCustomers = new(Data.PrintCustomerList(w=>w.Id==id));
                if (tempDataCustomers.Count==0)
                    throw new BlException("Customer does not exixt");
                    return Data.PrintCustomer(id);
            }
            catch (DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DO.Parcel DisplayParcel(int id)
        {
            try
            {
                List<DO.Parcel> tempDataParcel= new(Data.PrintParcelList(w => w.Id == id));
                if (tempDataParcel.Count == 0)
                    throw new BlException("parcel does not exixt");
                return Data.PrintParcel(id);
            }
            catch (DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public List<StationToList> DisplayStationList(Predicate<StationToList> predicate = null)
        {
            try
            {
                List<DO.Station> tempDataStations = new(Data.PrintStationList());
                List<StationToList> stationList = new();
                foreach (DO.Station s in tempDataStations)
                {
                    stationList.Add(BLStationToList(s));
                }
                return stationList.FindAll(x => predicate == null ? true : predicate(x));
            }
            catch (DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }
        /// <summary>
        /// the func returns a Ienumrable of Bo parcels from Do Prcels
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<BO.Parcel> DisplayParcelLists(Predicate<BO.Parcel> predicate = null)
        {

            IEnumerable<BO.Parcel> A;
            IEnumerable<DO.Parcel> B = Data.PrintParcelList();
            A = B.Select(w => BLParcel(w));
            return A.Where(x => predicate == null ? true : predicate(x)); ;
       
        }

        public List<DroneToList> DisplayDroneList(Predicate<DroneToList> predicate = null)
        {
            return DroneList.FindAll(x => predicate == null ? true : predicate(x));
        }

        public List<CustomerToList> DisplayCustomerList(Predicate<CustomerToList> predicate = null)
        {
            List<DO.Customer> tempDataCustomers = new(Data.PrintCustomerList());
            List<CustomerToList> customerList = new();
            foreach(DO.Customer customer in tempDataCustomers)
            {
                customerList.Add(BLCustomerToList(customer));
            }
            return customerList.FindAll(x => predicate == null ? true : predicate(x));
        }

        public IEnumerable<ParcelToList> DisplayParcelList(Predicate<ParcelToList> predicate = null)
        {
            IEnumerable<DO.Parcel> tempDataParcels = Data.PrintParcelList();
            List<ParcelToList> parcelList = new();
            foreach (DO.Parcel parcel in tempDataParcels)
            {
                parcelList.Add(BLParcelToList(parcel));
            }
            return parcelList.FindAll(x => predicate == null ? true : predicate(x));
        }

        public IEnumerable<DroneInCharging> DisplayDronesInCharging(Predicate<DroneInCharging> predicate = null)
        {
            IEnumerable<DO.DroneCharge> tempDataDronesInCharge = Data.DisplayDronesInCharging();
            IEnumerable<DroneInCharging> DronesInChargingList = tempDataDronesInCharge.Select(w => BLDroneInCharging1(w.DroneId));
            return DronesInChargingList.Where(x => predicate == null ? true : predicate(x));
        }
    }
}
