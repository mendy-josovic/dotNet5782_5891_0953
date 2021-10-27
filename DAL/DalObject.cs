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

        public void AddStation(IDAL.DO.Station sta)  //just adding to the last place
        {
            DataSource.Stations[DataSource.Config.StationsIndex] = sta;
            DataSource.Config.StationsIndex++;
        }
        public void AddDrone(IDAL.DO.Drone dro)  //same
        {
            DataSource.Drones[DataSource.Config.DronesIndex] = dro;
            DataSource.Config.DronesIndex++;
        }

        public void AddCustomer(IDAL.DO.Customer cst)  //same
        {
            DataSource.Customers[DataSource.Config.CustomersIndex] = cst;
            DataSource.Config.CustomersIndex++;
        }

        public void AddSParcel(IDAL.DO.Parcel prc)  //same
        {
            DataSource.Parcels[DataSource.Config.ParcelsIndex] = prc;
            DataSource.Config.ParcelsIndex++;
        }
        public void DroneStatusDelivery(int drnId)
        {
            int i = Array.FindIndex(DataSource.Drones, w => w.Id == drnId);  //find the drone to assign 
            DataSource.Drones[i].Status = IDAL.DO.STATUS.DELIVERY;
        }

        public void ParcelScheduled(int prcId)
        { 
            int i = Array.FindIndex(DataSource.Parcels, w => w.Id == prcId);  //find the parcel to assign
            DataSource.Parcels[i].Scheduled = DateTime.Now;  //get assigning time
        }

        public void DroneIdOfPArcel(int prcId, int drnId)
        {
            int i = Array.FindIndex(DataSource.Parcels, w => w.Id == prcId);
            DataSource.Parcels[i].DroneId = drnId;
        }

        public void PickUp(int prcId)
        {
            int i = Array.FindIndex(DataSource.Parcels, w => w.Id == prcId);  //find the parcel that was picked up
            DataSource.Parcels[i].PickedUp = DateTime.Now;  //update the pickup time
        }

        //public void Supplied(int prcId, int drnId)
        //{
        //    int i = Array.FindIndex(DataSource.Parcels, w => w.Id == prcId);  //find the parcel that was supplied 
        //    DataSource.Parcels[i].Delivered = DateTime.Now;  //update that it was delivered
        //    i = Array.FindIndex(DataSource.Drones, w => w.Id == drnId);  //find the drone that was supplied the parcel
        //    DataSource.Drones[i].Status = IDAL.DO.STATUS.AVAILABLE;  //update that now the drone is available
        //}

        public void UpdateTimeOfSupplied(int prcId)
        {
            int i = Array.FindIndex(DataSource.Parcels, w => w.Id == prcId);  //find the parcel that was supplied 
            DataSource.Parcels[i].Delivered = DateTime.Now;  //update that it was delivered
        }

        public void DroneStatusAvailable1(int prcId)
        {
            int i = Array.FindIndex(DataSource.Parcels, w => w.Id == prcId);
            int drnId = DataSource.Parcels[i].DroneId;
            i = Array.FindIndex(DataSource.Drones, w => w.Id == drnId);  //find the drone that was supplied the parcel
            DataSource.Drones[i].Status = IDAL.DO.STATUS.AVAILABLE;  //update that now the drone is availabl
        }

        public void DroneStatusMaintenanse(int drnId)
        {
            int i = Array.FindIndex(DataSource.Drones, w => w.Id == drnId);  //finds the drone and update that it's not available
            DataSource.Drones[i].Status = IDAL.DO.STATUS.MAINTENANSE;
        }

        public void UpdateReadyStandsInStation(int staId)
        {
            int i = Array.FindIndex(DataSource.Stations, w => w.Id == staId);
            DataSource.Stations[i].ReadyChargeStands--;
        }

        public void CreateANewDroneCharge(int staId, int drnId)
        {
            DataSource.DroneCharges[DataSource.Config.DroneChargesIndex] = new IDAL.DO.DroneCharge(drnId, staId);
        }
        public void DroneStatusAvailable(int drnId)
        {
            int i = Array.FindIndex(DataSource.Drones, w => w.Id == drnId);  //finds the drone and update that it's available
            DataSource.Drones[i].Status = IDAL.DO.STATUS.AVAILABLE;
        }

        public void ClearDroneCharge(int drnId)
        {
            int i = Array.FindIndex(DataSource.DroneCharges, w => w.DroneId == drnId);
            DataSource.DroneCharges[i].DroneId = 0;
            DataSource.DroneCharges[i].StationId = 0;
        }

        public void UpdateDroneChargesIndex(int drnId)
        {
            int i = Array.FindIndex(DataSource.DroneCharges, w => w.DroneId == drnId);
            if (DataSource.Config.DroneChargesIndex > i)
                DataSource.Config.DroneChargesIndex = i;
        }

        public IDAL.DO.Station PrintStation(int id)  //finds the station and sends a replica
        {
            int i = Array.FindIndex(DataSource.Stations, w => w.Id == id);
            return (DataSource.Stations[i]);
        }
    

        public IDAL.DO.Drone PrintDrone(int id)  //finds the drone and sends a replica
        {
          int i = Array.FindIndex(DataSource.Drones, w => w.Id == id);
          return (DataSource.Drones[i]);
        }

        public IDAL.DO.Customer PrintCustomer(int id)  //finds the customer and sends a replica
        {
            int i = Array.FindIndex(DataSource.Customers, w => w.Id == id);
            return (DataSource.Customers[i]);
        }

        public IDAL.DO.Parcel PrintParcel(int id)  //finds the station and sends a replica
        {
            int i = Array.FindIndex(DataSource.Parcels, w => w.Id == id);
            return (DataSource.Parcels[i]);
        }

        public IDAL.DO.Station[] PrintStationList()  //creates a new array and returns that
        {
            Station[] arr = new Station[DataSource.Config.StationsIndex];
            Array.Copy(DataSource.Stations, arr, DataSource.Config.StationsIndex);
            return arr;
        }

        public Drone[] PrintDroneList()  //creates a new array and returns that
        {
            Drone[] arr = new Drone[DataSource.Config.DronesIndex];
            Array.Copy(DataSource.Drones,arr, DataSource.Config.DronesIndex);
            return arr;
         }

        public IDAL.DO.Customer[] PrintCustomerList()  //creates a new array and returns that
        {
            Customer[] arr = new Customer[DataSource.Config.CustomersIndex];
            Array.Copy(DataSource.Customers, arr, DataSource.Config.CustomersIndex);
            return arr;
        }

        public IDAL.DO.Parcel[] PrintParcelList()  //creates a new array and returns that
        {
            Parcel[] arr = new Parcel[DataSource.Config.ParcelsIndex];
            Array.Copy(DataSource.Parcels, arr, DataSource.Config.ParcelsIndex);
            return arr;
        }

        public IDAL.DO.Parcel[] PrintUnassignedParcels()  //creates a new array with the condition and returns that
        {
            Parcel[] arr = new Parcel[DataSource.Config.ParcelsIndex];
            arr = Array.FindAll(DataSource.Parcels, x => x.DroneId == 0 && x.Id != 0);  //find all the parcels that are not assigned and are initialized
            return arr;
        }
        public IDAL.DO.Station[] PrintAvailableChargingStations()  //creates a new array of available charging slots and returns that
        {
            Station[] arr = new Station[DataSource.Config.StationsIndex];
            arr = Array.FindAll(DataSource.Stations, x => x.ReadyChargeStands > 0 && x.Id != 0);  //find all stations have available stands
            return arr;
        }
    }
}
