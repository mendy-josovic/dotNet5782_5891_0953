using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
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
                return "Station:\nID: " + Id + "\nName: " + Name
                    + "\nNumber of stands that are ready in station: " + ReadyStandsInStation
                    + "\nNumber of stands that are occupied in station: " + OccupiedStandsInStation + "\n";
            }
        }
    }
}
