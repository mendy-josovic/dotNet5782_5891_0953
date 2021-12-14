using System;
using System.Collections.Generic;
using System.Text;

    namespace BO
    {
        public class CustomerInParcel
        {
            public int Id { get; set; }
            public String Name { get; set; }
            public override string ToString()//override the to-string to print it nice
            {
                return "Customer ID: " + Id + "\nName: " + Name + "\n";
            }
        }
    }
