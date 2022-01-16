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

        public void AddStation(Station sta)  //just adding to the last place
        {
            int i = DataSource.stations.FindIndex(w => w.Equals(sta));
            if (i > 0)
                throw new DalExceptions("Station Alredy exsits");///theowin the exciption of elerdy exsit
            DataSource.stations.Add(sta);
        }
        public Station DisplayStation(int id)  //finds the station and sends a replica
        {
            return (DataSource.stations.Find(w => w.Id == id));
        }
        public IEnumerable<Station> PrintStationList(Predicate<Station> predicate = null)  //returns a new list of stations
        {
            return DataSource.stations.FindAll(x => predicate == null ? true : predicate(x));
        }

        public void UpdatStation(int StationId, string Name = "", int NumOfCarg = -1)
        {
            int i = DataSource.stations.FindIndex(w => w.Id ==StationId);
            if (i < 0)
                throw new DalExceptions("Station dosent exist");
            Station temp = DisplayStation(StationId);
            if (!string.IsNullOrEmpty(Name))
            {
                temp.Name = Name;
            }
            if (!(NumOfCarg==-1))
            {
                temp.ReadyChargeStands = NumOfCarg;
            }
            DataSource.stations[i] = temp;

        }
    }
}
