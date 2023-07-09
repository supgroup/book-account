﻿using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookAccountApp.ApiClasses;
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
        public static void serialsReport(IEnumerable<PosSerials> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();

          //  paramarr.Add(new ReportParameter("trVendor", MainWindow.resourcemanagerreport.GetString("trVendor")));
            rep.DataSources.Add(new ReportDataSource("DataSetSerials", Query));

      

        }
        public static void serialsMailReport(IEnumerable<PosSerials> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();

            //  paramarr.Add(new ReportParameter("trVendor", MainWindow.resourcemanagerreport.GetString("trVendor")));
            rep.DataSources.Add(new ReportDataSource("DataSetSerials", Query));



        }
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
        public static void CustomersReport(IEnumerable<Customers> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
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
            paramarr.Add(new ReportParameter("trCompanyName", MainWindow.resourcemanagerreport.GetString("trName")));
            paramarr.Add(new ReportParameter("trMobile", MainWindow.resourcemanagerreport.GetString("trMobile")));


            DateFormConv(paramarr);
        }

        public static void packagesReport(IEnumerable<Packages> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();
            rep.DataSources.Add(new ReportDataSource("DataSet", Query));
            //title
            paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("trPackages")));
            //table columns
            paramarr.Add(new ReportParameter("trCode", MainWindow.resourcemanagerreport.GetString("trCode")));
            paramarr.Add(new ReportParameter("trPackage", MainWindow.resourcemanagerreport.GetString("trPackage")));
            paramarr.Add(new ReportParameter("trProgram", MainWindow.resourcemanagerreport.GetString("trProgram")));
            paramarr.Add(new ReportParameter("trVersion", MainWindow.resourcemanagerreport.GetString("trVersion")));


            DateFormConv(paramarr);
        }
        public static void  versionsReport(IEnumerable<Versions> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();
            rep.DataSources.Add(new ReportDataSource("DataSet", Query));
            //title
            paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("trVersions")));
            //table columns
            paramarr.Add(new ReportParameter("trCode", MainWindow.resourcemanagerreport.GetString("trCode")));
            paramarr.Add(new ReportParameter("trName", MainWindow.resourcemanagerreport.GetString("trName")));
            paramarr.Add(new ReportParameter("trProgram", MainWindow.resourcemanagerreport.GetString("trProgram")));

            DateFormConv(paramarr);
        }
        public static void programsReport(IEnumerable<Programs> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();
            rep.DataSources.Add(new ReportDataSource("DataSet", Query));
            //title
            paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("trPrograms")));
            //table columns
            paramarr.Add(new ReportParameter("trCode", MainWindow.resourcemanagerreport.GetString("trCode")));
            paramarr.Add(new ReportParameter("trName", MainWindow.resourcemanagerreport.GetString("trName")));
            paramarr.Add(new ReportParameter("trProgram", MainWindow.resourcemanagerreport.GetString("trProgram")));

            DateFormConv(paramarr);
        }
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
        public static void BookSale(IEnumerable<PackageUser> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();
            rep.DataSources.Add(new ReportDataSource("DataSetBook", Query));
            //title
            paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("trServiceBook")));
            //table columns
            //paramarr.Add(new ReportParameter("trProcessNumTooltip", MainWindow.resourcemanagerreport.GetString("trProcessNumTooltip")));
            //paramarr.Add(new ReportParameter("trBookNum", MainWindow.resourcemanagerreport.GetString("trBookNum")));
            //paramarr.Add(new ReportParameter("trProcessDate", MainWindow.resourcemanagerreport.GetString("trProcessDate")));
            //paramarr.Add(new ReportParameter("trExpirationDate", MainWindow.resourcemanagerreport.GetString("trExpirationDate")));
            //paramarr.Add(new ReportParameter("trPaid", MainWindow.resourcemanagerreport.GetString("trPaid")));

            DateFormConv(paramarr);
        }

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

        //public static void orderReport(IEnumerable<Invoice> invoiceQuery, LocalReport rep, string reppath)
        //{
        //    rep.ReportPath = reppath;
        //    rep.EnableExternalImages = true;
        //    rep.DataSources.Clear();
        //    foreach(var o in invoiceQuery)
        //    {
        //        string status = "";
        //        switch (o.status)
        //        {
        //            case "tr":
        //                status = MainWindow.resourcemanager.GetString("trDelivered");
        //                break;
        //            case "rc":
        //                status = MainWindow.resourcemanager.GetString("trInDelivery");
        //                break;
        //            default:
        //                status = "";
        //                break;
        //        }
        //        o.status = status;
        //        o.deserved = decimal.Parse(SectionData.DecTostring(o.deserved));
        //    }
        //    rep.DataSources.Add(new ReportDataSource("DataSetInvoice", invoiceQuery));
        //}

        public static void orderReport(IEnumerable<PosSerials> invoiceQuery, LocalReport rep, string reppath, List<ReportParameter> paramarr)
        {
            rep.ReportPath = reppath;
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();

            rep.DataSources.Add(new ReportDataSource("DataSetInvoice", invoiceQuery));
        }

//        public static void DeliverStateConv(List<ReportParameter> paramarr)
//        {
//            paramarr.Add(new ReportParameter("trDelivered", MainWindow.resourcemanagerreport.GetString("trDelivered")));
//            paramarr.Add(new ReportParameter("trInDelivery", MainWindow.resourcemanagerreport.GetString("trInDelivery")));

//        }

//        public static void bankAccReport(IEnumerable<CashTransfer> cash, LocalReport rep, string reppath)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();

//            foreach (var c in cash)
//            {
//                ///////////////////
//                c.cash = decimal.Parse(SectionData.DecTostring(c.cash));
//                string s;
//                switch (c.processType)
//                {
//                    case "cash":
//                        s = MainWindow.resourcemanagerreport.GetString("trCash");
//                        break;
//                    case "doc":
//                        s = MainWindow.resourcemanagerreport.GetString("trDocument");
//                        break;
//                    case "cheque":
//                        s = MainWindow.resourcemanagerreport.GetString("trCheque");
//                        break;
//                    case "balance":
//                        s = MainWindow.resourcemanagerreport.GetString("trCredit");
//                        break;
//                    case "card":
//                        s = MainWindow.resourcemanagerreport.GetString("trCreditCard");
//                        break;
//                    default:
//                        s = c.processType;
//                        break;
//                }
//                ///////////////////
//                c.processType = s;
//                string name = "";
//                switch (c.side)
//                {
//                    case "bnd": break;
//                    case "v": name = MainWindow.resourcemanagerreport.GetString("trVendor"); break;
//                    case "c": name = MainWindow.resourcemanagerreport.GetString("trCustomer"); break;
//                    case "u": name = MainWindow.resourcemanagerreport.GetString("trUser"); break;
//                    case "s": name = MainWindow.resourcemanagerreport.GetString("trSalary"); break;
//                    case "e": name = MainWindow.resourcemanagerreport.GetString("trGeneralExpenses"); break;
//                    case "m":
//                        if (c.transType == "d")
//                            name = MainWindow.resourcemanagerreport.GetString("trAdministrativeDeposit");
//                        if (c.transType == "p")
//                            name = MainWindow.resourcemanagerreport.GetString("trAdministrativePull");
//                        break;
//                    case "sh": name = MainWindow.resourcemanagerreport.GetString("trShippingCompany"); break;
//                    default: break;
//                }
//                string fullName = "";
//                if (!string.IsNullOrEmpty(c.agentName))
//                    fullName = name + " " + c.agentName;
//                else if (!string.IsNullOrEmpty(c.usersLName))
//                    fullName = name + " " + c.usersLName;
//                else if (!string.IsNullOrEmpty(c.shippingCompanyName))
//                    fullName = name + " " + c.shippingCompanyName;
//                else
//                    fullName = name;
//                /////////////////////
//                c.side = fullName;

//                string type;
//                if (c.transType.Equals("p")) type = MainWindow.resourcemanagerreport.GetString("trPull");
//                else type = MainWindow.resourcemanagerreport.GetString("trDeposit");
//                ////////////////////
//                c.transType = type;
//            }
//            rep.DataSources.Add(new ReportDataSource("DataSetBankAcc", cash));
//        }

//        public static void paymentAccReport(IEnumerable<CashTransfer> cash, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();

//            //foreach (var c in cash)
//            //{
//            //    ///////////////////
//            //    c.cash = decimal.Parse(SectionData.DecTostring(c.cash));
//            //    string s;
//            //    switch (c.processType)
//            //    {
//            //        case "cash":
//            //            s = MainWindow.resourcemanagerreport.GetString("trCash");
//            //            break;
//            //        case "doc":
//            //            s = MainWindow.resourcemanagerreport.GetString("trDocument");
//            //            break;
//            //        case "cheque":
//            //            s = MainWindow.resourcemanagerreport.GetString("trCheque");
//            //            break;
//            //        case "balance":
//            //            s = MainWindow.resourcemanagerreport.GetString("trCredit");
//            //            break;
//            //        default:
//            //            s = c.processType;
//            //            break;
//            //    }


//            //}
//            foreach (var c in cash)
//            {

//                c.cash = decimal.Parse(SectionData.DecTostring(c.cash));
//            }

//            AccountSideConv(paramarr);

//            cashTransTypeConv(paramarr);
//            cashTransferProcessTypeConv(paramarr);


//            rep.DataSources.Add(new ReportDataSource("DataSetBankAcc", cash));
//        }

//        public static void receivedAccReport(IEnumerable<CashTransfer> cash, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {

//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();

//            foreach (var c in cash)
//            {

//                c.cash = decimal.Parse(SectionData.DecTostring(c.cash));
//            }
//            DateFormConv(paramarr);
//            AccountSideConv(paramarr);

//            cashTransTypeConv(paramarr);
//            cashTransferProcessTypeConv(paramarr);


//            rep.DataSources.Add(new ReportDataSource("DataSetBankAcc", cash));
//        }

//        public static void posAccReport(IEnumerable<CashTransfer> cash, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            foreach (var c in cash)
//            {

//                c.cash = decimal.Parse(SectionData.DecTostring(c.cash));
//            }
//            paramarr.Add(new ReportParameter("trPull", MainWindow.resourcemanagerreport.GetString("trPull")));
//            paramarr.Add(new ReportParameter("trDeposit", MainWindow.resourcemanagerreport.GetString("trDeposit")));

//            rep.DataSources.Add(new ReportDataSource("DataSetBankAcc", cash));
//        }
//        public static void invItem(IEnumerable<InventoryItemLocation> itemLocations, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();

//            rep.DataSources.Add(new ReportDataSource("DataSetInvItemLocation", itemLocations));
//            paramarr.Add(new ReportParameter("dateForm", MainWindow.dateFormat));
//        }
//        public static void section(IEnumerable<Section> sections, LocalReport rep, string reppath)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("DataSetSection", sections));
//        }
//        public static void location(IEnumerable<Location> locations, LocalReport rep, string reppath)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("DataSetLocation", locations));
//        }
//        public static void itemLocation(IEnumerable<ItemLocation> itemLocations, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("DataSetItemLocation", itemLocations));
//            paramarr.Add(new ReportParameter("dateForm", MainWindow.dateFormat));
//        }
//        public static void bankReport(IEnumerable<Bank> banksQuery, LocalReport rep, string reppath)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("DataSetBank", banksQuery));
//        }
//        public static void ErrorsReport(IEnumerable<ErrorClass> Query, LocalReport rep, string reppath)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("DataSetError", Query));
//        }

//        public static void couponReport(IEnumerable<Coupon> CouponQuery2, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {

//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            foreach (var c in CouponQuery2)
//            {
//                c.discountValue = decimal.Parse(SectionData.DecTostring(c.discountValue));
//                c.invMin = decimal.Parse(SectionData.DecTostring(c.invMin));
//                c.invMax = decimal.Parse(SectionData.DecTostring(c.invMax));

//                string state = "";

//                if ((c.isActive == 1) && (c.endDate > DateTime.Now) && (c.quantity > 0))
//                    state = MainWindow.resourcemanager.GetString("trValid");
//                else
//                    state = MainWindow.resourcemanager.GetString("trExpired");

//                c.state = state;

//            }

//            rep.DataSources.Add(new ReportDataSource("DataSetCoupon", CouponQuery2));
//            paramarr.Add(new ReportParameter("dateForm", MainWindow.dateFormat));
//            paramarr.Add(new ReportParameter("Title", MainWindow.resourcemanagerreport.GetString("trCoupon")));
//            paramarr.Add(new ReportParameter("trCode", MainWindow.resourcemanagerreport.GetString("trCode")));
//            paramarr.Add(new ReportParameter("trName", MainWindow.resourcemanagerreport.GetString("trName")));
//            paramarr.Add(new ReportParameter("trDiscount", MainWindow.resourcemanagerreport.GetString("trValue")));
//            paramarr.Add(new ReportParameter("trQuantity", MainWindow.resourcemanagerreport.GetString("trQuantity")));
//            paramarr.Add(new ReportParameter("trRemainQ", MainWindow.resourcemanagerreport.GetString("trRemainQuantity")));
//            paramarr.Add(new ReportParameter("trEndDate", MainWindow.resourcemanagerreport.GetString("trvalidity")));

//        }
//        public static void couponExportReport(LocalReport rep, string reppath, List<ReportParameter> paramarr, string barcode)
//        {

//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();

//            ReportCls repcls = new ReportCls();


//            paramarr.Add(new ReportParameter("invNumber", barcode));
//            paramarr.Add(new ReportParameter("barcodeimage", "file:\\" + repcls.BarcodeToImage(barcode, "barcode")));

//        }

//        public static void packageReport(IEnumerable<Item> packageQuery, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();


//            rep.DataSources.Add(new ReportDataSource("DataSetItem", packageQuery));
//            //    paramarr.Add(new ReportParameter("dateForm", MainWindow.dateFormat));
//            paramarr.Add(new ReportParameter("Title", MainWindow.resourcemanagerreport.GetString("trPackageItems")));
//            paramarr.Add(new ReportParameter("trCode", MainWindow.resourcemanagerreport.GetString("trCode")));
//            paramarr.Add(new ReportParameter("trName", MainWindow.resourcemanagerreport.GetString("trPackage")));
//            paramarr.Add(new ReportParameter("trDetails", MainWindow.resourcemanagerreport.GetString("trDetails")));
//            paramarr.Add(new ReportParameter("trCategory", MainWindow.resourcemanagerreport.GetString("trCategorie")));

//        }
//        public static void serviceReport(IEnumerable<Item> serviceQuery, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();


//            rep.DataSources.Add(new ReportDataSource("DataSetItem", serviceQuery));
//            //    paramarr.Add(new ReportParameter("dateForm", MainWindow.dateFormat));trTheService trTheServices
//            paramarr.Add(new ReportParameter("Title", MainWindow.resourcemanagerreport.GetString("trTheServices")));
//            paramarr.Add(new ReportParameter("trCode", MainWindow.resourcemanagerreport.GetString("trCode")));
//            paramarr.Add(new ReportParameter("trName", MainWindow.resourcemanagerreport.GetString("trTheService")));
//            paramarr.Add(new ReportParameter("trDetails", MainWindow.resourcemanagerreport.GetString("trDetails")));
//            paramarr.Add(new ReportParameter("trCategory", MainWindow.resourcemanagerreport.GetString("trCategorie")));

//        }
//        public static void offerReport(IEnumerable<Offer> OfferQuery, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            foreach (var o in OfferQuery)
//            {

//                o.discountValue = decimal.Parse(SectionData.DecTostring(o.discountValue));
//            }

//            rep.DataSources.Add(new ReportDataSource("DataSetOffer", OfferQuery));
//            paramarr.Add(new ReportParameter("dateForm", MainWindow.dateFormat));
//            paramarr.Add(new ReportParameter("trCode", MainWindow.resourcemanagerreport.GetString("trCode")));
//            paramarr.Add(new ReportParameter("trName", MainWindow.resourcemanagerreport.GetString("trName")));
//            paramarr.Add(new ReportParameter("trDiscount", MainWindow.resourcemanagerreport.GetString("trValue")));
//            paramarr.Add(new ReportParameter("trStartDate", MainWindow.resourcemanagerreport.GetString("trStartDate")));
//            paramarr.Add(new ReportParameter("trEndDate", MainWindow.resourcemanagerreport.GetString("trEndDate")));



//        }
//        public static void cardReport(IEnumerable<Card> cardsQuery, LocalReport rep, string reppath)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("DataSetCard", cardsQuery));
//        }
//        public static void shippingReport(IEnumerable<ShippingCompanies> shippingCompanies, LocalReport rep, string reppath)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("DataSetShipping", shippingCompanies));
//        }
//        public static string itemTypeConverter(string value)
//        {
//            string s = "";
//            switch (value)
//            {
//                case "n": s = MainWindow.resourcemanagerreport.GetString("trNormals"); break;
//                case "d": s = MainWindow.resourcemanagerreport.GetString("trHaveExpirationDates"); break;
//                case "sn": s = MainWindow.resourcemanagerreport.GetString("trHaveSerialNumbers"); break;
//                case "sr": s = MainWindow.resourcemanagerreport.GetString("trServices"); break;
//                case "p": s = MainWindow.resourcemanagerreport.GetString("trPackages"); break;
//            }

//            return s;
//        }
//        public static void PurStsReport(IEnumerable<ItemTransferInvoice> tempquery, LocalReport rep, string reppath)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            foreach (var r in tempquery)
//            {
//                r.CopdiscountValue = decimal.Parse(SectionData.DecTostring(r.CopdiscountValue));
//                r.couponTotalValue = decimal.Parse(SectionData.DecTostring(r.couponTotalValue));//
//                r.OdiscountValue = decimal.Parse(SectionData.DecTostring(r.OdiscountValue));
//                r.offerTotalValue = decimal.Parse(SectionData.DecTostring(r.offerTotalValue));
//                r.ITprice = decimal.Parse(SectionData.DecTostring(r.ITprice));
//                r.subTotal = decimal.Parse(SectionData.DecTostring(r.subTotal));
//                r.totalNet = decimal.Parse(SectionData.DecTostring(r.totalNet));
//                r.discountValue = decimal.Parse(SectionData.DecTostring(r.discountValue));
//                r.tax = decimal.Parse(SectionData.DecTostring(r.tax));
//                if (r.itemAvg != null)
//                {
//                    r.itemAvg = double.Parse(SectionData.DecTostring(decimal.Parse(r.itemAvg.ToString())));

//                }
//            }
//            rep.DataSources.Add(new ReportDataSource("DataSetITinvoice", tempquery));
//        }

//        public static void saleitemStsReport(IEnumerable<ItemTransferInvoice> tempquery, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {

//            itemTypeConv(paramarr);
//            paramarr.Add(new ReportParameter("dateForm", MainWindow.dateFormat));
//            PurStsReport(tempquery, rep, reppath);

//        }

//        public static void SalePromoStsReport(IEnumerable<ItemTransferInvoice> tempquery, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            PurStsReport(tempquery, rep, reppath);

//            itemTransferDiscountTypeConv(paramarr);
//            paramarr.Add(new ReportParameter("dateForm", MainWindow.dateFormat));
//            /*
//             =IIF(Fields!CopdiscountType.Value="2",
//Parameters!trPercentageDiscount.Value,
//Parameters!trValueDiscount.Value)
//             * */
//        }
//        public static void itemTransferDiscountTypeConv(List<ReportParameter> paramarr)
//        {

//            paramarr.Add(new ReportParameter("trValueDiscount", MainWindow.resourcemanagerreport.GetString("trValueDiscount")));
//            paramarr.Add(new ReportParameter("trPercentageDiscount", MainWindow.resourcemanagerreport.GetString("trPercentageDiscount")));




//        }

//        public static void itemTypeConv(List<ReportParameter> paramarr)
//        {
//            paramarr.Add(new ReportParameter("trNormal", MainWindow.resourcemanagerreport.GetString("trNormal")));
//            paramarr.Add(new ReportParameter("trHaveExpirationDate", MainWindow.resourcemanagerreport.GetString("trHaveExpirationDate")));
//            paramarr.Add(new ReportParameter("trHaveSerialNumber", MainWindow.resourcemanagerreport.GetString("trHaveSerialNumber")));
//            paramarr.Add(new ReportParameter("trService", MainWindow.resourcemanagerreport.GetString("trService")));
//            paramarr.Add(new ReportParameter("trPackage", MainWindow.resourcemanagerreport.GetString("trPackage")));
//        }
//        //clsReports.SaleInvoiceStsReport(itemTransfers, rep, reppath, paramarr);

//        public static void SaleInvoiceStsReport(IEnumerable<ItemTransferInvoice> tempquery, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            PurStsReport(tempquery, rep, reppath);
//            paramarr.Add(new ReportParameter("isTax", MainWindow.tax.ToString()));
//            itemTransferInvTypeConv(paramarr);

//        }

//        public static void SaledailyReport(IEnumerable<ItemTransferInvoice> tempquery, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            string date = "";
//            PurStsReport(tempquery, rep, reppath);
//            if (tempquery == null || tempquery.Count() == 0)
//            {
//                date = "";
//            }
//            else
//            {
//                date = SectionData.DateToString(tempquery.FirstOrDefault().updateDate);
//            }

//            paramarr.Add(new ReportParameter("isTax", MainWindow.tax.ToString()));
//            paramarr.Add(new ReportParameter("invDate", date));
//            itemTransferInvTypeConv(paramarr);

//        }
//        public static void ProfitReport(IEnumerable<ItemUnitInvoiceProfit> tempquery, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            foreach (var r in tempquery)
//            {

//                r.totalNet = decimal.Parse(SectionData.DecTostring(r.totalNet));
//                r.invoiceProfit = decimal.Parse(SectionData.DecTostring(r.invoiceProfit));
//                r.itemProfit = decimal.Parse(SectionData.DecTostring(r.itemProfit));

//            }
//            rep.DataSources.Add(new ReportDataSource("DataSetProfit", tempquery));
//            paramarr.Add(new ReportParameter("title", MainWindow.resourcemanagerreport.GetString("trProfits")));
//            paramarr.Add(new ReportParameter("Currency", MainWindow.Currency));
//            itemTransferInvTypeConv(paramarr);

//        }

//        public static string ReportTabTitle(string firstTitle, string secondTitle)
//        {
//            string trtext = "";
//            //////////////////////////////////////////////////////////////////////////////
//            if (firstTitle == "invoice")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trInvoices");
//            else if (firstTitle == "quotation")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trQuotations");
//            else if (firstTitle == "promotion")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trThePromotion");
//            else if (firstTitle == "internal")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trInternal");
//            else if (firstTitle == "external")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trExternal");
//            else if (firstTitle == "banksReport")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trBanks");
//            else if (firstTitle == "destroied")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trDestructives");
//            else if (firstTitle == "usersReport")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trUsers");
//            else if (firstTitle == "storageReports")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trStorage");
//            else if (firstTitle == "stocktaking")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trStocktaking");
//            else if (firstTitle == "stock")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trStock");
//            else if ( firstTitle == "purchaseOrders")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trPurchaseOrders");
//            else if (firstTitle == "saleOrders" )
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trSalesOrders");
            
//            else if (firstTitle == "saleItems" || firstTitle == "purchaseItem")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trItems");
//            else if (firstTitle == "recipientReport")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trRecepient");
//            else if (firstTitle == "accountStatement")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trAccountStatement");
//            else if (firstTitle == "paymentsReport")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trPayments");
//            else if (firstTitle == "posReports")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trPOS");
//            else if (firstTitle == "dailySalesStatistic")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trDailySales");
//            else if (firstTitle == "accountProfits")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trProfits");
//            else if (firstTitle == "accountFund")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trCashBalance");
//            else if (firstTitle == "quotations")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trQTReport");
//            else if (firstTitle == "transfers")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trTransfers");
//            else if (firstTitle == "fund")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trCashBalance");
//            else if (firstTitle == "DirectEntry")
//                firstTitle = MainWindow.resourcemanagerreport.GetString("trDirectEntry");
//            //trCashBalance trDirectEntry
//            //trTransfers
//            //////////////////////////////////////////////////////////////////////////////

//            if (secondTitle == "branch")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trBranches");
//            else if (secondTitle == "pos")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trPOS");
//            else if (secondTitle == "vendors" || secondTitle == "vendor")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trVendors");
//            else if (secondTitle == "customers" || secondTitle == "customer")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trCustomers");
//            else if (secondTitle == "users" || secondTitle == "user")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trUsers");
//            else if (secondTitle == "items" || secondTitle == "item")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trItems");
//            else if (secondTitle == "coupon")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trCoupons");
//            else if (secondTitle == "offers")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trOffer");
//            else if (secondTitle == "invoice")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trInvoices");
//            else if (secondTitle == "order")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trOrders");
//            else if (secondTitle == "quotation")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trQTReport");
//            else if (secondTitle == "operator")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trOperator");
//            else if (secondTitle == "payments")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trPayments");
//            else if (secondTitle == "recipient")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trRecepient");
//            else if (secondTitle == "destroied")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trDestructives");
//            else if (secondTitle == "agent")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trCustomers");
//            else if (secondTitle == "stock")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trStock");
//            else if (secondTitle == "external")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trExternal");
//            else if (secondTitle == "internal")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trInternal");
//            else if (secondTitle == "stocktaking")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trStocktaking");
//            else if (secondTitle == "archives")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trArchive");
//            else if (secondTitle == "shortfalls")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trShortages");
//            else if (secondTitle == "location")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trLocation");
//            else if (secondTitle == "collect")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trCollect");
//            else if (secondTitle == "shipping")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trShipping");
//            else if (secondTitle == "salary")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trSalary");
//            else if (secondTitle == "generalExpenses")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trGeneralExpenses");
//            else if (secondTitle == "administrativePull")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trAdministrativePull");
//            else if (secondTitle == "BestSeller")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trBestSeller");
//            else if (secondTitle == "MostPurchased")
//                secondTitle = MainWindow.resourcemanagerreport.GetString("trMostPurchased");
//            //////////////////////////////////////////////////////////////////////////////

//            trtext = firstTitle + " / " + secondTitle;
//            return trtext;
//        }
//        public static void PurInvStsReport(IEnumerable<ItemTransferInvoice> tempquery, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {


//            PurStsReport(tempquery, rep, reppath);
//            itemTransferInvTypeConv(paramarr);


//        }

//        public static void PurOrderStsReport(IEnumerable<ItemTransferInvoice> tempquery, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            PurStsReport(tempquery, rep, reppath);
//            itemTransferInvTypeConv(paramarr);

//        }

//        public static void posReport(IEnumerable<Pos> possQuery, LocalReport rep, string reppath)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("DataSetPos", possQuery));
//        }

//        public static void customerReport(IEnumerable<Agent> customersQuery, LocalReport rep, string reppath)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("AgentDataSet", customersQuery));
//        }

//        public static void branchReport(IEnumerable<Branch> branchQuery, LocalReport rep, string reppath)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("DataSetBranches", branchQuery));
//        }

//        public static void userReport(IEnumerable<User> usersQuery, LocalReport rep, string reppath)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("DataSetUser", usersQuery));
//        }

//        public static void vendorReport(IEnumerable<Agent> vendorsQuery, LocalReport rep, string reppath)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("AgentDataSet", vendorsQuery));
//        }

//        public static void storeReport(IEnumerable<Branch> storesQuery, LocalReport rep, string reppath)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("DataSetBranches", storesQuery));
//        }
//        public static void purchaseInvoiceReport(List<ItemTransfer> invoiceItems, LocalReport rep, string reppath)
//        {
//            foreach (var i in invoiceItems)
//            {
//                i.price = decimal.Parse(SectionData.DecTostring(i.price));
//            }
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("DataSetItemTransfer", invoiceItems));
//            rep.EnableExternalImages = true;

//        }
//        public static void storage(IEnumerable<Storage> storageQuery, LocalReport rep, string reppath)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            foreach (var r in storageQuery)
//            {
//                if (r.startDate != null)
//                    r.startDate = DateTime.Parse(SectionData.DateToString(r.startDate));//
//                if (r.endDate != null)
//                    r.endDate = DateTime.Parse(SectionData.DateToString(r.endDate));
//                //r.inventoryDate = DateTime.Parse(SectionData.DateToString(r.inventoryDate));
//                //r.IupdateDate = DateTime.Parse(SectionData.DateToString(r.IupdateDate));

//                //r.diffPercentage = decimal.Parse(SectionData.DecTostring(r.diffPercentage));
//                r.storageCostValue = decimal.Parse(SectionData.DecTostring(r.storageCostValue));
//            }
//            rep.DataSources.Add(new ReportDataSource("DataSetStorage", storageQuery));
//        }

//        /* free zone
//         =iif((Fields!section.Value="FreeZone")And(Fields!location.Value="0-0-0"),
//"-",Fields!section.Value+"-"+Fields!location.Value)
//         * */

//        public static void storageStock(IEnumerable<Storage> storageQuery, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            storage(storageQuery, rep, reppath);
//            DateFormConv(paramarr);


//        }
//        // stocktaking
//        public static void StocktakingArchivesReport(IEnumerable<InventoryClass> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();

//            foreach (var r in Query)
//            {

//                //r.inventoryDate = DateTime.Parse(SectionData.DateToString(r.inventoryDate));
//                //r.IupdateDate = DateTime.Parse(SectionData.DateToString(r.IupdateDate));

//                r.diffPercentage = decimal.Parse(SectionData.DecTostring(r.diffPercentage));
//                //r.storageCostValue = decimal.Parse(SectionData.DecTostring(r.storageCostValue));
//            }


//            rep.DataSources.Add(new ReportDataSource("DataSetInventoryClass", Query));
//            DateFormConv(paramarr);
//            InventoryTypeConv(paramarr);
//        }

//        public static void StocktakingShortfallsReport(IEnumerable<ItemTransferInvoice> Query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();

//            //foreach (var r in Query)
//            //{
//            //    //if (r.startDate != null)
//            //    //    r.startDate = DateTime.Parse(SectionData.DateToString(r.startDate));//
//            //    //if (r.endDate != null)
//            //    //    r.endDate = DateTime.Parse(SectionData.DateToString(r.endDate));

//            //    //r.inventoryDate = DateTime.Parse(SectionData.DateToString(r.inventoryDate));
//            //    //r.IupdateDate = DateTime.Parse(SectionData.DateToString(r.IupdateDate));

//            //    //r.diffPercentage = decimal.Parse(SectionData.DecTostring(r.diffPercentage));
//            //    //r.storageCostValue = decimal.Parse(SectionData.DecTostring(r.storageCostValue));
//            //}


//            rep.DataSources.Add(new ReportDataSource("DataSetItemTransferInvoice", Query));
//            DateFormConv(paramarr);
//            InventoryTypeConv(paramarr);

//        }
//        /*
//        = Switch(Fields!inventoryType.Value="a",Parameters!trArchived.Value
//,Fields!inventoryType.Value="n",Parameters!trSaved.Value
//,Fields!inventoryType.Value="d",Parameters!trDraft.Value
//)

//         * */
//        //

//        public static void cashTransferStsBank(IEnumerable<CashTransferSts> cashTransfers, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            cashTransferSts(cashTransfers, rep, reppath);
//            paramarr.Add(new ReportParameter("dateForm", MainWindow.dateFormat));
//            paramarr.Add(new ReportParameter("trPull", MainWindow.resourcemanagerreport.GetString("trPull")));
//            paramarr.Add(new ReportParameter("trDeposit", MainWindow.resourcemanagerreport.GetString("trDeposit")));


//        }

//        public static string StsStatementPaymentConvert(string value)
//        {
//            string s = "";
//            switch (value)
//            {
//                case "cash":
//                    s = MainWindow.resourcemanagerreport.GetString("trCash");
//                    break;
//                case "doc":
//                    s = MainWindow.resourcemanagerreport.GetString("trDocument");
//                    break;
//                case "cheque":
//                    s = MainWindow.resourcemanagerreport.GetString("trCheque");
//                    break;
//                case "balance":
//                    s = MainWindow.resourcemanagerreport.GetString("trCredit");
//                    break;
//                case "card":
//                    s = MainWindow.resourcemanagerreport.GetString("trAnotherPaymentMethods");
//                    break;
//                case "inv":
//                    s = MainWindow.resourcemanagerreport.GetString("trInv");
//                    break;
//                default:
//                    s = value;
//                    break;


//            }
//            return s;
//        }
//        public static void cashTransferStsStatement(IEnumerable<CashTransferSts> cashTransfers, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            cashTransferStatSts(cashTransfers, rep, reppath);

//            paramarr.Add(new ReportParameter("dateForm", MainWindow.dateFormat));


//        }
//        public static void cashTransferStsPayment(IEnumerable<CashTransferSts> cashTransfers, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            cashTransferSts(cashTransfers, rep, reppath);

//            cashTransferProcessTypeConv(paramarr);
//            DateFormConv(paramarr);

//        }
//        public static void cashTransferStsPos(IEnumerable<CashTransferSts> cashTransfers, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            cashTransferSts(cashTransfers, rep, reppath);
//            cashTransTypeConv(paramarr);
//            DateFormConv(paramarr);

//        }

//        public static void cashTransferSts(IEnumerable<CashTransferSts> cashTransfers, LocalReport rep, string reppath)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            foreach (var r in cashTransfers)
//            {
//                r.updateDate = DateTime.Parse(SectionData.DateToString(r.updateDate));
//                r.cash = decimal.Parse(SectionData.DecTostring(r.cash));


//            }
//            rep.DataSources.Add(new ReportDataSource("DataSetCashTransferSts", cashTransfers));
//        }
//        public static void cashTransferStatSts(IEnumerable<CashTransferSts> cashTransfers, LocalReport rep, string reppath)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            foreach (CashTransferSts r in cashTransfers)
//            {
//                r.updateDate = DateTime.Parse(SectionData.DateToString(r.updateDate));
//                r.cash = decimal.Parse(SectionData.DecTostring(r.cash));

//                r.paymentreport = StsStatementPaymentConvert(r.Description3);


//            }
//            rep.DataSources.Add(new ReportDataSource("DataSetCashTransferSts", cashTransfers));
//        }

//        public static void FundStsReport(IEnumerable<BalanceSTS> query, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            foreach (var r in query)
//            {

//                r.balance = decimal.Parse(SectionData.DecTostring(r.balance));
//            }
//            rep.DataSources.Add(new ReportDataSource("DataSetBalanceSTS", query));
//            paramarr.Add(new ReportParameter("title", MainWindow.resourcemanagerreport.GetString("trBalance")));
//            paramarr.Add(new ReportParameter("Currency", MainWindow.Currency));


//        }


//        public static void cashTransferStsRecipient(IEnumerable<CashTransferSts> cashTransfers, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            cashTransferSts(cashTransfers, rep, reppath);

//            cashTransferProcessTypeConv(paramarr);
//            DateFormConv(paramarr);

//        }
//        public static void itemTransferInvoice(IEnumerable<ItemTransferInvoice> itemTransferInvoices, LocalReport rep, string reppath)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("DataSetItemTransferInvoice", itemTransferInvoices));

//        }
//        public static void DateFormConv(List<ReportParameter> paramarr)
//        {

//            paramarr.Add(new ReportParameter("dateForm", MainWindow.dateFormat));
//        }

//        public static void InventoryTypeConv(List<ReportParameter> paramarr)
//        {

//            paramarr.Add(new ReportParameter("trArchived", MainWindow.resourcemanagerreport.GetString("trArchived")));
//            paramarr.Add(new ReportParameter("trSaved", MainWindow.resourcemanagerreport.GetString("trSaved")));
//            paramarr.Add(new ReportParameter("trDraft", MainWindow.resourcemanagerreport.GetString("trDraft")));
//        }
//        public static void cashTransTypeConv(List<ReportParameter> paramarr)
//        {

//            paramarr.Add(new ReportParameter("trPull", MainWindow.resourcemanagerreport.GetString("trPull")));
//            paramarr.Add(new ReportParameter("trDeposit", MainWindow.resourcemanagerreport.GetString("trDeposit")));

//        }

//        public static void cashTransferProcessTypeConv(List<ReportParameter> paramarr)
//        {
//            paramarr.Add(new ReportParameter("trCash", MainWindow.resourcemanagerreport.GetString("trCash")));
//            paramarr.Add(new ReportParameter("trDocument", MainWindow.resourcemanagerreport.GetString("trDocument")));
//            paramarr.Add(new ReportParameter("trCheque", MainWindow.resourcemanagerreport.GetString("trCheque")));
//            paramarr.Add(new ReportParameter("trCredit", MainWindow.resourcemanagerreport.GetString("trCredit")));
//            paramarr.Add(new ReportParameter("trInv", MainWindow.resourcemanagerreport.GetString("trInv")));
//            paramarr.Add(new ReportParameter("trCard", MainWindow.resourcemanagerreport.GetString("trCreditCard")));
//        }
//        public static void itemTransferInvTypeConv(List<ReportParameter> paramarr)
//        {
//            paramarr.Add(new ReportParameter("dateForm", MainWindow.dateFormat));
//            paramarr.Add(new ReportParameter("trPurchaseInvoice", MainWindow.resourcemanagerreport.GetString("trPurchaseInvoice")));
//            paramarr.Add(new ReportParameter("trPurchaseInvoiceWaiting", MainWindow.resourcemanagerreport.GetString("trPurchaseInvoiceWaiting")));
//            paramarr.Add(new ReportParameter("trSalesInvoice", MainWindow.resourcemanagerreport.GetString("trSalesInvoice")));
//            paramarr.Add(new ReportParameter("trSalesReturnInvoice", MainWindow.resourcemanagerreport.GetString("trSalesReturnInvoice")));
//            paramarr.Add(new ReportParameter("trPurchaseReturnInvoice", MainWindow.resourcemanagerreport.GetString("trPurchaseReturnInvoice")));
//            paramarr.Add(new ReportParameter("trPurchaseReturnInvoiceWaiting", MainWindow.resourcemanagerreport.GetString("trPurchaseReturnInvoiceWaiting")));
//            paramarr.Add(new ReportParameter("trDraftPurchaseBill", MainWindow.resourcemanagerreport.GetString("trDraftPurchaseBill")));
//            paramarr.Add(new ReportParameter("trSalesDraft", MainWindow.resourcemanagerreport.GetString("trSalesDraft")));
//            paramarr.Add(new ReportParameter("trSalesReturnDraft", MainWindow.resourcemanagerreport.GetString("trSalesReturnDraft")));

//            paramarr.Add(new ReportParameter("trSaleOrderDraft", MainWindow.resourcemanagerreport.GetString("trSaleOrderDraft")));
//            paramarr.Add(new ReportParameter("trSaleOrder", MainWindow.resourcemanagerreport.GetString("trSaleOrder")));
//            paramarr.Add(new ReportParameter("trPurchaceOrderDraft", MainWindow.resourcemanagerreport.GetString("trPurchaceOrderDraft")));
//            paramarr.Add(new ReportParameter("trPurchaceOrder", MainWindow.resourcemanagerreport.GetString("trPurchaceOrder")));
//            paramarr.Add(new ReportParameter("trQuotationsDraft", MainWindow.resourcemanagerreport.GetString("trQuotationsDraft")));
//            paramarr.Add(new ReportParameter("trQuotations", MainWindow.resourcemanagerreport.GetString("trQuotations")));
//            paramarr.Add(new ReportParameter("trDestructive", MainWindow.resourcemanagerreport.GetString("trDestructive")));
//            paramarr.Add(new ReportParameter("trShortage", MainWindow.resourcemanagerreport.GetString("trShortage")));
//            paramarr.Add(new ReportParameter("trImportDraft", MainWindow.resourcemanagerreport.GetString("trImportDraft")));
//            paramarr.Add(new ReportParameter("trImport", MainWindow.resourcemanagerreport.GetString("trImport")));
//            paramarr.Add(new ReportParameter("trImportOrder", MainWindow.resourcemanagerreport.GetString("trImportOrder")));
//            paramarr.Add(new ReportParameter("trExportDraft", MainWindow.resourcemanagerreport.GetString("trExportDraft")));

//            paramarr.Add(new ReportParameter("trExport", MainWindow.resourcemanagerreport.GetString("trExport")));

//            paramarr.Add(new ReportParameter("trExportOrder", MainWindow.resourcemanagerreport.GetString("trExportOrder")));

//        }
//        public static void invoiceSideConv(List<ReportParameter> paramarr)
//        {


//            paramarr.Add(new ReportParameter("trVendor", MainWindow.resourcemanagerreport.GetString("trVendor")));
//            paramarr.Add(new ReportParameter("trCustomer", MainWindow.resourcemanagerreport.GetString("trCustomer")));


//        }
//        public static void AccountSideConv(List<ReportParameter> paramarr)
//        {

//            paramarr.Add(new ReportParameter("dateForm", MainWindow.dateFormat));

//            paramarr.Add(new ReportParameter("trVendor", MainWindow.resourcemanagerreport.GetString("trVendor")));
//            paramarr.Add(new ReportParameter("trCustomer", MainWindow.resourcemanagerreport.GetString("trCustomer")));
//            paramarr.Add(new ReportParameter("trUser", MainWindow.resourcemanagerreport.GetString("trUser")));
//            paramarr.Add(new ReportParameter("trSalary", MainWindow.resourcemanagerreport.GetString("trSalary")));
//            paramarr.Add(new ReportParameter("trGeneralExpenses", MainWindow.resourcemanagerreport.GetString("trGeneralExpenses")));

//            paramarr.Add(new ReportParameter("trAdministrativeDeposit", MainWindow.resourcemanagerreport.GetString("trAdministrativeDeposit")));

//            paramarr.Add(new ReportParameter("trAdministrativePull", MainWindow.resourcemanagerreport.GetString("trAdministrativePull")));
//            paramarr.Add(new ReportParameter("trShippingCompany", MainWindow.resourcemanagerreport.GetString("trShippingCompany")));


//        }
//        public static void itemTransferInvoiceExternal(IEnumerable<ItemTransferInvoice> itemTransferInvoices, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {



//            itemTransferInvTypeConv(paramarr);
//            invoiceSideConv(paramarr);

//            itemTransferInvoice(itemTransferInvoices, rep, reppath);


//        }
//        public static void itemTransferInvoiceInternal(IEnumerable<ItemTransferInvoice> itemTransferInvoices, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            itemTransferInvTypeConv(paramarr);
//            itemTransferInvoice(itemTransferInvoices, rep, reppath);

//        }
//        public static void itemTransferInvoiceDestroied(IEnumerable<ItemTransferInvoice> itemTransferInvoices, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            itemTransferInvoice(itemTransferInvoices, rep, reppath);
//            paramarr.Add(new ReportParameter("dateForm", MainWindow.dateFormat));

//        }
//        public static void categoryReport(IEnumerable<Category> categoryQuery, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            foreach (var r in categoryQuery)
//            {
//                r.taxes = decimal.Parse(SectionData.DecTostring(r.taxes));
//            }
//            rep.DataSources.Add(new ReportDataSource("DataSetCategory", categoryQuery));
//            paramarr.Add(new ReportParameter("Title", MainWindow.resourcemanagerreport.GetString("trCategories")));
//            paramarr.Add(new ReportParameter("trCode", MainWindow.resourcemanagerreport.GetString("trCode")));
//            paramarr.Add(new ReportParameter("trName", MainWindow.resourcemanagerreport.GetString("trName")));
//            paramarr.Add(new ReportParameter("trDetails", MainWindow.resourcemanagerreport.GetString("trDetails")));
//        }
//        //public static void itemReport(IEnumerable<Item> itemQuery, LocalReport rep, string reppath)
//        //{
//        //    rep.ReportPath = reppath;
//        //    rep.EnableExternalImages = true;
//        //    rep.DataSources.Clear();
//        //    rep.DataSources.Add(new ReportDataSource("DataSetItem", itemQuery));

//        //}
//        public static void itemReport(IEnumerable<Item> itemQuery, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            foreach (var r in itemQuery)
//            {
//                r.taxes = decimal.Parse(SectionData.DecTostring(r.taxes));
//            }
//            rep.DataSources.Add(new ReportDataSource("DataSetItem", itemQuery));
//            paramarr.Add(new ReportParameter("Title", MainWindow.resourcemanagerreport.GetString("trItems")));
//            paramarr.Add(new ReportParameter("trCode", MainWindow.resourcemanagerreport.GetString("trCode")));
//            paramarr.Add(new ReportParameter("trName", MainWindow.resourcemanagerreport.GetString("trName")));
//            paramarr.Add(new ReportParameter("trDetails", MainWindow.resourcemanagerreport.GetString("trDetails")));
//            paramarr.Add(new ReportParameter("trCategory", MainWindow.resourcemanagerreport.GetString("trCategorie")));
//        }
//        public static void properyReport(IEnumerable<Property> propertyQuery, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("DataSetProperty", propertyQuery));
//            paramarr.Add(new ReportParameter("Title", MainWindow.resourcemanagerreport.GetString("trProperties")));
//            paramarr.Add(new ReportParameter("trName", MainWindow.resourcemanagerreport.GetString("trProperty")));
//            paramarr.Add(new ReportParameter("trValues", MainWindow.resourcemanagerreport.GetString("trValues")));
//        }
//        public static void storageCostReport(IEnumerable<StorageCost> storageCostQuery, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            foreach (var s in storageCostQuery)
//            {
//                s.cost = decimal.Parse(SectionData.DecTostring(s.cost));
//            }
//            rep.DataSources.Add(new ReportDataSource("DataSetStorageCost", storageCostQuery));
//            paramarr.Add(new ReportParameter("Title", MainWindow.resourcemanagerreport.GetString("trStorageCost")));
//            paramarr.Add(new ReportParameter("trName", MainWindow.resourcemanagerreport.GetString("trName")));
//            paramarr.Add(new ReportParameter("trCost", MainWindow.resourcemanagerreport.GetString("trStorageCost")));

//        }
//        public static void unitReport(IEnumerable<Unit> unitQuery, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("DataSetUnit", unitQuery));
//            paramarr.Add(new ReportParameter("Title", MainWindow.resourcemanagerreport.GetString("trUnit")));
//            paramarr.Add(new ReportParameter("trName", MainWindow.resourcemanagerreport.GetString("trUnitName")));
//            paramarr.Add(new ReportParameter("trNotes", MainWindow.resourcemanagerreport.GetString("trNote")));

//        }
//        public static void inventoryReport(IEnumerable<InventoryItemLocation> invItemsLocations, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("DataSetInventory", invItemsLocations));
//            paramarr.Add(new ReportParameter("Title", MainWindow.resourcemanagerreport.GetString("trStocktakingItems")));// tt
//            paramarr.Add(new ReportParameter("trNum", MainWindow.resourcemanagerreport.GetString("trNum")));
//            paramarr.Add(new ReportParameter("trSec_Loc", MainWindow.resourcemanagerreport.GetString("trSectionLocation")));//
//            //paramarr.Add(new ReportParameter("trItem_UnitName", MainWindow.resourcemanagerreport.GetString("trUnitName")+"-" + MainWindow.resourcemanagerreport.GetString("")));
//            paramarr.Add(new ReportParameter("trItem_UnitName", MainWindow.resourcemanagerreport.GetString("trItemUnit")));
//            paramarr.Add(new ReportParameter("trRealAmount", MainWindow.resourcemanagerreport.GetString("trRealAmount")));
//            paramarr.Add(new ReportParameter("trInventoryAmount", MainWindow.resourcemanagerreport.GetString("trInventoryAmount")));
//            paramarr.Add(new ReportParameter("trDestroyCount", MainWindow.resourcemanagerreport.GetString("trDestoryCount")));
//        }


//        public static void ItemsExportReport(IEnumerable<ItemTransfer> invoiceItems, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("DataSetItemsExport", invoiceItems));
//            paramarr.Add(new ReportParameter("Title", MainWindow.resourcemanagerreport.GetString("trItemsImport/Export")));// tt
//            paramarr.Add(new ReportParameter("trNum", MainWindow.resourcemanagerreport.GetString("trNum")));
//            paramarr.Add(new ReportParameter("trItem", MainWindow.resourcemanagerreport.GetString("trItem")));
//            paramarr.Add(new ReportParameter("trUnit", MainWindow.resourcemanagerreport.GetString("trUnit")));
//            paramarr.Add(new ReportParameter("trAmount", MainWindow.resourcemanagerreport.GetString("trQuantity")));
//        }
//        public static void ReceiptPurchaseReport(IEnumerable<ItemTransfer> invoiceItems, LocalReport rep, string reppath, List<ReportParameter> paramarr)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("DataSetItemsExport", invoiceItems));
//            paramarr.Add(new ReportParameter("Title", MainWindow.resourcemanagerreport.GetString("trReceiptOfPurchasesBill")));// tt
//            paramarr.Add(new ReportParameter("trNum", MainWindow.resourcemanagerreport.GetString("trNum")));
//            paramarr.Add(new ReportParameter("trItem", MainWindow.resourcemanagerreport.GetString("trItem")));
//            paramarr.Add(new ReportParameter("trUnit", MainWindow.resourcemanagerreport.GetString("trUnit")));
//            paramarr.Add(new ReportParameter("trAmount", MainWindow.resourcemanagerreport.GetString("trQuantity")));
//        }
//        public static void itemLocation(IEnumerable<ItemLocation> itemLocations, LocalReport rep, string reppath)
//        {
//            rep.ReportPath = reppath;
//            rep.EnableExternalImages = true;
//            rep.DataSources.Clear();
//            rep.DataSources.Add(new ReportDataSource("DataSetItemLocation", itemLocations));
//        }
    }
}
