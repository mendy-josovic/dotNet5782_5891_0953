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
           
            int i = DataSource.customers.FindIndex(w => w.Equals(cst));
            if (i > 0)
                throw new IDAL.DO.DalExceptions("Customer Alredy exsits");
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
       /// <summary>
       /// the func gets the id of the customer and a new name or phone \
       /// and it with a defult empty and we use the replace func and null or empty..
       /// </summary>
       /// <param name="CusId"></param>
       /// <param name="Name"></param>
       /// <param name="phone"></param>
        public void UpdateCustomer(int CusId, string Name ="", string phone ="")
        {
            int i = DataSource.customers.FindIndex(w => w.Id == CusId);
            if (i < 0)
                throw new IDAL.DO.DalExceptions("Customer Dosen't exsits");
            if (!string.IsNullOrEmpty(Name))
            {
                DataSource.customers[i].Name.Replace(DataSource.customers[i].Name, Name);
            }
            if(!string.IsNullOrEmpty(phone))
            {
                DataSource.customers[i].Phone.Replace(DataSource.customers[i].Phone, Name);
            }
        }
      


    }
}
