using BookAccountApp.ApiClasses;
using BookAccountApp.Classes;
using netoaster;
using System;
using System.Collections.Generic;
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

namespace BookAccountApp.View.sales
{
    /// <summary>
    /// Interaction logic for uc_packageUser.xaml
    /// </summary>
    public partial class uc_packageUser : UserControl
    {
        private static uc_packageUser _instance;
        public static uc_packageUser Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_packageUser();
                return _instance;
            }
        }
        public uc_packageUser()
        {
            try
            {
                InitializeComponent();
                HelpClass.defaultDatePickerStyle(dp_bookDate);
                HelpClass.defaultDatePickerStyle(dp_expireDate);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        IEnumerable<PackageUser> packageUsersQuery;
        IEnumerable<PackageUser> packageUsers;
        PackageUser packageUser = new PackageUser();
        byte tgl_packageUserState;
        string searchText = "";
        public static List<string> requiredControlList;
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);
                requiredControlList = new List<string> { "" };
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

                await FillCombo.fillAgent(cb_agent);
                await FillCombo.fillCustomer(cb_customer);

                //Keyboard.Focus(tb_code);
                await Search();
                Clear();
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
            txt_active.Text = MainWindow.resourcemanager.GetString("trActive");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, MainWindow.resourcemanager.GetString("trSearchHint"));
            txt_baseInformation.Text = MainWindow.resourcemanager.GetString("trBaseInformation");
            txt_title.Text = MainWindow.resourcemanager.GetString("trAgentPackage");

            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_packageSaleCode, MainWindow.resourcemanager.GetString("trPackageSaleCodeHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_agent, MainWindow.resourcemanager.GetString("trAgentHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_customer, MainWindow.resourcemanager.GetString("trCustomerHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_packageNumber, MainWindow.resourcemanager.GetString("trProcessNumHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_customerServerCode, MainWindow.resourcemanager.GetString("trServerCodeHint"));
            //txt_isBooked.Text = MainWindow.resourcemanager.GetString("trBooked");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(dp_bookDate, MainWindow.resourcemanager.GetString("trBookDateHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(dp_expireDate, MainWindow.resourcemanager.GetString("trExpirationDateHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_notes, MainWindow.resourcemanager.GetString("trNoteHint"));

            btn_pay.Content = MainWindow.resourcemanager.GetString("trPay");
            btn_serials.Content = MainWindow.resourcemanager.GetString("trSerials");

            dg_packageUser.Columns[0].Header = MainWindow.resourcemanager.GetString("trProcessNumTooltip");
            dg_packageUser.Columns[1].Header = MainWindow.resourcemanager.GetString("trAgent");
            dg_packageUser.Columns[2].Header = MainWindow.resourcemanager.GetString("trCustomer");
            dg_packageUser.Columns[3].Header = MainWindow.resourcemanager.GetString("trCode");

            tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");
            tt_refresh.Content = MainWindow.resourcemanager.GetString("trRefresh");
            tt_report.Content = MainWindow.resourcemanager.GetString("trPdf");
            tt_print.Content = MainWindow.resourcemanager.GetString("trPrint");
            tt_excel.Content = MainWindow.resourcemanager.GetString("trExcel");
            tt_count.Content = MainWindow.resourcemanager.GetString("trCount");
        }

        private async void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            /*
             * 
            pkuitem.createUserId = 1;
            pkuitem.packageId = 1;
            pkuitem.notes = "notee";
            pkuitem.customerServerCode = "cc";
            pkuitem.packageSaleCode = "ca";
            //  pkuitem.packageNumber="p1" ;
            pkuitem.userId = 1;
            pkuitem.customerId =4;
            pkuitem.isActive = 1;
            pkuitem.isBooked = 1;
          pkuitem.expireDate =DateTime.Now;
            pkuitem.packageNumber = "p11";
            msg = pkumodel.save(pkuitem);
             * */

            //add
            try
            {
                HelpClass.StartAwait(grid_main);
              //  packageUser = new PackageUser();
                if (HelpClass.validate(requiredControlList, this))
                {
                    packageUser.packageSaleCode = tb_packageSaleCode.Text;
                    packageUser.userId = (int)cb_agent.SelectedValue;
                    packageUser.customerId = (int)cb_customer.SelectedValue;
                    packageUser.packageNumber = tb_packageNumber.Text;
                    packageUser.customerServerCode = tb_customerServerCode.Text;
                    //packageUser.isBooked = (bool) tgl_isBooked.IsChecked;
                    if (dp_bookDate.SelectedDate != null)
                        packageUser.bookDate = dp_bookDate.SelectedDate.Value;
                    else packageUser.bookDate = null;

                    if (dp_expireDate.SelectedDate != null)
                        packageUser.expireDate = dp_expireDate.SelectedDate.Value;
                    else packageUser.expireDate = null;

                    packageUser.notes = tb_notes.Text;
                    packageUser.isActive = 1;
                    packageUser.createUserId = MainWindow.userLogin.userId;
                    packageUser.updateUserId = MainWindow.userLogin.userId;
                    // packageUser.countryPackageId  = ; //new
                    //  packageUser.canRenew =;//new

                    decimal s = await packageUser.Save(packageUser);
                    if (s <= 0)
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                    else
                    {
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopSave"), animation: ToasterAnimation.FadeIn);
                        Clear();
                        await RefreshPackageUserList();
                        await Search();
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
        private async void Btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {//refresh

                HelpClass.StartAwait(grid_main);
                await RefreshPackageUserList();
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
                if (packageUsers is null)
                    await RefreshPackageUserList();
                tgl_packageUserState = 1;
                await Search();
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
                if (packageUsers is null)
                    await RefreshPackageUserList();
                tgl_packageUserState = 0;
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
        private async void Dg_packageUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { //selection
            try
            {
                HelpClass.StartAwait(grid_main);

                if (dg_packageUser.SelectedIndex != -1)
                {
                    packageUser = dg_packageUser.SelectedItem as PackageUser;
                    this.DataContext = packageUser;

                    if (packageUser != null)
                    {
                        Packages packageModel = new Packages();
                        Packages package = await packageModel.GetByID(packageUser.packageId.Value);

                    }
                }

                clearValidate();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        #region Refresh & Search
        async Task Search()
        {
            //search
            if (packageUsers is null)
                await RefreshPackageUserList();

            searchText = tb_search.Text.ToLower();
            packageUsersQuery = packageUsers.Where(s => (s.packageSaleCode.ToLower().Contains(searchText) 
            || s.userName.ToLower().Contains(searchText) 
            || s.customerName.ToLower().Contains(searchText)
            || s.packageNumber.ToLower().Contains(searchText)
            ) && s.isActive == tgl_packageUserState);

            RefreshPackageUserView();
        }
        async Task<IEnumerable<PackageUser>> RefreshPackageUserList()
        {
            packageUsers = await packageUser.GetAll();
            return packageUsers;
        }
        void RefreshPackageUserView()
        {
            dg_packageUser.ItemsSource = packageUsersQuery;
            txt_count.Text = packageUsersQuery.Count().ToString();
        }
        #endregion
        #region validate - clearValidate - textChange - lostFocus - . . . . 
        void Clear()
        {
            this.DataContext = new PackageUser();
            clearValidate();
        }
        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                //only  digits
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
        bool validate()
        {
            bool isValid = true;
            try
            {
                foreach (var control in requiredControlList)
                {
                    TextBox textBox = FindControls.FindVisualChildren<TextBox>(this).Where(x => x.Name == "tb_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(this).Where(x => x.Name == "p_error_" + control)
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
                    Path path = FindControls.FindVisualChildren<Path>(this).Where(x => x.Name == "p_error_" + control)
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
        void clearValidate()
        {
            try
            {
                foreach (var control in requiredControlList)
                {
                    Path path = FindControls.FindVisualChildren<Path>(this).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (path != null)
                        HelpClass.clearValidate(path);
                }
            }
            catch { }
        }
        #endregion
        private async void Dg_packageUser_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {//double click
            //try
            //{
                //HelpClass.StartAwait(grid_main);

                Packages package = new Packages();
                if (dg_packageUser.SelectedIndex != -1)
                {
                    if (packageUser.packageId > 0)
                    {
                        package = await package.GetByID(packageUser.packageId.Value);
                        MainWindow.mainWindow.Btn_sales_Click(MainWindow.mainWindow.btn_sales, null);
                        uc_sales.Instance.Window_Loaded(null, null);
                        uc_sales.Instance.Btn_sale_Click(uc_sales.Instance.btn_sale, null);
                        uc_sale.Instance.UserControl_Loaded(null, null);
                        uc_sale.Instance.package = package;
                       
                        uc_sale.Instance.oldCustomerId = packageUser.customerId.Value;
                        uc_sale.Instance.oldAgentId = packageUser.userId.Value;
                        uc_sale.Instance.oldPackageId = packageUser.packageId.Value;
                        try
                        {
                            uc_sale.Instance.oldCountryPackageId = packageUser.countryPackageId.Value;
                        }
                        catch { }
                        await uc_sale.Instance.fillInputs(package , packageUser.userId.Value , packageUser.customerId.Value);
                    }
                }

                //HelpClass.EndAwait(grid_main);
            //}
            //catch (Exception ex)
            //{
            //    HelpClass.EndAwait(grid_main);
            //    HelpClass.ExceptionMessage(ex, this);
            //}
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            GC.Collect();
        }
    }
}
