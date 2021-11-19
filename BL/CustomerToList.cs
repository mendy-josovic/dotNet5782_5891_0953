using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class CustomerToList
        {
            public int Id { get; set; }
            public String Name { get; set; }
            public String Phone { get; set; }
            public int ParcelsSentAndDelivered { get; set; }
            public int ParcelsSentAndNotDelivered { get; set; }
            public int ParcelsReceived { get; set; }
            public int ParcelsOnWayToCustomer { get; set; }
            public override string ToString()//override the to-string to print it nice
            {
                return "Customer ID: " + Id + "\nName: " + Name + "\nPhone" + Phone + "\nNumber of parcels that the customer sendered and they provided: "
                    + ParcelsSentAndDelivered + "\nNumber of parcels that the customer sendered but they doesn't provided yet: " + ParcelsSentAndNotDelivered
                    + "\nNumber of parcels that the customer got" + ParcelsReceived + "\nNumber of parcels on the way to the customer: " + ParcelsOnWayToCustomer + "\n";
            }
        }
    }
}
