using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookAccountApp.ApiClasses;
using Newtonsoft.Json;
//using POS.View.storage;
namespace BookAccountApp.Classes
{
    class clsReports
    {
        public static void setReportLanguage(List<ReportParameter> paramarr)
        {

            paramarr.Add(new ReportParameter("lang", MainWindow.Reportlang));

        }

        public static void Header(List<ReportParameter> paramarr)
        {

            ReportCls rep = new ReportCls();
            paramarr.Add(new ReportParameter("companyName", FillCombo.companyName));
            paramarr.Add(new ReportParameter("Fax", FillCombo.Fax));
            paramarr.Add(new ReportParameter("Tel", FillCombo.Mobile));
            paramarr.Add(new ReportParameter("Address", FillCombo.Address));
            paramarr.Add(new ReportParameter("Email", FillCombo.Email));
            paramarr.Add(new ReportParameter("logoImage", "file:\\" + rep.GetLogoImagePath()));
            paramarr.Add(new ReportParameter("trcomAddress", MainWindow.resourcemanagerreport.GetString("trAddress")));
            paramarr.Add(new ReportParameter("trcomTel", MainWindow.resourcemanagerreport.GetString("tel")));
            paramarr.Add(new ReportParameter("trcomFax", MainWindow.resourcemanagerreport.GetString("fax")));
            paramarr.Add(new ReportParameter("trcomEmail", MainWindow.resourcemanagerreport.GetString("email")));


        }
        public static void HeaderNoLogo(List<ReportParameter> paramarr)
        {

            ReportCls rep = new ReportCls();
            paramarr.Add(new ReportParameter("companyName", FillCombo.companyName));
            paramarr.Add(new ReportParameter("Fax", FillCombo.Fax));
            paramarr.Add(new ReportParameter("Tel", FillCombo.Mobile));
            paramarr.Add(new ReportParameter("Address", FillCombo.Address));
            paramarr.Add(new ReportParameter("Email", FillCombo.Email));


        }
        //public static void bankdg(List<ReportParameter> paramarr)
        //{

        //    ReportCls rep = new ReportCls();
        //    paramarr.Add(new ReportParameter("trTransferNumber", MainWindow.resourcemanagerreport.GetString("trTransferNumberTooltip")));


        //}
        //public static void serialsReport(IEnumerable<PosSerials> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        //{
        //    rep.ReportPath = reppath;
        //    rep.EnableExternalImages = true;
        //    rep.DataSources.Clear();

        //  //  paramarr.Add(new ReportParameter("trVendor", MainWindow.resourcemanagerreport.GetString("trVendor")));
        //    rep.DataSources.Add(new ReportDataSource("DataSetSerials", Query));

      

        //}
        //public static void serialsMailReport(IEnumerable<PosSerials> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        //{
        //    rep.ReportPath = reppath;
        //    rep.EnableExternalImages = true;
        //    rep.DataSources.Clear();

        //    //  paramarr.Add(new ReportParameter("trVendor", MainWindow.resourcemanagerreport.GetString("trVendor")));
        //    rep.DataSources.Add(new ReportDataSource("DataSetSerials", Query));



        //}
        public static void BookReport(IEnumerable<BookSts> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();
            rep.DataSources.Add(new ReportDataSource("DataSetBook", Query));
            //title
            paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("trBookings")));
            //table columns
            paramarr.Add(new ReportParameter("trNO", MainWindow.resourcemanagerreport.GetString("trNO")));
            paramarr.Add(new ReportParameter("trBookDate", MainWindow.resourcemanagerreport.GetString("trBookDate")));
            paramarr.Add(new ReportParameter("trUpgradeDate", MainWindow.resourcemanagerreport.GetString("trUpgradeDate")));
            paramarr.Add(new ReportParameter("trPackage", MainWindow.resourcemanagerreport.GetString("trPackage")));
            paramarr.Add(new ReportParameter("trAgent", MainWindow.resourcemanagerreport.GetString("trAgent")));
            paramarr.Add(new ReportParameter("trCustomer", MainWindow.resourcemanagerreport.GetString("trCustomer")));
            paramarr.Add(new ReportParameter("trPrice", MainWindow.resourcemanagerreport.GetString("trPrice")));

            DateFormConv(paramarr);
        }
        public static void agentReport(IEnumerable<Users> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();
            rep.DataSources.Add(new ReportDataSource("DataSet", Query));
            //title
            paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("trAgents")));
            //table columns
            paramarr.Add(new ReportParameter("trCode", MainWindow.resourcemanagerreport.GetString("trCode")));
            paramarr.Add(new ReportParameter("trName", MainWindow.resourcemanagerreport.GetString("trName")));
            paramarr.Add(new ReportParameter("trMobile", MainWindow.resourcemanagerreport.GetString("trMobile")));
          

            DateFormConv(paramarr);
        }
        public static void usersReport(IEnumerable<Users> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();
            rep.DataSources.Add(new ReportDataSource("DataSet", Query));
            //title
            paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("trAgents")));
            //table columns
            paramarr.Add(new ReportParameter("trCode", MainWindow.resourcemanagerreport.GetString("trCode")));
            paramarr.Add(new ReportParameter("trName", MainWindow.resourcemanagerreport.GetString("trName")));
            paramarr.Add(new ReportParameter("trMobile", MainWindow.resourcemanagerreport.GetString("trMobile")));


            DateFormConv(paramarr);
        }
        public static void PassengersReport(IEnumerable<Passengers> QueryList, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();
            List<Passengers> Query = JsonConvert.DeserializeObject<List<Passengers>>(JsonConvert.SerializeObject(QueryList));
            rep.DataSources.Add(new ReportDataSource("DataSet", Query));
            //title
            paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("passenger")));
            //table columns
            paramarr.Add(new ReportParameter("trNo", MainWindow.resourcemanagerreport.GetString("trNo.")));
            paramarr.Add(new ReportParameter("trName", MainWindow.resourcemanagerreport.GetString("trName")));
            paramarr.Add(new ReportParameter("trLastName", MainWindow.resourcemanagerreport.GetString("trLastName")));
            paramarr.Add(new ReportParameter("father", MainWindow.resourcemanagerreport.GetString("father")));
            paramarr.Add(new ReportParameter("mother", MainWindow.resourcemanagerreport.GetString("mother")));
            

            //DateFormConv(paramarr);
        }

        public static void OfficesReport(IEnumerable<Office> QueryList, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();
            List<Office> Query = JsonConvert.DeserializeObject<List<Office>>(JsonConvert.SerializeObject(QueryList));
            foreach (Office row in Query)
            {
                row.strjoinDate = dateFrameConverter(row.joinDate);
                
            }

            rep.DataSources.Add(new ReportDataSource("DataSet", Query));
            //title
            paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("trOffices")));
            //table columns
            paramarr.Add(new ReportParameter("trNo", MainWindow.resourcemanagerreport.GetString("trNo.")));
            paramarr.Add(new ReportParameter("officeName", MainWindow.resourcemanagerreport.GetString("officeName")));
            paramarr.Add(new ReportParameter("joinDate", MainWindow.resourcemanagerreport.GetString("joinDate")));
            paramarr.Add(new ReportParameter("mobileNum", MainWindow.resourcemanagerreport.GetString("mobileNum")));
            paramarr.Add(new ReportParameter("trUserName", MainWindow.resourcemanagerreport.GetString("trUserName")));
            paramarr.Add(new ReportParameter("passwordSyr", MainWindow.resourcemanagerreport.GetString("passwordSyr")));
            paramarr.Add(new ReportParameter("passwordSoto", MainWindow.resourcemanagerreport.GetString("passwordSoto")));

            /*
             * joinDate
mobileNum
trUserName
passwordSyr
passwordSoto

             * */
            //DateFormConv(paramarr);
        }


        public static string dateFrameConverter(DateTime? date)
        {
            DateTime dateval;
            if (date is DateTime)
                dateval = (DateTime)date;
            else return date.ToString();

            switch (MainWindow.dateFormat)
            {
                case "ShortDatePattern":
                    return dateval.ToString(@"dd/MM/yyyy");
                case "LongDatePattern":
                    return dateval.ToString(@"dddd, MMMM d, yyyy");
                case "MonthDayPattern":
                    return dateval.ToString(@"MMMM dd");
                case "YearMonthPattern":
                    return dateval.ToString(@"MMMM yyyy");
                default:
                    return dateval.ToString(@"dd/MM/yyyy");
            }

        }

        public static void FlightReport(IEnumerable<Flights> QueryList, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();
            List<Flights> Query = JsonConvert.DeserializeObject<List<Flights>>(JsonConvert.SerializeObject(QueryList));
            //foreach (Office row in Query)
            //{
            //    row.strjoinDate = dateFrameConverter(row.joinDate);

            //}

            rep.DataSources.Add(new ReportDataSource("DataSet", Query));
            //title
            paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("flights")));
            //table columns
            paramarr.Add(new ReportParameter("trNo", MainWindow.resourcemanagerreport.GetString("trNo.")));
            paramarr.Add(new ReportParameter("airline", MainWindow.resourcemanagerreport.GetString("airline")));
            paramarr.Add(new ReportParameter("flight", MainWindow.resourcemanagerreport.GetString("flight")));
            paramarr.Add(new ReportParameter("from", MainWindow.resourcemanagerreport.GetString("from")));
            paramarr.Add(new ReportParameter("to", MainWindow.resourcemanagerreport.GetString("to")));
      
         
        }

        public static void OperationReport(IEnumerable<Operations> QueryList, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();
            List<Operations> Query = JsonConvert.DeserializeObject<List<Operations>>(JsonConvert.SerializeObject(QueryList));
            //foreach (Office row in Query)
            //{
            //    row.strjoinDate = dateFrameConverter(row.joinDate);

            //}

            rep.DataSources.Add(new ReportDataSource("DataSet", Query));
            //title
            paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("operations")));
            //table columns
            paramarr.Add(new ReportParameter("trNo", MainWindow.resourcemanagerreport.GetString("trNo.")));
            paramarr.Add(new ReportParameter("operationName", MainWindow.resourcemanagerreport.GetString("operationName")));
            paramarr.Add(new ReportParameter("opStatement", MainWindow.resourcemanagerreport.GetString("opStatement")));
            paramarr.Add(new ReportParameter("duration", MainWindow.resourcemanagerreport.GetString("duration")));
    


        }

        public static void SystemReport(IEnumerable<Systems> QueryList, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();
            List<Systems> Query = JsonConvert.DeserializeObject<List<Systems>>(JsonConvert.SerializeObject(QueryList));
            foreach (Systems row in Query)
            {
                row.company_commission = decimal.Parse(HelpClass.DecTostring(row.company_commission));

            }

            rep.DataSources.Add(new ReportDataSource("DataSet", Query));
            //title
            paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("bookSystems")));
            //table columns
            paramarr.Add(new ReportParameter("trNo", MainWindow.resourcemanagerreport.GetString("trNo.")));
            paramarr.Add(new ReportParameter("name", MainWindow.resourcemanagerreport.GetString("bookSystem")));
            paramarr.Add(new ReportParameter("trcompany_commission", MainWindow.resourcemanagerreport.GetString("companyCommission")));
            paramarr.Add(new ReportParameter("trNotes", MainWindow.resourcemanagerreport.GetString("trNote")));



        }

        public static void SaleReport(IEnumerable<ServiceData> QueryList, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();
            List<ServiceData> Query = JsonConvert.DeserializeObject<List<ServiceData>>(JsonConvert.SerializeObject(QueryList));
            foreach (ServiceData row in Query)
            {
                row.strServiceDate = dateFrameConverter(row.serviceDate);
                row.total =decimal.Parse( HelpClass .DecTostring(row.total));
            }

            rep.DataSources.Add(new ReportDataSource("DataSet", Query));
            //title
         
            //table columns
            paramarr.Add(new ReportParameter("trNo", MainWindow.resourcemanagerreport.GetString("trNo.")));
            paramarr.Add(new ReportParameter("passengerName", MainWindow.resourcemanagerreport.GetString("passengerName")));
            paramarr.Add(new ReportParameter("ticketNum", MainWindow.resourcemanagerreport.GetString("ticketNum")));
            paramarr.Add(new ReportParameter("airlineFlight", MainWindow.resourcemanagerreport.GetString("airlineFlight")));
            paramarr.Add(new ReportParameter("officeName", MainWindow.resourcemanagerreport.GetString("officeName")));
            paramarr.Add(new ReportParameter("trDate", MainWindow.resourcemanagerreport.GetString("trDate")));
            paramarr.Add(new ReportParameter("total", MainWindow.resourcemanagerreport.GetString("total")));
        
        }

        public static void paymentAccReport(IEnumerable<PayOp> query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            List<PayOp> cash = JsonConvert.DeserializeObject<List<PayOp>>(JsonConvert.SerializeObject(query));

            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();
            
            ReportCls repcls = new ReportCls();
            foreach (var c in cash)
            {
           
               c.cash = decimal.Parse(HelpClass.DecTostring(c.cash));
                c.sideAr = repcls.sideNameConverter(c);
                c.currency = repcls.currencyConverter(c.currency);
                c.stropDate = dateFrameConverter(c.opDate);
               
            }

            paramarr.Add(new ReportParameter("trNo", MainWindow.resourcemanagerreport.GetString("trNo.")));
            paramarr.Add(new ReportParameter("sideOrResponseble", MainWindow.resourcemanagerreport.GetString("sideOrResponseble")));

            paramarr.Add(new ReportParameter("trRecepient", MainWindow.resourcemanagerreport.GetString("trRecepient")));
            paramarr.Add(new ReportParameter("recivedFrom", MainWindow.resourcemanagerreport.GetString("recivedFrom")));
            paramarr.Add(new ReportParameter("trCashTooltip", MainWindow.resourcemanagerreport.GetString("trCashTooltip")));
            paramarr.Add(new ReportParameter("currency", MainWindow.resourcemanagerreport.GetString("currency")));
            paramarr.Add(new ReportParameter("payDate", MainWindow.resourcemanagerreport.GetString("payDate")));
            

           // DateFormConv(paramarr);


         
            rep.DataSources.Add(new ReportDataSource("DataSet", cash));
        }

        public static void BookStsReport(IEnumerable<BookSts> QueryList, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();
            List<BookSts> Query = JsonConvert.DeserializeObject<List<BookSts>>(JsonConvert.SerializeObject(QueryList));
            foreach (BookSts row in Query)
            {
                row.strServiceDate = dateFrameConverter(row.createDate);
                row.priceBeforTax = decimal.Parse(HelpClass.DecTostring(row.priceBeforTax));
                row.profit = decimal.Parse(HelpClass.DecTostring(row.profit));
                row.strCreateDate = dateFrameConverter(row.createDate);
            }

            rep.DataSources.Add(new ReportDataSource("DataSet", Query));
            //title

            //table columns
            paramarr.Add(new ReportParameter("trNo", MainWindow.resourcemanagerreport.GetString("trNo.")));
            paramarr.Add(new ReportParameter("bookSystem", MainWindow.resourcemanagerreport.GetString("bookSystem")));
            paramarr.Add(new ReportParameter("airlineFlightSys", MainWindow.resourcemanagerreport.GetString("airlineFlightSys")));
            paramarr.Add(new ReportParameter("trOffice", MainWindow.resourcemanagerreport.GetString("trOffice")));
            paramarr.Add(new ReportParameter("priceBeforTax", MainWindow.resourcemanagerreport.GetString("priceBeforTax")));
            paramarr.Add(new ReportParameter("profits", MainWindow.resourcemanagerreport.GetString("profits")));
            paramarr.Add(new ReportParameter("trDate", MainWindow.resourcemanagerreport.GetString("trDate")));

         

        }

        //public static void CustomersReport(IEnumerable<Customers> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        //{
        //    rep.ReportPath = reppath;
        //    rep.EnableExternalImages = true;
        //    rep.DataSources.Clear();
        //    rep.DataSources.Add(new ReportDataSource("DataSet", Query));
        //    //title
        //    paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("trAgents")));
        //    //table columns
        //    paramarr.Add(new ReportParameter("trCode", MainWindow.resourcemanagerreport.GetString("trCode")));
        //    paramarr.Add(new ReportParameter("trName", MainWindow.resourcemanagerreport.GetString("trName")));
        //    paramarr.Add(new ReportParameter("trCompanyName", MainWindow.resourcemanagerreport.GetString("trName")));
        //    paramarr.Add(new ReportParameter("trMobile", MainWindow.resourcemanagerreport.GetString("trMobile")));


        //    DateFormConv(paramarr);
        //}

        //public static void packagesReport(IEnumerable<Packages> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        //{
        //    rep.ReportPath = reppath;
        //    rep.EnableExternalImages = true;
        //    rep.DataSources.Clear();
        //    rep.DataSources.Add(new ReportDataSource("DataSet", Query));
        //    //title
        //    paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("trPackages")));
        //    //table columns
        //    paramarr.Add(new ReportParameter("trCode", MainWindow.resourcemanagerreport.GetString("trCode")));
        //    paramarr.Add(new ReportParameter("trPackage", MainWindow.resourcemanagerreport.GetString("trPackage")));
        //    paramarr.Add(new ReportParameter("trProgram", MainWindow.resourcemanagerreport.GetString("trProgram")));
        //    paramarr.Add(new ReportParameter("trVersion", MainWindow.resourcemanagerreport.GetString("trVersion")));


        //    DateFormConv(paramarr);
        //}
        //public static void  versionsReport(IEnumerable<Versions> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        //{
        //    rep.ReportPath = reppath;
        //    rep.EnableExternalImages = true;
        //    rep.DataSources.Clear();
        //    rep.DataSources.Add(new ReportDataSource("DataSet", Query));
        //    //title
        //    paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("trVersions")));
        //    //table columns
        //    paramarr.Add(new ReportParameter("trCode", MainWindow.resourcemanagerreport.GetString("trCode")));
        //    paramarr.Add(new ReportParameter("trName", MainWindow.resourcemanagerreport.GetString("trName")));
        //    paramarr.Add(new ReportParameter("trProgram", MainWindow.resourcemanagerreport.GetString("trProgram")));

        //    DateFormConv(paramarr);
        //}
        //public static void programsReport(IEnumerable<Programs> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        //{
        //    rep.ReportPath = reppath;
        //    rep.EnableExternalImages = true;
        //    rep.DataSources.Clear();
        //    rep.DataSources.Add(new ReportDataSource("DataSet", Query));
        //    //title
        //    paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("trPrograms")));
        //    //table columns
        //    paramarr.Add(new ReportParameter("trCode", MainWindow.resourcemanagerreport.GetString("trCode")));
        //    paramarr.Add(new ReportParameter("trName", MainWindow.resourcemanagerreport.GetString("trName")));
        //    paramarr.Add(new ReportParameter("trProgram", MainWindow.resourcemanagerreport.GetString("trProgram")));

        //    DateFormConv(paramarr);
        //}
        public static void PaymentsReport(IEnumerable<PaymentsSts> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();
            rep.DataSources.Add(new ReportDataSource("DataSetPayments", Query));
            //title
            paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("trPayments")));
            //table columns
            paramarr.Add(new ReportParameter("trProcess", MainWindow.resourcemanagerreport.GetString("trProcess")));
            paramarr.Add(new ReportParameter("tr_Book", MainWindow.resourcemanagerreport.GetString("tr_Book")));
            paramarr.Add(new ReportParameter("trPackage", MainWindow.resourcemanagerreport.GetString("trPackage")));
            paramarr.Add(new ReportParameter("trProcessDate", MainWindow.resourcemanagerreport.GetString("trProcessDate")));
            paramarr.Add(new ReportParameter("trExpireDate", MainWindow.resourcemanagerreport.GetString("trExpireDate")));
            paramarr.Add(new ReportParameter("trAgent", MainWindow.resourcemanagerreport.GetString("trAgent")));
            paramarr.Add(new ReportParameter("trCustomer", MainWindow.resourcemanagerreport.GetString("trCustomer")));
            paramarr.Add(new ReportParameter("trPrice", MainWindow.resourcemanagerreport.GetString("trPrice")));
            paramarr.Add(new ReportParameter("trPayments", MainWindow.resourcemanagerreport.GetString("trPayments")));

            DateFormConv(paramarr);
        }
        public static void PaymentssysTrans(IEnumerable<PaymentsSts> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();
            rep.DataSources.Add(new ReportDataSource("DataSetPayments", Query));
            //title
            paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("trPayments")));
            //table columns
            paramarr.Add(new ReportParameter("trProcess", MainWindow.resourcemanagerreport.GetString("trProcess")));
            paramarr.Add(new ReportParameter("tr_Book", MainWindow.resourcemanagerreport.GetString("tr_Book")));
            paramarr.Add(new ReportParameter("trPackage", MainWindow.resourcemanagerreport.GetString("trPackage")));
            paramarr.Add(new ReportParameter("trProcessDate", MainWindow.resourcemanagerreport.GetString("trProcessDate")));
            paramarr.Add(new ReportParameter("trExpireDate", MainWindow.resourcemanagerreport.GetString("trExpireDate")));
            paramarr.Add(new ReportParameter("trAgent", MainWindow.resourcemanagerreport.GetString("trAgent")));
            paramarr.Add(new ReportParameter("trCustomer", MainWindow.resourcemanagerreport.GetString("trCustomer")));
            paramarr.Add(new ReportParameter("trPrice", MainWindow.resourcemanagerreport.GetString("trPrice")));
            paramarr.Add(new ReportParameter("trPayments", MainWindow.resourcemanagerreport.GetString("trPayments")));

            DateFormConv(paramarr);
        }

        public static void PaymentsSale(IEnumerable<PayOp> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();
            rep.DataSources.Add(new ReportDataSource("DataSetPayments", Query));
            paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("trPayments")));

            PaymentsSaleParam(paramarr);
            DateFormConv(paramarr);
        }
        public static void PaymentsSaleParam(  List<ReportParameter> paramarr)
        {
             
          
            //table columns
            paramarr.Add(new ReportParameter("trProcessNumTooltip", MainWindow.resourcemanagerreport.GetString("trProcessNumTooltip")));
            paramarr.Add(new ReportParameter("trBookNum", MainWindow.resourcemanagerreport.GetString("trBookNum")));
            paramarr.Add(new ReportParameter("trPackage", MainWindow.resourcemanagerreport.GetString("trPackage")));
            paramarr.Add(new ReportParameter("trProcessDate", MainWindow.resourcemanagerreport.GetString("trProcessDate")));
            paramarr.Add(new ReportParameter("trExpirationDate", MainWindow.resourcemanagerreport.GetString("trExpirationDate")));
            paramarr.Add(new ReportParameter("trPaid", MainWindow.resourcemanagerreport.GetString("trPaid")));

         
        }
        public static void PaymentsPaySale(IEnumerable<PayOp> Query, LocalReport rep, string reppath)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();
            rep.DataSources.Add(new ReportDataSource("DataSetPayments", Query));


        }
        //public static void BookSale(IEnumerable<PackageUser> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        //{
        //    rep.ReportPath = reppath;
        //    rep.EnableExternalImages = true;
        //    rep.DataSources.Clear();
        //    rep.DataSources.Add(new ReportDataSource("DataSetBook", Query));
        //    //title
        //    paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("trServiceBook")));
        //    //table columns
        //    //paramarr.Add(new ReportParameter("trProcessNumTooltip", MainWindow.resourcemanagerreport.GetString("trProcessNumTooltip")));
        //    //paramarr.Add(new ReportParameter("trBookNum", MainWindow.resourcemanagerreport.GetString("trBookNum")));
        //    //paramarr.Add(new ReportParameter("trProcessDate", MainWindow.resourcemanagerreport.GetString("trProcessDate")));
        //    //paramarr.Add(new ReportParameter("trExpirationDate", MainWindow.resourcemanagerreport.GetString("trExpirationDate")));
        //    //paramarr.Add(new ReportParameter("trPaid", MainWindow.resourcemanagerreport.GetString("trPaid")));

        //    DateFormConv(paramarr);
        //}

        public static void DateFormConv(List<ReportParameter> paramarr)
        {

            paramarr.Add(new ReportParameter("dateForm", MainWindow.dateFormat));
        }

        //public static void bondsDocReport(LocalReport rep, string reppath, List<ReportParameter> paramarr)
        //{
        //    rep.ReportPath = reppath;
        //    rep.EnableExternalImages = true;
        //    rep.DataSources.Clear();



        //    DateFormConv(paramarr);


        //}

        

        //public static void orderReport(IEnumerable<PosSerials> invoiceQuery, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        //{
        //    rep.ReportPath = reppath;
        //    rep.EnableExternalImages = true;
        //    rep.DataSources.Clear();

        //    rep.DataSources.Add(new ReportDataSource("DataSetInvoice", invoiceQuery));
        //}

 
    }
}
