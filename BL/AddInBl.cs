using System;
using System.Collections.Generic;
using System.Text;
using IDAL;
using IBL.BO;
using System.Collections;
using IBL;
namespace BL
{
    public partial class BL : IBl
    {
        /// <summary>
        /// gets a station of BL and adds a station to the data
        /// </summary>
        /// <param name="sta"></param>
        public void AddStation(Station sta)
        {
            IDAL.DO.Station station = new IDAL.DO.Station();
            station.Id = sta.Id;
            station.Name = sta.Name;
            station.Longitude = sta.location.Longitude;
            station.Latitude = sta.location.Latitude;
            station.ReadyChargeStands = sta.ReadyStandsInStation;
            List<IDAL.DO.Station> tempDataStations = new List<IDAL.DO.Station>(Data.PrintStationList());
            foreach(IDAL.DO.Station state in tempDataStations)
            {
                if (state.Id == station.Id)
                    throw "The station is already exists\n";
            }
            Data.AddStation(station);          
        }

        /// <summary>
        /// gets a drone of BL and adds a drone to the data
        /// </summary>
        /// <param name="dro"></param>
        public void AddDrone(Drone dro)
        {
            IDAL.DO.Drone drone = new IDAL.DO.Drone();
            drone.Id = dro.Id;
            drone.Model = dro.Model;
            drone.MaxWeight = (IDAL.DO.WEIGHT)dro.MaxWeight;
            foreach(DroneToList drn in DroneList)
            {
                if (dro.Id == drn.Id)
                    throw "The drone is already exists\n";
            }
            Data.AddDrone(drone);
            IDAL.DO.Station station = new IDAL.DO.Station();
            List<IDAL.DO.Station> tempDataStations = new List<IDAL.DO.Station>(Data.PrintStationList());
            if (tempDataStations.FindIndex(w => w.Longitude == dro.ThisLocation.Longitude) < 0)
                throw "there is not a station with this ID\n";
            drone.ThisLocation =
        }
    }
}
