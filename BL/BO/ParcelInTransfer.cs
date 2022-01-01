using System;
using System.Collections.Generic;
using System.Text;

   namespace BO
    {
        public class ParcelInTransfer
        {
            public int Id { get; set; }
            public bool PickedUp { get; set; }
            public Priority Priority { get; set; }
            public Weight Weight { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Recipient { get; set; }
            public Location LocationOfPickedUp { get; set; }
            public Location LocationOfDestination { get; set; }
            public double Distance { get; set; }
            public override string ToString()//override the to-string to print it nice
            {
                return "The parcel in transfer:\nID: " + Id + "Picked up: " + PickedUp + "\nPriority: " + Priority
                    + "\nWeigh: " + Weight + "\nSender:\n" + Sender + "\nRecipient:\n" + Recipient
                    + "\nLocation of picked up: " + LocationOfPickedUp + "\nLocation of destination: " + LocationOfDestination
                    + "\nTransport distance: " + Distance + "\n";
            }
        }
    }
