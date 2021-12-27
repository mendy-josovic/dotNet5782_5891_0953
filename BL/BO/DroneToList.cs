using System;
using System.Collections.Generic;
using System.Text;


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

            public override string ToString()
            {
                return "Drone " + Id + ", " + Model + ", " + MaxWeight
                    + ", Battery: " + Battery + "%, : " + status
                    + ", Location: Longitude - " + ThisLocation.Longitude + ", Latitude - " + ThisLocation.Latitude + ", Parcel in transfer - " + ParcelId + "\n";
            }
        }
    }

