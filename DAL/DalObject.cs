using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
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

        public void AssignDrone(int prcId, int drnId)
        {
            drn.Status = IDAL.DO.STATUS.DELIVERY;
            prc.SchedulId = DateTime.Now;
        }
        public void PickUp(int prcId)
        {
            prc.PickedUp = DateTime.Now;
        }

       public void Supplied(int prcId, int drnId)
        {
            prc.Delivered = DateTime.Now;
            drn.Status = IDAL.DO.STATUS.AVAILABLE;
        }

        public void SendToCharge(int staId, int drnId)
        {
            drn.Status = IDAL.DO.STATUS.MAINTENANSE;
            sta.ReadyChargeStands--;
            DataSource.DroneCharges[DataSource.Config.DroneChargesIndex] = new IDAL.DO.DroneCharge(drn.Id, sta.Id);
        }

        public void EndCharge(int drnId)
        {
            drn.Status = IDAL.DO.STATUS.AVAILABLE;
            int i = Array.FindIndex(DataSource.DroneCharges, w => w.DroneId == drn.Id);
            DataSource.DroneCharges[i].DroneId = 0;
            DataSource.DroneCharges[i].StationId = 0;
            if (DataSource.Config.DroneChargesIndex > i)
                DataSource.Config.DroneChargesIndex = i;
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
            for (int i = 0; i < DataSource.Config.DronesIndex; i++)
            {
                if (Id == DataSource.Drones[i].Id)
                {
                    Console.WriteLine(DataSource.Drones[i].ToString());
                    return;
                }
            }
        }
        public void PrintCustomer(int Id)
        {
            for (int i = 0; i < DataSource.Config.CustomersIndex; i++)
            {
                if (Id == DataSource.Customers[i].Id)
                {
                    Console.WriteLine(DataSource.Customers[i].ToString());
                    return;
                }
            }
        }
        public void PrintParcel(int Id)
        {
            for (int i = 0; i < DataSource.Config.ParcelsIndex; i++)
            {
                if (Id == DataSource.Parcels[i].Id)
                {
                    Console.WriteLine(DataSource.Parcels[i].ToString());
                    return;
                }
            }
        }
        public void PrintStationList()
        {
            for (int i = 0; i < DataSource.Config.ParcelsIndex; i++)
            { 
                    Console.WriteLine(DataSource.Parcels[i].ToString());
                    return;
            }
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
