using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            public DateTime? Requested { set; get; }
            public DateTime? Scheduled { set; get; }
            public DateTime? PickedUp { set; get; }
            public DateTime? Delivered { set; get; }
            public override string ToString()//override the to-string to print it nice
            {
                return "ID: " + Id + "\nSender ID: " + SenderId + " \nTarget ID: " + TargetId + "\nWeigh: " + Weigh + "\nPriority: " + Priority + "\nDrone ID: " + DroneId + "\nTime of requested: " + Requested + "\nTime of scheduled: " + Scheduled + "\nTime of picked up: " + PickedUp + "\nTime of delivered: " + Delivered + "\n";
            }
        }
    }

