using BookAccountApp.ApiClasses;
using BookAccountApp.Classes;
using netoaster;
using POS.View.windows;
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
using System.Windows.Shapes;

namespace BookAccountApp.View.windows
{
    /// <summary>
    /// Interaction logic for wd_countryPackageDate.xaml
    /// </summary>
    public partial class wd_countryPackageDate : Window
    {
        public static List<string> requiredControlList;
        CountryPackageDate countryPackageDate = new CountryPackageDate();
        CountryPackageDate countryPackageDateModel = new CountryPackageDate();
        IEnumerable<CountryPackageDate> countryPackageDateQuery;
        IEnumerable<CountryPackageDate> countryPackageDates;

        public int packageID = 0;
        byte tgl_countryPackageDateState;

        public wd_countryPackageDate()
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

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);
                requiredControlList = new List<string> { "country", "month", "price" };
            
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

                await FillCombo.fillCountriesNames(cb_country);
                await FillCombo.fillCountriesNames(cb_country_);
                FillCombo.fillPackageMonth(cb_month);

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

        #region methods
        private void translate()
        {
            txt_countryPackageDate.Text = MainWindow.resourcemanager.GetString("trPackagePrices");
            txt_packageDetails.Text = MainWindow.resourcemanager.GetString("trPackageDetails");
            txt_active.Text = MainWindow.resourcemanager.GetString("trActive");
            txt_currency.Text = MainWindow.resourcemanager.GetString("trCurrency");
            chk_allCountries.Content = MainWindow.resourcemanager.GetString("trAll");

            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_country, MainWindow.resourcemanager.GetString("trCountryHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_country_, MainWindow.resourcemanager.GetString("trCountryHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_month, MainWindow.resourcemanager.GetString("trPeriodHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_price, MainWindow.resourcemanager.GetString("trPriceHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_notes, MainWindow.resourcemanager.GetString("trNoteHint"));

            dg_package.Columns[0].Header = MainWindow.resourcemanager.GetString("trCountry");
            dg_package.Columns[1].Header = MainWindow.resourcemanager.GetString("trCurrency");
            dg_package.Columns[2].Header = MainWindow.resourcemanager.GetString("trPeriod");
            dg_package.Columns[3].Header = MainWindow.resourcemanager.GetString("trPrice");

            tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");
            tt_refresh.Content = MainWindow.resourcemanager.GetString("trRefresh");
            tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");

            btn_save.Content = MainWindow.resourcemanager.GetString("trAdd");
            btn_update.Content = MainWindow.resourcemanager.GetString("trUpdate");
            btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");

        }

        async Task Search()
        {//search
            if (countryPackageDates is null)
                await RefreshCountryPackageDateList();

            countryPackageDateQuery = countryPackageDates
                .Where(c => 
                (
                cb_country_.SelectedIndex != -1 ? c.countryId == Convert.ToInt32(cb_country_.SelectedValue) : true
                ) 
                && c.isActive == tgl_countryPackageDateState);

            RefreshCountryPackageDateView();
        }
        async Task<IEnumerable<CountryPackageDate>> RefreshCountryPackageDateList()
        {
            countryPackageDates = await countryPackageDateModel.GetAll();
            countryPackageDates = countryPackageDates.Where(c => c.packageId == packageID);
            return countryPackageDates;
        }
        void RefreshCountryPackageDateView()
        {
            dg_package.ItemsSource = countryPackageDateQuery;
        }
        void Clear()
        {
            countryPackageDate.Id = 0;
            cb_country.SelectedIndex = -1;
            cb_month.SelectedIndex = -1;
            this.DataContext = new CountryPackageDate();

            // last 
            HelpClass.clearValidateWindow(requiredControlList, this);
        }

        private void validate()
        {
            try
            {
                HelpClass.validateWindow(requiredControlList, this);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async Task activate()
        {//activate
            countryPackageDate.isActive = 1;
            decimal s = await countryPackageDateModel.Save(countryPackageDate);
            if (s <= 0)
                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
            else
            {
                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopActive"), animation: ToasterAnimation.FadeIn);
                await RefreshCountryPackageDateList();
                await Search();
            }
        }

        private bool chkIfExist()
        {
            int month = 0, year = 0;
            switch (cb_month.SelectedValue.ToString())
            {
                case "1": month = 1; year = 0; break;
                case "3": month = 3; year = 0; break;
                case "6": month = 6; year = 0; break;
                case "12": month = 0; year = 1; break;
                case "0": month = 0; year = 0; break;
                default: break;
            }
            if (countryPackageDates.Any(c => c.countryId == Convert.ToInt32(cb_country.SelectedValue) && c.monthCount == month && c.yearCount == year))
                return true;
            else
                return false;
        }
        #endregion

        #region events

        private async void Tgl_isActive_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                if (countryPackageDates is null)
                    await RefreshCountryPackageDateList();
                tgl_countryPackageDateState = 1;
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
                if (countryPackageDates is null)
                    await RefreshCountryPackageDateList();
                tgl_countryPackageDateState = 0;
                await Search();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            GC.Collect();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {

            }
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);

                if (e.Key == Key.Return)
                {
                    Btn_save_Click(null, null);
                }
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

        private void Btn_colse_Click(object sender, RoutedEventArgs e)
        {//close
            try
            {
                this.Close();
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

        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {//decimal
            try
            {
                var regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
                if (regex.IsMatch(e.Text) && !(e.Text == "." && ((TextBox)sender).Text.Contains(e.Text)))
                    e.Handled = false;

                else
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void validateEmpty_LostFocus(object sender, RoutedEventArgs e)
        {
            validate();
        }

        private void ValidateEmpty_TextChange(object sender, TextChangedEventArgs e)
        {
            validate();
        }
        private async void Cb_country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//select country
            try
            {
                if (cb_country.SelectedIndex != -1)
                    txt_currency.Text = FillCombo.countrynum.Where(c => c.countryId == Convert.ToInt32(cb_country.SelectedValue)).FirstOrDefault().currency;
                else
                    txt_currency.Text = "";
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Chk_allCountries_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                cb_country_.IsEnabled = false;
                cb_country_.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Chk_allCountries_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                cb_country_.IsEnabled = true;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Cb_country__SelectionChanged(object sender, SelectionChangedEventArgs e)
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
        #endregion

        #region save  update delete  clear  search   select  refresh

        private async void Btn_refresh_Click(object sender, RoutedEventArgs e)
        {//refresh
            try
            {
                HelpClass.StartAwait(grid_main);

                chk_allCountries.IsChecked = true;
                await RefreshCountryPackageDateList();
                await Search();

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }

        }
        private void Dg_package_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//selection
            try
            {
                if (dg_package.SelectedIndex != -1)
                {
                    countryPackageDate = dg_package.SelectedItem as CountryPackageDate;
                    this.DataContext = countryPackageDate;
                    if (countryPackageDate != null)
                    {
                        this.DataContext = countryPackageDate;

                    if (!countryPackageDate.islimitDate)
                            cb_month.SelectedValue = 0;
                        else
                        {
                            switch (countryPackageDate.monthCount)
                            {
                                case 1 : cb_month.SelectedValue = 1;  break;
                                case 3 : cb_month.SelectedValue = 3;  break;
                                case 6 : cb_month.SelectedValue = 6;  break;
                                case 12: cb_month.SelectedValue = 12; break;
                            }
                        }

                        this.DataContext = countryPackageDate;

                        #region delete
                        if (countryPackageDate.canDelete)
                            btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");
                        else
                        {
                            if (countryPackageDate.isActive == 0)
                                btn_delete.Content = MainWindow.resourcemanager.GetString("trActive");
                            else
                                btn_delete.Content = MainWindow.resourcemanager.GetString("trInActive");
                        }
                        #endregion
                    }
                }

                HelpClass.clearValidateWindow(requiredControlList, this);

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
        private async void Btn_save_Click(object sender, RoutedEventArgs e)
        {//save
            try
            {
                HelpClass.StartAwait(grid_main);

                countryPackageDate = new CountryPackageDate();
                
                if (!chkIfExist())
                {
                    if (HelpClass.validateWindow(requiredControlList, this))
                    {
                        countryPackageDate.countryId = Convert.ToInt32(cb_country.SelectedValue);
                        countryPackageDate.packageId = packageID;
                        countryPackageDate.monthCount = Convert.ToInt32(cb_month.SelectedValue);
                        countryPackageDate.price = decimal.Parse(tb_price.Text);
                        if (Convert.ToInt32(cb_month.SelectedValue) == 0)
                            countryPackageDate.islimitDate = false;
                        else
                            countryPackageDate.islimitDate = true;
                        countryPackageDate.notes = tb_notes.Text;
                        countryPackageDate.isActive = 1;
                        countryPackageDate.createUserId = MainWindow.userLogin.userId;
                        countryPackageDate.updateUserId = MainWindow.userLogin.userId;

                        decimal s = await countryPackageDateModel.Save(countryPackageDate);
                        if (s <= 0)
                            Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                        else
                        {
                            Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopAdd"), animation: ToasterAnimation.FadeIn);

                            Clear();
                            await RefreshCountryPackageDateList();
                            await Search();
                        }
                    }
                }
                else
                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopAlreadyExist"), animation: ToasterAnimation.FadeIn);
                HelpClass.EndAwait(grid_main);
            }
                catch (Exception ex)
                {
                    HelpClass.EndAwait(grid_main);
                    HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Btn_update_Click(object sender, RoutedEventArgs e)
        {//update
            try
            {
                HelpClass.StartAwait(grid_main);

                if (!chkIfExist())
                {
                    if ((HelpClass.validateWindow(requiredControlList, this)) && (countryPackageDate.Id != 0))
                    {
                        countryPackageDate.countryId = Convert.ToInt32(cb_country.SelectedValue);
                        countryPackageDate.packageId = packageID;
                        countryPackageDate.monthCount = Convert.ToInt32(cb_month.SelectedValue);
                        countryPackageDate.price = decimal.Parse(tb_price.Text);
                        if (Convert.ToInt32(cb_month.SelectedValue) == 0)
                            countryPackageDate.islimitDate = false;
                        else
                            countryPackageDate.islimitDate = true;
                        countryPackageDate.notes = tb_notes.Text;
                        countryPackageDate.isActive = 1;
                        countryPackageDate.createUserId = MainWindow.userLogin.userId;
                        countryPackageDate.updateUserId = MainWindow.userLogin.userId;

                        decimal s = await countryPackageDateModel.Save(countryPackageDate);
                        if (s <= 0)
                            Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                        else
                        {
                            Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopUpdate"), animation: ToasterAnimation.FadeIn);

                            await RefreshCountryPackageDateList();
                            await Search();
                        }
                    }
                }
                else
                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopAlreadyExist"), animation: ToasterAnimation.FadeIn);
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Btn_delete_Click(object sender, RoutedEventArgs e)
        {//delete
            try
            {
                HelpClass.StartAwait(grid_main);
                if (countryPackageDate.Id != 0)
                {
                    if ((!countryPackageDate.canDelete) && (countryPackageDate.isActive == 0))
                    {
                        #region
                        Window.GetWindow(this).Opacity = 0.2;
                        wd_acceptCancelPopup w = new wd_acceptCancelPopup();
                        w.contentText = MainWindow.resourcemanager.GetString("trMessageBoxActivate");
                        w.ShowDialog();
                        Window.GetWindow(this).Opacity = 1;
                        #endregion

                        if (w.isOk)
                            await activate();
                    }
                    else
                    {
                        #region
                        Window.GetWindow(this).Opacity = 0.2;
                        wd_acceptCancelPopup w = new wd_acceptCancelPopup();
                        if (countryPackageDate.canDelete)
                            w.contentText = MainWindow.resourcemanager.GetString("trMessageBoxDelete");
                        if (!countryPackageDate.canDelete)
                            w.contentText = MainWindow.resourcemanager.GetString("trMessageBoxDeactivate");
                        w.ShowDialog();
                        Window.GetWindow(this).Opacity = 1;
                        #endregion

                        if (w.isOk)
                        {
                            string popupContent = "";
                            if (countryPackageDate.canDelete) popupContent = MainWindow.resourcemanager.GetString("trPopDelete");
                            if ((!countryPackageDate.canDelete) && (countryPackageDate.isActive == 1)) popupContent = MainWindow.resourcemanager.GetString("trPopInActive");

                            decimal s = await countryPackageDate.Delete(countryPackageDate.Id, MainWindow.userLogin.userId, countryPackageDate.canDelete);
                            if (s < 0)
                                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                            else
                            {
                                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopDelete"), animation: ToasterAnimation.FadeIn);

                                await RefreshCountryPackageDateList();
                                await Search();
                                Clear();
                            }
                        }
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



        #endregion

       
    }
}
