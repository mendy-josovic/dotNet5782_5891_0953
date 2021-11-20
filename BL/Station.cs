using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class Station
        {
            public int Id { get; set; }
            public String Name { get; set; }
            public Location location { get; set; }
            public int ReadyStandsInStation { get; set; }
            public List<DroneInCharging> ListOfDrones = new List<DroneInCharging>();
            public Station() { }
            public Station(IDAL.DO.Station st)
            {
                Id = st.Id;
                Name = st.Name;
                location.Longitude = st.Longitude;
                location.Latitude = st.Latitude;
            }
            public override string ToString()//override the to-string to print it nice
            {
                return "Station:\nID: " + Id + "\nName: " + Name + "\nLocation: " + location +
                    "\nNumber of stands that are ready in station: " + ReadyStandsInStation
                    + "\nList of drones in charge: " + ListOfDrones + "\n";
            }
        }
    }
}