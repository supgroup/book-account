using BookAccountApp.ApiClasses;
using BookAccountApp.Classes;
using BookAccountApp.View.windows;
using Microsoft.Reporting.WinForms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Newtonsoft.Json;
namespace BookAccountApp.View.reports
{
    /// <summary>
    /// Interaction logic for uc_systemTransactions.xaml
    /// </summary>
    public partial class uc_systemTransactions : UserControl
    {
        public uc_systemTransactions()
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
        private static uc_systemTransactions _instance;
        public static uc_systemTransactions Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_systemTransactions();
                return _instance;
            }
        }

        SaveFileDialog saveFileDialog = new SaveFileDialog();
        PaymentsSts bookSts = new PaymentsSts();
        IEnumerable<PaymentsSts> paymentsQuery ;
        IEnumerable<PaymentsSts> paymentsList ;
        Statistics StatisticsModel = new Statistics();
        PayOp PayOpRow = new PayOp();
        byte tgl_bookStsstate;
        string searchText = "";
        string code = "";
        public static List<string> requiredControlList;
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);

                requiredControlList = new List<string> { "", };

                #region translate

                translate();
                #endregion

                await FillCombo.fillpaySideSys(cb_bookSales, "p");
                //Keyboard.Focus(cb_duration);
                 RefreshPaymentsList();
                  Search();

                Clear();
                //await fillcombos();
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
            //transaction syrSoto currentBalance  durationPayments totalPayments
          
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, MainWindow.resourcemanager.GetString("trSearchHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_bookSales, MainWindow.resourcemanager.GetString("syrSoto"));

            txt_active.Text = MainWindow.resourcemanager.GetString("trActive");
            txt_title.Text = MainWindow.resourcemanager.GetString("transaction");
           txt_balanceTitle.Text = MainWindow.resourcemanager.GetString("currentBalance");
            txt_durationPaidTitle.Text = MainWindow.resourcemanager.GetString("durationPayments");
            txt_totalPaidTitle.Text = MainWindow.resourcemanager.GetString("totalPayments");
             

            txt_invoicePrintButton.Text = MainWindow.resourcemanager.GetString("printInvoice");
            txt_invoicePreviewButton.Text = MainWindow.resourcemanager.GetString("previewInvoice");
            txt_invoicePdfButton.Text = MainWindow.resourcemanager.GetString("electronicInvoice");
 
            MaterialDesignThemes.Wpf.HintAssist.SetHint(dp_fromDate, MainWindow.resourcemanager.GetString("fromDate"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(dp_toDate, MainWindow.resourcemanager.GetString("toDate"));
            //   paymentsList            
            dg_bookSts.Columns[0].Header = MainWindow.resourcemanager.GetString("trNo.");
            dg_bookSts.Columns[1].Header = MainWindow.resourcemanager.GetString("bookSystem");
            dg_bookSts.Columns[2].Header = MainWindow.resourcemanager.GetString("airlineFlightSys");
            dg_bookSts.Columns[3].Header = MainWindow.resourcemanager.GetString("trOffice");
            dg_bookSts.Columns[4].Header = MainWindow.resourcemanager.GetString("trCashTooltip");
            dg_bookSts.Columns[5].Header = MainWindow.resourcemanager.GetString("exchangePrice");
            //dg_bookSts.Columns[6].Header = MainWindow.resourcemanager.GetString("currency");
            dg_bookSts.Columns[6].Header = MainWindow.resourcemanager.GetString("trDate");


            tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");
            tt_report.Content = MainWindow.resourcemanager.GetString("trPdf");
            tt_excel.Content = MainWindow.resourcemanager.GetString("trExcel");
            tt_preview.Content = MainWindow.resourcemanager.GetString("trPreview");
            tt_print.Content = MainWindow.resourcemanager.GetString("trPrint");
            //tt_count.Content = MainWindow.resourcemanager.GetString("trCount");


        }


        #region events
        private   void Tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                 Search();
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
                if (paymentsList is null)
                    await RefreshPaymentsList();
                tgl_bookStsstate = 1;
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
        private   void Tgl_isActive_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                if (paymentsList is null)
                      RefreshPaymentsList();
                tgl_bookStsstate = 0;
                  Search();
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
                RefreshPaymentsList();
                Search();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Dg_bookSts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                HelpClass.clearValidate(requiredControlList, this);
                //selection

                if (dg_bookSts.SelectedIndex != -1)
                {
                    bookSts = dg_bookSts.SelectedItem as PaymentsSts;
                    PayOpRow = JsonConvert.DeserializeObject<PayOp>(JsonConvert.SerializeObject(bookSts));
                    //PayOpRow = dg_bookSts.SelectedItem as PayOp;
                    //  this.DataContext = bookSts;
                    if (bookSts != null)
                    {
                       // this.DataContext = bookSts;


                    }
                }


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
                dp_toDate.SelectedDate = null;
                dp_fromDate.SelectedDate = null;
                tb_search.Text = "";
                 RefreshPaymentsList();
                  Search();
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
        void Search()
        {
            //search
            if (paymentsList is null)
                RefreshPaymentsList();
            string booksale = cb_bookSales.SelectedValue == null ? "" : cb_bookSales.SelectedValue.ToString();
            searchText = tb_search.Text.ToLower();
            paymentsQuery = paymentsList.Where(s => (s.sideStr== booksale && (searchText == "" ? true :
            (
          (s.code == null ? false : (s.code.ToLower().Contains(searchText))) ||
           (s.systemName == null ? false : (s.systemName.ToLower().Contains(searchText))) ||
             (s.airlineStr == null ? false : (s.airlineStr.ToLower().Contains(searchText))) ||
         (s.officeName == null ? false : (s.officeName.ToLower().Contains(searchText)))
            )))
            &&( (dp_fromDate.SelectedDate == null && dp_toDate.SelectedDate == null) ? true:(
            //start date
            ((dp_fromDate.SelectedDate != null  ) ? s.createDate == null ? false : (s.createDate.Value.Date >= dp_fromDate.SelectedDate.Value.Date) : true)
            &&
            //end date
            ((dp_toDate.SelectedDate != null ) ? s.createDate == null ? false : (s.createDate.Value.Date <= dp_toDate.SelectedDate.Value.Date) : true)
            ))
            );

            RefreshPaymentsView();
            viewPeriodSum();
            viewTotalSum(booksale);
        }

        private  IEnumerable<PaymentsSts> RefreshPaymentsList()
        {

             paymentsList =  StatisticsModel.GetPaySystemsTransfer(dp_fromDate.SelectedDate, dp_toDate.SelectedDate);

            return paymentsList;
        }
        void RefreshPaymentsView()
        {
            dg_bookSts.ItemsSource = paymentsQuery;
        }
        private void viewPeriodSum( )
        {         
                txt_durationPaid.Text = getperiodPayments( );
        }
    
private string getBalance(string code)
        {
            string balance = "";
         decimal amount= (decimal)  FillCombo.PaySidesSysList.Where(p => p.code == code).FirstOrDefault().balance;
        
            //amount = HelpClass.ConvertToSYP(amount, "usd", FillCombo.exchangeValue);

           balance = HelpClass.DecTostring(amount);
            return balance;
        }
        private string getperiodPayments()
        {
            string balance = "";
            //  decimal amount = (decimal)FillCombo.PaySidesSysList.Where(p => p.code == code).FirstOrDefault().balance;
            decimal amount = 0;
            if (code == "soto")
            {
                 amount = (decimal)paymentsQuery.Where(x => x.side == "system" && x.processType == "book").Sum(s => s.usdCash);
            }
            else
            {
                 amount = (decimal)paymentsQuery.Where(x => x.side == "system" && x.processType == "book").Sum(s => s.syCash);

            }
            balance = HelpClass.DecTostring(amount);
            return balance;
        }

        private string getTotalPayments( string side)
        {
            string balance = "";
            //  decimal amount = (decimal)FillCombo.PaySidesSysList.Where(p => p.code == code).FirstOrDefault().balance;
            decimal amount = 0;
                if (code == "soto")
            {
                 amount = (decimal)paymentsList.Where(x => x.side == "system" && x.processType == "book" && x.sideStr == side).Sum(s => s.usdCash);
            }
            else
            {
                 amount = (decimal)paymentsList.Where(x => x.side == "system" && x.processType == "book" && x.sideStr == side).Sum(s => s.syCash);

            }
            balance = HelpClass.DecTostring(amount);
            return balance;
        }
        private void viewTotalSum(string side)
        {
            txt_totalPaid.Text = getTotalPayments(side);
        }

        #endregion

        #region validate - clearValidate - textChange - lostFocus - . . . . 

        void Clear()
        {
            //this.DataContext = new PaymentsSts();
            cb_bookSales.SelectedIndex = -1;
            dp_toDate.SelectedDate = null;
            dp_fromDate.SelectedDate = null;

            // last 
            //  HelpClass.clearValidate(requiredControlList, this);
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
        //     SaveFileDialog saveFileDialog = new SaveFileDialog();

        public void BuildReport()
        {
            List<ReportParameter> paramarr = new List<ReportParameter>();
            string startDate = "";
            string endDate = "";
            string searchval = "";
            string Allchk = "";
            //  List<string> invTypelist = new List<string>();
            bool isArabic = ReportCls.checkLang();
            string all = MainWindow.resourcemanagerreport.GetString("trAll");
            string addpath;

            //if (isArabic)
            //{
            addpath = @"\Reports\statisticReports\Ar\ArPaytrans.rdlc";
            //}
            //else addpath = @"\Reports\Account\En\PayAccReport.rdlc";

            //filter
            startDate = dp_fromDate.SelectedDate != null ? clsReports.dateFrameConverter(dp_fromDate.SelectedDate) : "";
            endDate = dp_toDate.SelectedDate != null ? clsReports.dateFrameConverter(dp_toDate.SelectedDate) : "";
            Allchk = dp_fromDate.SelectedDate == null && dp_toDate.SelectedDate == null ? all : "";
            paramarr.Add(new ReportParameter("StartDateVal", startDate));
            paramarr.Add(new ReportParameter("EndDateVal", endDate));
            paramarr.Add(new ReportParameter("alldateval", Allchk));
            paramarr.Add(new ReportParameter("trDate", MainWindow.resourcemanagerreport.GetString("trDate")));
            paramarr.Add(new ReportParameter("trSearch", MainWindow.resourcemanagerreport.GetString("trSearch")));
            paramarr.Add(new ReportParameter("trStartDate", MainWindow.resourcemanagerreport.GetString("trStartDate")));
            paramarr.Add(new ReportParameter("trEndDate", MainWindow.resourcemanagerreport.GetString("trEndDate")));
            searchval = tb_search.Text;
            paramarr.Add(new ReportParameter("searchVal", searchval));
            //end filter
            paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("paySysTrans")));
            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);

            ReportCls.checkLang();
            //cashesQueryExcel = cashesQuery.ToList();
            clsReports.PaymentssysTrans( paymentsQuery , rep, reppath, paramarr);
            //clsReports.setReportLanguage(paramarr);
            clsReports.Header(paramarr);

            rep.SetParameters(paramarr);
            rep.Refresh();
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
                LocalReportExtensions.PrintToPrinterbyNameAndCopy(rep, FillCombo.rep_printer_name, FillCombo.rep_print_count == null ? short.Parse("1") : short.Parse(FillCombo.rep_print_count));
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


        //
       

        #region VoucherReport
        public void BuildVoucherReport()
        {
            List<ReportParameter> paramarr = new List<ReportParameter>();
            string addpath;
            bool isArabic = ReportCls.checkLang();
            //if (isArabic)
            //{
            if (FillCombo.docPapersize == "A4")
            {
                addpath = @"\Reports\Account\Ar\Voucher\ArPayReportA4.rdlc";
            }
            else //A5
            {
                addpath = @"\Reports\Account\Ar\Voucher\ArPayReport.rdlc";
            }

            //}
            //else
            //{
            //    if (FillCombo.docPapersize == "A4")
            //    {
            //        addpath = @"\Reports\Account\En\PayReportA4.rdlc";
            //    }
            //    else //A5
            //    {
            //        addpath = @"\Reports\Account\En\PayReport.rdlc";
            //    }
            //}
            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
            rep.ReportPath = reppath;
            rep.DataSources.Clear();
            rep.EnableExternalImages = true;
            //  servicemodel= await servicemodel.GetByID((int)payOp.serviceId);
            PayOp repPayop = PayOpRow;
            repPayop.cash = PayOpRow.syCash;
            repPayop.currency = "syp";
            paramarr = reportclass.fillPayReport(repPayop);
            clsReports.Header(paramarr);
            rep.SetParameters(paramarr);
            rep.Refresh();
        }

        private void Btn_invoicePrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                if (dg_bookSts.SelectedIndex != -1)
                {

             
                    #region
                    BuildVoucherReport();
                LocalReportExtensions.PrintToPrinterbyNameAndCopy(rep, FillCombo.rep_printer_name, FillCombo.rep_print_count == null ? short.Parse("1") : short.Parse(FillCombo.rep_print_count));
                    #endregion
                }
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_invoicePreview_Click(object sender, RoutedEventArgs e)
        {
            //preview
            try
            {
                HelpClass.StartAwait(grid_main);
                if (dg_bookSts.SelectedIndex != -1)
                {

                    #region
                    Window.GetWindow(this).Opacity = 0.2;

                    string pdfpath = "";
                    //
                    pdfpath = @"\Thumb\report\temp.pdf";
                    pdfpath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, pdfpath);

                    BuildVoucherReport();

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
                }
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_invoicePdf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                if (dg_bookSts.SelectedIndex != -1)
                {

                    #region
                    BuildVoucherReport();

                    saveFileDialog.Filter = "PDF|*.pdf;";

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string filepath = saveFileDialog.FileName;
                        LocalReportExtensions.ExportToPDF(rep, filepath);
                    }
                    #endregion
                }
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        #endregion
        #endregion





        private void Cb_bookSales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                if (cb_bookSales.SelectedItem != null)
                {//passenger office soto other
                      code = cb_bookSales.SelectedValue.ToString();
                  Search();
                    txt_balance.Text = getBalance(code);
                    //txt_balance.Text = getBalance(cb_bookSales.SelectedValue.ToString());
                    string currency = "";
                    if (code == "soto")
                    {
                         currency = "$";
                    }
                    else
                    {
                         currency = "SYP";
                        
                    }
                        tb_moneyIconBalance.Text = currency;
                        tb_moneyIconDur.Text = currency;
                        tb_moneyIconTotal.Text = currency;                   

                }
                else
                {
                    txt_balance.Text = "0";
                }

            

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
       
        }

        private void Dp_fromDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);

                RefreshPaymentsList();
                Search();

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Dp_toDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                //if (cb_custname.SelectedItem != null)
                //{

                //}
                RefreshPaymentsList();
                Search();

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
