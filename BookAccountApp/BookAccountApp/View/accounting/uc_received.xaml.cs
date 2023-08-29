﻿using BookAccountApp.ApiClasses;
using BookAccountApp.Classes;
using BookAccountApp.View.windows;
using Microsoft.Win32;
using netoaster;
using POS.View.windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Reporting.WinForms;
using BookAccountApp.View.sectionData;

namespace BookAccountApp.View.accounting
{
    /// <summary>
    /// Interaction logic for uc_received.xaml
    /// </summary>
    public partial class uc_received : UserControl
    {
        public uc_received()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private static uc_received _instance;
        public static uc_received Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_received();
                return _instance;
            }
        }

        PayOp payOp = new PayOp();
        IEnumerable<PayOp> payOpsQuery;
        IEnumerable<PayOp> payOps;
        byte tgl_payOpstate;
        string searchText = "";
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        public static List<string> requiredControlList;
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);

                requiredControlList = new List<string> { "airline", "passenger", "total" };

                #region translate
                //if (MainWindow.lang.Equals("en"))
                //{
                //    MainWindow.resourcemanager = new ResourceManager("BookAccountApp.en_file", Assembly.GetExecutingAssembly());
                //    grid_main.FlowDirection = FlowDirection.LeftToRight;
                //}
                //else
                //{
                //    MainWindow.resourcemanager = new ResourceManager("BookAccountApp.ar_file", Assembly.GetExecutingAssembly());
                //    grid_main.FlowDirection = FlowDirection.RightToLeft;
                //}
                translate();
                #endregion

                //await FillCombo.fillCountries(cb_areaMobile);
                //await FillCombo.fillCountries(cb_areaPhone);
                //await FillCombo.fillCountries(cb_areaFax);
                //await FillCombo.fillCountriesNames(cb_country);
                //FillCombo.fillAgentLevel(cb_custlevel);

                Keyboard.Focus(tb_code);

                await RefreshPayOpsList();
                await Search();

                Clear();
                await fillcombos();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void translate()
        {

            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, MainWindow.resourcemanager.GetString("trSearchHint"));
            //txt_baseInformation.Text = MainWindow.resourcemanager.GetString("trBaseInformation");
            txt_active.Text = MainWindow.resourcemanager.GetString("trActive");
            txt_title.Text = MainWindow.resourcemanager.GetString("bookInfoSoto");

            //MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_passenger, MainWindow.resourcemanager.GetString("passengerNameHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_ticketNum, MainWindow.resourcemanager.GetString("ticketNumHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_airline, MainWindow.resourcemanager.GetString("airlineFlightHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_operation, MainWindow.resourcemanager.GetString("operationHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_office, MainWindow.resourcemanager.GetString("officeNameHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(dp_serviceDate, MainWindow.resourcemanager.GetString("trDateHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_total, MainWindow.resourcemanager.GetString("totalHint"));

            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_notes, MainWindow.resourcemanager.GetString("trNoteHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(dp_fromDateSearch, MainWindow.resourcemanager.GetString("fromDate"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(dp_toDateSearch, MainWindow.resourcemanager.GetString("toDate"));
            //txt_exportDocsButton.Text = MainWindow.resourcemanager.GetString("docExport");
            //txt_uploadDocsButton.Text = MainWindow.resourcemanager.GetString("docUpload");
            //txt_contactInformation.Text = MainWindow.resourcemanager.GetString("trContactInformation");

            //MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_custlevel, MainWindow.resourcemanager.GetString("trLevelHint"));
            //txt_contactInformation.Text = MainWindow.resourcemanager.GetString("trContactInformation");
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_notes, MainWindow.resourcemanager.GetString("trNoteHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_address, MainWindow.resourcemanager.GetString("trAdressHint"));

            //   payOps
            /*
            dg_payOp.Columns[0].Header = MainWindow.resourcemanager.GetString("trNo.");
            dg_payOp.Columns[1].Header = MainWindow.resourcemanager.GetString("passengerName");
            dg_payOp.Columns[2].Header = MainWindow.resourcemanager.GetString("ticketNum");
            dg_payOp.Columns[3].Header = MainWindow.resourcemanager.GetString("airlineFlight");
            dg_payOp.Columns[4].Header = MainWindow.resourcemanager.GetString("officeName");
            dg_payOp.Columns[5].Header = MainWindow.resourcemanager.GetString("trDate");
            dg_payOp.Columns[6].Header = MainWindow.resourcemanager.GetString("total");
            */

            //dg_payOp.Columns[3].Header = MainWindow.resourcemanager.GetString("trMobile");

            tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");
            tt_report.Content = MainWindow.resourcemanager.GetString("trPdf");
            tt_excel.Content = MainWindow.resourcemanager.GetString("trExcel");
            tt_preview.Content = MainWindow.resourcemanager.GetString("trPreview");
            tt_print.Content = MainWindow.resourcemanager.GetString("trPrint");
            //tt_count.Content = MainWindow.resourcemanager.GetString("trCount");



        }
        #region Add - Update - Delete - Search - Tgl - Clear - DG_SelectionChanged - refresh

        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {

        }
        private async Task activate()
        {//activate
            /*
            payOp.isActive = 1;
            decimal s = await payOp.Save(payOp);
            if (s <= 0)
                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
            else
            {
                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopActive"), animation: ToasterAnimation.FadeIn);
                await RefreshPayOpsList();
                await Search();
            }
            */
        }
        #endregion
        #region events
        private async void Tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                await Search();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Tgl_isActive_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                /*
                if (payOps is null)
                    await RefreshPayOpsList();
                tgl_payOpstate = 1;
                await Search();
                */
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Tgl_isActive_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                if (payOps is null)
                    await RefreshPayOpsList();
                tgl_payOpstate = 0;
                await Search();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Btn_clear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                Clear();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Dg_payOp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                HelpClass.clearValidate(requiredControlList, this);
                //selection
                /*
                if (dg_payOp.SelectedIndex != -1)
                {
                    payOp = dg_payOp.SelectedItem as PayOp;
                    //  this.DataContext = payOp;
                    if (payOp != null)
                    {
                        //tb_custCode.Text = payOp.custCode;
                        cb_passenger.SelectedValue = payOp.passengerId;
                        cb_airline.SelectedValue = payOp.flightId;
                        cb_office.SelectedValue = payOp.officeId;
                        cb_operation.SelectedValue = payOp.operationId;

                        tb_total.Text = HelpClass.DecTostring(payOp.total);
                        this.DataContext = payOp;
                        //await getImg();
                        #region delete
                        //if (payOp.canDelete)
                        //    btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");
                        //else
                        //{
                        //    if (payOp.isActive == 0)
                        //        btn_delete.Content = MainWindow.resourcemanager.GetString("trActive");
                        //    else
                        //        btn_delete.Content = MainWindow.resourcemanager.GetString("trInActive");
                        //}
                        #endregion
                        //HelpClass.getMobile(payOp.mobile, cb_areaMobile, tb_mobile);
                        //HelpClass.getPhone(payOp.phone, cb_areaPhone, cb_areaPhoneLocal, tb_phone);
                        //HelpClass.getPhone(payOp.fax, cb_areaFax, cb_areaFaxLocal, tb_fax);
                    }
                }
                */

                //p_error_email.Visibility = Visibility.Collapsed;

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {//refresh

                HelpClass.StartAwait(grid_main);
                await RefreshPayOpsList();
                await Search();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        #endregion

        #region Refresh & Search
        async Task Search()
        {
            //search
            if (payOps is null)
                await RefreshPayOpsList();
            /*
            searchText = tb_search.Text.ToLower();
            payOpsQuery = payOps.Where(s => searchText == "" ? true :
            (
          (s.airline == null ? false : (s.airline.ToLower().Contains(searchText))) ||
           (s.passenger == null ? false : (s.passenger.ToLower().Contains(searchText))) ||
             (s.ticketNum == null ? false : (s.ticketNum.ToLower().Contains(searchText))) ||
         (s.officeName == null ? false : (s.officeName.ToLower().Contains(searchText)))

            )
            && (
            //start date
            ((dp_fromDateSearch.SelectedDate != null || dp_fromDateSearch.Text != "") ? s.serviceDate == null ? false : (s.serviceDate.Value.Date >= dp_fromDateSearch.SelectedDate.Value.Date) : true)
            &&
            //end date
            ((dp_toDateSearch.SelectedDate != null || dp_toDateSearch.Text != "") ? s.serviceDate == null ? false : (s.serviceDate.Value.Date <= dp_toDateSearch.SelectedDate.Value.Date) : true)

            )
            );

            //);
            */
            RefreshPayOpsView();
        }
        async Task<IEnumerable<PayOp>> RefreshPayOpsList()
        {
            /*
            payOps = await payOp.GetAll();
            payOps = payOps.Where(s => s.systemType == "soto").ToList();
            */
            return payOps;
        }
        void RefreshPayOpsView()
        {

            dg_payOp.ItemsSource = payOpsQuery;
            // txt_count.Text = payOpsQuery.Count().ToString();
        }
        public async Task fillcombos()
        {
            /*
            await FillCombo.fillPassengers(cb_passenger);
            await FillCombo.fillFlights(cb_airline);
            await FillCombo.fillOffice(cb_office);
            await FillCombo.fillOperations(cb_operation);
            */
        }

        #endregion

        #region validate - clearValidate - textChange - lostFocus - . . . . 

        void Clear()
        {
            this.DataContext = new PayOp();
            /*
            cb_passenger.SelectedIndex = -1;
            cb_airline.SelectedIndex = -1;
            cb_office.SelectedIndex = -1;
            cb_operation.SelectedIndex = -1;
            //cb_passenger.Text = "";
            //cb_airline.Text = "";
            //cb_office.Text = "";
            tb_total.Text = "";
            //cb_operation.Text ="";
            */
            // last 
            HelpClass.clearValidate(requiredControlList, this);
        }
        string input;
        decimal _decimal = 0;
        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        { //only  digits
            try
            {
                TextBox textBox = sender as TextBox;
                HelpClass.InputJustNumber(ref textBox);
                //if (textBox.Tag.ToString() == "int")
                //{
                //    Regex regex = new Regex("[^0-9]");
                //    e.Handled = regex.IsMatch(e.Text);
                //}
                //else if (textBox.Tag.ToString() == "decimal")
                //{
                input = e.Text;
                e.Handled = !decimal.TryParse(textBox.Text + input, out _decimal);
                //}
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Spaces_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                e.Handled = e.Key == Key.Space;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Code_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                //only english and digits
                Regex regex = new Regex("^[a-zA-Z0-9. -_?]*$");
                if (!regex.IsMatch(e.Text))
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }

        }

        private void ValidateEmpty_TextChange(object sender, TextChangedEventArgs e)
        {
            try
            {
                HelpClass.validate(requiredControlList, this);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void validateEmpty_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.validate(requiredControlList, this);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        #endregion



        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            // Collect all generations of memory.
            GC.Collect();
        }

        #region reports

        ReportCls reportclass = new ReportCls();
        LocalReport rep = new LocalReport();
        SaveFileDialog saveFileDialog2 = new SaveFileDialog();

        public void BuildReport()
        {
            /*
            //string firstTitle = "paymentsReport";
            ////string secondTitle = "";
            ////string subTitle = "";
            //string Title = "";

            List<ReportParameter> paramarr = new List<ReportParameter>();

            string addpath;
            bool isArabic = ReportCls.checkLang();
            //if (isArabic)
            //{
            addpath = @"\Reports\Sale\ArSales.rdlc";

            //}
            //else
            //{
            //    addpath = @"\Reports\SectionData\En\EnPayOps.rdlc";
            //}
            //D:\myproj\posproject3\BookAccountApp\BookAccountApp\Reports\statisticReports\En\EnBook.rdlc
            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
            //     subTitle = clsReports.ReportTabTitle(firstTitle, secondTitle);
            string title = MainWindow.resourcemanagerreport.GetString("book_sales") + " / " + MainWindow.resourcemanagerreport.GetString("syr");
            paramarr.Add(new ReportParameter("trTitle", title));
            clsReports.SaleReport(payOpsQuery, rep, reppath, paramarr);
            clsReports.setReportLanguage(paramarr);
            clsReports.Header(paramarr);

            rep.SetParameters(paramarr);

            rep.Refresh();
            */
        }

        private void Btn_pdf_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                HelpClass.StartAwait(grid_main);

                #region
                BuildReport();

                saveFileDialog.Filter = "PDF|*.pdf;";

                if (saveFileDialog.ShowDialog() == true)
                {
                    string filepath = saveFileDialog.FileName;
                    LocalReportExtensions.ExportToPDF(rep, filepath);
                }
                #endregion

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }

        }

        private void Btn_preview_Click(object sender, RoutedEventArgs e)
        {

            //preview
            try
            {
                HelpClass.StartAwait(grid_main);

                #region
                Window.GetWindow(this).Opacity = 0.2;

                string pdfpath = "";
                //
                pdfpath = @"\Thumb\report\temp.pdf";
                pdfpath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, pdfpath);

                BuildReport();

                LocalReportExtensions.ExportToPDF(rep, pdfpath);
                wd_previewPdf w = new wd_previewPdf();
                w.pdfPath = pdfpath;
                if (!string.IsNullOrEmpty(w.pdfPath))
                {
                    w.ShowDialog();
                    w.wb_pdfWebViewer.Dispose();


                }
                Window.GetWindow(this).Opacity = 1;
                #endregion

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }

        }

        private void Btn_print_Click(object sender, RoutedEventArgs e)
        {

            //print
            try
            {
                HelpClass.StartAwait(grid_main);

                #region
                BuildReport();
                LocalReportExtensions.PrintToPrinterbyNameAndCopy(rep, FillCombo.getdefaultPrinters(), FillCombo.rep_print_count == null ? short.Parse("1") : short.Parse(FillCombo.rep_print_count));
                #endregion

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }

        }

        private void Btn_exportToExcel_Click(object sender, RoutedEventArgs e)
        {

            //excel
            try
            {
                HelpClass.StartAwait(grid_main);

                #region
                //Thread t1 = new Thread(() =>
                //{
                BuildReport();
                this.Dispatcher.Invoke(() =>
                {
                    saveFileDialog.Filter = "EXCEL|*.xls;";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string filepath = saveFileDialog.FileName;
                        LocalReportExtensions.ExportToExcel(rep, filepath);
                    }
                });


                //});
                //t1.Start();
                #endregion

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }

        }

        private void Btn_pieChart_Click(object sender, RoutedEventArgs e)
        {

        }





        #endregion




        private void Btn_addAirline_Click(object sender, RoutedEventArgs e)
        {
            /*
          try
          {
              if (sender != null)
                  HelpClass.StartAwait(grid_main);
              Window.GetWindow(this).Opacity = 0.2;
              wd_flight w = new wd_flight();
              w.ShowDialog();
              await FillCombo.fillFlightTable(cb_flight);
              Window.GetWindow(this).Opacity = 1;

              if (sender != null)
                  HelpClass.EndAwait(grid_main);
          }
          catch (Exception ex)
          {
              Window.GetWindow(this).Opacity = 1;
              if (sender != null)
                  HelpClass.EndAwait(grid_main);
              HelpClass.ExceptionMessage(ex, this);
          }
          */
        }


        private void Btn_uploadDocs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_exportDocs_Click(object sender, RoutedEventArgs e)
        {

        }





        private async void Dp_fromDateSearch_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                //if (cb_custname.SelectedItem != null)
                //{

                //}

                await Search();

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Dp_toDateSearch_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                //if (cb_custname.SelectedItem != null)
                //{

                //}

                await Search();

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }



        private void Btn_addOperation_Click(object sender, RoutedEventArgs e)
        {
            /*  
      try
      {
          if (sender != null)
              HelpClass.StartAwait(grid_main);
          Window.GetWindow(this).Opacity = 0.2;
          wd_flight w = new wd_flight();
          w.ShowDialog();
          await FillCombo.fillFlightTable(cb_flight);
          Window.GetWindow(this).Opacity = 1;

          if (sender != null)
              HelpClass.EndAwait(grid_main);
      }
      catch (Exception ex)
      {
          Window.GetWindow(this).Opacity = 1;
          if (sender != null)
              HelpClass.EndAwait(grid_main);
          HelpClass.ExceptionMessage(ex, this);
      }
       */
        }

        private void Btn_invoicePrint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_invoicePreview_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_invoicePdf_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cb_side_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);



                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
    }
}