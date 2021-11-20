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
        public void AddStation(Station sta);
        public void AddDrone(Drone dro);
        public void AddDroneToList(DroneToList dro);
        public Location GetMinimumDistance(Location a, List<Station> stations);
        public double GetDistance(Location a, Location b);
        
    }
}
