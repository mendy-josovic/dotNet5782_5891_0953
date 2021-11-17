using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class ParcelAtCustomer
        {
            public int Id { get; set; }
            public WEIGHT Weight { get; set; }
            public PRIORITY Priority { get; set; }
            public STATUS_OF_PARCEL Status { get; set; }
            public CustomerInParcel TheOther { get; set; }
        }
    }
}
