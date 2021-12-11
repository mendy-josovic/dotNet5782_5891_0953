using IBL;
using IBL.BO;
using System;
using System.Collections.Generic;
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


        public bool GetBatteryUseAndRootFeasibility(IBL.BO.DroneToList dro,IDAL.DO.Parcel prc)
        {
            try
            {
                Location startingPiont = dro.ThisLocation;
                Location StapingPiont = GetSenderLo(prc);
                Location FinishingPiont = GetReceiverLo(prc);
                IDAL.DO.Customer sender = Data.PrintCustomer(prc.SenderId);                
                IDAL.DO.Customer Receiver = Data.PrintCustomer(prc.TargetId);        
                IDAL.DO.Station closeststation = Data.PrintStation(GetClosestStation(FinishingPiont));          
                Location ClosestCarging = Location(closeststation.Longitude, closeststation.Latitude);
                double batteryUse = Consumption(startingPiont, StapingPiont, IBL.BO.MODE_OF_DRONE_IN_MOVING.AVAILABLE) + Consumption(StapingPiont, FinishingPiont, (IBL.BO.MODE_OF_DRONE_IN_MOVING)prc.Weigh);
                if (dro.Battery - batteryUse < 20)
                {
                    batteryUse += Consumption(FinishingPiont, ClosestCarging, IBL.BO.MODE_OF_DRONE_IN_MOVING.AVAILABLE);
                    if (dro.Battery - batteryUse < 0)
                        return false;
                }
                return true;
            }
             catch (IDAL.DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }

        }
        /// <summary>
        /// the func returns location of thr sender 
        /// </summary>
        /// <param name="pr"></param>
        /// <returns></returns>
        public Location GetSenderLo(IDAL.DO.Parcel pr)
        {
            try
            {
                IDAL.DO.Customer cs = Data.PrintCustomer(pr.SenderId);
                Location newloc = Location(cs.Longitude, cs.Latitude);
                return newloc;
            }
            catch (IDAL.DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }
        /// <summary>
        /// the func returns location of the rtraget
        /// </summary>
        /// <param name="pr"></param>
        /// <returns></returns>
        public Location GetReceiverLo(IDAL.DO.Parcel pr)
        {
            try
            {
                IDAL.DO.Customer cs = Data.PrintCustomer(pr.TargetId);
                Location newloc = Location(cs.Longitude, cs.Latitude);
                return newloc;
            }
            catch (IDAL.DO.DalExceptions ex)
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
        public int GetClosestStation(Location a)
        {
            try
            {
                int i = 0;
                int closestID = 0;
                double minimum = 0;
                List<IDAL.DO.Station> tempDataStations = new(Data.PrintStationList());
                List<Station> stationsBL = new();
                foreach (IDAL.DO.Station station in tempDataStations)
                {
                    stationsBL.Add(BLStation(station));
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
            catch (IDAL.DO.DalExceptions ex)
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
                IDAL.DO.Station tempDataStations = Data.PrintStation(ID);            
                Location loc = Location(tempDataStations.Longitude, tempDataStations.Latitude);
                return loc;
            }
            catch (IDAL.DO.DalExceptions ex)
            {
                throw new BlException(ex.Message);
            }
        }

        /// <summary>
        /// Turn a DAL station into a BL station
        /// </summary>
        /// <param name="s">DAL station</param>
        /// <returns>BL station</returns>
        public Station BLStation(IDAL.DO.Station s)
        {
            Station station = new();
            station.Id = s.Id;
            station.Name = s.Name;
            station.location = new();
            station.location.Longitude = s.Longitude;
            station.location.Latitude = s.Latitude;
            station.ReadyStandsInStation = s.ReadyChargeStands;
            return station;
        }

        /// <summary>
        /// Turn a DroneToList drone into a BL Drone
        /// </summary>
        /// <param name="d">DroneToList drone</param>
        /// <returns>BL Drone</returns>
        public Drone BLDrone(DroneToList d)
        {
            Drone drone = new();
            drone.Id = d.Id;
            drone.Model = d.Model;
            drone.MaxWeight = d.MaxWeight;
            drone.Battery = d.Battery;
            drone.ThisLocation = d.ThisLocation;
            drone.status = d.status;
            if (drone.status == STATUS_OF_DRONE.DELIVERY)
            {             
                IDAL.DO.Parcel p = Data.PrintParcel(d.ParcelId);
                drone.parcel = BLParcelInTransfer(p);
            }
            return drone;
        }

        /// <summary>
        /// Turn a DAL customer into a BL customer
        /// </summary>
        /// <param name="c">DAL customer </param>
        /// <returns>BL customer</returns>
        public Customer BLCustomer(IDAL.DO.Customer c)
        {
            List<IDAL.DO.Parcel> tempDataParcels = new(Data.PrintParcelList());
            Customer customer = new();
            customer.Id = c.Id;
            customer.Phone = c.Phone;
            customer.Name = c.Name;
            customer.location = Location(c.Longitude, c.Latitude);
            //From - a list okf parcels that sendered by this customer
            var From = tempDataParcels.FindAll(w => w.SenderId == customer.Id);
            customer.FromCustomer = new();
            foreach (IDAL.DO.Parcel p in From)
            {
                customer.FromCustomer.Add(BLParcelAtCustomer(p, true));
            }
            //To - a list okf parcels that sendered to this customer
            var To = tempDataParcels.FindAll(w => w.TargetId == customer.Id);
            customer.ToCustomer = new();
            foreach(IDAL.DO.Parcel p in To)
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
        public Parcel BLParcel(IDAL.DO.Parcel p)
        {
            List<IDAL.DO.Customer> tempDataCustomers = new(Data.PrintCustomerList());
            Parcel parcel = new();
            parcel.Id = p.Id;
            parcel.Weight = (WEIGHT)p.Weigh;
            parcel.Priority = (PRIORITY)p.Priority;
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
        public ParcelAtCustomer BLParcelAtCustomer(IDAL.DO.Parcel p, bool sender)
        {
            List<IDAL.DO.Customer> tempDataCustomers = new(Data.PrintCustomerList());
            ParcelAtCustomer par = new();
            par.Id = p.Id;
            par.Weight = (WEIGHT)p.Weigh;
            par.Priority = (PRIORITY)p.Priority;
            if (p.Scheduled == null)
                par.Status = STATUS_OF_PARCEL.CREATED;
            else if (p.PickedUp == null)
                par.Status = STATUS_OF_PARCEL.ASSOCIATED;
            else if (p.Delivered == null)
                par.Status = STATUS_OF_PARCEL.PICKEDUP;
            else
                par.Status = STATUS_OF_PARCEL.DELIVERED;
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
        public ParcelInTransfer BLParcelInTransfer(IDAL.DO.Parcel p)
        {
            List<IDAL.DO.Customer> tempDataCustomers = new(Data.PrintCustomerList());
            ParcelInTransfer par = new();
            par.Id = p.Id;
            par.PickedUp = p.PickedUp != null;
            par.Priority = (PRIORITY)p.Priority;
            par.Weight = (WEIGHT)p.Weigh;
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
        public CustomerInParcel BLCustomerInParcel(IDAL.DO.Customer DalCus)
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
        public StationToList BLStationToList(IDAL.DO.Station s)
        {
            StationToList stationToList = new();
            Station station = BLStation(s);
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
        public DroneToList BLDroneToList(Drone d)
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
        public CustomerToList BLCustomerToList(IDAL.DO.Customer c)
        {
            CustomerToList customerToList = new();
            Customer customer = BLCustomer(c);
            customerToList.Id = customer.Id;
            customerToList.Name = customer.Name;
            customerToList.Phone = customer.Phone;
            var sumOfDelivered = customer.FromCustomer.FindAll(w=>w.Status==STATUS_OF_PARCEL.DELIVERED);
            customerToList.ParcelsSentAndDelivered = sumOfDelivered.Count;
            var sumOfSendered = customer.FromCustomer.FindAll(w => w.Status == STATUS_OF_PARCEL.PICKEDUP);
            customerToList.ParcelsSentAndNotDelivered = sumOfSendered.Count;
            var sumOfgot = customer.ToCustomer.FindAll(w => w.Status == STATUS_OF_PARCEL.DELIVERED);
            customerToList.ParcelsReceived = sumOfgot.Count;
            var sumOfOnWay = customer.ToCustomer.FindAll(w => w.Status == STATUS_OF_PARCEL.PICKEDUP);
            customerToList.ParcelsOnWayToCustomer = sumOfOnWay.Count;
            return customerToList;
        }

        /// <summary>
        /// Turn a DAL parcel into a ParcelToList
        /// </summary>
        /// <param name="c">DAL parcel</param>
        /// <returns>ParcelToList</returns>
        public ParcelToList BLParcelToList(IDAL.DO.Parcel c)
        {
            ParcelToList parcelToList = new();
            Parcel parcel = BLParcel(c);
            parcelToList.Id = parcel.Id;
            parcelToList.Sender = parcel.Sender.Name;
            parcelToList.Recipient = parcel.Recipient.Name;
            parcelToList.Weight = parcel.Weight;
            parcelToList.Priority = parcel.Priority;
            if (parcel.Delivered != DateTime.MinValue)
                parcelToList.Status = STATUS_OF_PARCEL.DELIVERED;
            else if(parcel.PickedUp!=DateTime.MinValue)
                parcelToList.Status = STATUS_OF_PARCEL.PICKEDUP;
            else if(parcel.Scheduled!=DateTime.MinValue)
                parcelToList.Status = STATUS_OF_PARCEL.ASSOCIATED;
            else
                parcelToList.Status = STATUS_OF_PARCEL.CREATED;
            return parcelToList;
        }

        public Location GetLocationOfStation(StationToList s)
        {           
            IDAL.DO.Station st = Data.PrintStation(s.Id);
            Location location = Location(st.Longitude, st.Latitude);
            return location;
        }
    }
}
