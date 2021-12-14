using System;
using System.Collections.Generic;
using System.Text;


    namespace BO
    {
        public class DroneInParcel
        {
            public int Id { get; set; }
            public double Battery { get; set; }
            public Location ThisLocation { get; set; }
            public override string ToString()//override the to-string to print it nice
            {
                return "Drone ID: " + Id + "\nBattery percentages available: " + Battery + "\nLocation of drone:\n" + ThisLocation + "\n";
            }
        }
    }

