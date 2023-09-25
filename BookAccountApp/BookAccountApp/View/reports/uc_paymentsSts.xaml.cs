﻿using BookAccountApp.ApiClasses;
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
    /// Interaction logic for uc_paymentsStssSts.xaml
    /// </summary>
    public partial class uc_paymentsSts : UserControl
    {
        public uc_paymentsSts()
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
        private static uc_paymentsSts _instance;
        public static uc_paymentsSts Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_paymentsSts();
                return _instance;
            }
        }

        SaveFileDialog saveFileDialog = new SaveFileDialog();
        PaymentsSts paymentsSts = new PaymentsSts();
        Statistics StatisticsModel = new Statistics();
        IEnumerable<PaymentsSts> paymentsStssQuery;
        IEnumerable<PaymentsSts> paymentsStss;
        IEnumerable<PaymentsSts> passengerList;
        IEnumerable<PaymentsSts> officeList;
        IEnumerable<PaymentsSts> paySysList;
        IEnumerable<PaymentsSts> bookSysList;
        //
        IEnumerable<PaymentsSts>payUsdList;
        IEnumerable<PaymentsSts> paySypList;
        IEnumerable<PaymentsSts> depositUsdList;
        IEnumerable<PaymentsSts> depositSypList;
        decimal sumpayUsd = 0;
        decimal sumpaySyp = 0;
        decimal sumdepositUsd = 0;
        decimal sumdepositSyp = 0;
        PayOp PayOpRow = new PayOp();
        byte tgl_paymentsStsstate;
        string searchText = "";
        public static List<string> requiredControlList;
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);

                requiredControlList = new List<string> { "",  };

                #region translate
               
                translate();
                #endregion


                Keyboard.Focus(cb_side);
                await RefreshPaymentsStssList();
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
            txt_active.Text = MainWindow.resourcemanager.GetString("trActive");
            txt_title.Text = MainWindow.resourcemanager.GetString("accountPerson");
            btn_for.Content = MainWindow.resourcemanager.GetString("for");
            btn_to.Content = MainWindow.resourcemanager.GetString("onhim");
            txt_totalPayTitle.Text = MainWindow.resourcemanager.GetString("totalPaid");
            txt_totalWorthyTitle.Text = MainWindow.resourcemanager.GetString("totalOperations");
            //officeSysAirline totalOperations totalPaid
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_side, MainWindow.resourcemanager.GetString("officeSysAirline"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_sideValue, MainWindow.resourcemanager.GetString("trCompanyName"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_recipient, MainWindow.resourcemanager.GetString("trRecepientHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_recivedFrom, MainWindow.resourcemanager.GetString("recivedFromHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_cash, MainWindow.resourcemanager.GetString("trCashHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(dp_opDate, MainWindow.resourcemanager.GetString("opDateHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_notes, MainWindow.resourcemanager.GetString("trNoteHint"));

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

            //   paymentsStss            
            dg_paymentsSts.Columns[0].Header = MainWindow.resourcemanager.GetString("trNo.");
            dg_paymentsSts.Columns[1].Header = MainWindow.resourcemanager.GetString("sideOrResponseble");
            dg_paymentsSts.Columns[2].Header = MainWindow.resourcemanager.GetString("recivedFrom");
            dg_paymentsSts.Columns[3].Header = MainWindow.resourcemanager.GetString("trRecepient");
     
            dg_paymentsSts.Columns[4].Header = MainWindow.resourcemanager.GetString("trCashTooltip");
            dg_paymentsSts.Columns[5].Header = MainWindow.resourcemanager.GetString("payDate");
            //dg_paymentsSts.Columns[3].Header = MainWindow.resourcemanager.GetString("trMobile");

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
                await RefreshPaymentsStssList();
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
                if (paymentsStss is null)
                    await RefreshPaymentsStssList();
                tgl_paymentsStsstate = 1;
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
                if (paymentsStss is null)
                    await RefreshPaymentsStssList();
                tgl_paymentsStsstate = 0;
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
              await  RefreshPaymentsStssList();
               await Search();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Dg_paymentsSts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                HelpClass.clearValidate(requiredControlList, this);
                //selection

                if (dg_paymentsSts.SelectedIndex != -1)
                {
                    paymentsSts = dg_paymentsSts.SelectedItem as PaymentsSts;
                    PayOpRow = JsonConvert.DeserializeObject<PayOp>(JsonConvert.SerializeObject(paymentsSts));
                    //  this.DataContext = paymentsSts;
                    if (paymentsSts != null)
                    {
                        //this.DataContext = paymentsSts;


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
                await RefreshPaymentsStssList();
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
            if (paymentsStss is null)
                await RefreshPaymentsStssList();

            searchText = tb_search.Text.ToLower();
            string side = cb_side.SelectedItem == null ? "" : (cb_side.SelectedValue).ToString();
            int sidevalId =( side != "paysys" && side != "other" && side != "") ? Convert.ToInt32(cb_sideValue.SelectedValue) : 0;
            string paysysValue = (side == "paysys" && cb_sideValue.SelectedItem != null )? (cb_sideValue.SelectedValue).ToString() : "";
            paymentsStssQuery = paymentsStss.Where(s =>( (searchText == "" ? true :
            (
          (s.code == null ? false : (s.code.ToLower().Contains(searchText))) ||
           (s.sideStr == null ? false : (s.sideStr.ToLower().Contains(searchText))) ||
             (s.recipient == null ? false : (s.recipient.ToLower().Contains(searchText))) ||
         (s.recivedFrom == null ? false : (s.recivedFrom.ToLower().Contains(searchText)))
            ))
            //&&
            //(
            ////start date
            //((dp_fromDateSearch.SelectedDate != null || dp_fromDateSearch.Text != "") ? s.opDate == null ? false : (s.opDate.Value.Date >= dp_fromDateSearch.SelectedDate.Value.Date) : true)
            //&&
            ////end date
            //((dp_toDateSearch.SelectedDate != null || dp_toDateSearch.Text != "") ? s.opDate == null ? false : (s.opDate.Value.Date <= dp_toDateSearch.SelectedDate.Value.Date) : true)
            //)
            )&&(
            (side == "passenger" && sidevalId>0 )? s.passengerId == sidevalId  && s.side == "passenger" :
           ( side == "office" && sidevalId > 0 )? s.officeId == sidevalId && s.side == "office":
            ( side == "system" && sidevalId > 0) ? s.systemId == sidevalId && s.side == "system":
             (side == "other"  )?  s.side == "other" :
            ( side == "paysys" && paysysValue !="" )?
             ((s.opType == "p" && (s.side == paysysValue)) ||
                            (s.opType == "p" && s.side == "system" && (s.fromSide == paysysValue) && s.processType == "book"))
             : false
             )
            );
            //(cb_side.SelectedValue).ToString() == "passenger"
            RefreshPaymentsStssView();
        }
        async Task<IEnumerable<PaymentsSts>> RefreshPaymentsStssList()
        {

             paymentsStss = await StatisticsModel.GetPayments(dp_fromDateSearch.SelectedDate,dp_toDateSearch.SelectedDate);

            return paymentsStss;
        }
        void RefreshPaymentsStssView()
        {
            dg_paymentsSts.ItemsSource = paymentsStssQuery;
        }
        public async Task fillcombos()
        {
            await FillCombo.fillpaySidereport(cb_side);
           
        }
        #region fill sideValues
        private void fillPassenger()
        {
            passengerList = paymentsStss.Where(g => g.passengerId != null && g.side == "passenger").GroupBy(g => g.passengerId).Select(g => new PaymentsSts { passengerId = g.FirstOrDefault().passengerId.Value, passenger = g.FirstOrDefault().passenger }).ToList();
            cb_sideValue.SelectedValuePath = "passengerId";
            cb_sideValue.DisplayMemberPath = "passenger";
            cb_sideValue.ItemsSource = passengerList;
        }
        private void fillOffice()
        {
            officeList = paymentsStss.Where(g => g.officeId != null && g.side == "office").GroupBy(g => g.officeId).Select(g => new PaymentsSts { officeId = g.FirstOrDefault().officeId.Value, officeName = g.FirstOrDefault().officeName }).ToList();
            cb_sideValue.SelectedValuePath = "officeId";
            cb_sideValue.DisplayMemberPath = "officeName";
            cb_sideValue.ItemsSource = officeList;
        }
        private void fillpaySys()
        {
            paySysList = paymentsStss.Where(S => (S.opType == "p" && (S.side == "soto" || S.side == "syr")) ||
                            (S.opType == "p" && S.side == "system" && (S.fromSide == "soto" || S.fromSide == "syr") && S.processType == "book"))
                .GroupBy(g => g.sideStr).Select(g => new PaymentsSts { sideStr = g.FirstOrDefault().sideStr, sideAr = g.FirstOrDefault().sideAr }).ToList();
            cb_sideValue.SelectedValuePath = "sideStr";
            cb_sideValue.DisplayMemberPath = "sideAr"; 
 

            cb_sideValue.ItemsSource = paySysList;
        }
        private void fillbookSys()
        {
            // d system cash (comm)
            //p company_commission
            //p system book

            bookSysList = paymentsStss.Where(S => S.systemId != null && (S.side == "system")
            ).GroupBy(g => g.systemId).Select(g => new PaymentsSts { systemId = g.FirstOrDefault().systemId.Value, systemName = g.FirstOrDefault().systemName }).ToList();
            cb_sideValue.SelectedValuePath = "systemId";
            cb_sideValue.DisplayMemberPath = "systemName";
            cb_sideValue.ItemsSource = bookSysList;
        }
        #endregion

        #region sum
        private void viewontoSumUsd(string side)
        {
            //txt_totalPay.Text = getTotalPayUsd(side);
            
        }
        private void viewTotalSum(string side)
        {
            //txt_totalWorthy.Text = getTotalPayments(side);
             
        }
        private decimal getTotalPayUsd(string side,string paysys)
        {
          //  string balance = "";
            decimal amount = 0;
            //  decimal amount = (decimal)FillCombo.PaySidesSysList.Where(p => p.code == code).FirstOrDefault().balance;
            if (side== "paysys")
            {
                amount = (decimal)paymentsStssQuery.Where(x => x.opType == "p" && x.side == paysys).Sum(s => s.cash);
            }
            else
            {
                amount = (decimal)paymentsStssQuery.Where(x => x.opType == "p" && x.currency == "usd").Sum(s => s.cash);
            }
            
           // balance = HelpClass.DecTostring(amount);
            return amount;
        }
        private decimal getTotalPaySyp(string side )
        {
            string balance = "";
            decimal amount = 0;
            //  decimal amount = (decimal)FillCombo.PaySidesSysList.Where(p => p.code == code).FirstOrDefault().balance;
            if (side == "paysys")
            {
                amount =0;
            }
            else
            {
                amount = (decimal)paymentsStssQuery.Where(x => x.opType == "p" && x.currency == "syp").Sum(s => s.cash);
            }

            balance = HelpClass.DecTostring(amount);
            return amount;
        }
        private decimal getTotalDepositUsd(string side)
        {
            //  string balance = "";
            decimal amount = 0;
            //  decimal amount = (decimal)FillCombo.PaySidesSysList.Where(p => p.code == code).FirstOrDefault().balance;
            if (side == "paysys")
            {
                amount = (decimal)paymentsStssQuery.Where(x => x.opType == "p" && x.side == "system").Sum(s => s.cash);
            }
            else
            {
                amount = (decimal)paymentsStssQuery.Where(x => x.opType == "d" && x.currency == "usd").Sum(s => s.cash);
            }

            // balance = HelpClass.DecTostring(amount);
            return amount;
        }
        private decimal getTotalDepositSyp(string side)
        {
            //  string balance = "";
            decimal amount = 0;
            //  decimal amount = (decimal)FillCombo.PaySidesSysList.Where(p => p.code == code).FirstOrDefault().balance;
            if (side == "paysys")
            {
                amount =0;
            }
            else
            {
                amount = (decimal)paymentsStssQuery.Where(x => x.opType == "d" && x.currency == "syp").Sum(s => s.cash);
            }

            // balance = HelpClass.DecTostring(amount);
            return amount;
        }
        private decimal getTotalUsd()
        {
            decimal amount = 0;
            amount = sumpayUsd - sumdepositUsd;
            return amount;
        }

        private decimal getTotalSyp()
        {
            decimal amount = 0;
            amount = sumpaySyp - sumdepositSyp;
            return amount;
        }
        #endregion
        #endregion

        #region validate - clearValidate - textChange - lostFocus - . . . . 

        void Clear()
        {
            //this.DataContext = new PaymentsSts();
            cb_side.SelectedIndex = -1;
            cb_sideValue.SelectedIndex = -1;
            brdr_name.Visibility = Visibility.Collapsed;
           
            
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
            //    addpath = @"\Reports\SectionData\En\EnPaymentsStss.rdlc";
            //}
            //D:\myproj\posproject3\BookAccountApp\BookAccountApp\Reports\statisticReports\En\EnBook.rdlc
            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
            //     subTitle = clsReports.ReportTabTitle(firstTitle, secondTitle);
            string title = MainWindow.resourcemanagerreport.GetString("book_sales") + " / " + MainWindow.resourcemanagerreport.GetString("syr");
            paramarr.Add(new ReportParameter("trTitle", title));
            clsReports.SaleReport(paymentsStssQuery, rep, reppath, paramarr);
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
                await RefreshPaymentsStssList();
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
                await RefreshPaymentsStssList();
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

        private void Btn_for_Click(object sender, RoutedEventArgs e)
        {
            #region
            btn_for.BorderBrush =
            btn_for.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;
            btn_to.BorderBrush =
            btn_to.Foreground = Application.Current.Resources["LightGrey"] as SolidColorBrush;
            #endregion
        }

        private void Btn_to_Click(object sender, RoutedEventArgs e)
        {
            #region
            btn_for.BorderBrush =
            btn_for.Foreground = Application.Current.Resources["LightGrey"] as SolidColorBrush;
            btn_to.BorderBrush =
            btn_to.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;
            #endregion
        }

        private async void Cb_side_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (cb_side.SelectedItem != null)
                {//passenger office soto other
                    if ((cb_side.SelectedValue).ToString() == "passenger")
                    {
                        MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_sideValue, MainWindow.resourcemanager.GetString("passenger"));

                        brdr_name.Visibility = Visibility.Visible;
                        fillPassenger();
                        //paymentsStssQuery = new List<PaymentsSts>();

                    }
                    else if ((cb_side.SelectedValue).ToString() == "office")
                    {
                        MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_sideValue, MainWindow.resourcemanager.GetString("trOffice"));

                        brdr_name.Visibility = Visibility.Visible;
                        fillOffice();
                        //paymentsStssQuery = new List<PaymentsSts>();
                    }
                    else if ((cb_side.SelectedValue).ToString() == "system")
                    {
                        MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_sideValue, MainWindow.resourcemanager.GetString("bookSystems"));

                        brdr_name.Visibility  = Visibility.Visible;
                        fillbookSys();
                        //paymentsStssQuery = new List<PaymentsSts>();
                    }
                    else if ((cb_side.SelectedValue).ToString() == "paysys")
                    {
                        cb_sideValue.SelectedItem = null;
                        brdr_name.Visibility = Visibility.Visible;
                        //cb_sideValue.Visibility = Visibility.Visible;
                       fillpaySys();
                        //paymentsStssQuery = new List<PaymentsSts>();

                    }
                    //else if ((cb_side.SelectedValue).ToString() == "syr")
                    //{
                    //    cb_sideValue.SelectedItem = null;
                    //    cb_sideValue.Visibility = Visibility.Collapsed;
                       

                    //}
                    //else if ((cb_side.SelectedValue).ToString() == "syr")
                    //{
                    //    cb_sideValue.SelectedItem = null;
                    //    grid_sideValue.Visibility = Visibility.Collapsed;
                    //    btn_invoicesSide.Visibility = Visibility.Visible;

                    //}
                    else
                    {
                        //other
                        cb_sideValue.SelectedItem = null;
                        brdr_name.Visibility = Visibility.Collapsed;

                      
                    }
                    

                }
                else
                {
                    cb_sideValue.SelectedItem = null;
                    brdr_name.Visibility = Visibility.Collapsed;
                    

                }
                await RefreshPaymentsStssList();
                await Search();
            }
            catch (Exception ex)
            {

                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Cb_sideValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (cb_sideValue.SelectedItem != null)
                {
                    if ((cb_side.SelectedValue).ToString() != "other")
                    {
                        await RefreshPaymentsStssList();
                        await Search();
                    }
                      
                    //passenger office soto other

                    //if ((cb_side.SelectedValue).ToString() == "passenger")
                    //{
                    //    MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_sideValue, MainWindow.resourcemanager.GetString("passenger"));

                    //    brdr_name.Visibility = Visibility.Visible;
                    //    fillPassenger();

                    //}
                    //else if ((cb_side.SelectedValue).ToString() == "office")
                    //{
                    //    MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_sideValue, MainWindow.resourcemanager.GetString("trOffice"));

                    //    brdr_name.Visibility = Visibility.Visible;
                    //    fillOffice();
                    //}
                    //else if ((cb_side.SelectedValue).ToString() == "system")
                    //{
                    //    MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_sideValue, MainWindow.resourcemanager.GetString("bookSystems"));

                    //    brdr_name.Visibility = Visibility.Visible;
                    //    fillbookSys();
                    //}
                    //else if ((cb_side.SelectedValue).ToString() == "paysys")
                    //{
                    //    cb_sideValue.SelectedItem = null;
                    //    brdr_name.Visibility = Visibility.Visible;
                    //    //cb_sideValue.Visibility = Visibility.Visible;
                    //    fillpaySys();


                    //}
                 
                    //else
                    //{
                    //    //other
                    //    cb_sideValue.SelectedItem = null;
                    //    brdr_name.Visibility = Visibility.Collapsed;


                    //}
                }
                else
                {
                  
                }

            }
            catch (Exception ex)
            {

                HelpClass.ExceptionMessage(ex, this);
            }
        }
    }
}
