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
            char ch = Convert.ToChar(Console.ReadLine());
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
                            char ch1 = Convert.ToChar(Console.ReadLine());
                            while (ch1 != 'e')
                            {
                                switch (ch1)
                                {
                                    case 'a':
                                        {

                                            IDAL.DO.Station station = new IDAL.DO.Station();

                                            Console.WriteLine("Enter station id");
                                            station.Id = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Enter station name");
                                            station.Name = Console.ReadLine();
                                            Console.WriteLine("Enter longitud");
                                            station.Longitude =Convert.ToDouble(Console.ReadLine());
                                            Console.WriteLine("Enter lattitude ");
                                            station.Lattitude = Convert.ToDouble(Console.ReadLine());
                                            Console.WriteLine("Enter amount of charging slats");
                                            station.ChargeSlots = Convert.ToInt32(Console.ReadLine());
                                            DalObject.DalObject dalObject = new DalObject.DalObject();
                                            dalObject.AddStation(station);

                                            break;
                                        }
                                    case 'b':
                                        {
                                            IDAL.DO.Drone drone = new IDAL.DO.Drone();

                                            Console.WriteLine("Enter drone id");
                                            drone.Id = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Enter Dron model");
                                            drone.Model = Console.ReadLine();
                                            Console.WriteLine(", Drone max-whight");
                                            drone.MaxWeight = (IDAL.DO.WEIGHT)Convert.ToInt32(Console.ReadLine());
                                            drone.Status = IDAL.DO.STATUS.AVAILABLE;
                                            drone.Battery = 100;                              
                                            DalObject.DalObject dalObject = new DalObject.DalObject();
                                            dalObject.AddDrone(drone);
                                            break;
                                        }
                                    case 'c':
                                        {
                                            IDAL.DO.Customer customer = new IDAL.DO.Customer();
                                            Console.WriteLine("Enter customer id");
                                            customer.ID = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Enter customer name");
                                            customer.Name = Console.ReadLine();
                                            Console.WriteLine("Enter customer phone");
                                            customer.Phone = Console.ReadLine();
                                            Console.WriteLine("Enter longitud");
                                            customer.Longitute = Convert.ToDouble(Console.ReadLine());
                                            Console.WriteLine("Enter lattitude ");
                                            customer.Lattitude = Convert.ToDouble(Console.ReadLine());
                                            DalObject.DalObject dalObject = new DalObject.DalObject();
                                            dalObject.AddCustomer(customer);
                                            break;
                                        }
                                    case 'd':
                                        {
                                            IDAL.DO.Parcel parcel = new IDAL.DO.Parcel();
                                            Console.WriteLine("Enter parcel id");
                                            parcel.ID = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Enter sender id");
                                            parcel.SenderId = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Enter target id");
                                            parcel.TargetId = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Enter parcel weight");
                                            parcel.Weigh = (IDAL.DO.WEIGHT)(Convert.ToInt32(Console.ReadLine()));
                                            Console.WriteLine("Enter parcel Priority");
                                            parcel.Priority = (IDAL.DO.PRIORITY)(Convert.ToInt32(Console.ReadLine()));
                                            parcel.Requested = DateTime.Now;
                                            parcel.DroneId = 0;
                                            DalObject.DalObject dalObject = new DalObject.DalObject();
                                            dalObject.AddSParcel(parcel);
                                            break;
                                        }
                                }
                                Console.WriteLine("Enter your next choice");
                                ch1 = Convert.ToChar(Console.ReadLine());
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
                            char ch2 = Convert.ToChar(Console.ReadLine());
                            while (ch2 != 'e')
                            {
                                switch (ch2)
                                {
                                    case 'a':
                                        {
                                            Console.WriteLine("Enter drone id");
                                            int droneid = Convert.ToInt32(Console.ReadLine());
                                            Console.WriteLine("Enter parcel id");
                                            int pacelid = Convert.ToInt32(Console.ReadLine());
                                            DalObject.DalObject dalObject = new DalObject.DalObject();

                                            break;
                                        }
                                    case 'b':
                                        {
                                            break;
                                        }
                                    case 'c':
                                        {
                                            break;
                                        }
                                    case 'd':
                                        {
                                            break;
                                        }
                                }
                                Console.WriteLine("Enter your next choice");
                                ch2 = Convert.ToChar(Console.ReadLine());
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
                            char ch3 = Convert.ToChar(Console.ReadLine());
                            while (ch3 != 'e')
                            {
                                switch (ch3)
                                {
                                    case 'a':
                                        {
                                            break;
                                        }
                                    case 'b':
                                        {
                                            break;
                                        }
                                    case 'c':
                                        {
                                            break;
                                        }
                                    case 'd':
                                        {
                                            break;
                                        }
                                }
                                Console.WriteLine("Enter your next choice");
                                ch3 = Convert.ToChar(Console.ReadLine());
                            }
                            break;
                        }
                    case 'D':
                        {
                            Console.WriteLine(@"Enter 'a'to display the station-list
                            Enter 'b' to display the drone-list 
                            Enter 'c' to display the customer-list
                             Enter 'd' to display the parcel-list
                            Enter 'e' to exit");
                            char ch4 = Convert.ToChar(Console.ReadLine());
                            while (ch4 != 'e')
                            {
                                switch (ch4)
                                {
                                    case 'a':
                                        {
                                            break;
                                        }
                                    case 'b':
                                        {
                                            break;
                                        }
                                    case 'c':
                                        {
                                            break;
                                        }
                                    case 'd':
                                        {
                                            break;
                                        }
                                }
                                Console.WriteLine("Enter your next choice");
                                ch4 = Convert.ToChar(Console.ReadLine());
                            }
                            break;
                        }
                }
                Console.WriteLine("Enter your next choice");
                ch = Convert.ToChar(Console.ReadLine());
            }
        }
    }
}
