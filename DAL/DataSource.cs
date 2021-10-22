using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
namespace DalObject
{
  public  class DataSource
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
            internal static int ParcelCounter { set; get; } = 1000;

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
            String[] names = new String[] { "Moshe", "Aharon", "David", "Yosef", "Reuven", "Nachman", "Avraham", "lot", "Moav", "Amon" };
            String[] phones = new String[] { "051-123456", "052-123456", "053-123456", "054-123456", "055-123456", "056-123456", "057-123456", "058-123456", "059-123456", "050-123456" };
            for (int i = 0; i < 10; i++)
            {
                Customers[i].ID = r.Next(1, 10000);
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
            for (int i = 0; i <10; i++)//creating id and a counter
            {
                Parcels[i].ID = r.Next(1, 1000);
                Config.ParcelCounter++;
            }
            for (int i = 0; i < 10; i++)//every sender sends to the customer in the arry in index  9-i
            {
                Parcels[i].SenderId = Customers[i].ID;
                Parcels[i].TargetId = Customers[9 - i].ID;
                Parcels[i].Requested = new DateTime(2021, 10, i + 1,(i*35)/24,(i*65)/60,(i*102)/60);
            }
            for (int i=0;i<10;i++)
            {
                Parcels[i].Priority = ((IDAL.DO.PRIORITY)r.Next(0, 3));
                Parcels[i].Weigh = ((IDAL.DO.WEIGHT)r.Next(0, 3));
            }
            for (int i = 0; i < 5; i++)//5 parcels alredy  done and dilivred
            {
                TimeSpan time = new TimeSpan(0, r.Next(1, 11), r.Next(0, 60));
                Parcels[i].SchedulId =Parcels[i].Requested+time;
                time = new TimeSpan(r.Next(0, 2), r.Next(0, 60), r.Next(0, 60));
                Parcels[i].PickedUp = Parcels[i].SchedulId + time;
                time = new TimeSpan(0, r.Next(15, 30), r.Next(0, 60));
                Parcels[i].Delivered = Parcels[i].PickedUp + time;
                Parcels[i].DroneId = Drones[i].Id;
            }
            for (int i = 5; i < 8; i++)
            {
                TimeSpan time = new TimeSpan(0, r.Next(1, 11), r.Next(0, 60));
                Parcels[i].SchedulId = Parcels[i].Requested + time;
                time = new TimeSpan(r.Next(0, 2), r.Next(0, 60), r.Next(0, 60));
                Parcels[i].PickedUp = Parcels[i].SchedulId + time;
                Parcels[i].DroneId = Drones[i - 4].Id;
            }
            TimeSpan t = new TimeSpan(0, r.Next(1, 11), r.Next(0, 60));
            Parcels[8].SchedulId = Parcels[8].Requested + t;
            Parcels[8].DroneId = Drones[4].Id;

            for (int i = 0; i < 10; i++)
            {
                Customers[i].ID = r.Next(1, 1000);
            }

        }
    }
}
