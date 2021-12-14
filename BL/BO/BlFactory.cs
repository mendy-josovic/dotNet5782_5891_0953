using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BO
{
    public static class BlFactory
    {
        public static IBl GetBl()
        {
            return BL.BL.Instance;
        }
    }
}
