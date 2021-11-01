using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
namespace DalObject
{
  public class DataSource
    {
        internal static List<Drone> drones = new List<Drone>();
        internal static List<Station> stations = new List<Station>();
        internal static List<Parcel> parcels = new List<Parcel>();
        internal static List<Customer> customers = new List<Customer>();
        internal static List<DroneCharge> droneCharges = new List<DroneCharge>();

        public static Random r = new Random();
        internal static void Initialize()
        {
            for (int i = 0; i < 2; i++)  //initializing the stations
            {
                Station sta = new Station();
                sta.Id = r.Next(1, 501);
                sta.Latitude = r.Next(1, 501);
                sta.Longitude = r.NextDouble() / 1.234;
                sta.ReadyChargeStands = r.Next(3, 7);
                stations.Add(sta);
            }
            Station tempStation = stations[0];
            tempStation.Name = "The centrial station";
            stations[0] = tempStation;
            tempStation = stations[1];
            tempStation.Name = "The 'HERTZEL' station";
            stations[1] = tempStation;

            String[] names = new String[] { "Moshe", "Aharon", "David", "Yosef", "Reuven", "Nachman", "Avraham", "lot", "Moav", "Amon" };
            String[] phones = new String[] { "051-123456", "052-123456", "053-123456", "054-123456", "055-123456", "056-123456", "057-123456", "058-123456", "059-123456", "050-123456" };
            for (int i = 0; i < 10; i++)  //initializing a customer
            {
                Customer cst = new Customer();
                cst.Id = r.Next(1, 10000);
                cst.Latitude = r.NextDouble() / 1.234;
                cst.Longitude = r.NextDouble() / 1.234;
                cst. Name = names[i];
                cst.Phone = phones[i];
            }

            for (int i = 0; i < 6; i++)  //we have 5 drones 1 in MAINTENANSE 3 in DELIVERY and 1 in AVAILABLE
            {
                Drone drn = new Drone();
                drn.Id = r.Next(1, 1000);
                drn.MaxWeight = WEIGHT.HEAVY;
            }
            Drone tempDrone = drones[0];
            Drones[0].Model = "AB123";
            Drones[1].Model = "CD123";
            Drones[2].Model = "EF123";
            Drones[3].Model = "GH123";
            Drones[4].Model = "IJ123";
            Drones[5].Model = "KL123";
            for (int i = 0; i < 10; i++)  //creating ID and a counter
            {
                Parcels[i].Id = Config.RuningNumber++;
            }
            for (int i = 0; i < 10; i++)  //every sender sends to the customer in the arry in index 9-i
            {
                Parcels[i].SenderId = Customers[i].Id;
                Parcels[i].TargetId = Customers[9 - i].Id;
                Parcels[i].Requested = new DateTime(2021, 10, i + 1, (i * 35) / 24, (i * 65) / 60, (i * 102) / 60);
                Config.ParcelsIndex++;
            }
            for (int i = 0; i < 10; i++)
            {
                Parcels[i].Priority = ((IDAL.DO.PRIORITY)r.Next(0, 3));
                Parcels[i].Weigh = ((IDAL.DO.WEIGHT)r.Next(0, 3));
            }
            for (int i = 0; i < 5; i++)  //5 parcels alredy done and delivered
            {
                TimeSpan time = new TimeSpan(0, r.Next(1, 11), r.Next(0, 60));
                Parcels[i].Scheduled = Parcels[i].Requested + time;
                time = new TimeSpan(r.Next(0, 2), r.Next(0, 60), r.Next(0, 60));
                Parcels[i].PickedUp = Parcels[i].Scheduled + time;
                time = new TimeSpan(0, r.Next(15, 30), r.Next(0, 60));
                Parcels[i].Delivered = Parcels[i].PickedUp + time;
                Parcels[i].DroneId = Drones[i].Id;
            }
            for (int i = 5; i < 8; i++)
            {
                TimeSpan time = new TimeSpan(0, r.Next(1, 11), r.Next(0, 60));
                Parcels[i].Scheduled = Parcels[i].Requested + time;
                time = new TimeSpan(r.Next(0, 2), r.Next(0, 60), r.Next(0, 60));
                Parcels[i].PickedUp = Parcels[i].Scheduled + time;
                Parcels[i].DroneId = Drones[i - 4].Id;
            }
            TimeSpan t = new TimeSpan(0, r.Next(1, 11), r.Next(0, 60));
            Parcels[8].Scheduled = Parcels[8].Requested + t;
            Parcels[8].DroneId = Drones[4].Id;

            for (int i = 0; i < 10; i++)
            {
                Customers[i].Id = r.Next(1, 1000);
            }

        }
    }
}
