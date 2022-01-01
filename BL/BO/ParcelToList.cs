using System;
using System.Collections.Generic;
using System.Text;

    namespace BO
    {
        public class ParcelToList
        {
            public int Id { get; set; }
            public String Sender { get; set; }
            public String Recipient { get; set; }
            public Weight Weight { get; set; }
            public Priority Priority { get; set; }
            public StatusOfParcel Status { get; set; }
            public override string ToString()//override the to-string to print it nice
            {
                return "Parcel:\nID: " + Id + "\nSender:\n" + Sender + "\nRecipient:\n" + Recipient +
                    "\nWeigh: " + Weight + "\nPriority: " + Priority + "\nStatus: " + Status + "\n";
            }
        }
    }
