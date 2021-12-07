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
    public partial class DalObject : IDal
    {
        public void AddDrone(Drone dro)  //same
        {
            int i = DataSource.drones.FindIndex(w => w.Equals(dro));
            if (i > 0)
                throw new IDAL.DO.DalExceptions("Drone Alredy exsits");
            DataSource.drones.Add(dro);
        }
        public void DroneIdOfPArcel(int prcId, int drnId)
        {
            int i = DataSource.parcels.FindIndex(w => w.Id == prcId);
            if (i < 0)
                throw new IDAL.DO.DalExceptions("Drone Dosen't exsits");
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
                throw new IDAL.DO.DalExceptions("Drone Dosen't exsits");
            DataSource.drones[i].Model.Replace(DataSource.drones[i].Model,Name);
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
        public void UpdatParcel(int parclId, int SenderId = 0, int TargetId = 0,int DroneId=0, WEIGHT whihgt = 0, PRIORITY priorty = 0, int Updatereqwested = 0, int UpdatSchedueld = 0, int UpdatPicedup = 0, int UpdateDeleverd = 0)
        {
            int i = DataSource.parcels.FindIndex(w => w.Id == parclId);
            if (i < 0)
                throw new IDAL.DO.DalExceptions("Drone Dosen't exsits");
            if (SenderId != 0)
                DataSource.parcels[i].SenderId.CompareTo(SenderId);
            if (TargetId != 0)
                DataSource.parcels[i].TargetId.CompareTo(TargetId);
            if (DroneId != 0)
                DataSource.parcels[i].DroneId.CompareTo(DroneId);
            if (whihgt != 0)
                DataSource.parcels[i].Weigh.CompareTo(whihgt);
            if (priorty != 0)
                DataSource.parcels[i].Priority.CompareTo(priorty);
            //if (Updatereqwested != 0)
            //    DataSource.parcels[i].Requested=DateTime.Now;
            //if (UpdatSchedueld != 0)
            //    DataSource.parcels[i].Scheduled.CompareTo(DateTime.Now);
            //if (UpdatPicedup != 0)
            //    DataSource.parcels[i].PickedUp.CompareTo(DateTime.Now);
            //if (UpdateDeleverd != 0)
            //    DataSource.parcels[i].Delivered.CompareTo(DateTime.Now);
        }

    }
}
