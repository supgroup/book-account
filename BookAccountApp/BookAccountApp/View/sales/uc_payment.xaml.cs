using BookAccountApp.ApiClasses;
using BookAccountApp.Classes;
using Microsoft.Reporting.WinForms;
using Microsoft.Win32;
using netoaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
using System.IO;
using BookAccountApp.View.windows;

namespace BookAccountApp.View.sales
{
    /// <summary>
    /// Interaction logic for uc_payment.xaml
    /// </summary>
    public partial class uc_payment : UserControl
    {
        private static uc_payment _instance;
        public static uc_payment Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_payment();
                return _instance;
            }
        }
        public uc_payment()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }

        }

        public static List<string> requiredControlList;
        PackageUser packageUserModel = new PackageUser();
        PackageUser packageUser = new PackageUser();
        Packages package = new Packages();
        IEnumerable<PayOp> payOps;
        IEnumerable<PayOp> payOpQuery;
        PayOp payOp = new PayOp();
        decimal totalNet = 0;
        decimal discount = 0;
        public bool isFirstTime = true;
        public int cusID = 0, packuserID = 0;
        public decimal discount_ = 0;
        string searchText = "";
        public async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                if(sender != null)
                    HelpClass.StartAwait(grid_main);

                requiredControlList = new List<string> { "customer", "packageNumber" };

                #region translate
                if (MainWindow.lang.Equals("en"))
                {
                    MainWindow.resourcemanager = new ResourceManager("BookAccountApp.en_file", Assembly.GetExecutingAssembly());
                    grid_main.FlowDirection = FlowDirection.LeftToRight;
                }
                else
                {
                    MainWindow.resourcemanager = new ResourceManager("BookAccountApp.ar_file", Assembly.GetExecutingAssembly());
                    grid_main.FlowDirection = FlowDirection.RightToLeft;
                }
                translate();
                #endregion

                if (MainWindow.userLogin.type.Equals("ag"))
                    try { await FillCombo.fillCustomerByAgent(cb_customer, MainWindow.userLogin.userId); }
                    catch { await FillCombo.fillCustomerByAgent(cb_customer, MainWindow.userLogin.userId); }
                else
                    try { await FillCombo.fillCustomer(cb_customer); }
                    catch { await FillCombo.fillCustomer(cb_customer); }

                if (isFirstTime)
                    Clear();
                else
                {
                    cb_customer.SelectedValue = cusID;
                    if (!MainWindow.userLogin.type.Equals("ag"))
                        try { await FillCombo.fillBookNum(cb_packageNumber, (int)cb_customer.SelectedValue); }
                        catch { await FillCombo.fillBookNum(cb_packageNumber, (int)cb_customer.SelectedValue); }
                    else
                        try { await FillCombo.fillBookNumAgent(cb_packageNumber, (int)cb_customer.SelectedValue, MainWindow.userLogin.userId); }
                        catch { await FillCombo.fillBookNumAgent(cb_packageNumber, (int)cb_customer.SelectedValue, MainWindow.userLogin.userId); }
                    cb_packageNumber.SelectedValue = packuserID;

                    tb_discount.IsEnabled = true;

                    btn_upgrade.IsEnabled = true;

                    packageUser = cb_packageNumber.SelectedItem as PackageUser;

                    txt_period.Visibility = Visibility.Visible;

                    int index = packageUser.packageNumber.IndexOf("     ");
                    string s = packageUser.packageNumber.Substring(0, index);
                    txt_packageNumberTitle.Text = s;

                    grid_packageDetails.DataContext = packageUser;
                    grid_payDetails.DataContext = packageUser;

                    tb_discount.Text = discount_.ToString();


                    #region fill Package details
                    if (packageUser != null)
                    {
                        try
                        {
                            txt_total.Text = (decimal.Parse(txt_price.Text) - discount_).ToString();
                        }
                        catch { }
                    }
                    #endregion

                    isFirstTime = true;
                }
                if(sender != null)
                    HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                if(sender != null)
                    HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        #region methods
        private void translate()
        {
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, MainWindow.resourcemanager.GetString("trSearchHint"));
            txt_baseInformation.Text = MainWindow.resourcemanager.GetString("trBaseInformation");
            txt_title.Text = MainWindow.resourcemanager.GetString("trPayment");
            txt_packageDetails.Text = MainWindow.resourcemanager.GetString("trPackageDetails");
            txt_payDetails.Text = MainWindow.resourcemanager.GetString("trPayDetails");

            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_customer, MainWindow.resourcemanager.GetString("trCustomerHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_packageNumber, MainWindow.resourcemanager.GetString("trBookNumHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_discount, MainWindow.resourcemanager.GetString("trDiscountHint"));

            txt_packageNameTitle.Text = MainWindow.resourcemanager.GetString("trPackageName");
            txt_periodTitle.Text = MainWindow.resourcemanager.GetString("trPeriod");
            txt_expirDateTitle.Text = MainWindow.resourcemanager.GetString("trExpirationDate");
            txt_priceTitle.Text = MainWindow.resourcemanager.GetString("trPrice");
            txt_discountTitle.Text = MainWindow.resourcemanager.GetString("trDiscount");
            txt_totalTitle.Text = MainWindow.resourcemanager.GetString("trTotal");

            btn_pay.Content = MainWindow.resourcemanager.GetString("trPay");

            dg_payments.Columns[0].Header = MainWindow.resourcemanager.GetString("trProcessNumTooltip");
            dg_payments.Columns[1].Header = MainWindow.resourcemanager.GetString("trBookNum");
            dg_payments.Columns[2].Header = MainWindow.resourcemanager.GetString("trPackage");
            dg_payments.Columns[3].Header = MainWindow.resourcemanager.GetString("trProcessDate");
            dg_payments.Columns[4].Header = MainWindow.resourcemanager.GetString("trExpirationDate");
            dg_payments.Columns[5].Header = MainWindow.resourcemanager.GetString("trPaid");

            tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");
            tt_refresh.Content = MainWindow.resourcemanager.GetString("trRefresh");
            tt_report.Content = MainWindow.resourcemanager.GetString("trPdf");
            tt_print.Content = MainWindow.resourcemanager.GetString("trPrint");
            tt_excel.Content = MainWindow.resourcemanager.GetString("trExcel");
            tt_count.Content = MainWindow.resourcemanager.GetString("trCount");
        }

        void clearValidate()
        {
            try
            {
                foreach (var control in requiredControlList)
                {
                    System.Windows.Shapes.Path path = FindControls.FindVisualChildren<System.Windows.Shapes.Path>(this).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (path != null)
                        HelpClass.clearValidate(path);
                }
            }
            catch { }
        }

        void Clear()
        {
            cb_customer.SelectedIndex = -1;
            dg_payments.ItemsSource = null;
            grid_packageDetails.DataContext = null;

            clearValidate();
        }

        void ClearPackageUser()
        {
            cb_packageNumber.SelectedIndex = -1;
            txt_packageNumberTitle.Text = "";

            tb_discount.Text = "0";
            tb_discount.IsEnabled = false;

            txt_total.Text = "0";

            txt_period.Visibility = Visibility.Hidden;

            grid_packageDetails.DataContext = new PackageUser();
            grid_payDetails.DataContext = new PackageUser();

            clearValidate();
        }

        bool validate()
        {
            bool isValid = true;
            try
            {
                foreach (var control in requiredControlList)
                {
                    TextBox textBox = FindControls.FindVisualChildren<TextBox>(this).Where(x => x.Name == "tb_" + control)
                        .FirstOrDefault();
                    System.Windows.Shapes.Path path = FindControls.FindVisualChildren<System.Windows.Shapes.Path>(this).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    Border border = FindControls.FindVisualChildren<Border>(this).Where(x => x.Name == "brd_" + control)
                         .FirstOrDefault();
                    if (textBox != null && path != null)
                        if (!HelpClass.validateEmpty(textBox.Text, path))
                            isValid = false;
                }
                foreach (var control in requiredControlList)
                {
                    ComboBox comboBox = FindControls.FindVisualChildren<ComboBox>(this).Where(x => x.Name == "cb_" + control)
                        .FirstOrDefault();
                    System.Windows.Shapes.Path path = FindControls.FindVisualChildren<System.Windows.Shapes.Path>(this).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    Border border = FindControls.FindVisualChildren<Border>(this).Where(x => x.Name == "brd_" + control)
                         .FirstOrDefault();
                    if (comboBox != null && path != null)
                        if (!HelpClass.validateEmpty(comboBox.Text, path))
                            isValid = false;
                }
            }
            catch { }
            return isValid;
        }

        #endregion

        private async void Btn_refresh_Click(object sender, RoutedEventArgs e)
        {//refresh
            try
            {
                HelpClass.StartAwait(grid_main);

                await RefreshPayOpList();
                await Search();

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {//search
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
        private void Btn_clear_Click(object sender, RoutedEventArgs e)
        {//clear
            try
            {
                HelpClass.StartAwait(grid_main);

                Clear();
                ClearPackageUser();

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        #region validate - clearValidate - textChange - lostFocus - . . . . 

        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        { //only  digits
            try
            {
                TextBox textBox = sender as TextBox;
                HelpClass.InputJustNumber(ref textBox);
                Regex regex = new Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
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
        private void ValidateEmpty_TextChange(object sender, TextChangedEventArgs e)
        {
            try
            {
                validate();
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
                validate();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        #endregion

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            GC.Collect();
        }

        #region reports

        ReportCls reportclass = new ReportCls();
        LocalReport rep = new LocalReport();
        SaveFileDialog saveFileDialog = new SaveFileDialog();

        public void BuildReport()
        {

            //string firstTitle = "paymentsReport";
            ////string secondTitle = "";
            ////string subTitle = "";
            //string Title = "";

            List<ReportParameter> paramarr = new List<ReportParameter>();

            string addpath;
            bool isArabic = ReportCls.checkLang();
            if (isArabic)
            {
                addpath = @"\Reports\Sale\Payments\Ar\ArPayments.rdlc";
            }
            else
            {
                addpath = @"\Reports\Sale\Payments\En\EnPayments.rdlc";
            }

            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
            //     subTitle = clsReports.ReportTabTitle(firstTitle, secondTitle);
            //  Title = MainWindow.resourcemanagerreport.GetString("trAccountantReport");

            clsReports.PaymentsSale(payOps, rep, reppath, paramarr);
            paramarr.Add(new ReportParameter("customerCompany", cb_customer.Text));
            clsReports.setReportLanguage(paramarr);
            clsReports.Header(paramarr);

            rep.SetParameters(paramarr);

            rep.Refresh();

        }

        private void Btn_pdf_Click(object sender, RoutedEventArgs e)
        {//pdf
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
        {//preview
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
        {//print
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
        {//excel
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

        //pay preview

        public async void  printOnPay()
        {
            if (packageUser.packageUserId > 0)
            {
                
                    packUserRep =await  packUserRep.GetByID(packageUser.packageUserId);

                 await GetPrintdata();
                
                #region
                BuildPayReport(0);

                this.Dispatcher.Invoke(() =>
                {
                    LocalReportExtensions.PrintToPrinterbyNameAndCopy(rep, FillCombo.getdefaultPrinters(), 1);
                });
              //  return "1";
                #endregion
            }
            else
            {
                //return "0";
            }
        }
        public async void Previewpayoprow(int payOpId)
        {
            if (payOpId > 0)
            {
                PayOpModel = new PayOp();
                PayOpModel = await PayOpModel.GetByID(payOpId);

                await GetPayrowdata();

                Window.GetWindow(this).Opacity = 0.2;
                string pdfpath = "";
                //
                pdfpath = @"\Thumb\report\temp.pdf";
                pdfpath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, pdfpath);
                BuildPayReport(1);

                LocalReportExtensions.ExportToPDF(rep, pdfpath);
                wd_previewPdf w = new wd_previewPdf();
                w.pdfPath = pdfpath;
                if (!string.IsNullOrEmpty(w.pdfPath))
                {
                    w.ShowDialog();
                    w.wb_pdfWebViewer.Dispose();
                }
                Window.GetWindow(this).Opacity = 1;

            }
        }
        public void BuildPayReport(int isCopy)
        {
            //string firstTitle = "paymentsReport";
            ////string secondTitle = "";
            ////string subTitle = "";
            //string Title = "";

            List<ReportParameter> paramarr = new List<ReportParameter>();

            string addpath;
            bool isArabic = ReportCls.checkLang();
            if (isArabic)
            {
                addpath = @"\Reports\Sale\Payments\Ar\ArPayDetail.rdlc";
            }
            else
            {
                addpath = @"\Reports\Sale\Payments\En\EnPayDetail.rdlc";
            }

            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
            //     subTitle = clsReports.ReportTabTitle(firstTitle, secondTitle);
            //  Title = MainWindow.resourcemanagerreport.GetString("trAccountantReport");

            //   clsReports.PaymentsPaySale(paramarr);
            List<PayOp> tempQuery = new List<PayOp>();
            clsReports.PaymentsPaySale(tempQuery, rep, reppath);
            clsReports.setReportLanguage(paramarr);
            clsReports.Header(paramarr);
            SetPayparam(paramarr);
            paramarr.Add(new ReportParameter("isCopy",isCopy.ToString()));//
            rep.SetParameters(paramarr);

            rep.Refresh();



        }

        //end pay preview
        public void SetPayparam(List<ReportParameter> paramarr)
        {
            paramarr.Add(new ReportParameter("trBookNum", MainWindow.resourcemanagerreport.GetString("trBookNum")));
            paramarr.Add(new ReportParameter("trTitle", MainWindow.resourcemanagerreport.GetString("trPayDetail")));
            paramarr.Add(new ReportParameter("trAgentHint", MainWindow.resourcemanagerreport.GetString("trAgent")));//
            paramarr.Add(new ReportParameter("trCustomerHint", MainWindow.resourcemanagerreport.GetString("trCustomer")));//
            paramarr.Add(new ReportParameter("trPeriod", MainWindow.resourcemanagerreport.GetString("trPeriod")));//
            paramarr.Add(new ReportParameter("trName", MainWindow.resourcemanagerreport.GetString("trPackage")));//
            paramarr.Add(new ReportParameter("trPrice", MainWindow.resourcemanagerreport.GetString("trPrice")));//
            paramarr.Add(new ReportParameter("trExpirationDate", MainWindow.resourcemanagerreport.GetString("trExpirationDate")));//
            paramarr.Add(new ReportParameter("packageNumber", packUserRep.packageNumber.ToString()));//
            paramarr.Add(new ReportParameter("Agent", FillCombo.AgentNameConv(agentmodel)));//
            paramarr.Add(new ReportParameter("Customer", cumstomerModel.company));//
            paramarr.Add(new ReportParameter("Period", FillCombo.PeriodConv(CountryPackageDateModel)));//
            paramarr.Add(new ReportParameter("Name", PackagesModel.packageName));//
            paramarr.Add(new ReportParameter("Price", CountryPackageDateModel.price.ToString()));//
      
            paramarr.Add(new ReportParameter("ExpirationDate", FillCombo.DateConvert(PayOpModel.expireDate)));//

            paramarr.Add(new ReportParameter("trProcessNumTooltip", MainWindow.resourcemanagerreport.GetString("trProcessNumTooltip")));//
            paramarr.Add(new ReportParameter("PayNo", PayOpModel.code));//
            paramarr.Add(new ReportParameter("trSoftware", MainWindow.resourcemanagerreport.GetString("trSoftware")));//
            paramarr.Add(new ReportParameter("software", PackagesModel.programName + " " + PackagesModel.verName));//
            paramarr.Add(new ReportParameter("trDiscount", MainWindow.resourcemanagerreport.GetString("trDiscount")));//
            paramarr.Add(new ReportParameter("trTotal", MainWindow.resourcemanagerreport.GetString("trTotal")));//
            paramarr.Add(new ReportParameter("trPaid", MainWindow.resourcemanagerreport.GetString("trPaid")));//
            paramarr.Add(new ReportParameter("Discount", reportclass.DecTostring(PayOpModel.discountValue)));//
            paramarr.Add(new ReportParameter("Total", reportclass.DecTostring(PayOpModel.totalnet)));//
            paramarr.Add(new ReportParameter("currency", CountryPackageDateModel.currency));//

            paramarr.Add(new ReportParameter("Notes", PayOpModel.notes));

            paramarr.Add(new ReportParameter("trPayDate", MainWindow.resourcemanagerreport.GetString("trPayDate")));//trPayDate
            paramarr.Add(new ReportParameter("PayDate", FillCombo.DateConvert(PayOpModel.createDate)));//
        }

        #endregion

        #region email

        PackageUser packUserRep = new PackageUser();
        Users agentmodel = new Users();
        Customers cumstomerModel = new Customers();
        CountryPackageDate CountryPackageDateModel = new CountryPackageDate();
        // Country CountryModel = new Country();
        Packages PackagesModel = new Packages();
        PayOp PayOpModel = new PayOp();
        List<SetValues> SetValuesList = new List<SetValues>();
        SetValues terms = new SetValues();
        string result = "0";
        PosSerials posSerialModel = new PosSerials();
        SysEmails email = new SysEmails();
        EmailClass mailtosend = new EmailClass();

        SetValues setvmodel = new SetValues();
        List<SetValues> setvlist = new List<SetValues>();
        List<SetValues> setvUpgradelist = new List<SetValues>();
        List<PosSerials> repserialList = new List<PosSerials>();
        public async Task<string> getdata()
        {
            PayOpModel = new PayOp();
            PayOpModel = await PayOpModel.getLastPayOp(packUserRep.packageUserId);
            if (PayOpModel.payOpId <= 0)
            {
                return "0";
            }
            else
            {

                agentmodel = await agentmodel.GetByID((int)packUserRep.userId);

                cumstomerModel = await cumstomerModel.GetByID((int)packUserRep.customerId);

                CountryPackageDateModel = await CountryPackageDateModel.GetByID((int)PayOpModel.countryPackageId);
                PackagesModel = await PackagesModel.GetByID((int)CountryPackageDateModel.packageId);
                SetValuesList = await terms.GetBySetName("sale_note");
                terms = SetValuesList.FirstOrDefault();
                email = await email.GetByBranchIdandSide("sales");
                setvlist = await setvmodel.GetBySetName("sale_email_temp");
                setvUpgradelist = await setvmodel.GetBySetName("upgrade_email_temp");
                repserialList = await posSerialModel.GetByPackUserId(packUserRep.packageUserId);
                //  CountryPackageDateModel.monthCount;
                return "1";
            }

        }
        public async Task<string> GetPrintdata()
        {
            PayOpModel = new PayOp();
            PayOpModel = await PayOpModel.getLastPayOp(packUserRep.packageUserId);
            if (PayOpModel.payOpId <= 0)
            {
                return "0";
            }
            else
            {

                agentmodel = await agentmodel.GetByID((int)packUserRep.userId);

                cumstomerModel = await cumstomerModel.GetByID((int)packUserRep.customerId);

                CountryPackageDateModel = await CountryPackageDateModel.GetByID((int)PayOpModel.countryPackageId);
                PackagesModel = await PackagesModel.GetByID((int)CountryPackageDateModel.packageId);
        
              //  terms = SetValuesList.FirstOrDefault();

                //  CountryPackageDateModel.monthCount;
                return "1";
            }

        }
        public async Task<string> GetPayrowdata()
        {
            if (PayOpModel.packageUserId > 0)
            {
                packUserRep = await packUserRep.GetByID((int)PayOpModel.packageUserId);
                agentmodel = await agentmodel.GetByID((int)packUserRep.userId);

                cumstomerModel = await cumstomerModel.GetByID((int)packUserRep.customerId);

                CountryPackageDateModel = await CountryPackageDateModel.GetByID((int)PayOpModel.countryPackageId);
                PackagesModel = await PackagesModel.GetByID((int)CountryPackageDateModel.packageId);
            }

            //  CountryPackageDateModel.monthCount;
            return "1";
        }
        public async void   sendsaleEmail(int packageUserId)
        {
            try
            {
                if ((packageUserId > 0))
                {
                    packUserRep = await packUserRep.GetByID(packageUser.packageUserId);
                    await getdata();
                    //

                    {

                        if (cumstomerModel == null || cumstomerModel.custId == 0)
                        {

                            //edit warning message to customer
                            this.Dispatcher.Invoke(() =>
                            {
                                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trTheCustomerHasNoEmail"), animation: ToasterAnimation.FadeIn);
                            });

                        }
                        else
                        {
                            //  int? itemcount = invoiceItems.Count();
                            if (email.emailId == 0)
                                this.Dispatcher.Invoke(() =>
                                {
                                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trNoEmailForThisDept"), animation: ToasterAnimation.FadeIn);
                                });
                            else
                            {

                                {

                                    {

                                        if (cumstomerModel.email.Trim() == "")
                                        {
                                            this.Dispatcher.Invoke(() =>
                                            {
                                                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trTheCustomerHasNoEmail"), animation: ToasterAnimation.FadeIn);
                                            });
                                        }

                                        else
                                        {
                                            string msg = "";

                                            if (PayOpModel.type == "np" || PayOpModel.type == "chpr" || PayOpModel.type == "chpk")
                                            {
                                                //send upgrade email

                                                string pdfpath = "";
                                                pdfpath =  Saveupgradepdf();

                                                mailtosend = mailtosend.fillUpgradeTempData(packUserRep, PayOpModel, CountryPackageDateModel, PackagesModel, agentmodel, email, cumstomerModel, setvUpgradelist, terms);

                                                mailtosend.AddAttachTolist(pdfpath);

                                                this.Dispatcher.Invoke(() =>
                                                {
                                                    msg = mailtosend.Sendmail();// temp comment
                                                    if (msg == "Failure sending mail.")
                                                    {
                                                        // msg = "No Internet connection";

                                                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trNoMailConnection"), animation: ToasterAnimation.FadeIn);
                                                    }
                                                    else if (msg == "mailsent")
                                                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trMailSent"), animation: ToasterAnimation.FadeIn);
                                                    else
                                                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trMailNotSent"), animation: ToasterAnimation.FadeIn);
                                                });
                                            }
                                            /////
                                            mailtosend = mailtosend.fillSaleTempData(packUserRep, PayOpModel, CountryPackageDateModel, PackagesModel, agentmodel, email, cumstomerModel, setvlist);

                                            msg = "";
                                            this.Dispatcher.Invoke(() =>
                                            {
                                                msg = mailtosend.Sendmail();// temp comment
                                                if (msg == "Failure sending mail.")
                                                {
                                                    // msg = "No Internet connection";

                                                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trNoMailConnection"), animation: ToasterAnimation.FadeIn);
                                                }
                                                else if (msg == "mailsent")
                                                    Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trMailSent"), animation: ToasterAnimation.FadeIn);
                                                else
                                                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trMailNotSent"), animation: ToasterAnimation.FadeIn);
                                            });



                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                else
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trThereIsNoBook"), animation: ToasterAnimation.FadeIn);
                    });
                }


                //

            }
            catch (Exception ex)
            {
                this.Dispatcher.Invoke(() =>
                {
                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trEmailNotSend"), animation: ToasterAnimation.FadeIn);
                });
            }
           
        }

        public  void  BuildupgradeReport()
        {

            List<ReportParameter> paramarr = new List<ReportParameter>();

            string addpath = "";
            string firstTitle = "Serials";
            string secondTitle = "";
            string subTitle = "";
            string Title = "";

            bool isArabic = ReportCls.checkLang();

            addpath = @"\Reports\Sale\Book\Serials\En\serialsmail.rdlc";

            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
            PackageUser puModel = new PackageUser();
            Customers cuModel = new Customers();
            Users agModel = new Users();

            puModel = packUserRep;
            cuModel = cumstomerModel;
            agModel = agentmodel;
            string serkey = puModel.packageSaleCode;
            string agentname = "";

            agentname = FillCombo.AgentNameConv(agModel);


            //ReportCls.checkLang();
            //subTitle = clsReports.ReportTabTitle(firstTitle, secondTitle);
            Title = "Serials";
            paramarr.Add(new ReportParameter("trTitle", Title));

            paramarr.Add(new ReportParameter("Agent", agentname));
            paramarr.Add(new ReportParameter("Customer", cuModel.company));
            paramarr.Add(new ReportParameter("serverKey", serkey));
            paramarr.Add(new ReportParameter("ExpirationDate", FillCombo.DateConvert(PayOpModel.expireDate)));
            paramarr.Add(new ReportParameter("packageNumber", packUserRep.packageNumber.ToString()));
            paramarr.Add(new ReportParameter("trExpirationDate", MainWindow.resourcemanagerreport.GetString("trExpirationDate")));
            paramarr.Add(new ReportParameter("trBookNum", MainWindow.resourcemanagerreport.GetString("trBookNum")));

         //   List<PosSerials> repserialList = new List<PosSerials>();

         //   repserialList = await posSerialModel.GetByPackUserId(packUserRep.packageUserId);
            clsReports.serialsMailReport(repserialList.Where(s => s.isActive == 1), rep, reppath, paramarr);
            clsReports.setReportLanguage(paramarr);
            clsReports.Header(paramarr);

            rep.SetParameters(paramarr);

            rep.Refresh();
             
        }
        public  string Saveupgradepdf()
        {
            //for email
            List<ReportParameter> paramarr = new List<ReportParameter>();
            string pdfpath = "";
            //
            if ((packUserRep.packageUserId <= 0))
            {
                this.Dispatcher.Invoke(() =>
                {
                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trThereIsNoBook"), animation: ToasterAnimation.FadeIn);
                });
            }
            else
            {

                ////////////////////////
                string folderpath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, pdfpath) + @"\Thumb\report\";
                ReportCls.clearFolder(folderpath);

                pdfpath = @"\Thumb\report\File" + DateTime.Now.ToFileTime().ToString() + ".pdf";
                pdfpath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, pdfpath);
                 BuildupgradeReport();
                this.Dispatcher.Invoke(() =>
                {
                    LocalReportExtensions.ExportToPDF(rep, pdfpath);
                });

            }
            return pdfpath;
        }
        #endregion

        PayOp payOpModel = new PayOp();
        private async void Cb_customer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//select customer
            try
            {
                HelpClass.StartAwait(grid_main);

                if (cb_customer.SelectedIndex != -1)
                {
                    if (isFirstTime)
                    {
                        cb_packageNumber.SelectedIndex = -1;
                        cb_packageNumber.IsEnabled = true;
                        tb_discount.IsEnabled = true;
                        //this.DataContext = cb_customer.SelectedItem as PayOp;
                        if (!MainWindow.userLogin.type.Equals("ag"))
                            await FillCombo.fillBookNum(cb_packageNumber, (int)cb_customer.SelectedValue);
                        else
                            await FillCombo.fillBookNumAgent(cb_packageNumber, (int)cb_customer.SelectedValue, MainWindow.userLogin.userId);

                        if (cb_packageNumber.Items.Count > 0)
                            btn_upgrade.IsEnabled = true;
                        else
                            btn_upgrade.IsEnabled = false;
                    }
                    await RefreshPayOpList();
                    await Search();
                }

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }


        async Task<IEnumerable<PayOp>> RefreshPayOpList()
        {
            payOps = await payOpModel.GetByCustomerId((int)cb_customer.SelectedValue);
            return payOps;
        }
        void RefreshPayOpView()
        {//view
            dg_payments.ItemsSource = payOps;
            txt_count.Text = payOpQuery.Count().ToString();
        }
        async Task Search()
        {
            if (payOps is null)
                await RefreshPayOpList();

            searchText = tb_search.Text.ToLower();
            payOpQuery = payOps.Where(s => (s.code.ToLower().Contains(searchText) ||
                                            s.packageNumber.ToLower().Contains(searchText) ||
                                            s.updateDate.ToString().ToLower().Contains(searchText) ||
                                            s.expireDate.ToString().ToLower().Contains(searchText) ||
                                            s.totalnet.ToString().ToLower().Contains(searchText)
            ));

            RefreshPayOpView();
        }


        private async void Cb_packageNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//select package
            try
            {
                HelpClass.StartAwait(grid_main);

                if ((cb_packageNumber.SelectedIndex != -1) && (isFirstTime))
                {
                    tb_discount.Text = "0";

                    tb_discount.IsEnabled = true;

                    btn_upgrade.IsEnabled = true;

                    packageUser = cb_packageNumber.SelectedItem as PackageUser;

                    txt_period.Visibility = Visibility.Visible;

                    int index = packageUser.packageNumber.IndexOf("     ");
                    string s = packageUser.packageNumber.Substring(0, index);
                    txt_packageNumberTitle.Text = s;

                    grid_packageDetails.DataContext = packageUser;
                    grid_payDetails.DataContext = packageUser;

                    #region fill Package details
                    if (packageUser != null)
                    {
                        txt_total.Text = txt_price.Text;
                    }
                    #endregion

                }
                if(cb_packageNumber.SelectedIndex == -1)
                {
                    tb_discount.Text = "0";

                    tb_discount.IsEnabled = true;

                    btn_upgrade.IsEnabled = true;

                    packageUser = new PackageUser();

                    txt_period.Visibility = Visibility.Visible;

                    txt_packageNumberTitle.Text = "";

                    grid_packageDetails.DataContext = packageUser;
                    grid_payDetails.DataContext = packageUser;

                    #region fill Package details
                    if (packageUser != null)
                    {
                        txt_total.Text = txt_price.Text;
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

        private async void Btn_upgrade_Click(object sender, RoutedEventArgs e)
        {//upgrade
            try
            {
                HelpClass.StartAwait(grid_main);

                if (packageUser.packageId > 0)
                {
                    MainWindow.mainWindow.Btn_sales_Click(MainWindow.mainWindow.btn_sales, null);
                    uc_sales.Instance.Window_Loaded(null, null);
                    uc_sales.Instance.Btn_sale_Click(uc_sales.Instance.btn_sale, null);
                    uc_sale.Instance.UserControl_Loaded(null, null);
                    uc_sale.Instance.package = package;

                    uc_sale.Instance.oldCustomerId = packageUser.customerId.Value;
                    uc_sale.Instance.oldAgentId = packageUser.userId.Value;
                    uc_sale.Instance.oldPackageId = packageUser.packageId.Value;
                    uc_sale.Instance.oldCountryPackageId = packageUser.countryPackageId.Value;
                    uc_sale.Instance.packuser = packageUser;
                    uc_sale.Instance.btn_serials.IsEnabled = true;
                    uc_sale.Instance.tb_activationCode.Text = packageUser.packageSaleCode;
                    uc_sale.Instance.isOnline = packageUser.isOnlineServer.Value;
                    HelpClass.clearValidate(uc_sale.requiredControlList, this);
                }

                Clear();
                ClearPackageUser();

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Btn_pay_Click(object sender, RoutedEventArgs e)
        {//pay
            try
            {
                HelpClass.StartAwait(grid_main);
                decimal msg = 0;
                if (HelpClass.validate(requiredControlList, this))
                {
                    payOp.Price = decimal.Parse(txt_price.Text);
                    payOp.code = await payOpModel.generateNumber("po");//auto po-000000
                    payOp.type = packageUser.type;
                    payOp.packageUserId = packageUser.packageUserId;
                    payOp.createUserId = MainWindow.userID;
                    payOp.notes = "";
                    payOp.discountValue = discount;
                    if (MainWindow.userLogin.type.Equals("ag"))
                        payOp.agentId = MainWindow.userID;
                    else payOp.agentId = 3;
                    payOp.customerId = (int)cb_customer.SelectedValue;
                    payOp.countryPackageId = packageUser.countryPackageId;
                    payOp.expireDate = packageUser.expireDate;
                    totalNet = decimal.Parse(txt_price.Text) - discount;
                    payOp.totalnet = totalNet;

                    msg = await packageUserModel.packagePay(packageUser, payOp);
                    if (msg <= 0)
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                    else
                    {
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopPay"), animation: ToasterAnimation.FadeIn);


                        if (FillCombo.print_on_save_sale == "1")
                        {
                            //Thread t2 = new Thread(() =>
                            //{
                                printOnPay();
                            //});
                            //t2.Start();
                        }

                        if (FillCombo.email_on_save_sale == "1")
                        {
                            //Thread t1 = new Thread(() =>
                            //{
                           sendsaleEmail(packageUser.packageUserId);
                            //});
                            //t1.Start();
                       
                        
                        }
                     
                        await RefreshPayOpList();
                        await Search();

                        //ClearPackageUser();
                    }
                }
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_email_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);

                //  if (MainWindow.groupObject.HasPermissionAction(sendEmailPermission, MainWindow.groupObjects, "one"))
                {

                    //await sendsaleEmail();
                    ////
                    Thread t1 = new Thread(() =>
                    {
                        sendsaleEmail(packageUser.packageUserId);
                    });
                    t1.Start();
                    ////
                }
                //else
                //    Toaster.ShowInfo(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trdontHavePermission"), animation: ToasterAnimation.FadeIn);
                if (sender != null)
                    HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                if (sender != null)
                    HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Btn_pdfPaytest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);

                #region
                if (packageUser.packageUserId > 0)
                {
                    packUserRep = await packUserRep.GetByID(packageUser.packageUserId);
                    await getdata();
                    Window.GetWindow(this).Opacity = 0.2;
                    string pdfpath = "";
                    //
                    pdfpath = @"\Thumb\report\temp.pdf";
                    pdfpath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, pdfpath);
                    BuildPayReport(1);
                    LocalReportExtensions.ExportToPDF(rep, pdfpath);
                    wd_previewPdf w = new wd_previewPdf();
                    w.pdfPath = pdfpath;
                    if (!string.IsNullOrEmpty(w.pdfPath))
                    {
                        w.ShowDialog();
                        w.wb_pdfWebViewer.Dispose();
                    }
                    Window.GetWindow(this).Opacity = 1;
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

        private void Tb_discount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!tb_discount.Text.Equals(""))
                    discount = decimal.Parse(tb_discount.Text);
                txt_discount.Text = discount.ToString();
                totalNet = decimal.Parse(txt_price.Text) - discount;
                txt_total.Text = totalNet.ToString();
            }
            catch (Exception ex)
            {
            }
        }
        #region Button In DataGrid
        void printRowFromDatagrid(object sender, RoutedEventArgs e)
        {
            try
            {
                for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                    if (vis is DataGridRow)
                    {
                        PayOp row = (PayOp)dg_payments.SelectedItems[0];
                        Previewpayoprow(row.payOpId);

                    }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        #endregion	
    }
}
