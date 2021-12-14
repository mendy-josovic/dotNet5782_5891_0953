using System;
using System.Collections.Generic;
using System.Text;


    namespace BO
    {
        public class Parcel
        {
            public int Id { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Recipient { get; set; }
            public WEIGHT Weight { get; set; }
            public PRIORITY Priority { get; set; }
            public DroneInParcel Drone { get; set; }
            public DateTime? TimeOfCreation = DateTime.Now;
            public DateTime? Scheduled { set; get; }
            public DateTime? PickedUp { set; get; }
            public DateTime? Delivered { set; get; }
            public override string ToString()//override the to-string to print it nice
            {
                return "Parcel:\nID: " + Id + "\nSender:\n" + Sender + "\nRecipient:\n" + Recipient +
                    "\nWeigh: " + Weight + "\nPriority: " + Priority + Drone + "\nTime of creation: " + TimeOfCreation
                    + "\nTime of assignment: " + Scheduled +  "\nTime of parcel picked up: " + PickedUp
                    + "\nTime of delivered: " + Delivered + "\n";
            }
        }
    }
