using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        class CustomerToList
        {
            public int Id { get; set; }
            public String Name { get; set; }
            public String Phone { get; set; }
            public int ParcelsSentAndDelivered { get; set; }
            public int ParcelsSentAndNotDelivered { get; set; }
            public int ParcelsReceived { get; set; }
            public int ParcelsOnWayToCustomer { get; set; }
        }
    }
}
