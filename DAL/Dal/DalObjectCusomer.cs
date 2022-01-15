using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using DO;
using DalApi;
namespace DalObject
{
   internal partial class DalObject : IDal
    {
        public void AddCustomer(Customer cst)  //same
        {
           
            int i = DataSource.customers.FindIndex(w => w.Equals(cst));
            if (i > 0)
                throw new DO.DalExceptions("Customer Alredy exsits");
            DataSource.customers.Add(cst);
        }
        public Customer PrintCustomer(int id)  //finds the customer and sends a replica
        {
            return DataSource.customers.Find(w => w.Id == id);
        }

        public IEnumerable<Customer> PrintCustomerList(Predicate<Customer> predicate = null)  //returns a new list of customers
        {
            return DataSource.customers.FindAll(x => predicate == null ? true : predicate(x));
        }
 
        public void UpdateCustomer(int CusId, string Name ="", string phone ="")
        {
            int i = DataSource.customers.FindIndex(w => w.Id == CusId);
            if (i < 0)
                throw new DO.DalExceptions("Customer Dosen't exsits");
          Customer Tempcustomer = DataSource.customers[i];
            if (!string.IsNullOrEmpty(Name))
            {
                Tempcustomer.Name = Name;
            }
            if(!string.IsNullOrEmpty(phone))
            {
                Tempcustomer.Phone = phone;
            }
            DataSource.customers[i] = Tempcustomer;
        }
      


    }
}
