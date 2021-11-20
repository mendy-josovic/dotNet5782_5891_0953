using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class Customer
        {
            public int Id { get; set; }
            public String Name { get; set; }
            public String Phone { get; set; }
            public Location location { get; set; }
            public List<ParcelAtCustomer> FromCustomer { get; set; }
            public List<ParcelAtCustomer> ToCustomer { get; set; }
            public override string ToString()//override the to-string to print it nice
            {
                return "Customer:\nID: " + Id + "\nName: " + Name + "\nPhone" + Phone + "\nLocation" + location + "\nA list of parcels that sendered from the customer:\n" + FromCustomer + "\nA list of parcels that sendered to the customer:\n" + ToCustomer + "\n";
            }
        }
    }
}
