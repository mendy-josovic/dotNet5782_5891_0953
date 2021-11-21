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
        /// the func gets a drone and a parcle and checks if the drone can do the root and if can hoew much is the battery use of this root
        /// we creat 3 locations strting(the current drone positian)
        /// staping(the sender)
        /// finishing(the receiver)
        /// and if needed the closest charging station
        /// </summary>
        /// <param name="dro"></param>
        /// <param name="prc"></param>
        /// <returns></returns>
        public (bool,double) GetBatteryUseAndRootFeasibility(IBL.BO.DroneToList dro,IDAL.DO.Parcel prc)
        {                        
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


    }

}
