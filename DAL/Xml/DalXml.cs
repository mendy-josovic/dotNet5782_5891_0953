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
        public void UpdateReadyStandsInStation(int staId)
        {
            int i = DataSource.stations.FindIndex(w => w.Id == staId);  //find the station
            if (i < 0)
                throw new DalExceptions("Station dosen't exist");
            Station tempStation = DataSource.stations[i];
            tempStation.ReadyChargeStands--;
            DataSource.stations[i] = tempStation;
        }
        public Station DisplayStation(int id)  //finds the station and sends a replica
        {
            return (DataSource.stations.Find(w => w.Id == id));
        }
        public IEnumerable<Station> PrintStationList(Predicate<Station> predicate = null)  //returns a new list of stations
        {
            return DataSource.stations.FindAll(x => predicate == null ? true : predicate(x));
        }


        #endregion

        #region Drones
        #endregion

        #region Parcels
        #endregion

        #region DroneCharge
        #endregion
    }
}
