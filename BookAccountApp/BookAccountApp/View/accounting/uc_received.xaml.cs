using BookAccountApp.ApiClasses;
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
        public List<ServiceData> serviceLst = new List<ServiceData>();
        public List<PayOp> cashesLst = new List<PayOp>();
        byte tgl_payOpstate;
        string searchText = "";
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        public static List<string> requiredControlList;
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);

                requiredControlList = new List<string> { "opName", "side", "cash", "opDate", "currency" };

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

                Keyboard.Focus(tb_opName);
             //  btn_invoices.Visibility = Visibility.Collapsed;
                Cb_side_SelectionChanged(null, null);
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
            txt_title.Text = MainWindow.resourcemanager.GetString("payInvoice");
            //payInvoice operationName sideOrResponseble trRecepient trRecepientHint recivedFrom trCashHint opDate trNoteHint printInvoice
            // previewInvoice electronicInvoice trSave operationNameHint trCashTooltip
            //payInvoiceHint sideOrResponsebleHint recivedFromHint  opDateHint trCash
            //
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_opName, MainWindow.resourcemanager.GetString("operationNameHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_side, MainWindow.resourcemanager.GetString("sideOrResponsebleHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_recipient, MainWindow.resourcemanager.GetString("trRecepientHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_recivedFrom, MainWindow.resourcemanager.GetString("recivedFromHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_cash, MainWindow.resourcemanager.GetString("trCashHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(dp_opDate, MainWindow.resourcemanager.GetString("opDateHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_notes, MainWindow.resourcemanager.GetString("trNoteHint"));

            txt_invoicePrintButton.Text = MainWindow.resourcemanager.GetString("printInvoice");
            txt_invoicePreviewButton.Text = MainWindow.resourcemanager.GetString("previewInvoice");
            txt_invoicePdfButton.Text = MainWindow.resourcemanager.GetString("electronicInvoice");
            btn_save.Content = MainWindow.resourcemanager.GetString("trSave");
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
            dg_payOp.Columns[0].Header = MainWindow.resourcemanager.GetString("trNo.");
            dg_payOp.Columns[1].Header = MainWindow.resourcemanager.GetString("sideOrResponseble");
            dg_payOp.Columns[2].Header = MainWindow.resourcemanager.GetString("trRecepient");
            dg_payOp.Columns[3].Header = MainWindow.resourcemanager.GetString("recivedFrom");
            dg_payOp.Columns[4].Header = MainWindow.resourcemanager.GetString("trCashTooltip");
            dg_payOp.Columns[5].Header = MainWindow.resourcemanager.GetString("currency");
            dg_payOp.Columns[6].Header = MainWindow.resourcemanager.GetString("payDate");
            //dg_payOp.Columns[3].Header = MainWindow.resourcemanager.GetString("trMobile");

            tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");
            tt_report.Content = MainWindow.resourcemanager.GetString("trPdf");
            tt_excel.Content = MainWindow.resourcemanager.GetString("trExcel");
            tt_preview.Content = MainWindow.resourcemanager.GetString("trPreview");
            tt_print.Content = MainWindow.resourcemanager.GetString("trPrint");
            //tt_count.Content = MainWindow.resourcemanager.GetString("trCount");

            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_currency, MainWindow.resourcemanager.GetString("currencyHint"));

        }
        #region Add - Update - Delete - Search - Tgl - Clear - DG_SelectionChanged - refresh

        private async void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                //   HelpClass.StartAwait(grid_main);

                payOp = new PayOp();
                if (HelpClass.validate(requiredControlList, this))
                {
                    //tb_custCode.Text = await serviceData.generateCodeNumber("cu");
                    payOp.opName = tb_opName.Text.Trim();
                    payOp.code = await payOp.generateNumber(("d" + (cb_side.SelectedValue).ToString().Substring(0, 3)).ToUpper());
                   
                    payOp.cash = (tb_cash.Text == null || tb_cash.Text == "") ? 0 : Convert.ToDecimal(tb_cash.Text);
                    payOp.opType = "d";
                    payOp.side = (cb_side.SelectedValue).ToString();
                    payOp.serviceId = null;//
                    payOp.opStatus = "draft";
                    payOp.opDate = dp_opDate.SelectedDate;
                    payOp.notes = tb_notes.Text;

                    //payOp.createDate = newObject.createDate;
                    payOp.updateDate = DateTime.Now;
                    if (cb_side.SelectedItem != null)
                    {//passenger office soto other
                        if ((cb_side.SelectedValue).ToString() == "passenger")
                        {

                            payOp.passengerId = Convert.ToInt32(cb_sideValue.SelectedValue);
                            payOp.paysideId = FillCombo.PaySidesList.Where(x => x.code == "passenger").FirstOrDefault().paysideId;
                        }
                        else if ((cb_side.SelectedValue).ToString() == "office")
                        {
                            payOp.officeId = Convert.ToInt32(cb_sideValue.SelectedValue);
                            payOp.paysideId = FillCombo.PaySidesList.Where(x => x.code == "office").FirstOrDefault().paysideId;
                        }
                        else if ((cb_side.SelectedValue).ToString() == "soto")
                        {
                            payOp.paysideId = FillCombo.PaySidesList.Where(x => x.code == "soto").FirstOrDefault().paysideId;
                            payOp.systemType = "soto";

                        }
                        else if ((cb_side.SelectedValue).ToString() == "syr")
                        {
                            payOp.paysideId = FillCombo.PaySidesList.Where(x => x.code == "syr").FirstOrDefault().paysideId;

                            payOp.systemType = "syr";
                        }
                        else if ((cb_side.SelectedValue).ToString() == "system")
                        {
                            payOp.paysideId = FillCombo.PaySidesList.Where(x => x.code == "system").FirstOrDefault().paysideId;
                            payOp.systemId = Convert.ToInt32(cb_sideValue.SelectedValue);

                        }
                        else if ((cb_side.SelectedValue).ToString() == "other")
                        {
                            //other
                            payOp.paysideId = FillCombo.PaySidesList.Where(x => x.code == "other").FirstOrDefault().paysideId;

                        }
                    }
                    else
                    {
                        payOp.paysideId = null;
                    }
                    payOp.userId = null;//note used
                    payOp.recipient = tb_recipient.Text;
                    payOp.recivedFrom = tb_recivedFrom.Text;
                    payOp.currency = cb_currency.SelectedValue == null ? "syp" : cb_currency.SelectedValue.ToString();//
                    payOp.syValue = FillCombo.exchangeValue;
                    payOp.exchangeId = FillCombo.ExchangeModel.exchangeId;
                    payOp.fromSide = "";

                    payOp.createUserId = MainWindow.userLogin.userId;
                    payOp.updateUserId = MainWindow.userLogin.userId;

                    decimal s = 0;
                    int msglist = 0;
                    if ((cb_side.SelectedValue).ToString() == "system"  )
                    {
                        if (cashesLst.Count > 0)
                        {
                            //  tb_cash.IsReadOnly = true;
                            //pay by list
                            s = await payOp.payListCommissionCashes(cashesLst, payOp);
                        }
                        else
                        {
                            msglist = 1;
                            Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("msgEmptyList"), animation: ToasterAnimation.FadeIn);

                        }
                    }
                    //else if ((cb_side.SelectedValue).ToString() == "system" && tb_cash.IsReadOnly == false)
                    //{
                    //    s = await payOp.payCompanyCommissionByAmount((int)payOp.systemId, (decimal)payOp.cash, payOp);
                    //}
                    else if ((cb_side.SelectedValue).ToString() == "passenger"  )
                    {
                        if (cashesLst.Count > 0)
                        {
                            //  tb_cash.IsReadOnly = true;
                            //pay by list
                            s = await payOp.payListCommissionCashes(cashesLst, payOp);
                        }
                        else
                        {
                            msglist = 1;
                            Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("msgEmptyList"), animation: ToasterAnimation.FadeIn);

                        }
                    }
                    //else if ((cb_side.SelectedValue).ToString() == "passenger" && tb_cash.IsReadOnly == false)
                    //{
                    //    s = await payOp.payPassengerByAmount((int)payOp.passengerId, (decimal)payOp.cash, payOp);
                    //}
                    else if ((cb_side.SelectedValue).ToString() == "office" )
                    {
                        if (cashesLst.Count > 0)
                        {
                            //  tb_cash.IsReadOnly = true;
                            //pay by list
                            s = await payOp.payListCommissionCashes(cashesLst, payOp);
                        }
                        else
                        {
                            msglist = 1;
                            Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("msgEmptyList"), animation: ToasterAnimation.FadeIn);

                        }


                    }
                    //else if ((cb_side.SelectedValue).ToString() == "office" && tb_cash.IsReadOnly == false)
                    //{
                    //    s = await payOp.payBookOfficeByAmount((int)payOp.officeId, (decimal)payOp.cash, payOp);
                    //}
                    else
                    {
                        s = await payOp.Save(payOp);
                    }

                    if (s <= 0 && msglist == 0)
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                    else if (msglist == 0)
                    {
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopAdd"), animation: ToasterAnimation.FadeIn);


                        Clear();
                        await RefreshPayOpsList();
                        await Search();
                    }
                }

                //      HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
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
        private   void Dg_payOp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                HelpClass.clearValidate(requiredControlList, this);
                //selection

                if (dg_payOp.SelectedIndex != -1)
                {
                    payOp = dg_payOp.SelectedItem as PayOp;
                    //  this.DataContext = payOp;
                    if (payOp != null)
                    {
                        //tb_custCode.Text = payOp.custCode;
                    
                         
                        this.DataContext = payOp;
                        cb_side.SelectedValue = payOp.side;

                        tb_cash.Text = HelpClass.DecTostring(payOp.cash);

                        btn_save.IsEnabled = false;
                        cb_side.IsEnabled = false;
                        cb_sideValue.IsEnabled = false;
                        tb_opName.IsEnabled = false;
                        btn_invoices.IsEnabled = false;
               
                        tb_cash.IsEnabled = false;
                        tb_notes.IsEnabled = false;
                        tb_recipient.IsEnabled = false;
                        tb_recivedFrom.IsEnabled = false;
                        dp_opDate.IsEnabled = false;
                        cb_currency.IsEnabled = false;
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

            searchText = tb_search.Text.ToLower();
            payOpsQuery = payOps.Where(s => (searchText == "" ? true :
            (
          (s.code == null ? false : (s.code.ToLower().Contains(searchText))) ||
           //(s.sideAr == null ? false : (s.sideAr.ToLower().Contains(searchText))) ||
             (s.recipient == null ? false : (s.recipient.ToLower().Contains(searchText))) ||
         (s.recivedFrom == null ? false : (s.recivedFrom.ToLower().Contains(searchText)))
            ))
            && (
            //start date
            ((dp_fromDateSearch.SelectedDate != null || dp_fromDateSearch.Text != "") ? s.opDate == null ? false : (s.opDate.Value.Date >= dp_fromDateSearch.SelectedDate.Value.Date) : true)
            &&
            //end date
            ((dp_toDateSearch.SelectedDate != null || dp_toDateSearch.Text != "") ? s.opDate == null ? false : (s.opDate.Value.Date <= dp_toDateSearch.SelectedDate.Value.Date) : true)
            )
            );

            RefreshPayOpsView();
        }
        async Task<IEnumerable<PayOp>> RefreshPayOpsList()
        {

            payOps = await payOp.GetbyType("d");
          //  payOps = payOps;

            return payOps;
        }
        void RefreshPayOpsView()
        {

            dg_payOp.ItemsSource = payOpsQuery;
            // txt_count.Text = payOpsQuery.Count().ToString();
        }
        public async Task fillcombos()
        {
            await FillCombo.fillpaySide(cb_side, "d");
            FillCombo.fillCurrency(cb_currency);
            //await FillCombo.fillPassengers(cb_passenger);
            //await FillCombo.fillFlights(cb_airline);
            //await FillCombo.fillOffice(cb_office);
            //await FillCombo.fillOperations(cb_operation);
            //
        }

        #endregion

        #region validate - clearValidate - textChange - lostFocus - . . . . 

        void Clear()
        {
            this.DataContext = new PayOp();
            tb_cash.IsReadOnly = false;
            tb_cash.Text = "";

            cb_side.SelectedIndex = -1;
            cb_sideValue.SelectedIndex = -1;
          
            btn_save.IsEnabled = true;
            cb_side.IsEnabled = true;
            cb_sideValue.IsEnabled = true;
            tb_opName.IsEnabled = true;
            btn_invoices.IsEnabled = true;
            
            tb_cash.IsEnabled = true;
            tb_notes.IsEnabled = true;
            tb_recipient.IsEnabled = true;
            tb_recivedFrom.IsEnabled = true;
            dp_opDate.IsEnabled = true;
            cb_currency.SelectedIndex = -1;
            cb_currency.IsEnabled = true;
            // last 
            HelpClass.clearValidate(requiredControlList, this);
        }
        void disableforAll()
        {
            cb_side.IsEnabled = false;
            cb_sideValue.IsEnabled = false;
            tb_cash.IsEnabled = false;         
            tb_cash.IsReadOnly = true;
            cb_currency.IsEnabled = false;
        }
        void disableforSystem()
        {           
            cb_currency.IsEnabled = false;
            cb_currency.SelectedValue = "usd";          
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
        ServiceData servicemodel = new ServiceData();
        ReportCls reportclass = new ReportCls();
        LocalReport rep = new LocalReport();
        //     SaveFileDialog saveFileDialog = new SaveFileDialog();

        //public void BuildReport()
        //{
        //    /*
        //    //string firstTitle = "paymentsReport";
        //    ////string secondTitle = "";
        //    ////string subTitle = "";
        //    //string Title = "";

        //    List<ReportParameter> paramarr = new List<ReportParameter>();

        //    string addpath;
        //    bool isArabic = ReportCls.checkLang();
        //    //if (isArabic)
        //    //{
        //    addpath = @"\Reports\Sale\ArSales.rdlc";

        //    //}
        //    //else
        //    //{
        //    //    addpath = @"\Reports\SectionData\En\EnPayOps.rdlc";
        //    //}
        //    //D:\myproj\posproject3\BookAccountApp\BookAccountApp\Reports\statisticReports\En\EnBook.rdlc
        //    string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
        //    //     subTitle = clsReports.ReportTabTitle(firstTitle, secondTitle);
        //    string title = MainWindow.resourcemanagerreport.GetString("book_sales") + " / " + MainWindow.resourcemanagerreport.GetString("syr");
        //    paramarr.Add(new ReportParameter("trTitle", title));
        //    clsReports.SaleReport(payOpsQuery, rep, reppath, paramarr);
        //    clsReports.setReportLanguage(paramarr);
        //    clsReports.Header(paramarr);

        //    rep.SetParameters(paramarr);

        //    rep.Refresh();
        //    */
        //}

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
            try
            {
                HelpClass.StartAwait(grid_main);

                //if (FillCombo.groupObject.HasPermissionAction(basicsPermission, FillCombo.groupObjects, "report"))
                //{
                #region
                Window.GetWindow(this).Opacity = 0.2;
                win_lvc win = new win_lvc(payOpsQuery, 7, false);
                win.ShowDialog();
                Window.GetWindow(this).Opacity = 1;
                #endregion
                //}
                //else
                //    Toaster.ShowInfo(Window.GetWindow(this), message: AppSettings.resourcemanager.GetString("trdontHavePermission"), animation: ToasterAnimation.FadeIn);

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                Window.GetWindow(this).Opacity = 1;
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

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
            addpath = @"\Reports\Account\Ar\Grid\Arpayment.rdlc";
            //}
            //else addpath = @"\Reports\Account\En\PayAccReport.rdlc";

            //filter
            startDate = dp_fromDateSearch.SelectedDate != null ? clsReports.dateFrameConverter(dp_fromDateSearch.SelectedDate) : "";
            endDate = dp_toDateSearch.SelectedDate != null ? clsReports.dateFrameConverter(dp_toDateSearch.SelectedDate) : "";
            Allchk = dp_fromDateSearch.SelectedDate == null && dp_toDateSearch.SelectedDate == null ? all : "";
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
            paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("trReceived")));
            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
            ReportCls.checkLang();
            //cashesQueryExcel = cashesQuery.ToList();
            clsReports.paymentAccReport(payOpsQuery, rep, reppath, paramarr);
            //clsReports.setReportLanguage(paramarr);
            clsReports.Header(paramarr);

            rep.SetParameters(paramarr);
            rep.Refresh();
        }

        #endregion


        #region reports Voucher

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
            paramarr = reportclass.fillPayReport(payOp);
            clsReports.Header(paramarr);
            rep.SetParameters(paramarr);
            rep.Refresh();
        }
        private  void Btn_invoicePrint_Click(object sender, RoutedEventArgs e)
        {
            //print
            try
            {
                HelpClass.StartAwait(grid_main);

                #region
                BuildVoucherReport();
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

        private void Btn_invoicePreview_Click(object sender, RoutedEventArgs e)
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

                #region
                BuildVoucherReport();

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

  

        private async void Cb_side_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (cb_side.SelectedItem != null)
                {//passenger office soto other
                    if ((cb_side.SelectedValue).ToString() == "passenger")
                    {
                        MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_sideValue, MainWindow.resourcemanager.GetString("thePassenger"));
                        await FillCombo.fillPassengers(cb_sideValue);
                        //  cb_sideValue.Visibility = Visibility.Visible;
                        brdr_sideValue.Visibility = Visibility.Visible;

                        if (payOp.payOpId > 0)
                        {
                            cb_sideValue.SelectedValue = payOp.passengerId;

                            cb_currency.SelectedValue = payOp.currency;

                        }
                        cb_currency.IsEnabled = true;

                    }
                    else if ((cb_side.SelectedValue).ToString() == "office")
                    {
                        MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_sideValue, MainWindow.resourcemanager.GetString("trOffice"));
                        await FillCombo.fillOffice(cb_sideValue);
                        brdr_sideValue.Visibility = Visibility.Visible;
                        if (payOp.payOpId > 0)
                        {
                            cb_sideValue.SelectedValue = payOp.officeId;
                            cb_currency.SelectedValue = payOp.currency;
                        }
                        cb_currency.IsEnabled = true;
                    }
                    else if ((cb_side.SelectedValue).ToString() == "system")
                    {
                        MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_sideValue, MainWindow.resourcemanager.GetString("bookSystems"));
                        await FillCombo.fillSystems(cb_sideValue);
                        brdr_sideValue.Visibility = Visibility.Visible;
                        if (payOp.payOpId > 0)
                        {
                            cb_sideValue.SelectedValue = payOp.systemId;
                            cb_currency.SelectedValue = payOp.currency;
                        }
                        cb_currency.IsEnabled = true;
                    }
                    else if ((cb_side.SelectedValue).ToString() == "soto")
                    {
                        cb_sideValue.SelectedItem = null;                    
                        cb_currency.SelectedValue = "usd";
                        cb_currency.IsEnabled = false;

                    }
                    else if ((cb_side.SelectedValue).ToString() == "syr")
                    {
                        cb_sideValue.SelectedItem = null;                   
                        cb_currency.SelectedValue = "usd";
                        cb_currency.IsEnabled = false;
                    }
                    else
                    {
                        //other
                        cb_sideValue.SelectedItem = null;
                        brdr_sideValue.Visibility = Visibility.Collapsed;
                        cb_currency.IsEnabled = true;
                        if (payOp.payOpId > 0)
                        {

                            cb_currency.SelectedValue = payOp.currency;
                        }
                    }
                }
                else
                {
                    cb_sideValue.SelectedItem = null;
                    brdr_sideValue.Visibility = Visibility.Collapsed;
                    cb_currency.IsEnabled = true;
                }

            }
            catch (Exception ex)
            {

                HelpClass.ExceptionMessage(ex, this);
            }

        }

        private void Btn_invoices_Click(object sender, RoutedEventArgs e)
        {//invoices
            try
            {

                serviceLst.Clear();
                cashesLst.Clear();

                if (cb_side.SelectedItem != null)
                {//passenger office soto other
                    Window.GetWindow(this).Opacity = 0.2;
                    wd_invoicesList w = new wd_invoicesList();
                    w.side = (cb_side.SelectedValue).ToString();
                    w.opType = "d";
                    if (cb_sideValue.SelectedItem != null)
                    {
                        w.selectedId = Convert.ToInt32(cb_sideValue.SelectedValue);
                    }
                    else
                    {
                        w.selectedId = 0;
                    }

                    w.ShowDialog();
                    if (w.isActive)
                    {
                        tb_cash.Text = HelpClass.DecTostring(w.sum);
                        //tb_cash.IsReadOnly = true;

                        //cb_recipientC.IsEnabled = false;
                        //cb_recipientV.IsEnabled = false;
                        //cb_recipientU.IsEnabled = false;
                        //cb_recipientSh.IsEnabled = false;
                        //tb_recipientText.IsEnabled = false;
                        serviceLst.AddRange(w.selectedInvoices);
                        cashesLst.AddRange(w.selectedCashtansfers);
                        if (cashesLst.Count > 0 || serviceLst.Count > 0)
                        {
                            cb_currency.SelectedValue = w.currency;
                            disableforAll();
                            if (w.side == "system")
                            {
                                disableforSystem();
                            }
                        }

                    }
                    Window.GetWindow(this).Opacity = 1;
                }

                //if (cb_side.SelectedValue.ToString() == "soto")
                //    w.agentId = Convert.ToInt32(cb_recipientV.SelectedValue);
                //else if (cb_depositTo.SelectedValue.ToString() == "c")
                //    w.agentId = Convert.ToInt32(cb_recipientC.SelectedValue);
                //else if (cb_depositTo.SelectedValue.ToString() == "u")
                //    w.userId = Convert.ToInt32(cb_recipientU.SelectedValue);
                //else if (cb_depositTo.SelectedValue.ToString() == "sh")
                //    w.shippingCompanyId = Convert.ToInt32(cb_recipientSh.SelectedValue);

                //w.invType = "pay";





            }
            catch (Exception ex)
            {
                Window.GetWindow(this).Opacity = 1;
                HelpClass.ExceptionMessage(ex, this);
            }
        }
    }
}
