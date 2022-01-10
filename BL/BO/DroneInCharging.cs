using System;
using System.Collections.Generic;
using System.Text;


    namespace BO
    {
        public class DroneInCharging
        {
            public int Id { get; set; }
            public double Battery { get; set; }
            public DateTime? EntryTimeForLoading { get; set; }
            public DroneInCharging() { }
            public override string ToString()
            {
                return "ID: " + Id + " Battery: " + Battery + "%" + "\n";
            }
        }
    }

