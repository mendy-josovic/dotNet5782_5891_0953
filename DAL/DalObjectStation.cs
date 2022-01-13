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
        public void UpdateReadyStandsInStation(int staId)
        {
            int i = DataSource.stations.FindIndex(w => w.Id == staId);  //find the station
            if (i < 0)
                throw new DalExceptions("Station dosen't exist");
            Station tempStation = DataSource.stations[i];
            tempStation.ReadyChargeStands--;
            DataSource.stations[i] = tempStation;
        }
        public Station PrintStation(int id)  //finds the station and sends a replica
        {
            return (DataSource.stations.Find(w => w.Id == id));
        }
        public IEnumerable<Station> PrintStationList(Predicate<Station> predicate = null)  //returns a new list of stations
        {
            return DataSource.stations.FindAll(x => predicate == null ? true : predicate(x));
        }


        /// <summary>
        /// the func gets the id of the statoin and the name and number of cargin slots
        /// it is with a dfult empty so we can chang only one of the field
        /// </summary>
        /// <param name="StationId"></param>
        /// <param name="Name"></param>
        /// <param name="NumOfCarg"></param>
        public void UpdatStation(int StationId, string Name = "", int NumOfCarg = -1)
        {
            int i = DataSource.stations.FindIndex(w => w.Id ==StationId);
            if (i < 0)
                throw new DalExceptions("Station dosent exist");
            Station temp = PrintStation(StationId);
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
