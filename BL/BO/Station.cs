using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


    namespace BO
    {
        public class Station
        {
            public int Id { get; set; }
            public String Name { get; set; }
            public Location location { get; set; }
            public int ReadyStandsInStation { get; set; }
            public List<DroneInCharging> ListOfDrones = new();
            public override string ToString()//override the to-string to print it nice
            {
                if (ListOfDrones.Count > 0)
                    return "Station:\nID: " + Id + "\nName: " + Name + "\nLocation: " + location +
                        "Number of stands that are ready in station: " + ReadyStandsInStation
                        + "\nList of drones in charge: " + ListOfDrones + "\n";
                else
                    return "Station:\nID: " + Id + "\nName: " + Name + "\nLocation: " + location +
                       "Number of stands that are ready in station: " + ReadyStandsInStation
                       + "\n";
            }
        }
    }
