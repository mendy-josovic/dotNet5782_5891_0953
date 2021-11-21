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
        /// <summary>
        /// the func gets the status of the parcel and acourdingly returns the consumption
        /// </summary>
        /// <param name="a">location a</param>
        /// <param name="b">location b</param>
        /// <param name="mode">mode of the drone in moving: without a parcel, or with a parcel, and which mode of parcel</param>
        /// <returns>how much battery this moving need</returns>
        public double Consumption(Location a, Location b, MODE_OF_DRONE_IN_MOVING mode)
        {
            return GetDistance(a, b) * batteryConfig[(int)mode];
        }
        public (bool,int) GetBatteryUseAndRootFeasibility(IBL.BO.DroneToList dro,IDAL.DO.Parcel prc)
        {
            IDAL.DO.Customer sender = new IDAL.DO.Customer();
            sender= Data.PrintCustomer(prc.SenderId);
            IDAL.DO.Customer Receiver = new IDAL.DO.Customer();
            Receiver = Data.PrintCustomer(prc.TargetId);
            IDAL.DO.Station closeststation= new IDAL.DO.Station();
            closeststation=Data.PrintStation(Ge)
            Location startingPiont = new Location(dro.ThisLocation.Longitude, dro.ThisLocation.Latitude);
            Location StapingPiont = new Location(Receiver.Longitude, Receiver.Latitude);
            Location ClosestCarging=
            double batteryUse=
            return
            
        }

        public IEnumerable<DroneToList> BLDrones()
        {
            return DroneList;
        }
    }

}
