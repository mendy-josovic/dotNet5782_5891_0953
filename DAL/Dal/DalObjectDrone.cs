using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using DO;
using DalApi;
namespace DalObject
{
    internal partial class DalObject : IDal
    {
        public void AddDrone(Drone dro)  //same
        {
            int i = DataSource.drones.FindIndex(w => w.Id == dro.Id);
            if (i > 0)
                throw new DO.DalExceptions("Drone Already exists");
            if(dro.Id<=0)
                throw new DO.DalExceptions("Invalid ID, ID must be positive");
            DataSource.drones.Add(dro);
        }
        public void DroneIdOfPArcel(int prcId, int drnId)
        {
            int i = DataSource.parcels.FindIndex(w => w.Id == prcId);
            if (i < 0)
                throw new DO.DalExceptions("Drone Dosen't exist");
            Parcel tempParcel = DataSource.parcels[i];
            tempParcel.DroneId = drnId;
            DataSource.parcels[i] = tempParcel;
        }
        public Drone PrintDrone(int id)  //finds the drone and sends a replica
        {
            return (DataSource.drones.Find(w => w.Id == id));
        }
        public IEnumerable<Drone> PrintDroneList(Predicate<Drone> predicate = null)  //returns a new list of drones
        {
            return DataSource.drones.FindAll(x => predicate == null ? true : predicate(x));
        }


        /// <summary>
        /// the func gets a new name and replaces the name in the func with a new one
        ///using the library func replace"
        /// </summary>
        /// <param name="drnId"></param>
        /// <param name="Name"></param>
        public void UpdateDrone(int drnId, string Name)
        {
            int i = DataSource.drones.FindIndex(w => w.Id == drnId);
            if (i < 0)
                throw new DO.DalExceptions("Drone Dosen't exsits");
            Drone tempdrone = DataSource.drones[i];
            tempdrone.Model = Name;
            DataSource.drones[i] = tempdrone;
        }
        /// <summary>
        /// the funvc gets the updating parameters and fill in acording to what we have
        /// </summary>
        /// <param name="parclId"></param>
        /// <param name="SenderId"></param>
        /// <param name="TargetId"></param>
        /// <param name="whihgt"></param>
        /// <param name="priorty"></param>
        /// <param name="Updatereqwested"></param>
        /// <param name="UpdatSchedueld"></param>
        /// <param name="UpdatPicedup"></param>
        /// <param name="UpdateDeleverd"></param>
        public void UpdatParcel(int parclId, int SenderId = 0, int TargetId = 0, int DroneId = 0, Weight whihgt = 0, Priority priorty = 0, int Updatereqwested = 0, int UpdatSchedueld = 0, int UpdatPicedup = 0, int UpdateDeleverd = 0)
        {
            int index = DataSource.parcels.FindIndex(w => w.Id == parclId);

            if (index == -1)
                throw new DO.DalExceptions("Drone Parcel Dose Not exsits");

            Parcel parcel = DataSource.parcels[index];

            if (SenderId != 0)
                parcel.SenderId = SenderId;
            if (TargetId != 0)
                parcel.TargetId = TargetId; 
            if (DroneId != 0)
                parcel.DroneId = DroneId;
            if (whihgt != 0)
                parcel.Weigh = whihgt;
            if (priorty != 0)
                parcel.Priority = priorty;
            if (Updatereqwested != 0)
                parcel.Requested = DateTime.Now;
            if (UpdatPicedup != 0)
                parcel.PickedUp = DateTime.Now;
            if (UpdatSchedueld != 0)
                parcel.Scheduled = DateTime.Now;
            if (UpdateDeleverd != 0)
                parcel.Delivered = DateTime.Now;
            DataSource.parcels[index] = parcel;
        }
     
      
        public IEnumerable<DroneCharge> DisplayDronesInCharging(Predicate<DroneCharge> predicate = null)
        {
            return DataSource.droneCharges.FindAll(x => predicate == null ? true : predicate(x));
        }
    }
}
