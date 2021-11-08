﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        class Station
        {
            public int Id { get; set; }
            public String Name { get; set; }
            public Location location { get; set; }
            public int ReadyStandsInStation { get; set; }
            public List<DroneInCharging> ListOfDrones = new List<DroneInCharging>();
        }
    }
}