using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public class DalObject
    {
        public DalObject() 
        {
            DataSource.Initialize();
        }

        public void AddStation(IDAL.DO.Station sta)
        {
            DataSource.Stations[DataSource.Config.StationsIndex] = sta;
        }
       
        public void AddDrone(IDAL.DO.Drone dro)
        {
            DataSource.Drones[DataSource.Config.DronesIndex] = dro;
        }

        public void AddCustomer(IDAL.DO.Customer cst)
        {
            DataSource.Customers[DataSource.Config.CustomersIndex] = cst;
        }

        public void AddSParcel(IDAL.DO.Parcel prs)
        {
            DataSource.Parcels[DataSource.Config.ParcelsIndex] = prs;
        }

        public void UpdateStation(IDAL.DO.Station sta)
        {
            public int i = 0;
            while()
                i++;
        }
    }
}
