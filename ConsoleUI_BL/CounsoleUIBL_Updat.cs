using System;
using IBL.BO;
namespace ConsoleUI_BL
{
    public partial class ConsoleUI_BL
    {

        public static void Update()
        {
            Console.WriteLine(@"
            Enter 'A' to Update drone 
            Enter 'B' to Update station
            Enter 'C' to Update customer
            Enter 'D' to Send Drone To Charge
            Enter 'F' to Return Drone From Carge
            Enter 'G' to Usighe Drone to parcel
            Enter 'H' to Update pickUp
            Enter 'I' to Update Dilvery
            Enter 'E' to Exit
            ");
            char.TryParse(Console.ReadLine(), out char ch1);
            while (ch1 != 'E')
            {
                try
                {
                    switch (ch1)
                    {
                        case 'A':
                            {
                                Console.WriteLine("Enter Drone Id: ");
                                Int32.TryParse(Console.ReadLine(), out int x);
                                if (x < 0)
                                    throw new ConsoleBlException("Invalid Imput");
                                Console.WriteLine("Enter A new Model Name: ");
                                string str = Console.ReadLine();
                                BLObject.UpdatDroneName(x, str);
                                break;
                            }
                        case 'B':
                            {
                                Console.WriteLine("Enter Station Id: ");
                                Int32.TryParse(Console.ReadLine(), out int x);
                                if (x < 0)
                                    throw new ConsoleBlException("Invalid Imput");
                                Console.WriteLine("Enter A new Station Name And Charging Slots Copacitity (Choos one or two)");
                                string str = Console.ReadLine();
                                Int32.TryParse(Console.ReadLine(), out int j);
                                if (j < 0)
                                    throw new ConsoleBlException("Invalid Imput");
                                BLObject.UpdateStstion(x, str, j);
                                break;
                            }
                        case 'C':
                            {
                                Console.WriteLine("Enter Customer Id: ");
                                Int32.TryParse(Console.ReadLine(), out int x);
                                if (x < 0)
                                    throw new ConsoleBlException("Invalid Imput");
                                Console.WriteLine("Enter A new Customer Name And Phone (Choos one or two)");
                                string str = Console.ReadLine();
                                string j = Console.ReadLine();
                                BLObject.UpdateCosomerInfo(x, str, j);
                                break;
                            }
                        case 'D':
                            {
                                Console.WriteLine("Enter Drone Id: ");
                                Int32.TryParse(Console.ReadLine(), out int x);
                                if (x < 0)
                                    throw new ConsoleBlException("Invalid Imput");
                                BLObject.SendDroneToCarge(x);
                                break;
                            }
                        case 'E':
                            {
                                Console.WriteLine("Enter Drone Id: ");
                                Int32.TryParse(Console.ReadLine(), out int x);
                                if (x < 0)
                                    throw new ConsoleBlException("Invalid Imput");
                                Console.WriteLine("Enter Time in Charging: ");
                                Int32.TryParse(Console.ReadLine(), out int j);
                                if (j < 0)
                                    throw new ConsoleBlException("Invalid Imput");
                                BLObject.ReturnDroneFromeCharging(x, j);
                                break;
                            }
                        case 'F':
                            {
                                Console.WriteLine("Enter Drone Id: ");
                                Int32.TryParse(Console.ReadLine(), out int x);
                                if (x < 0)
                                    throw new ConsoleBlException("Invalid Imput");
                                BLObject.AssignDronToParcel(x);
                                break;
                            }
                        case 'H':
                            {
                                Console.WriteLine("Enter Drone Id: ");
                                Int32.TryParse(Console.ReadLine(), out int x);
                                if (x < 0)
                                    throw new ConsoleBlException("Invalid Imput");
                                BLObject.PickUp(x);
                                break;
                            }
                        case 'I':
                            {
                                Console.WriteLine("Enter Drone Id: ");
                                Int32.TryParse(Console.ReadLine(), out int x);
                                if (x < 0)
                                    throw new ConsoleBlException("Invalid Imput");
                                BLObject.Suuply(x);
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Wrong choice\n");
                                break;
                            }
                    }
                }
                catch (IBL.BO.BlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ConsoleBlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine("Enter your next choice in update menu");
                char.TryParse(Console.ReadLine(), out ch1);
            }
        }
    }
}
