using System;
using System.Collections.Generic;
using System.Text;

    namespace BO
    {
        public class Drone
        {
            public int Id { get; set; }
            public String Model { set; get; }
            public Weight MaxWeight { set; get; }
            public double Battery { get; set; }
            public StatusOfDrone status { get; set; }
            public ParcelInTransfer parcel { get; set; }
            public Location ThisLocation { get; set; }
            public override string ToString()
            {
                return "Drone:\nID: " + Id + "\nModel: " + Model + "\nMaximum weight: " + MaxWeight
                    + "\nBattery percentages available: " + Battery + "\nStatus: " + status
                    + "\nParcel in transfer: " + parcel + "\nLocation of the drone:\n" + ThisLocation + "\n";
            }
        }
    }
