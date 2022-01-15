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
        public void DeleteParcel(int id)
        {
            if (!DataSource.parcels.Exists(x => x.Id == id))
                throw new DO.DalExceptions("Drone Parcel Dose Not exsits");
            Parcel parcel = DataSource.parcels.Find(x => x.Id == id);
            DataSource.parcels.Remove(parcel);
        }

        public Parcel PrintParcel(int id)  //finds the station and sends a replica
        {
            return (DataSource.parcels.Find(w => w.Id == id));
        }
        
        public IEnumerable<Parcel> PrintParcelList(Predicate<Parcel> predicate = null)  //returns a new list of parcels
        {
            return DataSource.parcels.FindAll(x => predicate == null ? true : predicate(x));
        }
      
    }
}
