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
                List<IDAL.DO.Station> tempDataStations = new(Data.PrintStationList());
                if (!(tempDataStations.Exists(w => w.Id == id)))
                    throw new BlException("Station does not exists");
                    return (tempDataStations.Find(w => w.Id == id));
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
                List<IDAL.DO.Customer> tempDataCustomers = new(Data.PrintCustomerList());
                if (!(tempDataCustomers.Exists(w => w.Id == id)))
                    throw new BlException("Customer does not exixt");
                    return (tempDataCustomers.Find(w => w.Id == id));
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
                List<IDAL.DO.Parcel> tempDataParcels = new(Data.PrintParcelList());
                return (tempDataParcels.Find(w => w.Id == id));
            }
            catch (IDAL.DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public List<StationToList> DisplayStationList()
        {
            try
            {
                List<IDAL.DO.Station> tempDataStations = new(Data.PrintStationList());
                List<StationToList> stationList = new();
                foreach (IDAL.DO.Station s in tempDataStations)
                {
                    stationList.Add(BLStationToList(s));
                }
                return stationList;
            }
            catch (IDAL.DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public List<DroneToList> DisplayDroneList(Predicate<DroneToList> predicate = null)
        {
            return DroneList.FindAll(x => predicate == null ? true : predicate(x));
        }

        public List<CustomerToList> DisplayCustomerList()
        {
            List<IDAL.DO.Customer> tempDataCustomers = new(Data.PrintCustomerList());
            List<CustomerToList> customerList = new();
            foreach(IDAL.DO.Customer customer in tempDataCustomers)
            {
                customerList.Add(BLCustomerToList(customer));
            }
            return customerList;
        }

        public List<ParcelToList> DisplayParcelList()
        {
            List<IDAL.DO.Parcel> tempDataParcels = new(Data.PrintParcelList());
            List<ParcelToList> parcelList = new();
            foreach (IDAL.DO.Parcel parcel in tempDataParcels)
            {
                parcelList.Add(BLParcelToList(parcel));
            }
            return parcelList;
        }
    }
}
