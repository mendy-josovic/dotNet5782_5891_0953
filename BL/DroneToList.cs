using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace IBL
{
    namespace BO
    {
        public class DroneToList
        {
            public int Id { get; set; }
            public String Model { set; get; }
            public WEIGHT MaxWeight { set; get; }
            public double Battery { get; set; }
            public STATUS_OF_DRONE status { get; set; }
            public Location ThisLocation { get; set; }
            public int ParcelId { get; set; }

            /
            public override string ToString()
            {
                return "Drone:\nID: " + Id + "\nModel: " + Model + "\nMaximum weight: " + MaxWeight
                    + "\nBattery percentages available: " + Battery + "\nStatus: " + status
                    + "\nLocation of the drone: " + ThisLocation + "\nID of parcel in transfer" + ParcelId + "\n";
            }
        }
    }
}
