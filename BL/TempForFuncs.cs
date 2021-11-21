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
            Location startingPiont = dro.ThisLocation;
            Location StapingPiont = GetSenderLo(prc);
            Location FinishingPiont = GetReceiverLo(prc);
            closeststation = Data.PrintStation(GetClosestStation(FinishingPiont));
            Location ClosestCarging = new Location(closeststation.Longitude, closeststation.Latitude);
            double batteryUse = Consumption(startingPiont, StapingPiont, IBL.BO.MODE_OF_DRONE_IN_MOVING.AVAILABLE) + Consumption(StapingPiont, FinishingPiont, (IBL.BO.MODE_OF_DRONE_IN_MOVING)prc.Weigh);
            if (dro.Battery - batteryUse < 20)
            {
                batteryUse += Consumption(FinishingPiont, ClosestCarging, IBL.BO.MODE_OF_DRONE_IN_MOVING.AVAILABLE);
                if (dro.Battery - batteryUse < 0)
                    return (false, 0);
            }
            return (true, batteryUse);
            
        }
        /// <summary>
        /// the func returns location of thr sender 
        /// </summary>
        /// <param name="pr"></param>
        /// <returns></returns>
        public Location GetSenderLo(IDAL.DO.Parcel pr)
        {
            IDAL.DO.Customer cs = Data.PrintCustomer(pr.SenderId);
            Location newloc = new Location(cs.Longitude, cs.Latitude);
            return newloc;
        }
        /// <summary>
        /// the func returns location of the rtraget
        /// </summary>
        /// <param name="pr"></param>
        /// <returns></returns>
        public Location GetReceiverLo(IDAL.DO.Parcel pr)
        {
            IDAL.DO.Customer cs = Data.PrintCustomer(pr.TargetId);
            Location newloc = new Location(cs.Longitude, cs.Latitude);
            return newloc;
        }

        public IEnumerable<DroneToList> BLDrones()
        {
            return DroneList;
        }
    }

}
