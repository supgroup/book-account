using BookAccountApp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BookAccountApp.converters
{
    public class flightTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                int intType = int.Parse(value.ToString());
                string s = "";
                switch (intType)
                {
                    case 1:
                        s = MainWindow.resourcemanager.GetString("singleTrip");
                        break;
                    case 2:
                        s = MainWindow.resourcemanager.GetString("roundTrip");
                        break;
                   
                    default:
                        s = "";
                        break;
                }

                return s;
            }
            else return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                throw new NotImplementedException();

            }
            catch
            {
                return value;
            }
        }

      
    }
}
