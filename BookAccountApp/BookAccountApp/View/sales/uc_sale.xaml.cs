using BookAccountApp.ApiClasses;
using BookAccountApp.Classes;
using BookAccountApp.View.windows;
using Microsoft.Reporting.WinForms;
using Microsoft.Win32;
using netoaster;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace BookAccountApp.View.sales
{
    /// <summary>
    /// Interaction logic for uc_sale.xaml
    /// </summary>
    public partial class uc_sale : UserControl
    {
        public Packages package = new Packages();
        public int oldCustomerId, oldAgentId, oldPackageId, oldCountryPackageId;
        public bool isOnline;

        public static List<string> requiredControlList;

        public PackageUser packuser = new PackageUser();
        PackageUser packuserModel = new PackageUser();

        Users userModel = new Users();
        Users agent = new Users();

        public uc_sale()
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

        private static uc_sale _instance;
        public static uc_sale Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_sale();
                return _instance;
            }
        }

        public async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);

                package = new Packages();

                this.DataContext = package;

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

                requiredControlList = new List<string> { "package", "customer", "isOnline", "period" };

                FillCombo.fillServerState(cb_isOnline);

                if (MainWindow.userLogin.type.Equals("ag"))
                {
                    try { await FillCombo.fillCustomerByAgent(cb_customer, MainWindow.userLogin.userId); }
                    catch { await FillCombo.fillCustomerByAgent(cb_customer, MainWindow.userLogin.userId); }
                
                    try
                    {
                        agent = await userModel.GetByID(MainWindow.userLogin.userId);
                    }
                    catch { agent = await userModel.GetByID(MainWindow.userLogin.userId); }
                    await FillCombo.fillAgentPackage(cb_package, MainWindow.userLogin.userId);
                    cb_package.IsEnabled = true;
                }
                else
                {
                    try { await FillCombo.fillCustomer(cb_customer); }
                    catch { await FillCombo.fillCustomer(cb_customer); }
                    try
                    {
                        agent = await userModel.GetByID(3);
                    }
                    catch { agent = await userModel.GetByID(3); }
                    cb_agent.Visibility = Visibility.Collapsed;
                }
                if (oldPackageId == 0)
                {
                    Clear();
                    btn_add.Content = MainWindow.resourcemanager.GetString("trBook");
                }
                else
                {
                    #region expired date
                    string color = "#2491EA";
                    if (packuser.expireDate.Value < DateTime.Now)
                        color = "#FF0000";
                    txt_date.Text = HelpClass.setDateFormat(packuser.expireDate.Value);
                    txt_date.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
                    #endregion

                    btn_add.Content = MainWindow.resourcemanager.GetString("trUpgrade");
                    cb_customer.SelectedValue = oldCustomerId;
                    try
                    {
                        agent = await userModel.GetByID(MainWindow.userLogin.userId);
                    }
                    catch { agent = await userModel.GetByID(MainWindow.userLogin.userId); }
                    if (MainWindow.userLogin.type.Equals("ag"))
                    {
                        cb_package.SelectedValue = oldPackageId;
                        cb_period.SelectedValue = oldCountryPackageId;
                    }
                    cb_isOnline.SelectedValue = isOnline;
                    if (isOnline)
                    {
                        txt_offlineActivation.Visibility = Visibility.Collapsed;
                        btn_download.Visibility = Visibility.Collapsed;
                        txt_activationCodeTitle.Visibility = Visibility.Visible;
                        tb_activationCode.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        txt_offlineActivation.Visibility = Visibility.Visible;
                        btn_download.Visibility = Visibility.Visible;
                        txt_activationCodeTitle.Visibility = Visibility.Collapsed;
                        tb_activationCode.Visibility = Visibility.Collapsed;
                    }
                    tgl_isActive.IsChecked = Convert.ToBoolean(packuser.isActive);
                    tgl_device.IsChecked = Convert.ToBoolean(packuser.canChngSer);
                }

                cb_agent.Text = agent.name + " " + agent.lastName;

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

        private void translate()
        {

            txt_title.Text = MainWindow.resourcemanager.GetString("trSales");
            txt_saleDetails.Text = MainWindow.resourcemanager.GetString("trSaleDetails");
            tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");

            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_package, MainWindow.resourcemanager.GetString("trPackageHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_agent, MainWindow.resourcemanager.GetString("trAgentHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_customer, MainWindow.resourcemanager.GetString("trCustomerHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_isOnline, MainWindow.resourcemanager.GetString("trServerState") + "...");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_period, MainWindow.resourcemanager.GetString("trPeriod"));

            txt_packageDetails.Text = MainWindow.resourcemanager.GetString("trPackageDetails");
            txt_packageCodeTitle.Text = MainWindow.resourcemanager.GetString("trCode");
            txt_packageNameTitle.Text = MainWindow.resourcemanager.GetString("trName");
            txt_priceTitle.Text = MainWindow.resourcemanager.GetString("trPrice");
            txt_statusTitle.Text = MainWindow.resourcemanager.GetString("trStatus");
            txt_serialsTitle.Text = MainWindow.resourcemanager.GetString("trSerials");
            txt_dateTitle.Text = MainWindow.resourcemanager.GetString("trExpirationDate");

            txt_active.Text = MainWindow.resourcemanager.GetString("trActive");
            txt_device.Text = MainWindow.resourcemanager.GetString("trChangeDevice");

            txt_programDetails.Text = MainWindow.resourcemanager.GetString("trProgramDetails");
            txt_programTitle.Text = MainWindow.resourcemanager.GetString("trProgram");
            txt_versionTitle.Text = MainWindow.resourcemanager.GetString("trVersion");

            txt_packageLimits.Text = MainWindow.resourcemanager.GetString("trPackageLimits");
            txt_branchCountTitle.Text = MainWindow.resourcemanager.GetString("trBranches");
            txt_userCountTitle.Text = MainWindow.resourcemanager.GetString("trUsers");
            txt_customerCountTitle.Text = MainWindow.resourcemanager.GetString("trCustomers");
            txt_salesInvCountTitle.Text = MainWindow.resourcemanager.GetString("trInvoices");
            txt_storeCountNameTitle.Text = MainWindow.resourcemanager.GetString("trStores");
            txt_posCountNameTitle.Text = MainWindow.resourcemanager.GetString("trPOSs");
            txt_vendorCountNameTitle.Text = MainWindow.resourcemanager.GetString("trVendors");
            txt_itemCountNameTitle.Text = MainWindow.resourcemanager.GetString("trItems");

            txt_offlineActivation.Text = MainWindow.resourcemanager.GetString("trOfflineActivation");
        }

        #region events
        CountryPackageDate cpdModel = new CountryPackageDate();
        IEnumerable<CountryPackageDate> countryPackageDates;

        private async void Cb_package_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//selection
            try
            {
                if (cb_package.SelectedIndex != -1)
                {
                    package = cb_package.SelectedItem as Packages;

                    this.DataContext = package;
                    if (package != null)
                    {
                        try
                        {
                            await FillCombo.fillPeriod(cb_period, (int)cb_customer.SelectedValue, (int)cb_package.SelectedValue);

                            #region fill period 

                            if (oldPackageId != 0)
                                cb_period.SelectedValue = oldCountryPackageId;

                            #endregion
                        }
                        catch { }
                    }

                }

            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_clear_Click(object sender, RoutedEventArgs e)
        {//clear
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
        #endregion

        #region Refresh & Search
        //async Task<IEnumerable<Packages>> RefreshPackagesList()
        //{
        //    packages = await package.GetAll();
        //    return packages;
        //}
        #endregion

        #region validate - clearValidate - textChange - lostFocus - . . . . 
        void Clear()
        {
            try
            {
                this.DataContext = new Packages();
            }
            catch { }
            packuser = new PackageUser();
            cb_customer.SelectedIndex = -1;
            cb_isOnline.SelectedIndex = -1;
            cb_package.ItemsSource = null;
            cb_period.ItemsSource = null;
            tgl_device.IsChecked = false;
            tgl_isActive.IsChecked = true;
            oldPackageId = 0;
            oldCountryPackageId = 0;
            cb_package.IsEnabled = false;
            cb_period.IsEnabled = false;
            btn_serials.IsEnabled = false;
            tb_activationCode.Text = "";
            txt_date.Text = "";
            btn_add.Content = MainWindow.resourcemanager.GetString("trBook");
            HelpClass.clearValidate(requiredControlList, this);
        }

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
            _validate();
        }
        private void validateEmpty_LostFocus(object sender, RoutedEventArgs e)
        {
            _validate();
        }

        private void _validate()
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
        private async void Btn_add_Click(object sender, RoutedEventArgs e)
        {//book
            try
            {
                HelpClass.StartAwait(grid_main);

                decimal msg = 0;

                string pop = "";
                if (HelpClass.validate(requiredControlList, this))
                {
                    packuser.packageId = int.Parse(cb_package.SelectedValue.ToString());
                    packuser.userId = agent.userId;
                    packuser.customerId = int.Parse(cb_customer.SelectedValue.ToString());
                    packuser.createUserId = MainWindow.userID;
                    packuser.packageNumber = await packuserModel.generateNumber("si", agent.code, agent.userId);
                    if (tgl_isActive.IsChecked == true)
                        packuser.isActive = 1;
                    else
                        packuser.isActive = 0;
                    if (tgl_device.IsChecked == true)
                        packuser.canChngSer = 1;
                    else
                        packuser.canChngSer = 0;

                    packuser.canRenew = true;
                    packuser.packageSaleCode = packuser.packageSaleCode;
                    packuser.notes = "";
                    packuser.isOnlineServer = bool.Parse(cb_isOnline.SelectedValue.ToString());
                    packuser.countryPackageId = (int)cb_period.SelectedValue;
                    packuser.oldPackageId = oldPackageId;
                    packuser.oldCountryPackageId = oldCountryPackageId;

                    if (packuser.packageUserId == 0) pop = "trPopAddBook";
                    else pop = "trPopUpgradeSucceed";

                    msg = await packuserModel.packageBook(packuser);

                    if (msg <= 0)
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                    else
                    {
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString(pop), animation: ToasterAnimation.FadeIn);
                        if (pop.Equals("trPopAddBook"))
                            Clear();
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

        private void Btn_upgrade_Click(object sender, RoutedEventArgs e)
        {//upgrade
            try
            {
                oldPackageId = (int)cb_package.SelectedValue;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Btn_addCustomer_Click(object sender, RoutedEventArgs e)
        {//add customer
            try
            {
                Window.GetWindow(this).Opacity = 0.2;
                wd_newCustomer w = new wd_newCustomer();
                w.customerID = 0;
                w.ShowDialog();
                Window.GetWindow(this).Opacity = 1;

                if (w.isOk)
                {
                    await FillCombo.fillCustomer(cb_customer);
                    cb_customer.SelectedValue = w.customerID;
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Btn_updateCustomer_Click(object sender, RoutedEventArgs e)
        {//update customer
            try
            {
                Window.GetWindow(this).Opacity = 0.2;
                wd_newCustomer w = new wd_newCustomer();
                if (cb_customer.SelectedItem != null)
                    w.customerID = (int)cb_customer.SelectedValue;
                else
                    w.customerID = 0;
                w.ShowDialog();
                Window.GetWindow(this).Opacity = 1;

                if (w.isOk)
                {
                    await FillCombo.fillCustomer(cb_customer);
                    cb_customer.SelectedValue = w.customerID;
                }

            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_clearCustomer_Click(object sender, RoutedEventArgs e)
        {//clear customer
            try
            {
                cb_customer.SelectedIndex = -1;
                cb_package.IsEnabled = false;
                cb_period.IsEnabled = false;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_serials_Click(object sender, RoutedEventArgs e)
        {//serials
            try
            {
                Window.GetWindow(this).Opacity = 0.2;
                wd_seialsList w = new wd_seialsList();
                w.packageUserID = packuser.packageUserId;
                w.ShowDialog();
                Window.GetWindow(this).Opacity = 1;

            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Cb_customer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//select customer
            try
            {
                if (cb_customer.SelectedIndex != -1)
                {
                    cb_package.IsEnabled = true;
                    cb_period.IsEnabled = true;

                    if (MainWindow.userLogin.type != "ag")
                    {
                        try
                        {
                            await FillCombo.fillPackageByCustomer(cb_package, (int)cb_customer.SelectedValue);
                            cb_package.SelectedValue = oldPackageId;
                        }
                        catch { }
                    }
                    else
                    {
                        await FillCombo.fillAgentPackage(cb_package, MainWindow.userLogin.userId);
                    }
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            GC.Collect();
        }

        private async void Cb_period_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//select period
            try
            {
                if (cb_period.SelectedIndex != -1)
                {
                    CountryPackageDate cpd = new CountryPackageDate();
                    try
                    {
                        cpd = await cpdModel.GetByID((int)cb_period.SelectedValue);
                    }
                    catch { cpd = await cpdModel.GetByID((int)cb_period.SelectedValue); }
                    txt_price.Text = cpd.price.ToString() + " " + cpd.currency.ToString();
                }
                else
                {
                    txt_price.Text = "";
                }
            }
                catch (Exception ex)
                {
                    HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Cb_isOnline_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cb_isOnline.SelectedIndex != -1)
            {
                if (cb_isOnline.SelectedValue.ToString().Equals("False"))
                {
                    if (oldPackageId != 0)
                    {
                        btn_download.Visibility = Visibility.Visible;
                        txt_offlineActivation.Visibility = Visibility.Visible;
                        txt_activationCodeTitle.Visibility = Visibility.Collapsed;
                        tb_activationCode.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        btn_download.Visibility = Visibility.Collapsed;
                        txt_offlineActivation.Visibility = Visibility.Collapsed;
                        txt_activationCodeTitle.Visibility = Visibility.Visible;
                        tb_activationCode.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    btn_download.Visibility = Visibility.Collapsed;
                    txt_offlineActivation.Visibility = Visibility.Collapsed;
                    txt_activationCodeTitle.Visibility = Visibility.Visible;
                    tb_activationCode.Visibility = Visibility.Visible;
                }
            }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        public async Task fillInputs(Packages _package, int agentID, int custID)
        {
            try
            {
                package = _package;
                this.DataContext = package;
                if (package != null)
                {
                    #region fill period 
                    Users userModel = new Users();
                    Customers custModel = new Customers();
                    Customers cust = new Customers();
                    //cust = await custModel.GetByID((int)cb_customer.SelectedValue);
                    cust = await custModel.GetByID(custID);
                    countryPackageDates = await cpdModel.GetAll();
                    if (bdr_agent.Visibility == Visibility.Visible)
                    {
                        //Users agent = await userModel.GetByID((int)cb_agent.SelectedValue);
                        Users agent = await userModel.GetByID(agentID);
                        //countryPackageDates = countryPackageDates.Where(x => x.isActive == 1 && x.packageId == (int)cb_package.SelectedValue && x.countryId == cust.countryId);
                        countryPackageDates = countryPackageDates.Where(x => x.isActive == 1 && x.packageId == _package.packageId && x.countryId == cust.countryId);
                    }
                    else
                        //countryPackageDates = countryPackageDates.Where(x => x.isActive == 1 && x.packageId == (int)cb_package.SelectedValue && x.countryId == cust.countryId);
                        countryPackageDates = countryPackageDates.Where(x => x.isActive == 1 && x.packageId == _package.packageId && x.countryId == cust.countryId);


                    foreach (var p in countryPackageDates)
                    {
                        if (p.islimitDate)
                            p.notes = MainWindow.resourcemanager.GetString("trUnLimited");
                        else
                        {
                            switch (p.monthCount)
                            {
                                case 1: p.notes = MainWindow.resourcemanager.GetString("trOneMonth"); break;
                                case 3: p.notes = MainWindow.resourcemanager.GetString("trThreeMonth"); break;
                                case 6: p.notes = MainWindow.resourcemanager.GetString("trSixMonth"); break;
                                case 0: p.notes = MainWindow.resourcemanager.GetString("trTwelveMonth"); break;
                            }
                        }
                    }
                    cb_period.DisplayMemberPath = "notes";
                    cb_period.SelectedValuePath = "Id";
                    cb_period.ItemsSource = countryPackageDates;
                    if (oldCustomerId != 0) cb_period.SelectedValue = oldCountryPackageId;
                    #endregion
                }
            }
            catch (Exception ex)
            {

                HelpClass.ExceptionMessage(ex, this);
            }
        }

        #region reports
        ReportCls reportclass = new ReportCls();
        LocalReport rep = new LocalReport();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        PackageUser packUserRep = new PackageUser();
        Users agentmodel = new Users();
        Customers cumstomerModel = new Customers();
        CountryPackageDate CountryPackageDateModel = new CountryPackageDate();
       // Country CountryModel = new Country();
        Packages PackagesModel = new Packages();
        PayOp PayOpModel = new PayOp();
      List< SetValues> SetValuesList = new List<SetValues>();
        SetValues terms = new SetValues();
        string result = "0";
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
                PackagesModel = await PackagesModel.GetByID((int)PayOpModel.packageId);
                SetValuesList = await terms.GetBySetName("sale_note");
                terms = SetValuesList.FirstOrDefault();
                //  CountryPackageDateModel.monthCount;
                return "1";
            }

        }
        public async Task<string> BuildReport()
        {

            //string firstTitle = "paymentsReport";
            ////string secondTitle = "";
            ////string subTitle = "";
            //string Title = "";
           
            List<ReportParameter> paramarr = new List<ReportParameter>();
            List<PackageUser> purl = new List<PackageUser>();
            packUserRep = await packUserRep.GetByID(packuser.packageUserId);
            await getdata();
            if (PayOpModel.payOpId <= 0)
            {
                return "0";//not pay yet
            }
            else {
            string addpath;
            bool isArabic = ReportCls.checkLang();
            if (isArabic)
            {
                addpath = @"\Reports\Sale\Book\Ar\ArBook.rdlc";
            }
            else
            {
                addpath = @"\Reports\Sale\Book\En\EnBook.rdlc";
            }
            //D:\myproj\posproject3\BookAccountApp\BookAccountApp\Reports\Sale\Book\En\EnBook.rdlc
            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
            //     subTitle = clsReports.ReportTabTitle(firstTitle, secondTitle);
            //  Title = MainWindow.resourcemanagerreport.GetString("trAccountantReport");
          

                clsReports.setReportLanguage(paramarr);
            clsReports.Header(paramarr);
            SetReportparam(paramarr);
            clsReports.BookSale(purl, rep, reppath, paramarr);
            rep.SetParameters(paramarr);

            rep.Refresh();
            return "1";
            }

        }
        public void SetReportparam(List<ReportParameter> paramarr)
        {

            // txt_title.Text = MainWindow.resourcemanager.GetString("trSales");
            paramarr.Add(new ReportParameter("trSaleDetails", MainWindow.resourcemanagerreport.GetString("trSaleDetails")));
            paramarr.Add(new ReportParameter("trAgentHint", MainWindow.resourcemanagerreport.GetString("trAgent")));

            paramarr.Add(new ReportParameter("trCustomerHint", MainWindow.resourcemanagerreport.GetString("trCustomer")));
            paramarr.Add(new ReportParameter("trServerState", MainWindow.resourcemanagerreport.GetString("trServerState")));
            paramarr.Add(new ReportParameter("trPeriod", MainWindow.resourcemanagerreport.GetString("trPeriod")));
            paramarr.Add(new ReportParameter("trPackageDetails", MainWindow.resourcemanagerreport.GetString("trPackageDetails")));
            paramarr.Add(new ReportParameter("trCode", MainWindow.resourcemanagerreport.GetString("trCode")));
            paramarr.Add(new ReportParameter("trName", MainWindow.resourcemanagerreport.GetString("trName")));
            paramarr.Add(new ReportParameter("trPrice", MainWindow.resourcemanagerreport.GetString("trPrice")));
            paramarr.Add(new ReportParameter("trStatus", MainWindow.resourcemanagerreport.GetString("trStatus")));
            paramarr.Add(new ReportParameter("trSerials", MainWindow.resourcemanagerreport.GetString("trSerials")));
            paramarr.Add(new ReportParameter("trExpirationDate", MainWindow.resourcemanagerreport.GetString("trExpirationDate")));
            paramarr.Add(new ReportParameter("trActive", MainWindow.resourcemanagerreport.GetString("trActive")));
            paramarr.Add(new ReportParameter("trProgramDetails", MainWindow.resourcemanagerreport.GetString("trProgramDetails")));
            paramarr.Add(new ReportParameter("trProgram", MainWindow.resourcemanagerreport.GetString("trProgram")));
            paramarr.Add(new ReportParameter("trVersion", MainWindow.resourcemanagerreport.GetString("trVersion")));
            paramarr.Add(new ReportParameter("trPackageLimits", MainWindow.resourcemanagerreport.GetString("trPackageLimits")));
            paramarr.Add(new ReportParameter("trBranches", MainWindow.resourcemanagerreport.GetString("trBranches")));
            paramarr.Add(new ReportParameter("trUsers", MainWindow.resourcemanagerreport.GetString("trUsers")));
            paramarr.Add(new ReportParameter("trCustomers", MainWindow.resourcemanagerreport.GetString("trCustomers")));
            paramarr.Add(new ReportParameter("trInvoices", MainWindow.resourcemanagerreport.GetString("trInvoices")));
            paramarr.Add(new ReportParameter("trStores", MainWindow.resourcemanagerreport.GetString("trStores")));
            paramarr.Add(new ReportParameter("trPOSs", MainWindow.resourcemanagerreport.GetString("trPOSs")));
            paramarr.Add(new ReportParameter("trVendors", MainWindow.resourcemanagerreport.GetString("trVendors")));
            paramarr.Add(new ReportParameter("trItems", MainWindow.resourcemanagerreport.GetString("trItems")));
            paramarr.Add(new ReportParameter("trOfflineActivation", MainWindow.resourcemanagerreport.GetString("trActivationType")));
            paramarr.Add(new ReportParameter("trChangeDevice", MainWindow.resourcemanagerreport.GetString("trChangeDevice")));
            paramarr.Add(new ReportParameter("trBookNum", MainWindow.resourcemanagerreport.GetString("trBookNum")));
            paramarr.Add(new ReportParameter("trNotes", MainWindow.resourcemanagerreport.GetString("trTerms")));

            paramarr.Add(new ReportParameter("packageNumber", packUserRep.packageNumber.ToString()));
            paramarr.Add(new ReportParameter("Agent",FillCombo.AgentNameConv(agentmodel)));
            paramarr.Add(new ReportParameter("Customer", cumstomerModel.company));
            paramarr.Add(new ReportParameter("ServerState",""));
            paramarr.Add(new ReportParameter("Period", FillCombo.PeriodConv(CountryPackageDateModel)));
            paramarr.Add(new ReportParameter("Code", PackagesModel.packageCode));
            paramarr.Add(new ReportParameter("Name", PackagesModel.packageName));
            paramarr.Add(new ReportParameter("Price", CountryPackageDateModel.price.ToString() +" "+ CountryPackageDateModel.currency));
            paramarr.Add(new ReportParameter("Status", FillCombo.serverActiveConv(packUserRep.isActive)));
            paramarr.Add(new ReportParameter("ExpirationDate",FillCombo.DateConvert(PayOpModel.expireDate)));
          //  paramarr.Add(new ReportParameter("Active", ));
            paramarr.Add(new ReportParameter("Program", PackagesModel.programName));
            paramarr.Add(new ReportParameter("Version", PackagesModel.verName));
            paramarr.Add(new ReportParameter("Branches", FillCombo.UnlimitedConvert(PackagesModel.branchCount)));
            paramarr.Add(new ReportParameter("Users", FillCombo.UnlimitedConvert(PackagesModel.userCount)));
            paramarr.Add(new ReportParameter("Customers", FillCombo.UnlimitedConvert(PackagesModel.customerCount)));
            paramarr.Add(new ReportParameter("Invoices", FillCombo.UnlimitedConvert(PackagesModel.salesInvCount)));
            paramarr.Add(new ReportParameter("Stores", FillCombo.UnlimitedConvert(PackagesModel.storeCount)));
            paramarr.Add(new ReportParameter("POSs", FillCombo.UnlimitedConvert(PackagesModel.posCount)));
            paramarr.Add(new ReportParameter("Vendors", FillCombo.UnlimitedConvert(PackagesModel.vendorCount)));
            paramarr.Add(new ReportParameter("Items", FillCombo.UnlimitedConvert(PackagesModel.itemCount)));
            paramarr.Add(new ReportParameter("OfflineActivation", FillCombo.serverActiveationTypeConv(packUserRep.isOnlineServer)));
            paramarr.Add(new ReportParameter("ChangeDevice", ""));
            //trTerms
          
            paramarr.Add(new ReportParameter("Notes", terms.value));
        }
     




        private async void Btn_pdf_Click(object sender, RoutedEventArgs e)
        {//pdf
            try
            {
                HelpClass.StartAwait(grid_main);
                #region
                if (packuser.packageUserId <= 0)
                {
                    //no book saved
                    Toaster.ShowWarning(Window.GetWindow(this), message: "You have to book first", animation: ToasterAnimation.FadeIn);

                }
                else
                {
                    result = await BuildReport();

                    if (result=="0")
                    {
                        // not payed yet
                        Toaster.ShowWarning(Window.GetWindow(this), message: "The customer must pay first", animation: ToasterAnimation.FadeIn);

                    }
                    else
                    {
                        saveFileDialog.Filter = "PDF|*.pdf;";

                        if (saveFileDialog.ShowDialog() == true)
                        {
                            string filepath = saveFileDialog.FileName;
                            LocalReportExtensions.ExportToPDF(rep, filepath);
                        }
                    }
                   
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
        private async void Btn_print_Click(object sender, RoutedEventArgs e)
        {//print
            try
            {
                HelpClass.StartAwait(grid_main);
                #region

                if (packuser.packageUserId <= 0)
                {
                    //no book saved
                    Toaster.ShowWarning(Window.GetWindow(this), message: "You have to book first", animation: ToasterAnimation.FadeIn);

                }
                else
                {
                    result = await BuildReport();

                    if (result == "0")
                    {
                        // not payed yet
                        Toaster.ShowWarning(Window.GetWindow(this), message: "The customer must pay first", animation: ToasterAnimation.FadeIn);

                    }
                    else
                    {
                        LocalReportExtensions.PrintToPrinterbyNameAndCopy(rep, FillCombo.getdefaultPrinters(), FillCombo.rep_print_count == null ? short.Parse("1") : short.Parse(FillCombo.rep_print_count));

                    }
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

        private async void Btn_exportToExcel_Click(object sender, RoutedEventArgs e)
        {//excel
            try
            {
                HelpClass.StartAwait(grid_main);

                #region
                //Thread t1 = new Thread(() =>
                //{
                if (packuser.packageUserId <= 0)
                {
                    //no book saved
                    Toaster.ShowWarning(Window.GetWindow(this), message: "You have to book first", animation: ToasterAnimation.FadeIn);

                }
                else
                {
                    result = await BuildReport();

                    if (result == "0")
                    {
                        // not payed yet
                        Toaster.ShowWarning(Window.GetWindow(this), message: "The customer must pay first", animation: ToasterAnimation.FadeIn);

                    }
                    else
                    {
                        this.Dispatcher.Invoke(() =>
                {
                    saveFileDialog.Filter = "EXCEL|*.xls;";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string filepath = saveFileDialog.FileName;
                        LocalReportExtensions.ExportToExcel(rep, filepath);
                    }
                });
                    }

                    //});
                    //t1.Start();
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

        private async void Btn_preview_Click(object sender, RoutedEventArgs e)
        {//preview
            try
            {
                HelpClass.StartAwait(grid_main);

                #region
               

                string pdfpath = "";
                //
                pdfpath = @"\Thumb\report\temp.pdf";
                pdfpath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, pdfpath);

                if (packuser.packageUserId <= 0)
                {
                    //no book saved
                    Toaster.ShowWarning(Window.GetWindow(this), message: "You have to book first", animation: ToasterAnimation.FadeIn);

                }
                else
                {
                    result = await BuildReport();

                    if (result == "0")
                    {
                        // not payed yet
                        Toaster.ShowWarning(Window.GetWindow(this), message: "The customer must pay first", animation: ToasterAnimation.FadeIn);

                    }
                    else
                    {
                        Window.GetWindow(this).Opacity = 0.2;
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
                #endregion

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }

        }

        private void Btn_download_Click(object sender, RoutedEventArgs e)
        {//offline activation
            try
            {
                Window.GetWindow(this).Opacity = 0.2;
                wd_offlineActivation w = new wd_offlineActivation();
                w.packageUser = packuser;
                w.ShowDialog();
                Window.GetWindow(this).Opacity = 1;

            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }




        #endregion
    }
}
