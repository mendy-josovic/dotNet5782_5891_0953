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
    public partial class DalObject:IDal
    {
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

        public Parcel PrintParcel(int id)  //finds the station and sends a replica
        {
            return (DataSource.parcels.Find(w => w.Id == id));
        }

        public IEnumerable<Parcel> PrintParcelList()  //returns a new list of parcels
        {
            return DataSource.parcels;
        }
        public IEnumerable<Parcel> PrintUnassignedParcels()  //returns a new list of parcels with the condition
        {
            return (DataSource.parcels.FindAll(w => w.DroneId == 0));
        }

    }
}
