using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using IDAL.DO;
using IDAL;
namespace DalObject
{
    public class DalObject: IDal
    {
        public DalObject() 
        {
            DataSource.Initialize();
        }

        public void AddStation(Station sta)  //just adding to the last place
        {
            if (DataSource.stations.Equals(sta))
                throw "ERROR: This station is already exists";
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
        public void ClearDroneCharge(int drnId)
        {
            int i = DataSource.droneCharges.FindIndex(w => w.DroneId == drnId);  //find the parcel that was supplied
            DroneCharge tempDroneCharge = DataSource.droneCharges[i];
            DataSource.droneCharges.Remove(tempDroneCharge);//removes the drone-charge
        }


        public IDAL.DO.Station PrintStation(int id)  //finds the station and sends a replica
        {
           return (DataSource.stations.Find(w => w.Id == id));
        }
    

        public IDAL.DO.Drone PrintDrone(int id)  //finds the drone and sends a replica
        {
            return (DataSource.drones.Find(w => w.Id == id));
        }

        public IDAL.DO.Customer PrintCustomer(int id)  //finds the customer and sends a replica
        {
            return (DataSource.customers.Find(w => w.Id == id));
        }

        public IDAL.DO.Parcel PrintParcel(int id)  //finds the station and sends a replica
        {
            return (DataSource.parcels.Find(w => w.Id == id));
        }

        public IEnumerable<Station> PrintStationList()  //creates a new array and returns that
        {
            return DataSource.stations;
        }
        public IEnumerable<Drone> PrintDroneList()  //creates a new array and returns that
        {
            return DataSource.drones;
        }
        public IEnumerable<Customer> PrintCustomerList()  //creates a new array and returns that
        {
            return DataSource.customers;
        }
        public IEnumerable<Parcel> PrintParcelList()  //creates a new array and returns that
        {
            return DataSource.parcels;
        }
        public IEnumerable<Parcel> PrintUnassignedParcels()  //creates a new array with the condition and returns that
        {
          return  (DataSource.parcels.FindAll(w=>w.DroneId==0));
        }
        public IEnumerable<Station> PrintAvailableChargingStations()  //creates a new array of available charging slots and returns that
        {
            return DataSource.stations.FindAll(w => w.ReadyChargeStands > 0);
        }
    }
}
