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

namespace BookAccountApp.View.reports
{
    /// <summary>
    /// Interaction logic for uc_bookSts.xaml
    /// </summary>
    public partial class uc_bookSts : UserControl
    {
        public uc_bookSts()
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
        private static uc_bookSts _instance;
        public static uc_bookSts Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_bookSts();
                return _instance;
            }
        }

        SaveFileDialog saveFileDialog = new SaveFileDialog();
        BookSts bookSts = new BookSts();
        IEnumerable<BookSts> bookStssQuery;
        IEnumerable<BookSts> bookStss;
        Statistics StatisticsModel = new Statistics();
        byte tgl_bookStsstate;
        string searchText = "";
        string parentPeriod = "";
        string childPeriod = "";
        decimal sumpPrices = 0;
        decimal sumpProfits = 0;
        PayOp payopModel = new PayOp();
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


                Keyboard.Focus(cb_duration);
                FillCombo.fillPeriodParent(cb_duration);
                await RefreshBookStssList();
                await Search();

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
            //durationProfits yearMonth quarter1234 profitsNet totalOperations profitsNet
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, MainWindow.resourcemanager.GetString("trSearchHint"));
            txt_active.Text = MainWindow.resourcemanager.GetString("trActive");
            txt_title.Text = MainWindow.resourcemanager.GetString("durationProfits");
            txt_totalSaleTitle.Text = MainWindow.resourcemanager.GetString("totalOperations");
            txt_totalProfitTitle.Text = MainWindow.resourcemanager.GetString("profitsNet");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_duration, MainWindow.resourcemanager.GetString("yearMonth"));
           // MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_quarter, MainWindow.resourcemanager.GetString("quarter1234"));
          

            txt_invoicePrintButton.Text = MainWindow.resourcemanager.GetString("printInvoice");
            txt_invoicePreviewButton.Text = MainWindow.resourcemanager.GetString("previewInvoice");
            txt_invoicePdfButton.Text = MainWindow.resourcemanager.GetString("electronicInvoice");
            //btn_save.Content = MainWindow.resourcemanager.GetString("trSave");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(dp_fromDateSearch, MainWindow.resourcemanager.GetString("fromDate"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(dp_toDateSearch, MainWindow.resourcemanager.GetString("toDate"));
            //txt_exportDocsButton.Text = MainWindow.resourcemanager.GetString("docExport");
            //txt_uploadDocsButton.Text = MainWindow.resourcemanager.GetString("docUpload");
            //txt_contactInformation.Text = MainWindow.resourcemanager.GetString("trContactInformation");

            //MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_custlevel, MainWindow.resourcemanager.GetString("trLevelHint"));
            //txt_contactInformation.Text = MainWindow.resourcemanager.GetString("trContactInformation");
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_notes, MainWindow.resourcemanager.GetString("trNoteHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_address, MainWindow.resourcemanager.GetString("trAdressHint"));

            //   bookStss            
            dg_bookSts.Columns[0].Header = MainWindow.resourcemanager.GetString("trNo.");
            dg_bookSts.Columns[1].Header = MainWindow.resourcemanager.GetString("bookSystem");
            dg_bookSts.Columns[2].Header = MainWindow.resourcemanager.GetString("airlineFlightSys");
            dg_bookSts.Columns[3].Header = MainWindow.resourcemanager.GetString("trOffice");
            dg_bookSts.Columns[4].Header = MainWindow.resourcemanager.GetString("priceBeforTax");
            dg_bookSts.Columns[5].Header = MainWindow.resourcemanager.GetString("profits");
            dg_bookSts.Columns[6].Header = MainWindow.resourcemanager.GetString("currency");
            dg_bookSts.Columns[7].Header = MainWindow.resourcemanager.GetString("trDate");
         
            //dg_bookSts.Columns[3].Header = MainWindow.resourcemanager.GetString("trMobile");

            tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");
            tt_report.Content = MainWindow.resourcemanager.GetString("trPdf");
            tt_excel.Content = MainWindow.resourcemanager.GetString("trExcel");
            tt_preview.Content = MainWindow.resourcemanager.GetString("trPreview");
            tt_print.Content = MainWindow.resourcemanager.GetString("trPrint");
            //tt_count.Content = MainWindow.resourcemanager.GetString("trCount");


        }


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
                if (bookStss is null)
                    await RefreshBookStssList();
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
        private async void Tgl_isActive_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                if (bookStss is null)
                    await RefreshBookStssList();
                tgl_bookStsstate = 0;
                await Search();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Btn_clear_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                HelpClass.StartAwait(grid_main);
                Clear();
                await RefreshBookStssList();
                await Search();
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
                    bookSts = dg_bookSts.SelectedItem as BookSts;
                    //  this.DataContext = bookSts;
                    if (bookSts != null)
                    {
                      //  this.DataContext = bookSts;


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
                dp_toDateSearch.SelectedDate = null;
                dp_fromDateSearch.SelectedDate = null;
                tb_search.Text = "";
                await RefreshBookStssList();
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
            if (bookStss is null)
                await RefreshBookStssList();

            searchText = tb_search.Text.ToLower();

            parentPeriod = cb_duration.SelectedItem == null ? "" : (cb_duration.SelectedValue).ToString();
           // int sidevalId = (side != "paysys" && side != "other" && side != "") ? Convert.ToInt32(cb_sideValue.SelectedValue) : 0;
            childPeriod = ( cb_quarter.SelectedItem == null) ? "":(cb_quarter.SelectedValue).ToString() ;
            DateTime now = DateTime.Now;
            int currentYear = now.Year;
            bookStssQuery = bookStss.Where(s => (searchText == "" ? true :
            (
          (s.serviceNum == null ? false : (s.serviceNum.ToLower().Contains(searchText))) ||
          (s.systemName == null ? false : (s.systemName.ToLower().Contains(searchText))) ||
           (s.airline == null ? false : (s.airline.ToLower().Contains(searchText))) ||
             (s.officeName == null ? false : (s.officeName.ToLower().Contains(searchText)))          
            ))
            //&& (
            ////start date
            //((dp_fromDateSearch.SelectedDate != null || dp_fromDateSearch.Text != "") ? s.serviceDate == null ? false : (s.serviceDate.Value.Date >= dp_fromDateSearch.SelectedDate.Value.Date) : true)
            //&&
            ////end date
            //((dp_toDateSearch.SelectedDate != null || dp_toDateSearch.Text != "") ? s.serviceDate == null ? false : (s.serviceDate.Value.Date <= dp_toDateSearch.SelectedDate.Value.Date) : true)
            //)
            &&(
            childPeriod!=""?
            parentPeriod == "year" ? (((DateTime)s.updateDate).Year == Convert.ToInt32(childPeriod)) :
           (parentPeriod == "half" && ((DateTime)s.updateDate).Year == currentYear) ?(
           childPeriod == "1" ? (((DateTime)s.updateDate).Month >= 1 && ((DateTime)s.updateDate).Month <= 6) :
            childPeriod == "2" ? (((DateTime)s.updateDate).Month >= 7 && ((DateTime)s.updateDate).Month <= 12) :false
            ):
            (parentPeriod == "q" && ((DateTime)s.updateDate).Year == currentYear) ?
            (
            childPeriod == "1" ? (((DateTime)s.updateDate).Month >= 1 && ((DateTime)s.updateDate).Month <= 3) :
         childPeriod == "2" ? (((DateTime)s.updateDate).Month >= 4 && ((DateTime)s.updateDate).Month <= 6) :
           childPeriod == "3" ? (((DateTime)s.updateDate).Month >= 7 && ((DateTime)s.updateDate).Month <= 9) :
             childPeriod == "4" ? (((DateTime)s.updateDate).Month >= 10 && ((DateTime)s.updateDate).Month <= 12) :false
             ):
            ( parentPeriod == "month" && ((DateTime)s.updateDate).Year == currentYear )? (((DateTime)s.updateDate).Month == Convert.ToInt32(childPeriod)) :false
             : false
             //:false
            )
            );

            RefreshBookStssView();
            sumAll();
        }
        async Task<IEnumerable<BookSts>> RefreshBookStssList()
        {

            bookStss = await StatisticsModel.GetBookProfit(dp_fromDateSearch.SelectedDate,dp_toDateSearch.SelectedDate);

            return bookStss;
        }
        void RefreshBookStssView()
        {
            dg_bookSts.ItemsSource = bookStssQuery;
        }
        //public async Task fillcombos()
        //{

        //}
        IEnumerable<BookSts> yearList;
        private void fillYear()
        {
            yearList = bookStss.Where(g => g.createDate != null  ).GroupBy(g =>((DateTime) g.createDate).Year).Select(g => new BookSts { year = ((DateTime)g.FirstOrDefault().updateDate).Year , yearStr = (((DateTime)g.FirstOrDefault().updateDate).Year).ToString() }).ToList();
            cb_quarter.SelectedValuePath = "year";
            cb_quarter.DisplayMemberPath = "yearStr";
            cb_quarter.ItemsSource = yearList;
        }

        private decimal getsumpPrices()
        {
          sumpPrices =  bookStssQuery.Sum(s => (decimal)s.priceBeforTax);
//string balance = HelpClass.DecTostring(sumpPrices);
            return sumpPrices;
        }
        private decimal getsumpProfits()
        {
            sumpProfits= bookStssQuery.Sum(s => (decimal)s.profit);
            //string balance = HelpClass.DecTostring(sumpPrices);
            return sumpProfits;
        }
        private void sumAll()
        {
            getsumpPrices();
            getsumpProfits();

            txt_totalProfit.Text = HelpClass.DecTostring(sumpProfits);
            txt_totalSale.Text = HelpClass.DecTostring(sumpPrices);
 
        }

        #endregion

        #region validate - clearValidate - textChange - lostFocus - . . . . 

        void Clear()
        {
            //this.DataContext = new PaymentsSts();
            cb_duration.SelectedIndex = -1;
            cb_quarter.SelectedIndex = -1;
            


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
        //     SaveFileDialog saveFileDialog = new SaveFileDialog();

        public void BuildReport()
        {
          
            //string firstTitle = "paymentsReport";
            ////string secondTitle = "";
            ////string subTitle = "";
            //string Title = "";

            List<ReportParameter> paramarr = new List<ReportParameter>();

            string addpath;
            bool isArabic = ReportCls.checkLang();
            //if (isArabic)
            //{
            addpath = @"\Reports\statisticReports\Ar\ArBook.rdlc";

            //}
            //else
            //{
            //    addpath = @"\Reports\SectionData\En\EnBookStss.rdlc";
            //}
            //D:\myproj\posproject3\BookAccountApp\BookAccountApp\Reports\statisticReports\En\EnBook.rdlc
            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
            //     subTitle = clsReports.ReportTabTitle(firstTitle, secondTitle);
            string title = MainWindow.resourcemanagerreport.GetString("bookProfits") ;
            paramarr.Add(new ReportParameter("trTitle", title));
            clsReports.BookStsReport(bookStssQuery, rep, reppath, paramarr);
            clsReports.setReportLanguage(paramarr);
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


        #region VoucherReport
        public void BuildVoucherReport()
        {
            payopModel = payopModel.GetBookByserviceId((int)bookSts.serviceId);
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
            paramarr = reportclass.fillPayReport(payopModel);
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

        private async void Dp_fromDateSearch_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                //if (cb_custname.SelectedItem != null)
                //{

                //}
                await RefreshBookStssList();
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
                await RefreshBookStssList();
                await Search();

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }



        //private void Btn_invoicePrint_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void Btn_invoicePreview_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void Btn_invoicePdf_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private async void Cb_duration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (cb_duration.SelectedItem != null)
                {//passenger office soto other
                    if (cb_duration.SelectedValue.ToString()=="year")
                    {
                        fillYear();
                    }
                    else
                    {
                        FillCombo.fillPeriodchild(cb_quarter, cb_duration.SelectedValue.ToString());
                    }
                  
                }
                parentPeriod = cb_duration.SelectedItem == null ? "" : (cb_duration.SelectedValue).ToString();
                switch (parentPeriod)
                {

                    case "year": MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_quarter, MainWindow.resourcemanager.GetString("chooseYear")); break;
                    case "half": MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_quarter, MainWindow.resourcemanager.GetString("halfChoose")); break;
                    case "q": MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_quarter, MainWindow.resourcemanager.GetString("quarter1234")); break;
                    case "month": MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_quarter, MainWindow.resourcemanager.GetString("chooseMonth")); break;

                    default: MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_quarter, "..."); break;
                }
                //await RefreshBookStssList();
                //await Search();
            }
            catch (Exception ex)
            {

                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Cb_quarter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (cb_quarter.SelectedItem != null)
                {
                 
                }
                await RefreshBookStssList();
                await Search();

            }
            catch (Exception ex)
            {

                HelpClass.ExceptionMessage(ex, this);
            }
        }
    }
}
