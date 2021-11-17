using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class Parcel
        {
            public int Id { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Recipient { get; set; }
            public WEIGHT Weight { get; set; }
            public PRIORITY Priority { get; set; }
            public DroneInParcel Drone;
            public DateTime TimeOfCreation = DateTime.Now;
            public DateTime Scheduled { set; get; }
            public DateTime PickedUp { set; get; }
            public DateTime Delivered { set; get; }
        }
    }
}
