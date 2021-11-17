using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
       public class Location
        {

            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public Location(double lon,double lat) { Longitude = lon; Latitude = lat; }
        }
    }
}
