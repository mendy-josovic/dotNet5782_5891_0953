using System;
using System.Collections.Generic;
using System.Text;

    namespace BO
    {
        public class StationToList
        {
            public int Id { get; set; }
            public String Name { get; set; }
            public int ReadyStandsInStation { get; set; }
            public int OccupiedStandsInStation { get; set; }
            public override string ToString()//override the to-string to print it nice
            {
                return "Station " + Id + ", " + Name
                    + ", Ready-to-use positions: " + ReadyStandsInStation
                    + ", Occupied positions: " + OccupiedStandsInStation + "\n";
            }
        }
    }
