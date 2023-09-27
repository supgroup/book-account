using BookAccountApp.Classes;
using BookAccountApp;
using BookAccountApp.ApiClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BookAccountApp.converters
{
    class sideNameRepConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                //PayOp s = value as PayOp;
                PaymentsSts s = value as PaymentsSts;
                string name = "";
                switch (s.side)
                {
                   
                    case "passenger": name = MainWindow.resourcemanager.GetString("thePassenger")+" "+s.passenger; break;
                    case "office": name = (s.officeId == null ? FillCombo.companyName : (MainWindow.resourcemanager.GetString("trnoffice") + " " + s.officeName)); break;
                    //case "office": name = MainWindow.resourcemanager.GetString("trnoffice") + " " + s.officeName; break;
                    case "system": name = s.systemName; break;
                    case "syr": name = MainWindow.resourcemanager.GetString("trnsyr"); break;
                    case "soto": name = MainWindow.resourcemanager.GetString("trnsoto"); break;
                    case "other": name = MainWindow.resourcemanager.GetString("trnother"); break;
                    default: break;
                }
                return name;
            }
            catch
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    

}
