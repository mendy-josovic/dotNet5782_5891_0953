using System;
using IBL.BO;
namespace ConsoleUI_BL
{
    public partial class ConsoleUI_BL
    {
        public static void Update()
        {
            Console.WriteLine(@"Welcome:
            Enter 'A' to Update drone 
            Enter 'B' to Update station
            Enter 'C' to Update customer
            Enter 'D' to Send Drone To Charge
            Enter 'F' to Return Drone From Carge
            Enter 'G' to Usighe Drone to parcel
            Enter 'H' to Update pickUp
            Enter 'I' to Update Dilvery
            ");
        }
    }
}
