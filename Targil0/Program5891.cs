using System;
namespace Targil0
{
  partial  class Program
    {
        static void Main(string[] args)
        {
            Welcome5891();
            Welcome0953();
            Console.ReadKey();
        }
        static partial void Welcome0953();
        private static void Welcome5891()
        {
            Console.WriteLine("Enter your name");
            string mystr = Console.ReadLine();
            Console.WriteLine("{0}, Welcome to my first console application", mystr);
        }
    }
}
