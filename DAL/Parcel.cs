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
            public int Id;
            public int SenderId;
            public int TargetId;
            public WEIGHT Weigh;
            public PRIORITY Priority;
            public DateTime Requsted;
            public int DroneId;
            public DateTime Scheduled;
            public DateTime PickedUp;
            public DateTime Deliverd;
        }

    }
}
