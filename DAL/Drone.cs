using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace DO
    {
       public struct Drone
        {
            public int Id { get; set; }
            public String Model { set; get; }
            public WEIGHT MaxWeight  {set; get;}  
            public override string ToString()
            {
                return "ID: " + Id + "\nModel: " + Model  + "\nMaximum weight: " + MaxWeight + "\n";
            }
        }
    }
