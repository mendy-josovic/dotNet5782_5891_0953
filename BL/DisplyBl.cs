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
                return (tempDataStations.Find(w => w.Id == id));
            }
            catch (IDAL.DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public DroneToList DisplayDrone(int id)
        {
            try
            {
                return DroneList.Find(w => w.Id == id);
            }
            catch (IDAL.DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }

        public IDAL.DO.Customer DisplayCustomere(int id)
        {
            try
            {
                List<IDAL.DO.Customer> tempDataCustomers = new(Data.PrintCustomerList());
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

            return 
        }
    }
}
