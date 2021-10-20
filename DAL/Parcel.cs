using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            public int ID { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public WEIGHT Weigh { set; get; }
            public PRIORITY Priority { set; get; }
            public int DroneId { get; set; }
            public DateTime Requested;
            public DateTime SchedulId;
            public DateTime PickedUp;
            public DateTime Delivered;
        }

    }
}
