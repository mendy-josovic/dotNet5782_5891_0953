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
            DataSource.drones.Add(dro);
        }
        public void DroneIdOfPArcel(int prcId, int drnId)
        {
            int i = DataSource.parcels.FindIndex(w => w.Id == prcId);
            Parcel tempParcel = DataSource.parcels[i];
            tempParcel.DroneId = drnId;
            DataSource.parcels[i] = tempParcel;
        }
        public Drone PrintDrone(int id)  //finds the drone and sends a replica
        {
            return (DataSource.drones.Find(w => w.Id == id));
        }
        public IEnumerable<Drone> PrintDroneList()  //returns a new list of drones
        {
            return DataSource.drones;
        }

    }
}
