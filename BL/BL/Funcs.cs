using BlApi;
using BO;
using System;
using System.Linq;
using System.Collections.Generic;
using DalApi;
using DO;
namespace BL
{
    internal partial class BL : IBl
    {
        /// <summary>
        /// the func gets the status of the parcel and acourdingly returns the consumption
        /// </summary>
        /// <param name="a">location a</param>
        /// <param name="b">location b</param>
        /// <param name="mode">mode of the drone in moving: without a parcel, or with a parcel, and which mode of parcel</param>
        /// <returns>how much battery this moving need</returns>
        public double Consumption(Location a, Location b, ModeOfDroneInMoving mode)
        {
            return GetDistance(a, b) * batteryConfig[(int)mode];
        }
        public bool GetBatteryUseAndRootFeasibility(BO.DroneToList dro,DO.Parcel prc)
        {
            try
            {
                Location startingPiont = dro.ThisLocation;
                Location StapingPiont = GetSenderLo(prc);
                Location FinishingPiont = GetReceiverLo(prc);
                DO.Customer sender = Data.PrintCustomer(prc.SenderId);                
                DO.Customer Receiver = Data.PrintCustomer(prc.TargetId);        
                DO.Station closeststation = Data.PrintStation(GetClosestStation(FinishingPiont));          
                Location ClosestCarging = Location(closeststation.Longitude, closeststation.Latitude);
                double batteryUse = Consumption(startingPiont, StapingPiont, BO.ModeOfDroneInMoving.Available) + Consumption(StapingPiont, FinishingPiont, (BO.ModeOfDroneInMoving)prc.Weigh);
                if (dro.Battery - batteryUse < 20)
                {
                    batteryUse += Consumption(FinishingPiont, ClosestCarging,BO.ModeOfDroneInMoving.Available);
                    if (dro.Battery - batteryUse < 0)
                        return false;
                }
                return true;
            }
             catch (DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }

        }
        /// <summary>
        /// the func returns location of thr sender 
        /// </summary>
        /// <param name="pr"></param>
        /// <returns></returns>
        public Location GetSenderLo(DO.Parcel pr)
        {
            try
            {
                DO.Customer cs = Data.PrintCustomer(pr.SenderId);
                Location newloc = Location(cs.Longitude, cs.Latitude);
                return newloc;
            }
            catch (DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }
        /// <summary>
        /// the func returns location of the rtraget
        /// </summary>
        /// <param name="pr"></param>
        /// <returns></returns>
        public Location GetReceiverLo(DO.Parcel pr)
        {
            try
            {
                DO.Customer cs = Data.PrintCustomer(pr.TargetId);
                Location newloc = Location(cs.Longitude, cs.Latitude);
                return newloc;
            }
            catch (DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }


        /// <summary>
        /// turn DroneList into IEnumerable DroneList
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DroneToList> BLDrones(Predicate<DroneToList> predicate = null)
        {
            return DroneList.FindAll(x => predicate == null ? true : predicate(x));
        }

        /// <summary>
        /// the func hase  a option
        /// to get the distenc with difrrentn pararmeters
        /// </summary>
        /// <param name="a">if its jest a location to a location</param>
        /// <param name="b"></param>
        /// <param name="longA">if we wont to mack the disenc with a long and lat</param>
        /// <param name="latA"></param>
        /// <param name="longB"></param>
        /// <param name="latB"></param>
        /// <returns></returns>
        public double GetDistance(Location a, Location b, double longA = 0, double latA = 0, double longB = 0, double latB = 0)
        {
            if (a.Latitude == 0)
            {
                a.Latitude = latA;
                a.Longitude = longA;
            }
            if (b.Latitude == 0)
            {
                b.Latitude = latB;
                b.Longitude = longB;
            }
            return Math.Sqrt((Math.Pow(a.Longitude - b.Longitude, 2) + Math.Pow(a.Latitude - b.Latitude, 2)));
        }

        /// <summary>
        /// return the ID of the closest station with ready stands to the location
        /// </summary>
        /// <param name="a">the location we want to get it closest station</param>
        /// <returns>ID of the closest station</returns>
        public int GetClosestStation(Location a, IEnumerable<DO.Station> stations=null)
        {
            try
            {
                int i = 0;
                int closestID = 0;
                double minimum = 0;
                IEnumerable<DO.Station> tempDataStations;
                if (stations == null)
                    tempDataStations = Data.PrintStationList();
                else
                    tempDataStations = stations;
                List<BO.Station> stationsBL = new();
                foreach (DO.Station station in tempDataStations)
                {
                    stationsBL.Add(BLStation(station.Id));
                }
                if (stationsBL.Count == 0)
                    return closestID;
                while (i != stationsBL.Count)
                {
                    if (stationsBL[i].ReadyStandsInStation > 0)
                    {
                        closestID = stationsBL[0].Id;
                        minimum = GetDistance(a, stationsBL[0].location);
                        break;
                    }
                }
                if (i == stationsBL.Count)
                    throw new BlException("There is no station to charge");
                for (; i < stationsBL.Count; i++)
                {
                    if (minimum > GetDistance(a, stationsBL[i].location) && stationsBL[i].ReadyStandsInStation > 0)
                    {
                        closestID = stationsBL[i].Id;
                        minimum = GetDistance(a, stationsBL[i].location);
                    }
                }
                return closestID;
            }
            catch (DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// get ID of a station and return it location
        /// </summary>
        /// <param name="ID">ID of station</param>
        /// <returns>location of station</returns>
        public Location GetLocationOfStation(int ID)
        {
            try
            {
                DO.Station tempDataStations = Data.PrintStation(ID);            
                Location loc = Location(tempDataStations.Longitude, tempDataStations.Latitude);
                return loc;
            }
            catch (DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// Turn a DAL station into a BL station
        /// </summary>
        /// <param name="s">DAL station</param>
        /// <returns>BL station</returns>
        public BO.Station BLStation(int id)
        {
            DO.Station s = Data.PrintStation(id);
            BO.Station station = new();
            station.Id = s.Id;
            station.Name = s.Name;
            station.location = new();
            station.location.Latitude = s.Latitude;
            station.location.Longitude = s.Longitude;
            station.ReadyStandsInStation = s.ReadyChargeStands;
            station.ListOfDrones = DisplayDronesInCharging((w => GetDistance(station.location, DisplayDrone(w.Id).ThisLocation) == 0)).ToList();
            return station;
        }

        public BO.Station BLStation()
        {
            BO.Station station = new();
            station.location = new();
            station.ListOfDrones = new();
            return station;
        }
        
        /// <summary>
        /// Turn a DroneToList drone into a BL Drone
        /// </summary>
        /// <param name="d">DroneToList drone</param>
        /// <returns>BL Drone</returns>
        public BO.Drone BLDrone(DroneToList d)
        {
            BO.Drone drone = new();
            drone.Id = d.Id;
            drone.Model = d.Model;
            drone.MaxWeight = d.MaxWeight;
            drone.Battery = d.Battery;
            drone.ThisLocation = d.ThisLocation;
            drone.status = d.status;
            if (drone.status == StatusOfDrone.Delivery)
            {             
                DO.Parcel p = Data.PrintParcel(d.ParcelId);
                drone.parcel = BLParcelInTransfer(p);
            }
            return drone;
        }

        /// <summary>
        /// Turn a DAL customer into a BL customer
        /// gets int of the id
        /// </summary>
        /// <param name="c">DAL customer </param>
        /// <returns>BL customer</returns>
        public BO.Customer BLCustomer(int Id)
        {
            
            DO.Customer c = new();
            c = DisplayCustomer(Id);
            List<DO.Parcel> tempDataParcels = new(Data.PrintParcelList());
            BO.Customer customer = new();
            customer.Id = c.Id;
            customer.Phone = c.Phone;
            customer.Name = c.Name;
            customer.location = Location(c.Longitude, c.Latitude);
            //From - a list okf parcels that sendered by this customer
            var From = tempDataParcels.FindAll(w => w.SenderId == customer.Id);
            customer.FromCustomer = new();
            foreach (DO.Parcel p in From)
            {
                customer.FromCustomer.Add(BLParcelAtCustomer(p, true));
            }
            //To - a list okf parcels that sendered to this customer
            var To = tempDataParcels.FindAll(w => w.TargetId == customer.Id);
            customer.ToCustomer = new();
            foreach(DO.Parcel p in To)
            {
                customer.ToCustomer.Add(BLParcelAtCustomer(p, false));
            }
            return customer;
        }

        /// <summary>
        /// Turn a DAL parcel into a BL parcel 
        /// </summary>
        /// <param name="p">DAL parcel </param>
        /// <returns>BL parcel</returns>
        public BO.Parcel BLParcel(DO.Parcel p)
        {
            List<DO.Customer> tempDataCustomers = new(Data.PrintCustomerList());
            BO.Parcel parcel = new();
            parcel.Id = p.Id;
            parcel.Weight = (BO.Weight)p.Weigh;
            parcel.Priority = (BO.Priority)p.Priority;
            parcel.TimeOfCreation = p.Requested;
            parcel.Scheduled = p.Scheduled;
            parcel.PickedUp = p.PickedUp;
            parcel.Delivered = p.Delivered;
            parcel.Sender = BLCustomerInParcel(tempDataCustomers.Find(w => w.Id == p.SenderId));
            parcel.Recipient = BLCustomerInParcel(tempDataCustomers.Find(w => w.Id == p.TargetId));
            if(p.Scheduled != null)
            {
                parcel.Drone = BLDroneInParcel(DroneList.Find(w => w.Id == p.DroneId));
            }
            return parcel;
        }
       
        /// <summary>
        /// Turn a DroneToList drone into a DroneInParcel
        /// </summary>
        /// <param name="d">DroneToList</param>
        /// <returns>DroneInParcel</returns>
        public DroneInParcel BLDroneInParcel(DroneToList d)
        {
            DroneInParcel drone = new();
            drone.Id = d.Id;
            drone.Battery = d.Battery;
            drone.ThisLocation = d.ThisLocation;
            return drone;
        }

        /// <summary>
        /// Turn a DAL parcel into a BL ParcelAtCustomer
        /// </summary>
        /// <param name="p">DAL parcel</param>
        /// <param name="sender">a flag if the customer of ParcelAtCustomer is the sender or the recipient</param>
        /// <returns>BL ParcelAtCustomer</returns>
        public ParcelAtCustomer BLParcelAtCustomer(DO.Parcel p, bool sender)
        {
            List<DO.Customer> tempDataCustomers = new(Data.PrintCustomerList());
            ParcelAtCustomer par = new();
            par.Id = p.Id;
            par.Weight = (BO.Weight)p.Weigh;
            par.Priority = (BO.Priority)p.Priority;
            if (p.Scheduled == null)
                par.Status = StatusOfParcel.Created;
            else if (p.PickedUp == null)
                par.Status = StatusOfParcel.Associated;
            else if (p.Delivered == null)
                par.Status = StatusOfParcel.PickedUp;
            else
                par.Status = StatusOfParcel.Delivered;
            if (sender)
                par.TheOther = BLCustomerInParcel(tempDataCustomers.Find(w => w.Id == p.TargetId));
            else
                par.TheOther = BLCustomerInParcel(tempDataCustomers.Find(w => w.Id == p.SenderId));
            return par;
        }

        /// <summary>
        /// Turn a DAL parcel into a BL ParcelInTransfer
        /// </summary>
        /// <param name="p">DAL parcel</param>
        /// <returns>BL ParcelInTransfer</returns>
        public ParcelInTransfer BLParcelInTransfer(DO.Parcel p)
        {
            List<DO.Customer> tempDataCustomers = new(Data.PrintCustomerList());
            ParcelInTransfer par = new();
            par.Id = p.Id;
            par.PickedUp = p.PickedUp != null;
            par.Priority = (BO.Priority)p.Priority;
            par.Weight = (BO.Weight)p.Weigh;
            par.Sender = BLCustomerInParcel(tempDataCustomers.Find(w => w.Id == p.SenderId));
            par.Recipient = BLCustomerInParcel(tempDataCustomers.Find(w => w.Id == p.TargetId));
            par.LocationOfPickedUp = Location(tempDataCustomers.Find(w => w.Id == p.SenderId).Longitude, tempDataCustomers.Find(w => w.Id == p.SenderId).Latitude);
            par.LocationOfDestination = Location(tempDataCustomers.Find(w => w.Id == p.TargetId).Longitude, tempDataCustomers.Find(w => w.Id == p.TargetId).Latitude);
            par.Distance = GetDistance(par.LocationOfPickedUp, par.LocationOfDestination);
            return par;
        }

        /// <summary>
        /// Turn a DAL customer into a BL CustomerInParcel
        /// </summary>
        /// <param name="DalCus">DAL customer</param>
        /// <returns>BL CustomerInParcel</returns>
        public CustomerInParcel BLCustomerInParcel(DO.Customer DalCus)
        {
            CustomerInParcel c = new();
            c.Id = DalCus.Id;
            c.Name = DalCus.Name;
            return c;
        }

        /// <summary>
        /// Turn a DroneToList into a BL DroneInCharging
        /// </summary>
        /// <param name="d">DroneToList</param>
        /// <returns>DroneInCharging</returns>
        public DroneInCharging BLDroneInCharging(DroneToList d)
        {
            DroneInCharging drone = new();
            drone.Id = d.Id;
            drone.Battery = d.Battery;
            return drone;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DroneInCharging BLDroneInCharging1(int id)
        {
            DroneInCharging drone = new();
            DO.DroneCharge d = Data.PrintDronCarg(id);
            drone.Id = d.DroneId;
            drone.Battery = DroneList.Find(w => w.Id == id).Battery;
            drone.EntryTimeForLoading = d.EntryTimeForLoading;
            return drone;
        }

        /// <summary>
        /// create a Location
        /// </summary>
        /// <param name="lon">longitude point value</param>
        /// <param name="lat">latitude point value</param>
        /// <returns>Location</returns>
        public Location Location(double lon, double lat)
        {
            Location l = new();
            l.Longitude = lon;
            l.Latitude = lat;
            return l;
        }

        /// <summary>
        /// Turn a DAL station into a BL StationToList
        /// </summary>
        /// <param name="s">DAL station</param>
        /// <param name="s">DAL station</param>
        /// <returns>StationToList</returns>
        public StationToList BLStationToList(DO.Station s)
        {
            StationToList stationToList = new();
            BO.Station station = BLStation(s.Id);
            stationToList.Id = station.Id;
            stationToList.Name = station.Name;
            stationToList.ReadyStandsInStation = station.ReadyStandsInStation;
            stationToList.OccupiedStandsInStation = station.ListOfDrones.Count;
            return stationToList;
        }

        /// <summary>
        /// Turn a BL Drone into a DroneToList
        /// </summary>
        /// <param name="d"> BL Drone</param>
        /// <returns>DroneToList</returns>
        public DroneToList BLDroneToList(BO.Drone d)
        {
            DroneToList droneToList = new();
            droneToList.Id = d.Id;
            droneToList.Model = d.Model;
            droneToList.MaxWeight = d.MaxWeight;
            droneToList.Battery = d.Battery;
            droneToList.status = d.status;
            droneToList.ThisLocation = d.ThisLocation;
            if(d.parcel!=null)
                droneToList.ParcelId = d.parcel.Id;
            return droneToList;
        }

        /// <summary>
        /// Turn a DAL customer into a CustomerToList
        /// </summary>
        /// <param name="c">DAL customer</param>
        /// <returns>CustomerToList</returns>
        public CustomerToList BLCustomerToList(DO.Customer c)
        {
            CustomerToList customerToList = new();
            BO.Customer customer = BLCustomer(c.Id);
            customerToList.Id = customer.Id;
            customerToList.Name = customer.Name;
            customerToList.Phone = customer.Phone;
            var sumOfDelivered = customer.FromCustomer.FindAll(w=>w.Status==StatusOfParcel.Delivered);
            customerToList.ParcelsSentAndDelivered = sumOfDelivered.Count;
            var sumOfSendered = customer.FromCustomer.FindAll(w => w.Status == StatusOfParcel.PickedUp);
            customerToList.ParcelsSentAndNotDelivered = sumOfSendered.Count;
            var sumOfgot = customer.ToCustomer.FindAll(w => w.Status == StatusOfParcel.Delivered);
            customerToList.ParcelsReceived = sumOfgot.Count;
            var sumOfOnWay = customer.ToCustomer.FindAll(w => w.Status == StatusOfParcel.PickedUp);
            customerToList.ParcelsOnWayToCustomer = sumOfOnWay.Count;
            return customerToList;
        }

        /// <summary>
        /// Turn a DAL parcel into a ParcelToList
        /// </summary>
        /// <param name="c">DAL parcel</param>
        /// <returns>ParcelToList</returns>
        public ParcelToList BLParcelToList(DO.Parcel c)
        {
            ParcelToList parcelToList = new();
            BO.Parcel parcel = BLParcel(c);
            parcelToList.Id = parcel.Id;
            parcelToList.Sender = parcel.Sender.Name;
            parcelToList.Recipient = parcel.Recipient.Name;
            parcelToList.Weight = parcel.Weight;
            parcelToList.Priority = parcel.Priority;
            if (parcel.Delivered != DateTime.MinValue)
                parcelToList.Status = StatusOfParcel.Delivered;
            else if(parcel.PickedUp!=DateTime.MinValue)
                parcelToList.Status = StatusOfParcel.PickedUp;
            else if(parcel.Scheduled!=DateTime.MinValue)
                parcelToList.Status = StatusOfParcel.Associated;
            else
                parcelToList.Status = StatusOfParcel.Created;
            return parcelToList;
        }

        public Location GetLocationOfStation(StationToList s)
        {
            DO.Station st = Data.PrintStation(s.Id);
            Location location = Location(st.Longitude, st.Latitude);
            return location;
        }

        public static string ConvertToSexagesimal(double? point)
        {
            if (point == null)
                throw new NullReferenceException("point is null");

            int Degrees = (int)point;
            point -= Degrees;
            point *= 60;
            int Minutes = (int)point;
            point -= Minutes;
            point *= 60;
            double? Second = point;
            return $"{Degrees}° {Minutes}' {string.Format("{0:0.###}", Second)}\" ";
        }
        public void DeletAParcel(int Id)
        {
            Data.DeleteParcel(Id);
        }
    }
}
