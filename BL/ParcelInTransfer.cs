using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class ParcelInTransfer
        {
            public int Id { get; set; }
            public bool PickedUp { get; set; }
            public PRIORITY Priority { get; set; }
            public WEIGHT Weight { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Recipient { get; set; }
            public Location LocationOfPickedUp { get; set; }
            public Location LocationOfDestination { get; set; }
            public double Distance { get; set; }
        }
    }
}
