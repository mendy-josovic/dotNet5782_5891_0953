using System;
using System.Collections.Generic;
using System.Text;


    namespace BO
    {
        public class ParcelAtCustomer
        {
            public int Id { get; set; }
            public WEIGHT Weight { get; set; }
            public PRIORITY Priority { get; set; }
            public STATUS_OF_PARCEL Status { get; set; }
            public CustomerInParcel TheOther { get; set; }
            public override string ToString()//override the to-string to print it nice
            {
                return "Parcel ID: " + Id + "\nWeigh of parcel: " + Weight + "\nPriority: " + Priority + "\nStatus: " + Status + "\nThe other person: " + TheOther + "\n";
            }
        }
    }

