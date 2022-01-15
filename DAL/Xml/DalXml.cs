using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using DalObject;
using System.Globalization;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
namespace DalXml  
{
    class DalXml : IDal
    {
        public static string DroneXml = @"DroneXml.xml";
        public static string BaseStationXml = @"BaseStationXml.xml";
        public static string CustomerXml = @"CustomerXml.xml";
        public static string ParcelXml = @"ParcelXml.xml";
        public static string DroneChargeXml = @"DroneChargeXml.xml";


        /// <summary>
        /// defult constractors and singelton 
        /// </summary>
        #region Singelton+Constractors
        static DalXml()//  ctor to ensure instance init is done just before first usage
        {
            DalObject.DataSource.Initialize();
        }

        /// <summary>
        ///  ctor in the biginin of eace run we relese all the drons from charge
        /// </summary>
        private DalXml() //private  
        {
            List<DO.DroneCharge> droneCharge = (List<DO.DroneCharge>)XmlToolKit.LoadListFromXMLSerializer<DO.DroneCharge>(DroneChargeXml);
            List<DO.Station> St= (List<DO.Station>)XmlToolKit.LoadListFromXMLSerializer<DO.Station>(BaseStationXml);
            foreach (var item in droneCharge)
            {
                St.Where(x => x.Id == item.StationId).Select(x => x.ReadyChargeStands--);              
            }
            droneCharge.Clear();
            XmlToolKit.SaveListToXMLSerializer(droneCharge, DroneChargeXml);
            XmlToolKit.SaveListToXMLSerializer(St, BaseStationXml);
        }

        internal static DalXml Instance { get; } = new DalXml();// The public Instance property to use
        #endregion


        #region Stations
        public void AddStation(Station sta)  //just adding to the last place
        {
            List<Station> stations = XmlToolKit.LoadListFromXMLSerializer<Station>(BaseStationXml);
            if(stations.Exists(x=>x.Id==sta.Id))
                throw new DalExceptions("Station Alredy exsits");///theowin the exciption of elerdy exsit
          stations.Add(sta);
            XmlToolKit.SaveListToXMLSerializer(stations, BaseStationXml);
        }
       
        public void UpdatStation(int StationId, string Name = "", int NumOfCarg = -1)
        {
            List<Station> stations = XmlToolKit.LoadListFromXMLSerializer<Station>(BaseStationXml);
            if(!stations.Exists(x=>x.Id==StationId))
                throw new DalExceptions("Station dosent exist");
            Station temp = stations.Find(x=>x.Id==StationId);
            if (!string.IsNullOrEmpty(Name))
            {
                temp.Name = Name;
            }
            if (!(NumOfCarg == -1))
            {
                temp.ReadyChargeStands = NumOfCarg;
            }
            stations.Where(x => x.Id == StationId).Select(x => x = temp);
            XmlToolKit.SaveListToXMLSerializer(stations, BaseStationXml);
        }
        public Station DisplayStation(int id)  //finds the station and sends a replica
        {
            List<Station> st = XmlToolKit.LoadListFromXMLSerializer<Station>(BaseStationXml);
            return (st.Find(w => w.Id == id));
        }
        public IEnumerable<Station> PrintStationList(Predicate<Station> predicate = null)  //returns a new list of stations
        {
            List<Station> stations = XmlToolKit.LoadListFromXMLSerializer<Station>(BaseStationXml);
            return stations.FindAll(x => predicate == null ? true : predicate(x));
        }
        #endregion

        #region Customers
        public void AddCustomer(Customer cst) 
        {
           XElement element = XmlToolKit.LoadListFromXMLElement(CustomerXml);//get the wlwmnt

            XElement customer = (from cus in element.Elements()// gets if it alredy exsits
                                 where cus.Element("Id").Value == cst.Id.ToString()
                                 select cus).FirstOrDefault();
            if (customer != null)
            {
                throw new DalExceptions("Error adding an object with an existing ID number");
            }

            XElement CustomerElem = new XElement("Customer",//adding the info
                                 new XElement("Id", cst.Id),
                                 new XElement("Name", cst.Name),
                                 new XElement("PhoneNumber", cst.Phone),
                                 new XElement("Longitude", cst.Longitude),
                                 new XElement("Latitude", cst.Latitude));

            element.Add(CustomerElem);//add in the list

            XmlToolKit.SaveListToXMLElement(element, CustomerXml);
        }
        public void UpdateCustomer(int CusId, string Name = "", string phone = "")
        {
            XElement element = XmlToolKit.LoadListFromXMLElement(CustomerXml);//get the wlwmnt

            XElement customer = (from cus in element.Elements()// gets if it alredy exsits
                                 where cus.Element("Id").Value == CusId.ToString()
                                 select cus).FirstOrDefault();
            if (customer == null)
            {
                throw new DalExceptions("Customer Dose not exsits");
            }   
            if (!string.IsNullOrEmpty(Name))
            {             
                customer.Element("Name").Value= Name;
            }
            if (!string.IsNullOrEmpty(phone))
            {
               customer.Element("Phone").Value = phone;
            }
            XmlToolKit.SaveListToXMLElement(element, CustomerXml);
        }
        public Customer PrintCustomer(int id)  //finds the customer and sends a replica
        {

            XElement element = XmlToolKit.LoadListFromXMLElement(CustomerXml);//get the wlwmnt

            XElement customer = (from cus in element.Elements()// gets if it alredy exsits
                                 where cus.Element("Id").Value == id.ToString()
                                 select cus).FirstOrDefault();
            if (customer == null)
            {
                throw new DalExceptions("Customer Dose not exsits");
            }

            Customer customer1 = (from cus in element.Elements()
                                 where cus.Element("Id").Value == id.ToString()
                                 select new Customer()
                                 {
                                     Id = int.Parse(cus.Element("Id").Value),
                                     Name = cus.Element("Name").Value,
                                     Phone = cus.Element("Phone").Value,
                                     Longitude = double.Parse(cus.Element("Longitude").Value),
                                     Latitude = double.Parse(cus.Element("Latitude").Value)
                                 }
                        ).FirstOrDefault();
            return customer1;
        }
        public IEnumerable<Customer> PrintCustomerList(Predicate<Customer> predicate = null)  //returns a new list of customers
        {
            XElement element =XmlToolKit.LoadListFromXMLElement(CustomerXml);
            IEnumerable<Customer> customer = from cus in element.Elements()
                                             select new Customer()
                                             {
                                                 Id = int.Parse(cus.Element("Id").Value),
                                                 Name = cus.Element("Name").Value,
                                                 Phone = cus.Element("Phone").Value,
                                                 Longitude = double.Parse(cus.Element("Longitude").Value),
                                                 Latitude = double.Parse(cus.Element("Latitude").Value)
                                             };
            return customer.Select(item => item);
            return DataSource.customers.FindAll(x => predicate == null ? true : predicate(x));
        }


        #endregion

        #region Drones
        #endregion

        #region Parcels
        public void AddSParcel(Parcel prc)  //same
        {


            List<Parcel> parcels = XmlToolKit.LoadListFromXMLSerializer<Parcel>(ParcelXml);         
            if (parcels.Exists(x=>x.Id==prc.Id))
                throw new DO.DalExceptions("Parcel Alredy exsits");
            parcels.Add(prc);
            XmlToolKit.SaveListToXMLSerializer<Parcel>(parcels, ParcelXml);
        }
        public Parcel PrintParcel(int id)  //finds the station and sends a replica
        {
            List<Parcel> parcels= XmlToolKit.LoadListFromXMLSerializer<Parcel>(ParcelXml);
            if (!parcels.Exists(x => x.Id == id))
                throw new DalExceptions("Parcel dose not exsit");
            return parcels.Find(w => w.Id == id);
        }
        public IEnumerable<Parcel> PrintParcelList(Predicate<Parcel> predicate = null)  //returns a new list of parcels
        {
            List<Parcel> parcels = XmlToolKit.LoadListFromXMLSerializer<Parcel>(ParcelXml);

            return parcels.FindAll(x => predicate == null ? true : predicate(x));
        }
        public void DeleteParcel(int id)
        {
            List<Parcel> parcels = XmlToolKit.LoadListFromXMLSerializer<Parcel>(ParcelXml);
            if (!parcels.Exists(x => x.Id == id))
                throw new DO.DalExceptions("Parcel Dose Not exsits");
            Parcel parcel = parcels.Find(x => x.Id == id);
            parcels.Remove(parcel);
            XmlToolKit.SaveListToXMLSerializer<Parcel>(parcels, ParcelXml);
        }
        public void UpdatParcel(int parclId, int SenderId = 0, int TargetId = 0, int DroneId = 0, Weight whihgt = 0, Priority priorty = 0, int Updatereqwested = 0, int UpdatSchedueld = 0, int UpdatPicedup = 0, int UpdateDeleverd = 0)
        {         
            List<Parcel> parcels = XmlToolKit.LoadListFromXMLSerializer<Parcel>(ParcelXml);
            if (!parcels.Exists(x => x.Id == parclId))
                throw new DO.DalExceptions("Parcel Dose Not exsits");
            Parcel parcel = parcels.Find(x => x.Id == parclId);  
            if (SenderId != 0)
                parcel.SenderId = SenderId;
            if (TargetId != 0)
                parcel.TargetId = TargetId;
            if (DroneId != 0)
                parcel.DroneId = DroneId;
            if (whihgt != 0)
                parcel.Weigh = whihgt;
            if (priorty != 0)
                parcel.Priority = priorty;
            if (Updatereqwested != 0)
                parcel.Requested = DateTime.Now;
            if (UpdatPicedup != 0)
                parcel.PickedUp = DateTime.Now;
            if (UpdatSchedueld != 0)
                parcel.Scheduled = DateTime.Now;
            if (UpdateDeleverd != 0)
                parcel.Delivered = DateTime.Now;
            parcels.Where(x => x.Id == parclId).Select(x => parcel);
            XmlToolKit.SaveListToXMLSerializer<Parcel>(parcels, ParcelXml);
        }
        public void DroneIdOfPArcel(int prcId, int drnId)
        {
            List<Parcel> parcels = XmlToolKit.LoadListFromXMLSerializer<Parcel>(ParcelXml);
            if (!parcels.Exists(x => x.Id == prcId))
                throw new DalExceptions("Parcel Dose Not exsits");
            List<Drone> drones = XmlToolKit.LoadListFromXMLSerializer<Drone>(DroneXml);
            if  (!parcels.Exists(x => x.Id == drnId))
                    throw new DalExceptions("Drone Dose Not exsits");

            Parcel tempParcel = parcels.Find(x => x.Id == prcId);
            tempParcel.DroneId = drnId;
            parcels.Where(x => x.Id == prcId).Select(x => tempParcel);
            XmlToolKit.SaveListToXMLSerializer<Parcel>(parcels, ParcelXml);

        }
        #endregion

        #region DroneCharge
        #endregion
    }
}
