using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            public int Id { get; set; }
            public String Name { get; set; }
            public String Phone { get; set; }
            public double Longitute { get; set; }
            public double Lattitute { get; set; }
            public override string ToString()//override the to string to print it nice
            {

                return "id: "+Id+"\n"+"Name: " +Name+"\n"+"Fhone: "+Phone+"\n"+ " Longitute: "+Longitute+"\n"+ " Latitute: "+Lattitute;
            }
        }
    }
}
