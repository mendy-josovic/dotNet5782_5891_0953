using System;
using BO;
using BlApi;
namespace ConsoleUI_BL
{
    public partial class ConsoleUI_BL
    {
        public static void add()
        {
            try
            {                
                {
                    Console.WriteLine(@"
                            Enter 'a' to add a station
                            Enter 'b' to add a drone
                            Enter 'c' to add a customer
                            Enter 'd' to add a parcel 
                            Enter 'e' to exit");
                    IBl bl = BlFactory.GetBl();
                    char.TryParse(Console.ReadLine(), out char ch1);
                    while (ch1 != 'e')
                    {
                        switch (ch1)
                        {
                            case 'a':
                                {
                                    Station station = new Station();  //gets all the elements for a new station
                                    Console.WriteLine("Enter station ID: ");
                                    Int32.TryParse(Console.ReadLine(), out int x);
                                    if (x <= 0)
                                        throw new ConsoleBlException ("Invalid value");
                                    station.Id = x;
                                    Console.WriteLine("\nEnter a name for the station: ");
                                    station.Name = Console.ReadLine();
                                    Console.WriteLine("\nLocation:\nEnter longitude: ");
                                    double.TryParse(Console.ReadLine(), out double y);
                                    station.location = new();
                                    station.location.Longitude = y;
                                    Console.WriteLine("\nEnter latitude: ");
                                    double.TryParse(Console.ReadLine(), out y);
                                    station.location.Latitude = y;
                                    Console.WriteLine("\nEnter amount of charging slats: ");
                                    Int32.TryParse(Console.ReadLine(), out x);
                                    if (x < 0)
                                        throw new ConsoleBlException("Invalid value");
                                    station.ReadyStandsInStation = x;
                                    try
                                    {
                                        bl.AddStation(station);
                                    }
                                    catch (ConsoleBlException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    catch (BO.BlException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                }
                            case 'b':
                                {
                                    Drone drone = new Drone();  //gets all the elements for a new drone
                                    Console.WriteLine("Enter drone ID: ");
                                    Int32.TryParse(Console.ReadLine(), out int x);
                                    if (x <= 0)
                                        throw new ConsoleBlException( "Invalid value");
                                    drone.Id = x;
                                    Console.WriteLine("\nEnter drone model: ");
                                    drone.Model = Console.ReadLine();
                                    Console.WriteLine(@"
                                        Choose the maximum wheit the drone can take:
                                        Enter 0 for LIGHT
                                        Enter 1 for MEDIUM
                                        Enter 2 for HEAVY");
                                    Int32.TryParse(Console.ReadLine(), out x);
                                    drone.MaxWeight = (BO.Weight)x;
                                    Console.WriteLine("\nEnter the ID of the station to the first charging");
                                    Int32.TryParse(Console.ReadLine(), out x);
                                    if (x <= 0)
                                        throw new ConsoleBlException("Invalid value");
                                    try
                                    {
                                        ConsoleUI_BL.bl.AddDrone(drone, x);
                                    }
                                    catch (ConsoleBlException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    catch (BO.BlException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                }
                            case 'c':
                                {
                                    Customer customer = new();  //gets all the elements for a new customer
                                    Console.WriteLine("Enter customer ID: ");
                                    Int32.TryParse(Console.ReadLine(), out int x);
                                    if (x <= 0)
                                        throw new ConsoleBlException("Invalid value");
                                    customer.Id = x;
                                    Console.WriteLine("\nEnter customer name: ");
                                    customer.Name = Console.ReadLine();
                                    Console.WriteLine("\nEnter customer phone: ");
                                    customer.Phone = Console.ReadLine();
                                    Console.WriteLine("\nLocation:\nEnter longitude: ");
                                    double.TryParse(Console.ReadLine(), out double y);
                                    customer.location = new();
                                    customer.location.Longitude = y;
                                    Console.WriteLine("\nEnter latitude: ");
                                    double.TryParse(Console.ReadLine(), out y);
                                    customer.location.Latitude = y;
                                    try
                                    {
                                        ConsoleUI_BL.bl.AddCustomer(customer);  //sends to the func
                                    }
                                    catch (ConsoleBlException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    catch (BO.BlException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                }
                            case 'd':
                                {
                                    Parcel parcel = new();  //gets all the elements for a new parcel
                                    Console.WriteLine("Enter parcel ID: ");
                                    Int32.TryParse(Console.ReadLine(), out int x);
                                    if (x <= 0)
                                        throw new ConsoleBlException("Invalid value");
                                    parcel.Id = x;
                                    Console.WriteLine("\nEnter sender ID: ");
                                    Int32.TryParse(Console.ReadLine(), out x);
                                    if (x <= 0)
                                        throw new ConsoleBlException("Invalid value");
                                    parcel.Sender.Id = x;
                                    Console.WriteLine("\nEnter target ID: ");
                                    Int32.TryParse(Console.ReadLine(), out x);
                                    if (x <= 0)
                                        new ConsoleBlException("Invalid value");
                                    parcel.Recipient.Id = x;
                                    Console.WriteLine(@"
                                        Choose the wheit of the parcel:
                                        Enter 0 for LIGHT
                                        Enter 1 for MEDIUM
                                        Enter 2 for HEAVY");
                                    int.TryParse(Console.ReadLine(), out x);
                                    parcel.Weight = (BO.Weight)x;
                                    Console.WriteLine(@"
                                        Choose the priority of the parcel:
                                        Enter 0 for REGULAR
                                        Enter 1 for FAST
                                        Enter 2 for EMERGENCY");
                                    int.TryParse(Console.ReadLine(), out x);
                                    parcel.Priority = (BO.Priority)x;
                                    try
                                    {
                                        ConsoleUI_BL.bl.AddParcel(parcel);
                                    }
                                    catch (ConsoleBlException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    catch (BO.BlException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Wrong choice\n");
                                    break;
                                }
                        }
                        Console.WriteLine("Enter your next choice in add menu");
                        char.TryParse(Console.ReadLine(), out ch1);
                    }
                }
            }
            catch (ConsoleBlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
