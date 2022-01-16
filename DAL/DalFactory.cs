using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
namespace DO
{
   static public class DalFactory
    {
        /// <summary>
        /// the func 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public IDal GetDal(string str)
        {
            if (str == "Object")
                return DalObject.DalObject.Instance;
            if (str == "DalXml")
                return DalXml.DalXml.Instance;
            else
                throw new DO.DalExceptions("no reqwest");

        }
    }
}
