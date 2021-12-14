using System;
using System.Collections.Generic;
using System.Text;


    namespace BO
    {
       public class Location
        {
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public override string ToString()//override the to-string to print it nice
            {
                return "Longitude: " + Longitude + "\nLatitude: " + Latitude + "\n";
            }
        }
    }

