using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookAccountApp.ApiClasses;

namespace BookAccountApp.Classes
{
    class rptSectionData
    {
        public static void setReportLanguage(List<ReportParameter> paramarr)
        {
            paramarr.Add(new ReportParameter("lang", MainWindow.Reportlang));
        }


        public static void Header(List<ReportParameter> paramarr)
        {
          
           // List<ReportParameter> listParam = new List<ReportParameter>();

            ReportCls rep = new ReportCls();
            paramarr.Add(new ReportParameter("companyName", FillCombo.companyName));
            paramarr.Add(new ReportParameter("Fax", FillCombo.Fax));
            paramarr.Add(new ReportParameter("Tel", FillCombo.Mobile));
            paramarr.Add(new ReportParameter("Address", FillCombo.Address));
            paramarr.Add(new ReportParameter("Email", FillCombo.Email));
            paramarr.Add(new ReportParameter("logoImage", "file:\\" + rep.GetLogoImagePath()));
              
        }
        public static void bankReport(IEnumerable<PosSerials> banksQuery, LocalReport rep, string reppath)
        {
            rep.ReportPath = reppath;
            rep.DataSources.Clear();
            rep.DataSources.Add(new ReportDataSource("DataSetBank", banksQuery));
        }

        //public static void posReport(IEnumerable<Pos> possQuery, LocalReport rep, string reppath)
        //{
        //    rep.ReportPath = reppath;
        //    rep.DataSources.Clear();
        //    rep.DataSources.Add(new ReportDataSource("DataSetPos", possQuery));
        //}

        //public static void customerReport(IEnumerable<Agent> customersQuery, LocalReport rep, string reppath)
        //{
        //    rep.ReportPath = reppath;
        //    rep.DataSources.Clear();
        //    rep.DataSources.Add(new ReportDataSource("AgentDataSet", customersQuery));
        //}

        //public static void branchReport(IEnumerable<Branch> branchQuery, LocalReport rep, string reppath)
        //{
        //    rep.ReportPath = reppath;
        //    rep.DataSources.Clear();
        //    rep.DataSources.Add(new ReportDataSource("DataSetBranches", branchQuery));
        //}

        //public static void userReport(IEnumerable<User> usersQuery, LocalReport rep, string reppath)
        //{
        //    rep.ReportPath = reppath;
        //    rep.DataSources.Clear();
        //    rep.DataSources.Add(new ReportDataSource("DataSetUser", usersQuery));
        //}

        //public static void vendorReport(IEnumerable<Agent> vendorsQuery, LocalReport rep, string reppath)
        //{
        //    rep.ReportPath = reppath;
        //    rep.DataSources.Clear();
        //    rep.DataSources.Add(new ReportDataSource("AgentDataSet", vendorsQuery));
        //}

        //public static void storeReport(IEnumerable<Branch> storesQuery, LocalReport rep, string reppath)
        //{
        //    rep.ReportPath = reppath;
        //    rep.DataSources.Clear();
        //    rep.DataSources.Add(new ReportDataSource("DataSetBranches", storesQuery));
        //}

    }
}
