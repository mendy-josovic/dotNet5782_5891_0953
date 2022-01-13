using System;
using System.Collections.Generic;
using System.Text;


    namespace BO
    {
        public class DroneToList
        {
            public int Id { get; set; }
            public String Model { set; get; }
            public Weight MaxWeight { set; get; }
            public double Battery { get; set; }
            public StatusOfDrone status { get; set; }
            public Location ThisLocation { get; set; }
            public int ParcelId { get; set; }

            public override string ToString()
            {
                return "Drone " + Id + ", " + Model + ", " + MaxWeight
                    + ", Battery: " + Battery + "%, : " + status
                    + ", Location: Longitude - " + FuncForToString.ConvertToSexagesimal(ThisLocation.Longitude) + ", Latitude - " + FuncForToString.ConvertToSexagesimal( ThisLocation.Latitude) + ", Parcel in transfer - " + ParcelId + "\n";
            }
        }
    }

