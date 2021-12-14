using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    namespace DO
    {
        public class DroneCharge
        {
            public int DroneId { get; set; }
            public int StationId { get; set; }
            public DroneCharge(int drone, int station)
            {
                DroneId = drone;
                StationId = station;
            }
        }
    }
