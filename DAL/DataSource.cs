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
        internal class Config
        {
            public static int RuningNumber { get; set; } = 1000;
            public static double vacant { get; set; } = 1;
            public static double LightWeightCarrier { get; set; } =2;
            public static double MediumWeightCarrier { get; set; } = 3;
            public static double HeavyWeightCarrier  { get; set; } = 4;
            public static double ChargingRate { get; set; } = 20;
        };
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
                customers.Add(cst);
            }

            for (int i = 0; i < 6; i++)  //we have 5 drones 
            {
                Drone drn = new Drone();
                drn.Id = r.Next(1, 1000);
                drn.MaxWeight = WEIGHT.HEAVY;
                drones.Add(drn);
            }
            Drone tempDrone = drones[0];
            tempDrone.Model = "AB123";
            drones[0] = tempDrone;
            tempDrone = drones[1];
            tempDrone.Model = "CD123";
            drones[1] = tempDrone;
            tempDrone = drones[2];
            tempDrone.Model = "EF123";
            drones[2] = tempDrone;
            tempDrone = drones[3];
            tempDrone.Model = "GH123";
            drones[3] = tempDrone;
            tempDrone = drones[4];
            tempDrone.Model = "IJ123";
            drones[4] = tempDrone;
            Parcel tempParcel = new Parcel();
            for (int i = 0; i < 10; i++)  //creating ID and a counter for 10 parcels
            { 
                tempParcel.Id = Config.RuningNumber++;
                parcels.Add(tempParcel);
            }
            Customer tempcustomer = new Customer();
            for (int i = 0; i < 10; i++)  //every sender sends to the customer in the arry in index 9-i
            {
                tempParcel = parcels[i];
                tempcustomer = customers[i];
                tempParcel.SenderId = tempcustomer.Id;
                tempcustomer = customers[9 - i];
                tempParcel.TargetId = tempcustomer.Id; 
                tempParcel.Priority = ((IDAL.DO.PRIORITY)r.Next(0, 3));
                tempParcel.Weigh = ((IDAL.DO.WEIGHT)r.Next(0, 3));
                tempParcel.Requested = new DateTime(2021, 10, i + 1, (i * 35) / 24, (i * 65) / 60, (i * 102) / 60);
                parcels.Add(tempParcel);
            }
            for (int i = 0; i < 5; i++)  //5 parcels alredy done and delivered
            {
                TimeSpan time = new TimeSpan(0, r.Next(1, 11), r.Next(0, 60));
                tempParcel = parcels[i];
                tempParcel.Scheduled = tempParcel.Requested + time;
                time = new TimeSpan(r.Next(0, 2), r.Next(0, 60), r.Next(0, 60));
                tempParcel.PickedUp =tempParcel.Scheduled + time;
                time = new TimeSpan(0, r.Next(15, 30), r.Next(0, 60));
                tempParcel.Delivered = tempParcel.PickedUp + time;
                tempDrone = drones[i];
                tempParcel.DroneId = tempDrone.Id;
                parcels[i] = tempParcel;
            }
            for (int i = 5; i < 8; i++)//3 are on the way 
            {
                TimeSpan time = new TimeSpan(0, r.Next(1, 11), r.Next(0, 60));
                tempParcel = parcels[i];
                tempParcel.Scheduled = tempParcel.Requested + time;
                time = new TimeSpan(r.Next(0, 2), r.Next(0, 60), r.Next(0, 60));
                tempParcel.PickedUp = tempParcel.Scheduled + time;
                tempDrone = drones[i - 4];
                tempParcel.DroneId = tempDrone.Id;
                parcels[i] = tempParcel;
            }
            TimeSpan t = new TimeSpan(0, r.Next(1, 11), r.Next(0, 60));
            tempParcel = parcels[8];
            tempParcel.Scheduled = tempParcel.Requested + t;
            tempDrone = drones[4];
            tempParcel.DroneId = tempDrone.Id;
            parcels[8] = tempParcel;
        }
    }
}
