using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using IDAL.DO;
using IDAL;
namespace DalObject
{
    public partial class DalObject : IDal
    {
        public void AddCustomer(Customer cst)  //same
        {
            DataSource.customers.Add(cst);
        }
        public Customer PrintCustomer(int id)  //finds the customer and sends a replica
        {
            return (DataSource.customers.Find(w => w.Id == id));
        }

        public IEnumerable<Customer> PrintCustomerList()  //returns a new list of customers
        {
            return DataSource.customers;
        }


    }
}
