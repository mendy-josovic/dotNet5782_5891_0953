using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class DroneInParcel
        {
            public int Id { get; set; }
            public double Battery { get; set; }
            public Location ThisLocation { get; set; }
        }
    }
}
