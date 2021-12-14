using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace DO
    {
        public struct Customer
        {
            public int Id { get; set; }
            public String Name { get; set; }
            public String Phone { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public override string ToString()//override the to string to print it nice
            {

                return "ID: " + Id + "\nName: " + Name + "\nFhone: " + Phone + "\nLongitude: " + Longitude + "\nLatitude: " + Latitude + "\n";
            }
        }
    }
