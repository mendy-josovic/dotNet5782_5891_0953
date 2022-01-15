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
    internal partial class DalObject:IDal
    {
        public void AddSParcel(Parcel prc)  //same
        {
            int i = DataSource.parcels.FindIndex(w => w.Equals(prc));
            if (i > 0)
                throw new DO.DalExceptions("Parcel Alredy exsits");
            DataSource.parcels.Add(prc);
        }
        public void ParcelScheduled(int prcId)
        {
            int i = DataSource.parcels.FindIndex(w => w.Id == prcId);  //find the parcel to assign
            if (i < 0)
                throw new DalExceptions("Parcel dosent exist");
            Parcel tempParcel = DataSource.parcels[i];
            tempParcel.Scheduled = DateTime.Now;  //get assigning time
            DataSource.parcels[i] = tempParcel;
        }


        public void PickUp(int prcId)
        {
            int i = DataSource.parcels.FindIndex(w => w.Id == prcId);  //find the parcel that was picked up
            if (i < 0)
                throw new DalExceptions("Parcel dosent exist");
            Parcel tempParcel = DataSource.parcels[i];
            tempParcel.PickedUp = DateTime.Now;
            DataSource.parcels[i] = tempParcel;  //update the pickup time
        }
        public void UpdateTimeOfSupplied(int prcId)
        {
            int i = DataSource.parcels.FindIndex(w => w.Id == prcId);  //find the parcel that was supplied
            if (i < 0)
                throw new DalExceptions("Parcel dosent exist");
            Parcel tempParcel = DataSource.parcels[i];
            tempParcel.Delivered = DateTime.Now;
            DataSource.parcels[i] = tempParcel;  //update the time of supplied
        }

        public Parcel PrintParcel(int id)  //finds the station and sends a replica
        {
            return (DataSource.parcels.Find(w => w.Id == id));
        }
        
        public IEnumerable<Parcel> PrintParcelList(Predicate<Parcel> predicate = null)  //returns a new list of parcels
        {
            return DataSource.parcels.FindAll(x => predicate == null ? true : predicate(x));
        }
        public IEnumerable<Parcel> PrintUnassignedParcels()  //returns a new list of parcels with the condition
        {
            return (DataSource.parcels.FindAll(w => w.DroneId == 0));
        }

    }
}
