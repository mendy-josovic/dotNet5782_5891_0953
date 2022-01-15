using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using DO;
using DalApi;
namespace DalObject
{
    internal partial class DalObject : IDal
    {
        internal static IDal instance { get; } = new DalObject();
        public static IDal Instance { get => instance; }

        private DalObject() 
        {
            DataSource.Initialize();
        }

        public void CreateANewDroneCharge(int staId, int drnId)
        {

            DroneCharge Dc=new();
            Dc.DroneId = drnId;
            Dc.StationId = staId;
            Dc.EntryTimeForLoading = DateTime.Now;
            DataSource.droneCharges.Add(Dc);
        }  

        public void ClearDroneCharge(int drnId)
        {
            int i = DataSource.droneCharges.FindIndex(w => w.DroneId == drnId);  //find the parcel that was supplied

            DroneCharge tempDroneCharge = DataSource.droneCharges[i];
            DataSource.droneCharges.Remove(tempDroneCharge);//removes the drone-charge
        }

        /// 
        /// <summary>
        /// we finde the place with the station and the dronr we need 
        /// </summary>
        /// <param name="DroneId"></param>
        /// <param name="StationId"></param>
        /// <returns></returns>
        public DroneCharge DisplayDroneCharge(int DroneId = 0)
        {        
                if(!DataSource.droneCharges.Any(w => w.DroneId == DroneId))
                        throw new DalExceptions("No Drone Charg Exsits"); 
                return DataSource.droneCharges.Find(w => w.DroneId == DroneId);                      
        }

        public double[] Consumption()
        {
            double[] arr = new double[]{ DataSource.Config.vacant,  DataSource.Config.LightWeightCarrier,  DataSource.Config.MediumWeightCarrier,  DataSource.Config.HeavyWeightCarrier,  DataSource.Config.ChargingRate };
            return arr;
        }

         public int GetRuningNumber()
        {
            return ++DataSource.Config.RuningNumber;
        }
    }
}
