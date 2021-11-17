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
  public partial  class DalObject:IDal
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
        public double[] Consumption()
        {
            double[] arr = new double[]{ DataSource.Config.vacant,  DataSource.Config.LightWeightCarrier,  DataSource.Config.MediumWeightCarrier,  DataSource.Config.HeavyWeightCarrier,  DataSource.Config.ChargingRate };
            return arr;
        }
    }
}
