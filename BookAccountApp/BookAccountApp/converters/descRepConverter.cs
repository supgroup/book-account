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
    class descRepConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                //PayOp s = value as PayOp;
                PaymentsSts s = value as PaymentsSts;
                string name = "";
                string desc = "";
                //switch (s.side)
                //{

                //    //case "passenger": name = MainWindow.resourcemanager.GetString("thePassenger") + " " + s.passenger; break;
                //    //case "office": name = (MainWindow.resourcemanager.GetString("trnoffice") + " " + s.officeName); break;
                //    ////case "office": name = MainWindow.resourcemanager.GetString("trnoffice") + " " + s.officeName; break;
                //    //case "system": name = s.systemName; break;
                //    case "syr": name = MainWindow.resourcemanager.GetString("trnsyr"); break;
                //    case "soto": name = MainWindow.resourcemanager.GetString("trnsoto"); break;
                //    //case "other": name = MainWindow.resourcemanager.GetString("trnother"); break;
                //    default: break;
                //}
                if (s.opType=="p" && (s.side=="soto" || s.side == "syr"))
                {
                    desc =  "دفعة الى نظام الدفع";
                }
                else if (s.opType == "p" && s.side == "system" && s.processType=="book")
                {
                    desc =  " سحب من " + s.sideAr  + " مقابل الحجز في " + s.systemName;
                }
                else if (s.opType == "p" && s.side == "system" && s.processType == "company_commission")
                {
                    desc ="عمولة للشركة";
                }
                else if (s.opType == "d" && s.side == "system" && s.processType == "cash")
                {
                    desc = " قبض عمولة للشركة من نظام الحجز "  +s.systemName;
                }


                else if (s.opType == "p" && s.side == "office" && s.processType == "service")
                {
                    desc =   " دفعة مقابل الحجز عن طريق مكتب  "+ s.officeName;
                }
                else if (s.opType == "d" && s.side == "office" && s.processType == "cashservice")
                {
                    desc =   " قبض من مكتب " + s.officeName;
                }
                else if (s.opType == "d" && s.side == "office" && s.processType == "office_commission")
                {
                    desc =   " عمولة مكتب "+ s.officeName;
                }
                else if (s.opType == "p" && s.side == "office" && s.processType == "cash")
                {
                    desc = " دفعة مقابل عمولة لمكتب "+ s.officeName;
                }


                else if (s.opType == "p" && s.side == "passenger" && s.processType == "service")
                {
                    desc =   " دفعة مقابل الحجز للمسافر "+ s.passenger;
                }
                else if (s.opType == "d" && s.side == "passenger" && s.processType == "cashservice")
                {
                    desc =  " قبض من المسافر "+ s.passenger;
                }
                else if (s.opType == "d" && s.side == "passenger" && s.processType == "cash")
                {
                    desc =  " قبض من المسافر " + s.passenger;
                }
                else if (s.opType == "p" && s.side == "other"  )
                {
                    desc =  "دفعة";
                }
                else if (s.opType == "d" && s.side == "other")
                {
                    desc = "قبض";
                }

                return desc;
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
