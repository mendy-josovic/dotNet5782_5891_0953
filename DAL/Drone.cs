using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        struct Drone
        {
            public int Id { get; set; }
            public String Model { set; get; }
            public STATUS Status { set; get; }
            public WEIGHT MaxWeight  {set; get;}  
            public double Battery { set; get; }
        }
    }
}
