using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public class DataSource
    {
        internal static IDAL.DO.Drone[] Drones = new IDAL.DO.Drone[10];
        internal static IDAL.DO.Station[] Stations = new IDAL.DO.Station[5];
        internal static IDAL.DO.Customer[] Customers = new IDAL.DO.Customer[100];
        internal static IDAL.DO.Parcel[] Parcels = new IDAL.DO.Parcel[1000];
        public static Random r = new Random();
        public static void Initialize()
        {
            for(int i=0; i<2; i++)
            {
                Stations[i].Id = r.Next(1, 501);
                Stations[i].Lattitude = r.Next(0, 24)/1.1234;
                Stations[i].Longitude = r.Next(0, 24) / 1.1234;
            }
            Stations[0].Name = "The centrial station";
            Stations[1].Name = "Jafa station";
            for(int i=0; i<5; i++)
            {

            }
        }

        internal class Config
        {
            internal static int DronesIndex { get; set; } = 0;
            internal static int StationsIndex { get; set; } = 0;
            internal static int CustomersIndex { get; set; } = 0;
            internal static int ParcelsIndex { get; set; } = 0;
            internal static int num { get; set; }
        }
    }
}
