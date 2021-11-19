using System;
using IBL.BO;
using IBL;
namespace ConsoleUI_BL
{ 
    public partial class ConsoleUI_BL
    {
        public static void add()
        {
            try
            {
                char.TryParse(Console.ReadLine(), out char ch);
                IBl BLObject = new BL.BL(); //creating an object of BL class for all the functions
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
                                    Station station = new Station();  //gets all the elements for a new station
                                    Console.WriteLine("Enter station ID: ");
                                    Int32.TryParse(Console.ReadLine(), out int x);
                                    if (x <= 0)
                                        throw "Invalid value\n";
                                    station.Id = x;
                                    Console.WriteLine("\nEnter a name for the station: ");
                                    station.Name = Console.ReadLine();
                                    Console.WriteLine("\nEnter longitude: ");
                                    double.TryParse(Console.ReadLine(), out double y);
                                    station.location.Longitude = y;
                                    Console.WriteLine("\nEnter latitude: ");
                                    double.TryParse(Console.ReadLine(), out y);
                                    station.location.Longitude = y;
                                    Console.WriteLine("\nEnter amount of charging slats: ");
                                    Int32.TryParse(Console.ReadLine(), out x);
                                    if (x < 0)
                                        throw "Invalid value\n";
                                    station.ReadyStandsInStation = x;
                                    try
                                    {
                                        BLObject.AddStation(station);
                                    }
                                    catch ()
                                    {

                                    }
                                    break;
                                }
                            case 'b':
                                {
                                    Drone drone = new Drone();  //gets all the elements for a new drone
                                    Console.WriteLine("Enter drone ID: ");
                                    Int32.TryParse(Console.ReadLine(), out int x);
                                    if (x <= 0)
                                        throw "Invalid value\n";
                                    drone.Id = x;
                                    Console.WriteLine("\nEnter drone model: ");
                                    drone.Model = Console.ReadLine();
                                    Console.WriteLine("\nEnter the max whight the drone can take: ");
                                    Int32.TryParse(Console.ReadLine(), out x);
                                    drone.MaxWeight = (WEIGHT)x;
                                    Console.WriteLine("\nEnter the ID of the station to the first charging");
                                    Int32.TryParse(Console.ReadLine(), out x);
                                    if (x <= 0)
                                        throw "Invalid value\n";                                    
                                    BLObject.AddDrone(drone);
                                    break;
                                }
                            case 'c':
                                {
                                    IDAL.DO.Customer customer = new IDAL.DO.Customer();  //gets all the elements for a new customer
                                    Console.WriteLine("Enter customer ID");
                                    int.TryParse(Console.ReadLine(), out int x);
                                    customer.Id = x;
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
            }
            catch()
        }
}
