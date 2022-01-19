using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using DO;
using DalApi;
using System.Runtime.CompilerServices;
namespace DalObject
{
   internal partial class DalObject : IDal
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer cst)  //same
        {
           
            int i = DataSource.customers.FindIndex(w => w.Equals(cst));
            if (i > 0)
                throw new DO.DalExceptions("Customer Already exists");
            if (cst.Id <= 0)
                throw new DO.DalExceptions("Invalid ID, ID must be positive");
            DataSource.customers.Add(cst);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer PrintCustomer(int id)  //finds the customer and sends a replica
        {
            return DataSource.customers.Find(w => w.Id == id);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> PrintCustomerList(Predicate<Customer> predicate = null)  //returns a new list of customers
        {
            return DataSource.customers.FindAll(x => predicate == null ? true : predicate(x));
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
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
