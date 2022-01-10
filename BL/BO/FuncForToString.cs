using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class FuncForToString
    {
        public static string ConvertToSexagesimal(double? point)
        {
            if (point == null)
                throw new NullReferenceException("point is null");

            int Degrees = (int)point;
            point -= Degrees;
            point *= 60;
            int Minutes = (int)point;
            point -= Minutes;
            point *= 60;
            double? Second = point;
            return $"{Degrees}° {Minutes}' {string.Format("{0:0.###}", Second)}\" ";
        }
    }
}
