using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;
using BO;
using System.Text.RegularExpressions;
using System.Globalization;
using BO;

namespace PL
{
    class LocationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            double num = (double)value;

            return BO.FuncForToString.ConvertToSexagesimal(num);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string num = (string)value;
            double ret;
            if (double.TryParse(num, out ret))
                return ret;

            return 0;
        }
    }
}
