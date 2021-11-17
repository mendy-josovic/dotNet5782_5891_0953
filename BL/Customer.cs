using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public  class Customer
        {
            public int Id { get; set; }
            public String Name { get; set; }
            public String Phone { get; set; }
            public Location location;
            public ParcelAtCustomer FromCustomer { get; set; }
            public ParcelAtCustomer ToCustomer { get; set; }
        }
    }
}
