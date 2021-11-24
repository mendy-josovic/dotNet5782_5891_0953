using System;
using System.Linq;
using System.Collections.Generic;
using IBL.BO;
using IBL;

namespace ConsoleUI_BL
{
    public partial class ConsoleUI_BL
    {
        public static void Display()
        {
            //creating an object of BL class for all the functions
            {
                Console.WriteLine(@"
                            Enter 'a' to display an element
                            Enter 'b' to display a list");
                char.TryParse(Console.ReadLine(), out char ch);
                while (ch != 'e')
                {
                    try
                    {
                        switch (ch)
                        {
                            case 'a':
                                {
                                    Console.WriteLine(@"
                                Enter 'a' to display a station
                                Enter 'b' to display a drone
                                Enter 'c' to display a customer
                                Enter 'd' to display a parcel 
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
                                                        Console.WriteLine("Enter station ID: ");
                                                        Int32.TryParse(Console.ReadLine(), out int x);
                                                        if (x <= 0)
                                                            throw new ConsoleBlException("Invalid value");
                                                        IDAL.DO.Station station = BLObject.DisplayStation(x);
                                                        Station BLStation = BLObject.BLStation(station);
                                                        foreach (DroneToList drone in BLObject.BLDrones())
                                                        {
                                                            if (drone.ThisLocation == BLStation.location)
                                                                BLStation.ListOfDrones.Add(BLObject.BLDroneInCharging(drone));
                                                        }
                                                        Console.WriteLine(BLStation);
                                                        break;
                                                    }
                                                case 'b':
                                                    {
                                                        Console.WriteLine("Enter drone ID: ");
                                                        Int32.TryParse(Console.ReadLine(), out int x);
                                                        if (x <= 0)
                                                            throw new ConsoleBlException("Invalid value");
                                                        DroneToList drone = BLObject.DisplayDrone(x);
                                                        Drone BLDrone = BLObject.BLDrone(drone);
                                                        Console.WriteLine(BLDrone);
                                                        break;
                                                    }
                                                case 'c':
                                                    {
                                                        Console.WriteLine("Enter customer ID: ");
                                                        Int32.TryParse(Console.ReadLine(), out int x);
                                                        if (x <= 0)
                                                            throw new ConsoleBlException("Invalid value");
                                                        IDAL.DO.Customer customer = BLObject.DisplayCustomer(x);
                                                        Customer BLCustomer = BLObject.BLCustomer(customer);
                                                        Console.WriteLine(BLCustomer);
                                                        break;
                                                    }
                                                case 'd':
                                                    {
                                                        Console.WriteLine("Enter parcel ID: ");
                                                        Int32.TryParse(Console.ReadLine(), out int x);
                                                        if (x <= 0)
                                                            throw new ConsoleBlException("Invalid value");
                                                        IDAL.DO.Parcel parcel = BLObject.DisplayParcel(x);
                                                        Parcel BLParcel = BLObject.BLParcel(parcel);
                                                        Console.WriteLine(BLParcel);
                                                        break;
                                                    }
                                                default:
                                                    {
                                                        Console.WriteLine("Wrong choice\n");
                                                        break;
                                                    }
                                            }
                                        }
                                        catch (ConsoleBlException ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        catch (IBL.BO.BlException ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                    Console.WriteLine("Enter your next choice in display element menu");
                                        char.TryParse(Console.ReadLine(), out ch1);
                                    }
                                    break;
                                }
                            case 'b':
                                {
                                    Console.WriteLine(@"
                                Enter 'a' to display a list of stations
                                Enter 'b' to display a list of drones
                                Enter 'c' to display a list of customers
                                Enter 'd' to display a list of parcels
                                Enter 'f' to display a list of parcels not yet associated with drones
                                Enter 'g' to display a list of station with ready stands to charge 
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
                                                        List<StationToList> stations = BLObject.DisplayStationList();
                                                        foreach (StationToList station in stations)
                                                        {
                                                            Console.WriteLine(station + "\n");
                                                        }
                                                        break;
                                                    }
                                                case 'b':
                                                    {
                                                        List<DroneToList> drones = BLObject.DisplayDroneList();
                                                        foreach (DroneToList drone in drones)
                                                        {
                                                            Console.WriteLine(drone + "\n");
                                                        }
                                                        break;
                                                    }
                                                case 'c':
                                                    {
                                                        List<CustomerToList> customers = BLObject.DisplayCustomerList();
                                                        foreach (CustomerToList customer in customers)
                                                        {
                                                            Console.WriteLine(customer + "\n");
                                                        }
                                                        break;
                                                    }
                                                case 'd':
                                                    {
                                                        List<ParcelToList> parcels = BLObject.DisplayParcelList();
                                                        foreach (ParcelToList parcel in parcels)
                                                        {
                                                            Console.WriteLine(parcel + "\n");
                                                        }
                                                        break;
                                                    }
                                                case 'f':
                                                    {
                                                        List<ParcelToList> parcels = BLObject.DisplayParcelList();
                                                        var notAssociated = parcels.FindAll(w => w.Status == STATUS_OF_PARCEL.CREATED);
                                                        foreach (ParcelToList parcel in notAssociated)
                                                        {
                                                            Console.WriteLine(parcel + "\n");
                                                        }
                                                        break;
                                                    }
                                                case 'g':
                                                    {
                                                        List<StationToList> stations = BLObject.DisplayStationList();
                                                        var stationsWithReadyStands = stations.FindAll(w => w.ReadyStandsInStation > 0);
                                                        foreach (StationToList station in stationsWithReadyStands)
                                                        {
                                                            Console.WriteLine(station + "\n");
                                                        }
                                                        break;
                                                    }                                               
                                                default:
                                                    {
                                                        Console.WriteLine("Wrong choice\n");
                                                        break;
                                                    }
                                            }
                                        }
                                        catch (ConsoleBlException ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        catch (IBL.BO.BlException ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        Console.WriteLine("Enter your next choice in display list menu");
                                        char.TryParse(Console.ReadLine(), out ch2);
                                    }
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Wrong choice\n");
                                    break;
                                }
                        }
                    }
                    catch (ConsoleBlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (IBL.BO.BlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine("Enter your next choice in display menu");
                    char.TryParse(Console.ReadLine(), out ch);
                }
            }
        }
    }
}
