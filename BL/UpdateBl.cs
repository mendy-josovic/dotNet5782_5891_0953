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
        /// the func makes sure that the drone exists and 
        /// if yes send the parameters to the update func in dal objects
        /// these r the only tow places 
        /// </summary>
        /// <param name="DroneId"></param>
        /// <param name="Name"></param>
        public void UpdatDroneName(int DroneId, string Name)
        {
            if (!DroneList.Exists(w => w.Id == DroneId))
                throw "Drone doesn't exist\n";
            int i = DroneList.FindIndex(w => w.Id == DroneId);
            DroneList[i].Model = Name;
            Data.UpdateDrone(DroneId, Name);
        }
        /// <summary>
        /// the func gets the id and finds if in exsits (we assume tha the parameters are valid and not a problem (we chack that on the console level) )
        /// ane use the updat func
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        public void UpdateCosomerInfo(int Id,string Name, string Phone)
        {
            List<IDAL.DO.Customer> Tempcustomers = new List<IDAL.DO.Customer>(Data.PrintCustomerList());
            if(!(Tempcustomers.Exists(w=>w.Id==Id)))
                throw "Customer doesn't exsit\n";
            Data.UpdateCustomer(Id, Name, Phone);
        
        }
        /// <summary>
        /// the func gets the id finsds if it exsits and the changes 
        /// are with the pararmtets
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="numofCha"></param>
        public void UpdateStstion(int Id, string Name,int numofCha)
        {
            List<IDAL.DO.Station> Tempstation = new List<IDAL.DO.Station>(Data.PrintStationList());
            if (!(Tempstation.Exists(w => w.Id == Id)))
                throw "Customer doesn't exsit\n";
            Data.UpdatStation(Id, Name,numofCha);
        }
        /// <summary>
        /// we make 4 initial checks 1. macke sure that the drone exits
        /// 2. checkinfg if the drone is availble 
        /// 3. checking if the station is availble
        /// 4. checking if we have enough battery
        /// then we updat the drone in the dronre to-list
        /// </summary>
        /// <param name="DronId"></param>
   
        public void SendDroneToCarge(int DronId)
        {
            int i = DroneList.FindIndex(w => w.Id == DronId);
            if (i == 0)
                throw "Drone doesn't exist\n";
            if (!(DroneList[i].status == STATUS_OF_DRONE.AVAILABLE))
                throw "Charhing Not Possible (Drone nNot Availble)\n";

            int j = GetMinimumDistance(DroneList[i].ThisLocation);//geting the id of station that we need to charge
            IDAL.DO.Station tempstaton = new IDAL.DO.Station();
            tempstaton = Data.PrintStation(j);//a temporary station (like the one to charg) 
            if (tempstaton.ReadyChargeStands == 0)
                throw "Charhing Not Possible (Station Cargin slots are full)\n";
            Location location = new Location(tempstaton.Longitude, tempstaton.Latitude);//checking that we have enough battery by geting the ditence and the battery cunsomption
            if (DroneList[i].Battery < Consumption(DroneList[i].ThisLocation, location, IBL.BO.MODE_OF_DRONE_IN_MOVING.AVAILABLE))
                throw "Charhing Not Possible (Not Enough Battery)\n";
            DroneList[i].status = IBL.BO.STATUS_OF_DRONE.IN_MAINTENANCE;//updating the drone status
            DroneList[i].Battery -= Consumption(DroneList[i].ThisLocation, location, IBL.BO.MODE_OF_DRONE_IN_MOVING.AVAILABLE);//updating the battery
            DroneList[i].ThisLocation = location;//updating the location
            Data.UpdatStation(j,"",tempstaton.ReadyChargeStands-1); //updating the redy charging srtands     
            Data.CreateANewDroneCharge(DronId, j);//creating a new drone-charg

        }
        /// <summary>
        /// the func gets the drone id
        /// we mack 2 inital chacks
        /// 1. that we have such a drone 
        /// 2. that its in charging
        /// and then we update everything 2 in drone to-list an 1 in the data source
        /// </summary>
        /// <param name="DroneId"></param>
        /// <param name="Time"></param>
        public void ReturnDroneFromeCharging(int DroneId,int Time)
        {
            int i = DroneList.FindIndex(w => w.Id == DroneId);
            if (i == 0)
                throw "Drone doesn't exsit\n";
            if (!(DroneList[i].status == IBL.BO.STATUS_OF_DRONE.IN_MAINTENANCE))
                throw "ERROR: Dron Not In Cargong Mode\n";
            DroneList[i].status = IBL.BO.STATUS_OF_DRONE.AVAILABLE;
            DroneList[i].Battery = Time * batteryConfig[5];
            IDAL.DO.DroneCharge droneCharge = Data.PrintDronCarg(DroneId);
            IDAL.DO.Station station = Data.PrintStation(droneCharge.StationId);
            Data.UpdatStation(station.Id, "", station.ReadyChargeStands + 1);
            Data.ClearDroneCharge(DroneId);              
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DroneId"></param>
        public void AssignDronToParcel(int DroneId)
        {
            if (!(DroneList.Exists(w => w.Id == DroneId)))
                throw "Drone dosent exsit\n";          
            int i = DroneList.FindIndex(w => w.Id == DroneId);
            List<IDAL.DO.Parcel> tempDataParcels = new List<IDAL.DO.Parcel>(Data.PrintParcelList());
            tempDataParcels.RemoveAll(w => (int)w.Weigh > (int)DroneList[i].MaxWeight);
            (bool x,int y)=GetBatteryUseAndRootFeasibility()                
        }
    }
}
