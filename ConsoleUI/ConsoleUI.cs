using System;
using DO;
using System.Collections.Generic;
using DalApi;
using System.Runtime.Serialization;
namespace ConsoleUI
{
    class ConsoleUI
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
            try
            {
                char.TryParse(Console.ReadLine(), out char ch);
                IDal dalObject1 = DalFactory.GetDal("Object");//creating  the class for all the funcs
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
                                    try
                                    {
                                        switch (ch1)
                                        {
                                            case 'a':
                                                {

                                                    DO.Station station = new DO.Station();  //gets all the elements for a new station
                                                    Console.WriteLine("Enter station ID");
                                                    Int32.TryParse(Console.ReadLine(), out int x);
                                                    if (x <= 0)
                                                        throw new ConsoleException("Enter Id 1-999999");
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
                                                    DO.Drone drone = new DO.Drone();  //gets all the elements for a new drone
                                                    Console.WriteLine("Enter drone ID");
                                                    Int32.TryParse(Console.ReadLine(), out int x);
                                                    drone.Id = x;
                                                    if (x <= 0)
                                                        throw new ConsoleException("Enter Id 1-999999");
                                                    Console.WriteLine("Enter drone model");
                                                    drone.Model = Console.ReadLine();
                                                    Console.WriteLine("Enter drone max-whight");
                                                    Int32.TryParse(Console.ReadLine(), out x);
                                                    drone.MaxWeight = (DO.Weight)x;

                                                    dalObject1.AddDrone(drone);
                                                    break;
                                                }
                                            case 'c':
                                                {
                                                    DO.Customer customer = new DO.Customer();  //gets all the elements for a new customer
                                                    Console.WriteLine("Enter customer ID");
                                                    int.TryParse(Console.ReadLine(), out int x);
                                                    customer.Id = x;
                                                    if (x <= 0)
                                                        throw new ConsoleException("Enter Id 1-999999");
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
                                                    DO.Parcel parcel = new DO.Parcel();  //gets all the elements for a new parcel
                                                    Console.WriteLine("Enter parcel ID");
                                                    int.TryParse(Console.ReadLine(), out int x);
                                                    parcel.Id = x;
                                                    if (x <= 0)
                                                        throw new ConsoleException("Enter Id 1-999999");
                                                    Console.WriteLine("Enter sender ID");
                                                    int.TryParse(Console.ReadLine(), out x);
                                                    parcel.SenderId = x;
                                                    Console.WriteLine("Enter target ID");
                                                    int.TryParse(Console.ReadLine(), out x);
                                                    parcel.TargetId = x;
                                                    Console.WriteLine("Enter parcel weight");
                                                    int.TryParse(Console.ReadLine(), out x);
                                                    parcel.Weigh = (DO.Weight)x;
                                                    Console.WriteLine("Enter parcel Priority");
                                                    int.TryParse(Console.ReadLine(), out x);
                                                    parcel.Priority = (DO.Priority)x;
                                                    parcel.Requested = DateTime.Now;
                                                    parcel.DroneId = 0;
                                                    dalObject1.AddSParcel(parcel);
                                                    break;
                                                }
                                        }
                                    }
                                    catch (ConsoleException ex)
                                    {
                                        Console.WriteLine(ex.Message);
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
                                    try
                                    {
                                        switch (ch2)
                                        {
                                            case 'a':
                                                {
                                                    Console.WriteLine("Enter drone ID");  //gets the IDs and sends to the right place
                                                    int.TryParse(Console.ReadLine(), out int droneId);
                                                    if (droneId <= 0)
                                                        throw new ConsoleException("Enter Id 1-999999");
                                                    Console.WriteLine("Enter parcel ID");
                                                    int.TryParse(Console.ReadLine(), out int parcelId);
                                                    if (parcelId <= 0)
                                                        throw new ConsoleException("Enter Id 1-999999");
                                                    dalObject1.ParcelScheduled(parcelId);
                                                    dalObject1.DroneIdOfPArcel(parcelId, droneId);
                                                    break;
                                                }
                                            case 'b':
                                                {
                                                    Console.WriteLine("Enter parcel ID");  //sends the parcel to update                             
                                                    int.TryParse(Console.ReadLine(), out int parcelId);
                                                    if (parcelId <= 0)
                                                        throw new ConsoleException("Enter Id 1-999999");
                                                    dalObject1.PickUp(parcelId);
                                                    break;
                                                }
                                            case 'c':
                                                {
                                                    Console.WriteLine("Enter drone ID");
                                                    int.TryParse(Console.ReadLine(), out int dtoneId);
                                                    if (dtoneId <= 0)
                                                        throw new ConsoleException("Enter Id 1-999999");
                                                    Console.WriteLine("Enter ID of a station from the list:");
                                                    dalObject1.PrintStationList(w=>w.ReadyChargeStands > 0);
                                                    int.TryParse(Console.ReadLine(), out int stationId);
                                                    if (stationId <= 0)
                                                        throw new ConsoleException("Enter Id 1-999999");
                                                    dalObject1.UpdateReadyStandsInStation(stationId);
                                                    dalObject1.CreateANewDroneCharge(stationId, dtoneId);
                                                    break;
                                                }
                                            case 'd':
                                                {
                                                    Console.WriteLine("Enter drone ID");
                                                    int.TryParse(Console.ReadLine(), out int droneId);
                                                    if (droneId <= 0)
                                                        throw new ConsoleException("Enter Id 1-999999");
                                                    dalObject1.ClearDroneCharge(droneId);
                                                    break;
                                                }
                                            case 'f':
                                                {
                                                    Console.WriteLine("Enter parcel ID");
                                                    int.TryParse(Console.ReadLine(), out int parcelId);
                                                    if (parcelId <= 0)
                                                        throw new ConsoleException("Enter Id 1-999999");
                                                    dalObject1.UpdateTimeOfSupplied(parcelId);
                                                    break;
                                                }
                                        }
                                    }
                                    catch (ConsoleException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }

                                }
                                Console.WriteLine("Enter your next choice in update menu");
                                char.TryParse(Console.ReadLine(), out ch2);
                            }
                            break;                                  
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
                                    try
                                    {
                                        switch (ch3)
                                        {
                                            case 'a':
                                                {
                                                    Console.WriteLine("Enter station ID");
                                                    int.TryParse(Console.ReadLine(), out int stationId);
                                                    if (stationId <= 0)
                                                        throw new ConsoleException("Enter Id 1-999999");
                                                    DO.Station st = dalObject1.PrintStation(stationId);
                                                    Console.WriteLine(st);
                                                    break;
                                                }
                                            case 'b':
                                                {
                                                    Console.WriteLine("Enter drone ID");
                                                    int.TryParse(Console.ReadLine(), out int droneId);
                                                    if (droneId <= 0)
                                                        throw new ConsoleException("Enter Id 1-999999");
                                                    DO.Drone dr = dalObject1.PrintDrone(droneId);
                                                    Console.WriteLine(dr);
                                                    break;
                                                }
                                            case 'c':
                                                {
                                                    Console.WriteLine("Enter customer ID");
                                                    int.TryParse(Console.ReadLine(), out int customerId);
                                                    if (customerId <= 0)
                                                        throw new ConsoleException("Enter Id 1-999999");
                                                    DO.Customer cm = dalObject1.PrintCustomer(customerId);
                                                    Console.WriteLine(cm);
                                                    break;

                                                }
                                            case 'd':
                                                {
                                                    Console.WriteLine("Enter parcel ID");
                                                    int.TryParse(Console.ReadLine(), out int parcelId);
                                                    if (parcelId <= 0)
                                                        throw new ConsoleException("Enter Id 1-999999");
                                                    DO.Parcel pr = dalObject1.PrintParcel(parcelId);
                                                    Console.WriteLine(pr);
                                                    break;
                                                }
                                        }
                                    }
                                    catch (ConsoleException ex)
                                    {
                                        Console.WriteLine(ex.Message);
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
                                                List<Station> Stations = new (dalObject1.PrintStationList());
                                                foreach (Station item in Stations)
                                                {
                                                    Console.WriteLine();
                                                }
                                                break;
                                            }
                                        case 'b':
                                            {
                                                List<Drone> Drones = new (dalObject1.PrintDroneList());
                                                foreach (Drone item in Drones)
                                                {
                                                    Console.WriteLine();
                                                }
                                                break;
                                            }
                                        case 'c':
                                            {
                                                List<Customer> Customers = new (dalObject1.PrintCustomerList());
                                                foreach (Customer item in Customers)
                                                {
                                                    Console.WriteLine();
                                                }
                                                break;
                                            }
                                        case 'd':
                                            {
                                                List<Parcel> Parcels = new (dalObject1.PrintParcelList());
                                                foreach (Parcel item in Parcels)
                                                {
                                                    Console.WriteLine();
                                                }
                                                break;
                                            }
                                        case 'f':
                                            {
                                                List<Parcel> Parcels = new (dalObject1.PrintUnassignedParcels());
                                                foreach (Parcel item in Parcels)
                                                {
                                                    Console.WriteLine();
                                                }
                                                break;
                                            }
                                        case 'g':
                                            {
                                                List<Station> Stations = new (dalObject1.PrintStationList(w => w.ReadyChargeStands > 0));
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
            catch(ConsoleException c)
            {
                Console.WriteLine(c);
            }
            catch (DO.DalExceptions exc)
            {
                Console.WriteLine(exc);
            }
        }
    }
}