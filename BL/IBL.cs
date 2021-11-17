using System;
using System.Collections.Generic;
using System.Text;
using IBL.BO;
namespace IBL
{
    interface IBL
    {
        public void AddDroneToList(DroneToList drt);
        public  Location GetMinimumDistance(Location a, List<Station> stations);
        public double DistanceOfRout(Location lo, List<Location> lislo);
    }
}
