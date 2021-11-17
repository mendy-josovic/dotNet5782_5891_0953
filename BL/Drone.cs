using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class Drone
        {
            public int Id { get; set; }
            public String Model { set; get; }
            public WEIGHT MaxWeight { set; get; }
            public double Battery { get; set; }
            public STATUS_OF_DRONE status { get; set; }
            public ParcelInTransfer parcel { get; set; }
            public Location ThisLocation { get; set; }
        }
    }
}
