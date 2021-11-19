using System;
using System.Collections.Generic;
using System.Text;
using IDAL;
using IBL.BO;
using System.Collections;
using IBL;
namespace BL
{
    public partial class BL : IBl
    {
        public void AddDroneToList(DroneToList drt)
        {

        }
        /// <summary>
        /// the func gets the statis of the parcel and acourdingly 
        /// returns the consumption
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int Consumption(Location a, Location b, MODE_OF_DRONE_IN_MOVING mode)
        {
            return (int)(GetDistance(a, b) * batteryConfig[(int)mode]);
        }
    }
}
