﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using DO;
using DalApi;
using System.Runtime.CompilerServices;
namespace DalObject
{
    internal partial class DalObject : IDal
    {
        internal static IDal instance { get; } = new DalObject();//singelton
        public static IDal Instance { get => instance; }
        /// <summary>
        /// for the intilizing info  
        /// </summary>
        private DalObject() 
        {
            DataSource.Initialize();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CreateANewDroneCharge(int staId, int drnId)
        {

            DroneCharge Dc=new();
            Dc.DroneId = drnId;
            Dc.StationId = staId;
            Dc.EntryTimeForLoading = DateTime.Now;
            DataSource.droneCharges.Add(Dc);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ClearDroneCharge(int drnId)
        {
            int i = DataSource.droneCharges.FindIndex(w => w.DroneId == drnId);  //find the parcel that was supplied

            DroneCharge tempDroneCharge = DataSource.droneCharges[i];
            DataSource.droneCharges.Remove(tempDroneCharge);//removes the drone-charge
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneCharge DisplayDroneCharge(int DroneId = 0)
        {        
                if(!DataSource.droneCharges.Any(w => w.DroneId == DroneId))
                        throw new DalExceptions("No Drone charge exists"); 
                return DataSource.droneCharges.Find(w => w.DroneId == DroneId);                      
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] Consumption()
        {
            double[] arr = new double[]{ DataSource.Config.vacant,  DataSource.Config.LightWeightCarrier,  DataSource.Config.MediumWeightCarrier,  DataSource.Config.HeavyWeightCarrier,  DataSource.Config.ChargingRate };
            return arr;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int GetRuningNumber()
        {
            return ++DataSource.Config.RuningNumber;
        }
    }
}
