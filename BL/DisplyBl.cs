using System;
using System.Collections.Generic;
using System.Text;
using IDAL;
using IBL.BO;
using System.Collections;
using IBL;
namespace BL
{
    public partial class BL : IBl
    {
        public IDAL.DO.Station DisplayStation(int id)
        {
            try
            {
                List<IDAL.DO.Station> tempDataStations = new(Data.PrintStationList(w=>w.Id==id));
                if (tempDataStations.Count==0)
                    throw new BlException("Station does not exists");
                    return (Data.PrintStation(id));
            }
            catch (IDAL.DO.DalExceptions ex)
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

        public IDAL.DO.Customer DisplayCustomer(int id)
        {
            try
            {
                List<IDAL.DO.Customer> tempDataCustomers = new(Data.PrintCustomerList(w=>w.Id==id));
                if (tempDataCustomers.Count==0)
                    throw new BlException("Customer does not exixt");
                    return Data.PrintCustomer(id);
            }
            catch (IDAL.DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public IDAL.DO.Parcel DisplayParcel(int id)
        {
            try
            {
                List<IDAL.DO.Parcel> tempDataParcel= new(Data.PrintParcelList(w => w.Id == id));
                if (tempDataParcel.Count == 0)
                    throw new BlException("parcel does not exixt");
                return Data.PrintParcel(id);
            }
            catch (IDAL.DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public List<StationToList> DisplayStationList(Predicate<StationToList> predicate = null)
        {
            try
            {
                List<IDAL.DO.Station> tempDataStations = new(Data.PrintStationList());
                List<StationToList> stationList = new();
                foreach (IDAL.DO.Station s in tempDataStations)
                {
                    stationList.Add(BLStationToList(s));
                }
                return stationList.FindAll(x => predicate == null ? true : predicate(x));
            }
            catch (IDAL.DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public List<DroneToList> DisplayDroneList(Predicate<DroneToList> predicate = null)
        {
            List<DroneToList> droneList = new();
            foreach(DroneToList drone in DroneList)
            {
                droneList.Add(drone);
            }
            return droneList.FindAll(x => predicate == null ? true : predicate(x));
        }

        public List<CustomerToList> DisplayCustomerList(Predicate<CustomerToList> predicate = null)
        {
            List<IDAL.DO.Customer> tempDataCustomers = new(Data.PrintCustomerList());
            List<CustomerToList> customerList = new();
            foreach(IDAL.DO.Customer customer in tempDataCustomers)
            {
                customerList.Add(BLCustomerToList(customer));
            }
            return customerList.FindAll(x => predicate == null ? true : predicate(x));
        }

        public List<ParcelToList> DisplayParcelList(Predicate<ParcelToList> predicate = null)
        {
            List<IDAL.DO.Parcel> tempDataParcels = new(Data.PrintParcelList());
            List<ParcelToList> parcelList = new();
            foreach (IDAL.DO.Parcel parcel in tempDataParcels)
            {
                parcelList.Add(BLParcelToList(parcel));
            }
            return parcelList.FindAll(x => predicate == null ? true : predicate(x));
        }
    }
}
