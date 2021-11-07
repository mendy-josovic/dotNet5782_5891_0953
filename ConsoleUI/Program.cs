using System;
using DalObject;
using IDAL.DO;
using System.Collections.Generic;
namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"Welcome:
            Enter 'A' to add
            Enter 'B' to update
            Enter 'C' to diplay an element
            Enter 'D' to display data
            Enter 'E' to exit
            ");
            char.TryParse(Console.ReadLine(), out char ch);
            DalObject.DalObject dalObject1 = new DalObject.DalObject();//creating  the class for all the funcs
            while (ch != 'E')
            {
                switch (ch)
                {
                    case 'A':
                        {
                            Console.WriteLine(@"
                            Enter 'a' to add a station
                            Enter 'b' to add a drone
                            Enter 'c' to add a customer
                            Enter 'd' to add a parcel 
                            Enter 'e' to exit");                            
                            char.TryParse(Console.ReadLine(), out char ch1);
                            while (ch1 != 'e')
                            {
                                switch (ch1)
                                {
                                    case 'a':
                                        {
                                            
                                            IDAL.DO.Station station = new IDAL.DO.Station();  //gets all the elements for a new station
                                            Console.WriteLine("Enter station ID");                                     
                                            Int32.TryParse(Console.ReadLine(), out int x);
                                            station.Id = x;
                                            Console.WriteLine("Enter station name");
                                            station.Name = Console.ReadLine();
                                            Console.WriteLine("Enter longitud");                                           
                                            double.TryParse(Console.ReadLine(), out double y);
                                            station.Longitude = y;
                                            Console.WriteLine("Enter lattitude");                                           
                                            double.TryParse(Console.ReadLine(), out y);
                                            station.Longitude = y;
                                            Console.WriteLine("Enter amount of charging slats");                                            
                                            Int32.TryParse(Console.ReadLine(), out x);
                                            station.ReadyChargeStands = x;
                                            dalObject1.AddStation(station);
                                            break;
                                        }
                                    case 'b':
                                        {
                                            IDAL.DO.Drone drone = new IDAL.DO.Drone();  //gets all the elements for a new drone
                                            Console.WriteLine("Enter drone ID");                                            
                                            Int32.TryParse(Console.ReadLine(), out int x);
                                            drone.Id = x;
                                            Console.WriteLine("Enter drone model");
                                            drone.Model = Console.ReadLine();
                                            Console.WriteLine("Enter drone max-whight");                                           
                                            Int32.TryParse(Console.ReadLine(), out x);
                                            drone.MaxWeight = (IDAL.DO.WEIGHT)x;                                        
                                                              
                                            dalObject1.AddDrone(drone);
                                            break;
                                        }
                                    case 'c':
                                        {
                                            IDAL.DO.Customer customer = new IDAL.DO.Customer();  //gets all the elements for a new customer
                                            Console.WriteLine("Enter customer ID");                                           
                                            int.TryParse(Console.ReadLine(), out int x);
                                            customer.Id =x;
                                            Console.WriteLine("Enter customer name");
                                            customer.Name = Console.ReadLine();
                                            Console.WriteLine("Enter customer phone");
                                            customer.Phone = Console.ReadLine();
                                            Console.WriteLine("Enter longitude");                                            
                                            double.TryParse(Console.ReadLine(), out double y);
                                            customer.Longitude = y;
                                            Console.WriteLine("Enter latitude");                                            
                                            double.TryParse(Console.ReadLine(), out y);
                                            customer.Latitude = y;
                                            dalObject1.AddCustomer(customer);  //sends to the func
                                            break;
                                        }
                                    case 'd':
                                        {
                                            IDAL.DO.Parcel parcel = new IDAL.DO.Parcel();  //gets all the elements for a new parcel
                                            Console.WriteLine("Enter parcel ID");                                            
                                            int.TryParse(Console.ReadLine(), out int x);
                                            parcel.Id = x;
                                            Console.WriteLine("Enter sender ID");                                            
                                            int.TryParse(Console.ReadLine(), out x);
                                            parcel.SenderId = x;
                                            Console.WriteLine("Enter target ID");                                            
                                            int.TryParse(Console.ReadLine(), out x);
                                            parcel.TargetId = x;
                                            Console.WriteLine("Enter parcel weight");                                            
                                            int.TryParse(Console.ReadLine(), out x);
                                            parcel.Weigh = (IDAL.DO.WEIGHT)x;
                                            Console.WriteLine("Enter parcel Priority");                                           
                                            int.TryParse(Console.ReadLine(), out x);
                                            parcel.Priority = (IDAL.DO.PRIORITY)x;
                                            parcel.Requested = DateTime.Now;
                                            parcel.DroneId = 0;                             
                                            dalObject1.AddSParcel(parcel);
                                            break;
                                        }
                                }
                                Console.WriteLine("Enter your next choice in add menu");
                                char.TryParse(Console.ReadLine(), out ch1);
                            }
                            break;
                        }
                    case 'B':
                        {
                            Console.WriteLine(@"
                            Enter 'a' to assign a drone to a parcel
                            Enter 'b' to update a pickup for a parcel
                            Enter 'c' to send a drone to charge
                            Enter 'd' to return a drone from charging
                            Enter 'f' to supply a parcel to the customer
                            Enter 'e' to exit");                       
                            char.TryParse(Console.ReadLine(), out char ch2);
                            while (ch2 != 'e')
                            {
                                switch (ch2)
                                {
                                    case 'a':
                                        {
                                            Console.WriteLine("Enter drone ID");  //gets the IDs and sends to the right place
                                            int.TryParse(Console.ReadLine(), out int droneId);
                                            //dalObject1.DroneStatusDelivery(droneId);
                                            Console.WriteLine("Enter parcel ID");
                                            int.TryParse(Console.ReadLine(), out int parcelId);
                                            dalObject1.ParcelScheduled(parcelId);
                                            dalObject1.DroneIdOfPArcel(parcelId, droneId);
                                            break;
                                        }
                                    case 'b':
                                        {
                                            Console.WriteLine("Enter parcel ID");  //sends the parcel to update                             
                                            int.TryParse(Console.ReadLine(), out int parcelId);                                        
                                            dalObject1.PickUp(parcelId);
                                            break;
                                        }
                                    case 'c':
                                        {                                           
                                            Console.WriteLine("Enter drone ID");                                           
                                            int.TryParse(Console.ReadLine(), out int dtoneId);
                                            dalObject1.DroneStatusMaintenanse(dtoneId);
                                            Console.WriteLine("Enter ID of a station from the list:");
                                            dalObject1.PrintAvailableChargingStations();
                                            int.TryParse(Console.ReadLine(), out int stationId);
                                            dalObject1.UpdateReadyStandsInStation(stationId);
                                            dalObject1.CreateANewDroneCharge(stationId, dtoneId);
                                            break;
                                        }
                                    case 'd':
                                        {
                                            Console.WriteLine("Enter drone ID");
                                            int.TryParse(Console.ReadLine(), out int droneId);                        
                                            dalObject1.DroneStatusAvailable(droneId);
                                            dalObject1.ClearDroneCharge(droneId);
                                            dalObject1.UpdateDroneChargesIndex(droneId);
                                            break;
                                        }
                                    case 'f':
                                        {
                                            Console.WriteLine("Enter parcel ID");
                                            int.TryParse(Console.ReadLine(), out int parcelId);
                                            dalObject1.UpdateTimeOfSupplied(parcelId);
                                            dalObject1.DroneStatusAvailable1(parcelId);
                                            break;
                                        }
                                }
                                Console.WriteLine("Enter your next choice in update menu");
                                char.TryParse(Console.ReadLine(), out ch2); 
                            }
                            break;

                        }
                    case 'C':
                        {
                            Console.WriteLine(@"
                            Enter 'a' to display a station
                            Enter 'b' to display a drone 
                            Enter 'c' to display a customer
                            Enter 'd' to display a parcel
                            Enter 'e' to exit");                           
                            char.TryParse(Console.ReadLine(), out char ch3);
                            while (ch3 != 'e')
                            {
                                switch (ch3)
                                {
                                    case 'a':
                                        {                     
                                            Console.WriteLine("Enter station ID");                                           
                                            int.TryParse(Console.ReadLine(), out int stationId);
                                            IDAL.DO.Station st = dalObject1.PrintStation(stationId);
                                            Console.WriteLine(st);
                                            break;
                                        }
                                    case 'b':
                                        {
                                            Console.WriteLine("Enter drone ID");                                            
                                            int.TryParse(Console.ReadLine(), out int droneId);
                                            IDAL.DO.Drone dr = dalObject1.PrintDrone(droneId);
                                            Console.WriteLine(dr);
                                            break;
                                        }
                                    case 'c':
                                        {                                      
                                            Console.WriteLine("Enter customer ID");                                            
                                            int.TryParse(Console.ReadLine(), out int customerId);
                                            IDAL.DO.Customer cm = dalObject1.PrintCustomer(customerId);
                                            Console.WriteLine(cm);
                                            break;

                                        }
                                    case 'd':
                                        {
                                            Console.WriteLine("Enter parcel ID");                                             
                                            int.TryParse(Console.ReadLine(), out int parcelId);
                                            IDAL.DO.Parcel pr = dalObject1.PrintParcel(parcelId);
                                            Console.WriteLine(pr);
                                            break;
                                        }
                                }
                                Console.WriteLine("Enter your next choice in 'diplay an element' menu");
                                char.TryParse(Console.ReadLine(), out ch3);
                            }
                            break;
                        }
                    case 'D':
                        {
                            Console.WriteLine(@"
                            Enter 'a' to display the station-list
                            Enter 'b' to display the drone-list 
                            Enter 'c' to display the customer-list
                            Enter 'd' to display the parcel-list
                            Enter 'f' to display unassinged parcels
                            Enter 'g' to disply stations with available charging slots
                            Enter 'e' to exit");
                            char.TryParse(Console.ReadLine(), out char ch4);
                            while (ch4 != 'e')
                            {
                                switch (ch4)
                                {
                                    case 'a':
                                        {
                                            List<Station> Stations = new List<Station>(dalObject1.PrintStationList());                                         
                                           foreach (Station item in Stations)
                                            {
                                                Console.WriteLine();
                                            }
                                            break;
                                        }
                                    case 'b':
                                        {
                                            List<Drone> Drones = new List<Drone>(dalObject1.PrintDroneList());
                                            foreach (Drone item in Drones)
                                            {
                                                Console.WriteLine();
                                            }
                                            break;
                                        }
                                    case 'c':
                                        { 
                                            List<Customer> Customers= new List<Customer>(dalObject1.PrintCustomerList());
                                            foreach (Customer item in Customers)
                                            {
                                                Console.WriteLine();
                                            }
                                            break;
                                        }
                                    case 'd':
                                        {
                                            List<Parcel> Parcels = new List<Parcel>(dalObject1.PrintParcelList());
                                            foreach (Parcel item in Parcels)
                                            {
                                                Console.WriteLine();
                                            }
                                            break;
                                        }
                                    case 'f':
                                        {
                                            List<Parcel> Parcels = new List<Parcel>(dalObject1.PrintUnassignedParcels());
                                            foreach (Parcel item in Parcels)
                                            {
                                                Console.WriteLine();
                                            }
                                            break;
                                        }
                                    case 'g':
                                        {
                                            List<Station> Stations = new List<Station>(dalObject1.PrintAvailableChargingStations());
                                            foreach (Station item in Stations)
                                            {
                                                Console.WriteLine();
                                            }
                                            break;
                                        }
                                }
                                Console.WriteLine("Enter your next choice in 'display data' menu");
                                char.TryParse(Console.ReadLine(), out ch4);
                            }
                            break;
                        }
                }
                Console.WriteLine("Enter your next choice (in the main menu)");
                char.TryParse(Console.ReadLine(), out ch);
            }
        }
    }
}
