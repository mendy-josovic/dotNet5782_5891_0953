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
            public DroneToList(IDAL.DO.Drone dr)
            {
                Id = dr.Id;
                Model = dr.Model;
                MaxWeight = (WEIGHT)dr.MaxWeight;
            }
        }
    }
}
