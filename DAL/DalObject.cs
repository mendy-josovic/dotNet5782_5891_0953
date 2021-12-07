using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using IDAL.DO;
using IDAL;
namespace DalObject
{
  public partial class DalObject: IDal
    {
        public DalObject() 
        {
            DataSource.Initialize();
        }

        public void CreateANewDroneCharge(int staId, int drnId)
        {
            DataSource.droneCharges.Add(new DroneCharge(drnId, staId));
        }
        
        public void ClearDroneCharge(int drnId)
        {
            int i = DataSource.droneCharges.FindIndex(w => w.DroneId == drnId);  //find the parcel that was supplied
            DroneCharge tempDroneCharge = DataSource.droneCharges[i];
            DataSource.droneCharges.Remove(tempDroneCharge);//removes the drone-charge
        }
        /// <summary>
        /// we finde the place with the station and the dronr we need 
        /// </summary>
        /// <param name="DroneId"></param>
        /// <param name="StationId"></param>
        /// <returns></returns>
        public DroneCharge PrintDronCarg(int DroneId = 0, int StationId = 0)
        {
            int i = 0;
            if(DroneId!=0)
              i=  DataSource.droneCharges.FindIndex(w => w.DroneId == DroneId);
            if(StationId!=0)
                i= DataSource.droneCharges.FindIndex(w => w.StationId == StationId);
            if (i<0)
                throw new DalExceptions("ERROR Cant find (Dron Or Station Not Fuond) ");
            IDAL.DO.DroneCharge droneCharge = new IDAL.DO.DroneCharge(DataSource.droneCharges[i].DroneId, DataSource.droneCharges[i].StationId);
            return droneCharge;
        }
        public double[] Consumption()
        {
            double[] arr = new double[]{ DataSource.Config.vacant,  DataSource.Config.LightWeightCarrier,  DataSource.Config.MediumWeightCarrier,  DataSource.Config.HeavyWeightCarrier,  DataSource.Config.ChargingRate };
            return arr;
        }
    }
}
