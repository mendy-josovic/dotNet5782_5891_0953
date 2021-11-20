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

    }

}
