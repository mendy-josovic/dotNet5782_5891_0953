using System;
using System.Collections.Generic;
using System.Text;


    namespace BO
    {
        public class DroneInCharging
        {
            public int Id { get; set; }
            public double Battery { get; set; }
            public DroneInCharging() { }
            public override string ToString()
            {
                return "Drone:\nID: " + Id + "\nBattery percentages available: " + Battery + "\n";
            }
        }
    }

