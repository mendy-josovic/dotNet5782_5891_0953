using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    class DataSource
    {
        internal static IDAL.DO.Drone[] Drones = new IDAL.DO.Drone[10];
        internal static IDAL.DO.Station[] Stations = new IDAL.DO.Station[5];
        internal static IDAL.DO.Customer[] Customers = new IDAL.DO.Customer[100];
        internal static IDAL.DO.Parcel[] Parcels = new IDAL.DO.Parcel[1000];
        internal class Config
        {
            internal static int DronesIndex { set; get; } = 0;
            internal static int StationsIndex { set; get; } = 0;
            internal static int ParcelsIndex { set; get; } = 0;
            internal static int CustomersIndex { set; get; } = 0;
            internal static int num { set; get; }

        }
        public static Random r = new Random();
        internal static void Initialize()
        {
            for(int i=0;i<2;i++)///initielizeing Stations
            {
                Stations[i].Id = r.Next(1,501);
                Stations[i].Lattitude = r.NextDouble() / 1.234;
                Stations[i].Longitude = r.NextDouble() / 1.234;
                Stations[i].ChargeSlots = 5;
            }
            Stations[0].Name = "The centrial station";
            Stations[1].Name = "The 'HERTZEL' station";
            Customers[5].Name = "Nachman";
            String[] names = new String[] { "Moshe", "Aharon", "David", "Yosef", "Reuven", "Nachman", "Avraham", "lot", "Moav", "Amon" };
            String[] phones = new String[] { "051-123456", "052-123456", "053-123456", "054-123456", "055-123456", "056-123456", "057-123456", "058-123456", "059-123456", "050-123456" };
            for (int i = 0; i < 10; i++)
            {
                Customers[i].Id = r.Next(1, 10000);
                Customers[i].Lattitude = r.NextDouble() / 1.234;
                Customers[i].Longitute= r.NextDouble() / 1.234;
                Customers[i].Name = names[i];
                Customers[i].Phone = phones[i];
            }
            for(int i=0;i<5;i++)
            {
                Drones[i].Id = r.Next(1, 1000);
                Drones[i].MaxWeight = IDAL.DO.WEIGHT.HEAVY;
                Drones[i].Battery = r.Next(1, 100) / 5.5;
            }
            Drones[0].Model = "AB123";
            Drones[0].Status = IDAL.DO.STATUS.MAINTENANSE;
            Drones[1].Model = "CD123";
            Drones[1].Status = IDAL.DO.STATUS.DELIVERY;
            Drones[2].Status = IDAL.DO.STATUS.DELIVERY;
            Drones[2].Model = "EF123";
            Drones[3].Status = IDAL.DO.STATUS.DELIVERY;
            Drones[3].Model = "GH123";
            Drones[4].Status = IDAL.DO.STATUS.AVAILABLE;
            Drones[4].Model = "IJ123";
            for (int i = 0; i <10; i++)
            {
                Parcels[i].Id = r.Next(1, 1000);
            }
        }
    }
}
