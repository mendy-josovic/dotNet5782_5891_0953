using System;
using IBL.BO;
using IBL;

namespace ConsoleUI_BL
{
    public partial class ConsoleUI_BL
    {
       static IBl BLObject = new BL.BL();
        public static void Display()
        {
            try
            {
      //creating an object of BL class for all the functions
                {
                    Console.WriteLine(@"
                            Enter 'a' to display a station
                            Enter 'b' to display a drone");
                    char.TryParse(Console.ReadLine(), out char ch);
                    switch(ch)
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
                                    switch (ch1)
                                    {
                                        case 'a':
                                            {
                                                Console.WriteLine("Enter station ID: ");
                                                Int32.TryParse(Console.ReadLine(), out int x);
                                                if (x <= 0)
                                                    throw "Invalid value\n";
                                                IDAL.DO.Station station = BLObject.DisplayStation(x);
                                                Station BLStation = BLObject.BLStation(station);
                                                foreach(DroneToList drone in BLObject.BLDrones())
                                                {
                                                    if (drone.ThisLocation == BLStation.location)
                                                        BLStation.ListOfDrones.Add(new(drone));
                                                }                                                                                             
                                                break;
                                            }
                                    }
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
                                    switch (ch2)
                                    {
                                        case 'a':
                                            {
                                                break;
                                            }
                                    }
                                }
                                break;
                            }
                    }
                }
            }
            catch ()
            {

            }
        }
    }
}
