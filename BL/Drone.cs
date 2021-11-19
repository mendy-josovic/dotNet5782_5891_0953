using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class Drone
        {
            public int Id { get; set; }
            public String Model { set; get; }
            public WEIGHT MaxWeight { set; get; }
            public double Battery { get; set; }
            public STATUS_OF_DRONE status { get; set; }
            public ParcelInTransfer parcel { get; set; }
            public Location ThisLocation { get; set; }
            public override string ToString()
            {
                return "Drone:\nID: " + Id + "\nModel: " + Model + "\nMaximum weight: " + MaxWeight
                    + "\nBattery percentages available: " + Battery + "\nStatus: " + status
                    + "\nParcel in transfer: " + parcel + "\nLocation of the drone: " + ThisLocation + "\n";
            }
        }
    }
}
