using System;
using System.Collections.Generic;
using System.Text;
using IDAL;
using IBL.BO;
using System.Collections;
using IBL;
namespace BL
{
    public partial class BL : IBL
    {
        public void AddDroneToList(DroneToList drt)
        {

        }
        public int Consumption(Location a, Location b, MODE_OF_DRONE_IN_MOVING mode)
        {
            return (int)(GetDistance(a, b) * batteryConfig[(int)mode]);
        }
    }
}
