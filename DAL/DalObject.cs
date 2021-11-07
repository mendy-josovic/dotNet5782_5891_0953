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

        public void AddStation(Station sta)  //just adding to the last place
        {
            //if (DataSource.stations.Equals(sta))
            //    throw "ERROR: This station is already exists";
            DataSource.stations.Add(sta);
        }
        public void AddDrone(Drone dro)  //same
        {
            DataSource.drones.Add(dro);
        }

        public void AddCustomer(Customer cst)  //same
        {
            DataSource.customers.Add(cst);
        }

        public void AddSParcel(Parcel prc)  //same
        {
            DataSource.parcels.Add(prc);
        }
        //public void DroneStatusDelivery(int drnId)
        //{
        //    int ie = List.FindIndex(DataSource.Drones, w => w.Id == drnId);  //find the drone to assign 
        //    int i = DataSource.drones.FindIndex(w => w.Id == drnId);
        //    Drone tempDrone = DataSource.drones[i];
        //    tempDrone.
        //    DataSource.Drones[i].Status = IDAL.DO.STATUS.DELIVERY;
        //}

        public void ParcelScheduled(int prcId)
        { 
            int i = DataSource.parcels.FindIndex(w => w.Id == prcId);  //find the parcel to assign
            Parcel tempParcel = DataSource.parcels[i];
            tempParcel.Scheduled = DateTime.Now;  //get assigning time
            DataSource.parcels[i] = tempParcel;
        }

        public void DroneIdOfPArcel(int prcId, int drnId)
        {
            int i = DataSource.parcels.FindIndex(w => w.Id == prcId);
            Parcel tempParcel = DataSource.parcels[i];
            tempParcel.DroneId = drnId;
            DataSource.parcels[i] = tempParcel;
        }

        public void PickUp(int prcId)
        {
            int i = DataSource.parcels.FindIndex(w => w.Id == prcId);  //find the parcel that was picked up
            Parcel tempParcel = DataSource.parcels[i];
            tempParcel.PickedUp = DateTime.Now;
            DataSource.parcels[i] = tempParcel;  //update the pickup time
        }

        public void UpdateTimeOfSupplied(int prcId)
        {
            int i = DataSource.parcels.FindIndex(w => w.Id == prcId);  //find the parcel that was supplied
            Parcel tempParcel = DataSource.parcels[i];
            tempParcel.Delivered = DateTime.Now;
            DataSource.parcels[i] = tempParcel;  //update the time of supplied
        }

        //public void DroneStatusAvailable1(int prcId)
        //{
        //    int i = Array.FindIndex(DataSource.Parcels, w => w.Id == prcId);
        //    int drnId = DataSource.Parcels[i].DroneId;
        //    i = Array.FindIndex(DataSource.Drones, w => w.Id == drnId);  //find the drone that was supplied the parcel
        //    DataSource.Drones[i].Status = IDAL.DO.STATUS.AVAILABLE;  //update that now the drone is availabl
        //}

        //public void DroneStatusMaintenanse(int drnId)
        //{
        //    int i = Array.FindIndex(DataSource.Drones, w => w.Id == drnId);  //finds the drone and update that it's not available
        //    DataSource.Drones[i].Status = IDAL.DO.STATUS.MAINTENANSE;
        //}

        public void UpdateReadyStandsInStation(int staId)
        {
            int i = DataSource.stations.FindIndex(w => w.Id == staId);  //find the station
            Station tempStation = DataSource.stations[i];
            tempStation.ReadyChargeStands--;
            DataSource.stations[i] = tempStation;
        }

        public void CreateANewDroneCharge(int staId, int drnId)
        {
            DataSource.droneCharges.Add(new DroneCharge(drnId, staId));
        }
        //public void DroneStatusAvailable(int drnId)
        //{
        //    int i = Array.FindIndex(DataSource.Drones, w => w.Id == drnId);  //finds the drone and update that it's available
        //    DataSource.Drones[i].Status = IDAL.DO.STATUS.AVAILABLE;
        //}

        public void ClearDroneCharge(int drnId)
        {
            int i = DataSource.droneCharges.FindIndex(w => w.DroneId == drnId);  //find the parcel that was supplied
            DroneCharge tempDroneCharge = DataSource.droneCharges[i];
            tempDroneCharge.DroneId = 0;
            tempDroneCharge.StationId = 0;
            DataSource.droneCharges[i] = tempDroneCharge;
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
