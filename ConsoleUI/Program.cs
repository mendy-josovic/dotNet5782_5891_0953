using System;
using DalObject;
namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"Welcome:
            Enter 'A' to add
            Enter 'B' to update
            Enter 'C' for a diplay of a element
            Enter 'D' for a display of the data
            Enter 'E' to exit
            ");
            char ch;
            char.TryParse(Console.ReadLine(),out ch);
            DalObject.DalObject dalObject1 = new DalObject.DalObject();//creating  the class for all the funcs
            while(ch!='E')
            {
                switch (ch)
                {
                    case 'A':
                        {
                            Console.WriteLine(@"Enter 'a' to add a station
                            Enter 'b' to add a drone
                            Enter 'c' to add a customer
                            Enter 'd' to add a parcel 
                            Enter 'e' to exit");
                            char ch1;
                            char.TryParse(Console.ReadLine(), out ch1);
                            while (ch1 != 'e')
                            {
                                switch (ch1)
                                {
                                    case 'a':
                                        {

                                            IDAL.DO.Station station = new IDAL.DO.Station();//gets all the elements for the new station

                                            Console.WriteLine("Enter station id");
                                            int x;
                                            Int32.TryParse(Console.ReadLine(),out x );
                                            station.Id = x;
                                            Console.WriteLine("Enter station name");
                                            station.Name = Console.ReadLine();
                                            Console.WriteLine("Enter longitud");
                                            double y;
                                            double.TryParse(Console.ReadLine(),out y);
                                            station.Longitude = y;
                                            Console.WriteLine("Enter lattitude ");
                                            double z;
                                            double.TryParse(Console.ReadLine(), out z);
                                            station.Longitude = z;
                                            Console.WriteLine("Enter amount of charging slats");
                                            int a;
                                            Int32.TryParse(Console.ReadLine(), out a);
                                            station.ReadyChargeStands = a;
                                            dalObject1.AddStation(station);

                                            break;
                                        }
                                    case 'b':
                                        {
                                            IDAL.DO.Drone drone = new IDAL.DO.Drone();//gets all the elements for the new drone

                                            Console.WriteLine("Enter drone id");
                                            int x;
                                            Int32.TryParse(Console.ReadLine(), out x);

                                            drone.Id = x;
                                            Console.WriteLine("Enter Dron model");

                                            drone.Model = Console.ReadLine();
                                            Console.WriteLine("Enter Drone max-whight");
                                            int y;
                                            Int32.TryParse(Console.ReadLine(), out y);
                                            drone.MaxWeight =(IDAL.DO.WEIGHT) y;
                                            drone.Status = IDAL.DO.STATUS.AVAILABLE;
                                            drone.Battery = 100;                              
                                            dalObject1.AddDrone(drone);
                                            break;
                                        }
                                    case 'c':
                                        {
                                            IDAL.DO.Customer customer = new IDAL.DO.Customer();//gets all the elements for the new customer
                                            Console.WriteLine("Enter customer id");
                                            int x;
                                            int.TryParse(Console.ReadLine(), out x);
                                            customer.Id =x;
                                            Console.WriteLine("Enter customer name");
                                            customer.Name = Console.ReadLine();
                                            Console.WriteLine("Enter customer phone");
                                            customer.Phone = Console.ReadLine();
                                            Console.WriteLine("Enter longitud");
                                            double y;
                                            double.TryParse(Console.ReadLine(), out y);
                                            customer.Longitute = y;
                                            Console.WriteLine("Enter lattitude ");
                                            double z;
                                            double.TryParse(Console.ReadLine(), out z);
                                            customer.Longitute = z;
                                            dalObject1.AddCustomer(customer);//sends to the func
                                            break;
                                        }
                                    case 'd':
                                        {
                                            IDAL.DO.Parcel parcel = new IDAL.DO.Parcel();//gets all the elements for the new parcel
                                            Console.WriteLine("Enter parcel id");
                                            int x;
                                            int.TryParse(Console.ReadLine(), out x);
                                            parcel.Id = x;
                                            Console.WriteLine("Enter sender id");
                                            int y;
                                            int.TryParse(Console.ReadLine(), out y);
                                            parcel.SenderId = y;
                                            Console.WriteLine("Enter target id");
                                            int z;
                                            int.TryParse(Console.ReadLine(), out z);
                                            parcel.TargetId = z;
                                            Console.WriteLine("Enter parcel weight");
                                            int a;
                                            int.TryParse(Console.ReadLine(), out a);
                                            parcel.Weigh = (IDAL.DO.WEIGHT)a;
                                            Console.WriteLine("Enter parcel Priority");
                                            int b;
                                            int.TryParse(Console.ReadLine(), out b);
                                            parcel.Priority = (IDAL.DO.PRIORITY)b;
                                            parcel.Requested = DateTime.Now;
                                            parcel.DroneId = 0;                             
                                            dalObject1.AddSParcel(parcel);
                                            break;
                                        }
                                }
                                Console.WriteLine("Enter your next choice");
                                char.TryParse(Console.ReadLine(), out ch1);
                            }
                            break;
                        }
                    case 'B':
                        {
                            Console.WriteLine(@"Enter 'a to assing a drone to a parcel
                            Enter 'b' to update a pickup for a parcel
                            Enter 'c' to send a  drone to charg
                            Enter 'd' to to return a drone from chargeing
                            Enter 'e' to exit");
                            char ch2;
                            char.TryParse(Console.ReadLine(), out ch2);
                            while (ch2 != 'e')
                            {
                                switch (ch2)
                                {
                                    case 'a':
                                        {
                                            Console.WriteLine("Enter drone id");//gets the ids and sends to the right place
                                            int droneid;
                                            int.TryParse(Console.ReadLine(),out droneid);
                                            Console.WriteLine("Enter parcel id");
                                            int parcelid;
                                            int.TryParse(Console.ReadLine(), out parcelid);
                                          
                                            dalObject1.AssignDrone(parcelid, droneid);//assign gets pacel and drone

                                            break;
                                        }
                                    case 'b':
                                        {
                                            Console.WriteLine("Enter parcel id");//sends the parcel to update
                                            int parcelid;
                                            int.TryParse(Console.ReadLine(),out parcelid);
                                           
                                            dalObject1.PickUp(parcelid);
                                            break;
                                        }
                                    case 'c':
                                        {                                           
                                            Console.WriteLine("Enter drone id");
                                            int droneid;
                                            int.TryParse(Console.ReadLine(),out droneid);

                                            Console.WriteLine("Ente station id");                                      
                                            int stationid;
                                            int.TryParse(Console.ReadLine(), out stationid);
                                            dalObject1.SendToCharge( stationid,droneid);
                                            break;
                                        }
                                    case 'd':
                                        {
                                            Console.WriteLine("Enter drone id");
                                            int droneid;
                                            int.TryParse(Console.ReadLine(),out  droneid);                                     
                                            dalObject1.EndCharge(droneid);
                                            break;
                                        }
                                }
                                Console.WriteLine("Enter your next choice");
                                char.TryParse(Console.ReadLine(), out ch2); 
                            }
                            break;

                        }
                    case 'C':
                        {
                            Console.WriteLine(@"Enter 'a'to display a station
                            Enter 'b' to display a drone 
                            Enter 'c' to display a customer
                            Enter 'd' to display a parcel
                            Enter 'e' to exit");
                            char ch3;
                            char.TryParse(Console.ReadLine(), out ch3);
                            while (ch3 != 'e')
                            {
                                switch (ch3)
                                {
                                    case 'a':
                                        {                     
                                            Console.WriteLine("Enter station id");
                                            int stationid;
                                            int.TryParse(Console.ReadLine(), out stationid);
                                          IDAL.DO.Station st= dalObject1.PrintStation(stationid);
                                            Console.WriteLine(st);
                                            break;
                                        }
                                    case 'b':
                                        {
                                            Console.WriteLine("Enter drone id");
                                            int droneid;
                                            int.TryParse(Console.ReadLine(), out droneid);
                                           IDAL.DO.Drone dr= dalObject1.PrintDrone(droneid);
                                            Console.WriteLine(dr);
                                            break;
                                        }
                                    case 'c':
                                        {                                      
                                            Console.WriteLine("Enter customer id");
                                            int customerid;
                                            int.TryParse(Console.ReadLine(), out customerid);
                                           IDAL.DO.Customer cm= dalObject1.PrintCustomer(customerid);
                                            Console.WriteLine(cm);
                                            break;

                                        }
                                    case 'd':
                                        {
                                            Console.WriteLine("Enter parcel id");
                                            int parcelid;
                                            int.TryParse(Console.ReadLine(), out parcelid);
                                          IDAL.DO.Parcel pr=  dalObject1.PrintParcel(parcelid);
                                            Console.WriteLine(pr);
                                            break;
                                        }
                                }
                                Console.WriteLine("Enter your next choice");
                                char.TryParse(Console.ReadLine(), out ch3);
                            }
                            break;
                        }
                    case 'D':
                        {
                            Console.WriteLine(@"Enter 'a'to display the station-list
                            Enter 'b' to display the drone-list 
                            Enter 'c' to display the customer-list
                            Enter 'd' to display the parcel-list
                            Enter 'f' to display unassinged parcels
                            Enter 'g' to disply stations with available charging slots
                            Enter 'e' to exit");
                            char ch4;
                            char.TryParse(Console.ReadLine(), out ch4);
                            while (ch4 != 'e')
                            {
                                switch (ch4)
                                {
                                    case 'a':
                                        {
                                            IDAL.DO.Station[] station = dalObject1.PrintStationList();//prints all the objets
                                            for(int i=0;i<station.Length;i++)
                                            {
                                                Console.WriteLine(station[i]);
                                            }
                                              
                                            break;
                                        }
                                    case 'b':
                                        {
                                            IDAL.DO.Drone[] drones = dalObject1.PrintDroneList();
                                            for(int i=0;i<drones.Length;i++)
                                            {
                                                Console.WriteLine(drones[i]);
                                            }
                                            break;
                                        }
                                    case 'c':
                                        {
                                            IDAL.DO.Customer[] customer = dalObject1.PrintCustomerList();
                                            for (int i = 0; i < customer.Length; i++)
                                            {
                                                Console.WriteLine(customer[i]);
                                            }
                                            break;
                                        }
                                    case 'd':
                                        {
                                            IDAL.DO.Parcel[] parcel = dalObject1.PrintParcelList();
                                            for (int i = 0; i <parcel.Length; i++)
                                            {
                                                Console.WriteLine(parcel[i]);
                                            }
                                            break;
                                        }
                                    case 'f':
                                        {
                                            IDAL.DO.Parcel[] parcel = dalObject1.PrintUnassignedParcels();
                                            for (int i = 0; i < parcel.Length; i++)
                                            {
                                                Console.WriteLine(parcel[i]);
                                            }
                                            break;
                                        }
                                    case 'g':
                                        {
                                            IDAL.DO.Station[] station = dalObject1.PrintAvailableChargingStations();
                                            for (int i = 0; i < station.Length; i++)
                                            {
                                                Console.WriteLine(station[i]);
                                            }
                                            break;
                                        }
                                }
                                Console.WriteLine("Enter your next choice");
                                char.TryParse(Console.ReadLine(), out ch4);
                            }
                            break;
                        }
                }
                Console.WriteLine("Enter your next choice");
                char.TryParse(Console.ReadLine(), out ch);
            }
        }
    }
}
