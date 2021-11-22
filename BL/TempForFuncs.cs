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
            Location ClosestCarging = Location(closeststation.Longitude, closeststation.Latitude);
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
            Location newloc = Location(cs.Longitude, cs.Latitude);
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

        public Station BLStation(IDAL.DO.Station s)
        {
            Station station = new();
            station.Id = s.Id;
            station.Name = s.Name;
            station.location.Longitude = s.Longitude;
            station.location.Latitude = s.Latitude;
            station.ReadyStandsInStation = s.ReadyChargeStands;
            return station;
        }

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
                List<IDAL.DO.Parcel> tempDataParcels = new(Data.PrintParcelList());
                IDAL.DO.Parcel p = tempDataParcels.Find(w => w.Id == d.ParcelId);
                drone.parcel = new();
                drone.parcel = BLParcelInTransfer(p);
            }
            return drone;
        }

        public Customer BLCustomer(IDAL.DO.Customer c)
        {
            List<IDAL.DO.Parcel> tempDataParcels = new(Data.PrintParcelList());
            Customer customer = new();
            customer.Id = c.Id;
            customer.Phone = c.Phone;
            customer.Name = c.Name;
            customer.location = Location(c.Longitude, c.Latitude);
            var From = tempDataParcels.FindAll(w => w.SenderId == customer.Id);
            customer.FromCustomer = new();
            foreach (IDAL.DO.Parcel p in From)
            {
                customer.FromCustomer.Add(BLParcelAtCustomer(p, true));
            }
            var To = tempDataParcels.FindAll(w => w.TargetId == customer.Id);
            customer.ToCustomer = new();
            foreach(IDAL.DO.Parcel p in To)
            {
                customer.ToCustomer.Add(BLParcelAtCustomer(p, false));
            }
            return customer;
        }

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
            if(p.Scheduled != DateTime.MinValue)
            {
                parcel.Drone = BLDroneInParcel(DroneList.Find(w => w.Id == p.DroneId));
            }
            return parcel;
        }

        public DroneInParcel BLDroneInParcel(DroneToList d)
        {
            DroneInParcel drone = new();
            drone.Id = d.Id;
            drone.Battery = d.Battery;
            drone.ThisLocation = d.ThisLocation;
            return drone;
        }

        public ParcelAtCustomer BLParcelAtCustomer(IDAL.DO.Parcel p, bool sender)
        {
            List<IDAL.DO.Customer> tempDataCustomers = new(Data.PrintCustomerList());
            ParcelAtCustomer par = new();
            par.Id = p.Id;
            par.Weight = (WEIGHT)p.Weigh;
            par.Priority = (PRIORITY)p.Priority;
            if (p.Scheduled == DateTime.MinValue)
                par.Status = STATUS_OF_PARCEL.CREATED;
            else if (p.PickedUp == DateTime.MinValue)
                par.Status = STATUS_OF_PARCEL.ASSOCIATED;
            else if (p.Delivered == DateTime.MinValue)
                par.Status = STATUS_OF_PARCEL.PICKEDUP;
            else
                par.Status = STATUS_OF_PARCEL.DELIVERED;
            if (sender)
                par.TheOther = BLCustomerInParcel(tempDataCustomers.Find(w => w.Id == p.TargetId));
            else
                par.TheOther = BLCustomerInParcel(tempDataCustomers.Find(w => w.Id == p.SenderId));
            return par;
        }

        public ParcelInTransfer BLParcelInTransfer(IDAL.DO.Parcel p)
        {
            List<IDAL.DO.Customer> tempDataCustomers = new(Data.PrintCustomerList());
            ParcelInTransfer par = new();
            par.Id = p.Id;
            par.PickedUp = p.PickedUp != DateTime.MinValue;
            par.Priority = (PRIORITY)p.Priority;
            par.Weight = (WEIGHT)p.Weigh;
            par.Sender = BLCustomerInParcel(tempDataCustomers.Find(w => w.Id == p.SenderId));
            par.Recipient = BLCustomerInParcel(tempDataCustomers.Find(w => w.Id == p.TargetId));
            par.LocationOfPickedUp = Location(tempDataCustomers.Find(w => w.Id == p.SenderId).Longitude, tempDataCustomers.Find(w => w.Id == p.SenderId).Latitude);
            par.LocationOfDestination = Location(tempDataCustomers.Find(w => w.Id == p.TargetId).Longitude, tempDataCustomers.Find(w => w.Id == p.TargetId).Latitude);
            par.Distance = GetDistance(par.LocationOfPickedUp, par.LocationOfDestination);
            return par;
        }

        public CustomerInParcel BLCustomerInParcel(IDAL.DO.Customer DalCus)
        {
            CustomerInParcel c = new();
            c.Id = DalCus.Id;
            c.Name = DalCus.Name;
            return c;
        }

        public Location Location(double lon, double lat)
        {
            Location l = new();
            l.Longitude = lon;
            l.Latitude = lat;
            return l;
        }

        public StationToList BLStationToList(IDAL.DO.Station s)
        {
            StationToList station = new();

            return station;
        }
    }

}
