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
            List<IDAL.DO.Station> tempDataStations = new(Data.PrintStationList());
            return (tempDataStations.Find(w => w.Id == id));
        }

        public DroneToList DisplayDrone(int id)
        {
            return DroneList.Find(w => w.Id == id);
        }

        public IDAL.DO.Customer DisplayCustomere(int id)
        {
            List<IDAL.DO.Customer> tempDataCustomers = new(Data.PrintCustomerList());
            return (tempDataCustomers.Find(w => w.Id == id));
        }

        public IDAL.DO.Parcel DisplayParcel(int id)
        {
            List<IDAL.DO.Parcel> tempDataParcels = new(Data.PrintParcelList());
            return (tempDataParcels.Find(w => w.Id == id));
        }

        public List<StationToList> DisplayStationList()
        {


        }
    }
}
