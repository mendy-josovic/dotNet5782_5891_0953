using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace DO
    {
        public struct Station
        {
            public int Id { get; set; }
            public String Name { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public int ReadyChargeStands { get; set; }
            public override string ToString()
            {
                return "ID: " + Id + "\nName: " + Name + "\nLongitude: " +Longitude + "\nLatitude: " + Latitude + "\nReady charging stands: " + ReadyChargeStands + "\n";
            }
        }
    }
