using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DalApi;
using BO;
using System.Collections;
using BlApi;
using DO;
using System.Runtime.CompilerServices;
namespace BL
{
    internal partial class BL : IBl
    {

        [MethodImpl(MethodImplOptions.Synchronized)]

        public DO.Station DisplayStation(int id)
        {
            lock (Data)
            {
                try
                {
                    List<DO.Station> tempDataStations = new(Data.PrintStationList(w => w.Id == id));//finds the station
                    if (tempDataStations.Count == 0)
                        throw new BlException("Station does not exist");
                    return (Data.DisplayStation(id));
                }
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]

        public DroneToList DisplayDrone(int id)
        {
            lock (Data)
            {
                if (!(DroneList.Exists(w => w.Id == id)))
                    throw new BlException("Drone does not exist");
                return DroneList.Find(w => w.Id == id);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]

        public DO.Customer DisplayCustomer(int id)
        {
            lock (Data)
            {
                try
                {
                    List<DO.Customer> tempDataCustomers = new(Data.PrintCustomerList(w => w.Id == id));//findes it 
                    if (tempDataCustomers.Count == 0)
                        throw new BlException("Customer does not exist");
                    return Data.PrintCustomer(id);
                }
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Parcel DisplayParcel(int id)
        {
            lock (Data)
            {
                try
                {
                    List<DO.Parcel> tempDataParcel = new(Data.PrintParcelList(w => w.Id == id));
                    if (tempDataParcel.Count == 0)
                        throw new BlException("parcel does not exist");
                    return Data.PrintParcel(id);
                }
                catch (DO.DalExceptions ex)
                {
                    throw new BlException(ex.Message);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public List<StationToList> DisplayStationList(Predicate<StationToList> predicate = null)
        {
            lock (Data)
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
        }
 
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.Parcel> DisplayParcelLists(Predicate<BO.Parcel> predicate = null)
        {
            lock (Data)
            {
                IEnumerable<BO.Parcel> A;
                IEnumerable<DO.Parcel> B = Data.PrintParcelList();//converting all the parcel from Do to BO
                A = B.Select(w => BLParcel(w));
                return A.Where(x => predicate == null ? true : predicate(x));
            }
       
        }

        [MethodImpl(MethodImplOptions.Synchronized)]

        public List<DroneToList> DisplayDroneList(Predicate<DroneToList> predicate = null)
        {
            lock (Data)
            {
                return DroneList.FindAll(x => predicate == null ? true : predicate(x));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]

        public List<CustomerToList> DisplayCustomerList(Predicate<CustomerToList> predicate = null)
        {
            lock (Data)
            {
                List<DO.Customer> tempDataCustomers = new(Data.PrintCustomerList());
                List<CustomerToList> customerList = new();
                foreach (DO.Customer customer in tempDataCustomers)
                {
                    customerList.Add(BLCustomerToList(customer));//convert from customer of DL to CustomerInList of BO
                }
                return customerList.FindAll(x => predicate == null ? true : predicate(x));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> DisplayParcelList(Predicate<ParcelToList> predicate = null)
        {
            lock (Data)
            {
                IEnumerable<DO.Parcel> tempDataParcels = Data.PrintParcelList();
                List<ParcelToList> parcelList = new();
                foreach (DO.Parcel parcel in tempDataParcels)
                {
                    parcelList.Add(BLParcelToList(parcel));//Convert from parcel in DO to parcelToList in BO
                }
                return parcelList.FindAll(x => predicate == null ? true : predicate(x));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneInCharging> DisplayDronesInCharging(Predicate<DroneInCharging> predicate = null)
        {
            lock (Data)
            {
                IEnumerable<DO.DroneCharge> tempDataDronesInCharge = Data.DisplayDronesInCharging();
                IEnumerable<DroneInCharging> DronesInChargingList = tempDataDronesInCharge.Select(w => BLDroneInCharging1(w.DroneId));//fines all the places
                return DronesInChargingList.Where(x => predicate == null ? true : predicate(x));
            }
        }
    }
}
