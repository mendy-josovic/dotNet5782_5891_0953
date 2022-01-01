using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ConsoleUI_BL
{
    public partial class ConsoleUI_BL
    {
        static IBl BLObject = BlFactory.GetBl();
        static void Main(string[] args)
        {
            Console.WriteLine(@"Welcome:
            Enter 'A' to add
            Enter 'B' to update
            Enter 'C' to Diplay
            Enter 'E' to exit
            ");                           
            char.TryParse(Console.ReadLine(), out char ch);
            while (ch != 'E')
            {
                try
                {
                    switch (ch)
                    {
                        case 'A':
                            ConsoleUI_BL.add();
                            break;
                        case 'B':
                            ConsoleUI_BL.Update();
                            break;
                        case 'C':
                            ConsoleUI_BL.Display();
                            break;
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
                Console.WriteLine("Enter your next choice in main menu");
                char.TryParse(Console.ReadLine(), out ch);
            }

        }
    }
}
