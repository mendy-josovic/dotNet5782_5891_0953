using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using IBL.BO;
namespace IBL
{
    public interface IBl
    {
        public void AddDroneToList(DroneToList drt);
        public Location GetMinimumDistance(Location a, List<Station> stations);
        public double DistanceOfRout(Location lo, List<Location> lislo);
        public double GetDistance(Location a, Location b);
        
    }
}
