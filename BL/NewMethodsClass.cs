using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace ExtentionMethods
{
    static class NewMethodsClass
    {
        public static Drone BLDrone(this Drone d, IDAL.DO.Drone dro)
        {
            Drone drone = new();
            drone.Id = dro.Id;
            drone.Model = dro.Model;
            drone.MaxWeight = (WEIGHT)dro.MaxWeight;
            drone.Battery = dro.
            return drone;
        }
    }
}
