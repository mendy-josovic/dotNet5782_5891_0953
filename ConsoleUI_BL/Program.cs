
using IBL;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace ConsoleUI_BL
{
    public partial class ConsoleUI_BL
    {
        IBl bl = new BL.BL();
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
                    }
                }
                catch (ConsoleBlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
