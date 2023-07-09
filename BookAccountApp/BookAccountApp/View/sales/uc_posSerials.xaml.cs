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
    /// Interaction logic for uc_posSerials.xaml
    /// </summary>
    public partial class uc_posSerials : UserControl
    {

        IEnumerable<PosSerials> posSerialsQuery;
        IEnumerable<PosSerials> posSerials;
        PosSerials posSerial = new PosSerials();
        string searchText = "";
        public static List<string> requiredControlList;
        PackageUser pmodel = new PackageUser();

        private static uc_posSerials _instance;
        public static uc_posSerials Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_posSerials();
                return _instance;
            }
        }
        public uc_posSerials()
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


        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);

                requiredControlList = new List<string> { "" };

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

                await FillCombo.fillPackageUser(cb_package);
                FillCombo.fillBooked(cb_booked);
                chk_allPackages.IsChecked = true;

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
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, MainWindow.resourcemanager.GetString("trSearchHint"));
            txt_baseInformation.Text = MainWindow.resourcemanager.GetString("trBaseInformation");
            chk_allPackages.Content = MainWindow.resourcemanager.GetString("trAll");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_package, MainWindow.resourcemanager.GetString("trPackageHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_booked, MainWindow.resourcemanager.GetString("trBookHint"));
            txt_title.Text = MainWindow.resourcemanager.GetString("trPosSerial");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_packageNum, MainWindow.resourcemanager.GetString("trPackageCodeHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_serial, MainWindow.resourcemanager.GetString("trSerialNumHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_posDeviceCode, MainWindow.resourcemanager.GetString("trPosDeviceCodeHint"));
            txt_isBooked.Text = MainWindow.resourcemanager.GetString("trBooked");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_notes, MainWindow.resourcemanager.GetString("trNoteHint"));

            dg_posSerial.Columns[0].Header = MainWindow.resourcemanager.GetString("trPackageCode");
            dg_posSerial.Columns[1].Header = MainWindow.resourcemanager.GetString("trSerialNum");
            dg_posSerial.Columns[2].Header = MainWindow.resourcemanager.GetString("trBooked");

            tt_refresh.Content = MainWindow.resourcemanager.GetString("trRefresh");
            tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");
            tt_report.Content = MainWindow.resourcemanager.GetString("trPdf");
            tt_excel.Content = MainWindow.resourcemanager.GetString("trExcel");
            tt_preview.Content = MainWindow.resourcemanager.GetString("trPreview");
            tt_print.Content = MainWindow.resourcemanager.GetString("trPrint");
            tt_count.Content = MainWindow.resourcemanager.GetString("trCount");

            btn_add.Content = MainWindow.resourcemanager.GetString("trAdd");

        }

        private async void Btn_add_Click(object sender, RoutedEventArgs e)
        {//add

        }
        private async void Btn_refresh_Click(object sender, RoutedEventArgs e)
        {//refresh
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

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Dg_posSerial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//selection
            try
            {
                HelpClass.StartAwait(grid_main);
                
                if (dg_posSerial.SelectedIndex != -1)
                {
                    posSerial = dg_posSerial.SelectedItem as PosSerials;
                    this.DataContext = posSerial;

                    if (posSerial != null)
                    {

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
            if (posSerials is null)
                await RefreshPosSerialsList();

            searchText = tb_search.Text.ToLower();
            posSerialsQuery = posSerials.Where(s => 
            (  
            s.serial.ToLower().Contains(searchText)
            ||
            s.packageSaleCode.ToLower().Contains(searchText)
            ) 
            && (cb_booked.SelectedIndex != -1   ? cb_booked.SelectedValue.ToString() == "all" ? true :
                                                  s.isBooked == Convert.ToBoolean(cb_booked.SelectedValue)
                                                : true)
            && (cb_package.SelectedIndex != -1  ? s.packageUserId == Convert.ToInt32(cb_package.SelectedValue) : true)
            );

            RefreshPosSerialsView();
        }
        async Task<IEnumerable<PosSerials>> RefreshPosSerialsList()
        {
            posSerials = await posSerial.GetAll();
            return posSerials;
        }
        void RefreshPosSerialsView()
        {
            dg_posSerial.ItemsSource = posSerialsQuery;
            txt_count.Text = posSerialsQuery.Count().ToString();
        }
        #endregion
        #region validate - clearValidate - textChange - lostFocus - . . . . 
        void Clear()
        {
            this.DataContext = new PosSerials();
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

        private async void Cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void Chk_allPackages_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                cb_package.SelectedItem = null;
                cb_package.IsEnabled = false;
            }
            catch (Exception ex)
            {

                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Chk_allPackages_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                cb_package.IsEnabled = true;
            }
            catch (Exception ex)
            {

                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Dg_posSerial_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {//selection
            try
            {
                HelpClass.StartAwait(grid_main);

                if (dg_posSerial.SelectedIndex != -1)
                {
                    posSerial = dg_posSerial.SelectedItem as PosSerials;
                    this.DataContext = posSerial;
                    if (posSerial != null)
                    {
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

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            GC.Collect();
        }
    }
}
