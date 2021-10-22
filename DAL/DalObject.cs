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
            DataSource.Config.StationsIndex++;
        }
        public void AddDrone(IDAL.DO.Drone dro)
        {
            DataSource.Drones[DataSource.Config.DronesIndex] = dro;
            DataSource.Config.DronesIndex++;
        }

        public void AddCustomer(IDAL.DO.Customer cst)
        {
            DataSource.Customers[DataSource.Config.CustomersIndex] = cst;
            DataSource.Config.CustomersIndex++;
        }

        public void AddSParcel(IDAL.DO.Parcel prc)
        {
            DataSource.Parcels[DataSource.Config.ParcelsIndex] = prc;
            DataSource.Config.ParcelsIndex++;
        }

        public void AssignDrone(IDAL.DO.Parcel prc, IDAL.DO.Drone drn)
        {
            
        }
        public void PickUp(IDAL.DO.Parcel prc, IDAL.DO.Drone drn)
        {

        }

       public void Supplied(IDAL.DO.Parcel prc, IDAL.DO.Drone drn)
        {

        }

        public void SendToCharge(IDAL.DO.Station sta, IDAL.DO.Drone drn)
        {

        }

        public void EndCharge(IDAL.DO.Station sta, IDAL.DO.Drone drn)
        {
            
        }


        public void PrintStation(int Id)
        {
            for (int i = 0; i < DataSource.Config.StationsIndex; i++)
            {
                if (Id == DataSource.Stations[i].Id)
                {
                    Console.WriteLine(DataSource.Stations[i].ToString());
                    return;
                }
            }
        }

        public void PrintDrone(int Id)
        {

        }
        public void PrintCustomer(int Id)
        {

        }
        public void PrintParcel(int Id)
        {

        }
        public void PrintStationList()
        {

        }
        public void PrintDroneList()
        {

        }
        public void PrintCustomerList()
        {

        }
        public void PrintParcelList()
        {

        }
        public void PrintUnassignedParcels()
        {

        }
        public void PrintAvailableChargingStations()
        {
            
        }
    }
}
