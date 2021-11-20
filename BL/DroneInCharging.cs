using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class DroneInCharging
        {
            public int Id { get; set; }
            public double Battery { get; set; }
            public override string ToString()
            {
                return "Drone:\nID: " + Id + "\nBattery percentages available: " + Battery + "\n";
            }
        }
    }
}
