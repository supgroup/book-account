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
    public class currencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string curr = value.ToString();
                string s = "";
                switch (curr)
                {
                    case "usd":
                        s = "دولار امريكي";
                        break;
                    case "syp":
                        s = "ليرة سورية";
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
