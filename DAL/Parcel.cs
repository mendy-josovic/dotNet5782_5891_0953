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
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public WEIGHT Weigh { set; get; }
            public PRIORITY Priority { set; get; }
            public int DroneId { get; set; }
            public DateTime Requested { set; get; }
            public DateTime SchedulId { set; get; }
            public DateTime PickedUp { set; get; }
            public DateTime Delivered { set; get; }
            public override string ToString()//override the to-string to print it nice
            {
                return "Id: " + Id + "\n " + "SenderId: " + SenderId + " \n" + "TargetId: " + TargetId + " \n" + " Weigh: " + Weigh + " \n" + "Priority: " + Priority + " \n" + "DroneId: " + DroneId + "\n" + "Requested: " + Requested + "\n" + "SchedulId: " + SenderId + "\n" + "PickedUp: " + PickedUp + " \n" + "Delivered: " + Delivered + "\n";
            }
        }
    }
}
