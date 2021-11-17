using System;
using IBL.BO;
using System.Collections.Generic;
using IBL;
using BL;
using System.Runtime.Serialization;
namespace ConsoleUI_BL
{
    public  partial  class ConsoleUI_BL
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"Welcome:
            Enter 'A' to add
            Enter 'B' to update
            Enter 'C' to Diplay
            Enter 'E' to exit
            ");
            IBl bL = new BL.BL();
            char.TryParse(Console.ReadLine(), out char ch);
            while (ch != 'E')
            {
                switch (ch)
                {
                    case'A':
                        ConsoleUI_BL.add();
                        break;
                    case 'B':
                        ConsoleUI_BL.Update();
                        break;
                    case 'C':
                        ConsoleUI_BL.Display();
                        break;                     
                }
            }

        }
    }
}
