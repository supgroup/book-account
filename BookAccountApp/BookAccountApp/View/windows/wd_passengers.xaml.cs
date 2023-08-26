using netoaster;
using BookAccountApp.Classes;
using BookAccountApp.ApiClasses;
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
using Microsoft.Reporting.WinForms;
using Microsoft.Win32;
using POS.View.windows;
using BookAccountApp.View.sectionData;

namespace BookAccountApp.View.windows
{
    /// <summary>
    /// Interaction logic for wd_passengers.xaml
    /// </summary>
    public partial class wd_passengers : Window
    {
        #region variables

        Passengers passengersrow = new Passengers();

        IEnumerable<Passengers> passengersQuery;
        IEnumerable<Passengers> passengersList;
        public static List<string> requiredControlList;

        bool tgl_priceState = true;
        string searchText = "";
        BrushConverter bc = new BrushConverter();


        #endregion

        public wd_passengers()
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
                if (sender != null)
                    HelpClass.StartAwait(grid_sliceMain);
                requiredControlList = new List<string> { "name" };
                #region translate
                //if (AppSettings.lang.Equals("en"))
                //{
                //    MainWindow.resourcemanager = new ResourceManager("BookAccountApp.en_file", Assembly.GetExecutingAssembly());
                //    grid_main.FlowDirection = FlowDirection.LeftToRight;
                //}
                //else
                //{
                //    MainWindow.resourcemanager = new ResourceManager("BookAccountApp.ar_file", Assembly.GetExecutingAssembly());
                grid_main.FlowDirection = FlowDirection.RightToLeft;
                //}
                translate();
                #endregion

                //await FillCombo.fillItemCombo(cb_item);

                //if (FillCombo.itemunitsList is null)
                //    await FillCombo.RefreshItemUnits();

                await Search();

                #region key up
                // key_up item

                //cb_unit.Text = "";
                #endregion

                if (sender != null)
                    HelpClass.EndAwait(grid_sliceMain);
            }
            catch (Exception ex)
            {
                if (sender != null)
                    HelpClass.EndAwait(grid_sliceMain);
                HelpClass.ExceptionMessage(ex, this);
            }

        }

        #region methods
        private void translate()
        {

            txt_title.Text = MainWindow.resourcemanager.GetString("passenger");
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_custCode, MainWindow.resourcemanager.GetString("trCodeHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_name, MainWindow.resourcemanager.GetString("trNameHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_lastName, MainWindow.resourcemanager.GetString("trLastNameHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_father, MainWindow.resourcemanager.GetString("fatherHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_mother, MainWindow.resourcemanager.GetString("motherHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_notes, MainWindow.resourcemanager.GetString("trNoteHint"));
            btn_add.Content = MainWindow.resourcemanager.GetString("trAdd");
            btn_update.Content = MainWindow.resourcemanager.GetString("trUpdate");
            btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");

            btn_clear.ToolTip = MainWindow.resourcemanager.GetString("trClear");
 

        }
        async Task Search()
        {

            if (passengersList is null)
                await RefreshPassengersList();

            searchText = tb_search.Text.ToLower();

            passengersQuery = passengersList.Where(s =>
            (s.name.ToLower().Contains(searchText)));
            RefreshPassengersView();

        }

        async Task<IEnumerable<Passengers>> RefreshPassengersList()
        {
            passengersList = await passengersrow.GetAll();

            return passengersList;
        }
        void RefreshPassengersView()
        {
            dg_items.ItemsSource = passengersQuery;
            txt_count.Text = dg_items.Items.Count.ToString();
        }
        void Clear()
        {
            this.DataContext = new Flights();
            // last 
            HelpClass.clearValidate(requiredControlList, this);
        }
        public static bool validate(List<string> requiredControlList, UserControl userControl)
        {
            bool isValid = true;
            try
            {
                foreach (var control in requiredControlList)
                {
                    TextBox textBox = FindControls.FindVisualChildren<TextBox>(userControl).Where(x => x.Name == "tb_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (textBox != null && path != null)
                        if (!HelpClass.validateEmpty(textBox.Text, path))
                            isValid = false;
                }
                foreach (var control in requiredControlList)
                {
                    ComboBox comboBox = FindControls.FindVisualChildren<ComboBox>(userControl).Where(x => x.Name == "cb_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (comboBox != null && path != null)
                        if (!HelpClass.validateEmpty(comboBox.Text, path))
                            isValid = false;
                }
                foreach (var control in requiredControlList)
                {
                    PasswordBox passwordBox = FindControls.FindVisualChildren<PasswordBox>(userControl).Where(x => x.Name == "pb_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (passwordBox != null && path != null)
                        if (!HelpClass.validateEmpty(passwordBox.Password, path))
                            isValid = false;
                }
                #region Email
                //  IsValidEmail(userControl);
                #endregion


            }
            catch { }
            return isValid;

        }



        private async Task<bool> chkIfNameIsExists(string name, int uId)
        {
            bool isValid = true;
            /*
            if (passengersList == null)
                await RefreshPassengersList();
            if (passengersList.Any(i => i.name == name && i.passengerId != uId))
                isValid = false;
            if (!isValid)
                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trErrorDuplicateNameToolTip"), animation: ToasterAnimation.FadeIn);
           */
            return isValid;
        }

        #endregion

        #region events


        private void Dg_items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//selection
            try
            {
                HelpClass.StartAwait(grid_main);
                //selection
                if (dg_items.SelectedIndex != -1)
                {
                    passengersrow = dg_items.SelectedItem as Passengers;
                    //   this.DataContext = passengersrow;
                    if (passengersrow != null)
                    {
                        this.DataContext = passengersrow;
                    }
                }
                HelpClass.clearValidate(requiredControlList, this);
                //p_error_email.Visibility = Visibility.Collapsed;

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch //(Exception ex)
            {
                //HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    //Btn_save_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            GC.Collect();
        }
        private void Btn_colse_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                this.Close();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {//search
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_sliceMain);

                await Search();

                if (sender != null)
                    HelpClass.EndAwait(grid_sliceMain);
            }
            catch (Exception ex)
            {
                if (sender != null)
                    HelpClass.EndAwait(grid_sliceMain);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Btn_refresh_Click(object sender, RoutedEventArgs e)
        {//refresh
            try
            {//refresh

                HelpClass.StartAwait(grid_main);
                await RefreshPassengersList();
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
        //private void DecimalValidationTextBox(object sender, TextCompositionEventArgs e)
        //{
        //    try
        //    {
        //        var regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
        //        if (regex.IsMatch(e.Text) && !(e.Text == "." && ((TextBox)sender).Text.Contains(e.Text)))
        //            e.Handled = false;

        //        else
        //            e.Handled = true;
        //    }
        //    catch (Exception ex)
        //    {

        //        HelpClass.ExceptionMessage(ex, this);
        //    }
        //}
        //private void space_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        TextBox textBox = sender as TextBox;
        //        HelpClass.InputJustNumber(ref textBox);
        //        e.Handled = e.Key == Key.Space;
        //    }
        //    catch (Exception ex)
        //    {

        //        HelpClass.ExceptionMessage(ex, this);
        //    }
        //}
        //private void Tb_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        /*
        //        string name = sender.GetType().Name;
        //        validateEmpty(name, sender);
        //        */
        //    }
        //    catch (Exception ex)
        //    {
        //        HelpClass.ExceptionMessage(ex, this);
        //    }
        //}
        //private void Tb_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        /*
        //        string name = sender.GetType().Name;
        //        validateEmpty(name, sender);
        //        */
        //    }
        //    catch (Exception ex)
        //    {
        //        HelpClass.ExceptionMessage(ex, this);
        //    }
        //}
        #endregion

        #region repots



        #endregion

        #region add - update - delete
        private async void Btn_add_Click(object sender, RoutedEventArgs e)
        {//add
            try
            {
                //Country coumod = new Country();
                //TimeSpan ts = new TimeSpan();
                //ts = await coumod.GetOffsetTime(int.Parse(tb_custname.Text));
                //tb_lastName.Text = ts.ToString();

                HelpClass.StartAwait(grid_main);
                bool duplicaterName = false;
                duplicaterName = await chkIfNameIsExists(tb_name.Text, 0);
                passengersrow = new Passengers();
                if (HelpClass.validate(requiredControlList, this) && duplicaterName)
                {
                    //tb_custCode.Text = await flights.generateCodeNumber("cu");

                    passengersrow.name = tb_name.Text;
                    passengersrow.notes = tb_notes.Text;
                    //passengersrow.isActive = true;
                    decimal s = await passengersrow.Save(passengersrow);
                    if (s <= 0)
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                    else
                    {
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopAdd"), animation: ToasterAnimation.FadeIn);
                        Clear();
                        await RefreshPassengersList();
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
        private async void Btn_update_Click(object sender, RoutedEventArgs e)
        {//update
            try
            {

                HelpClass.StartAwait(grid_main);
                bool duplicaterName = false;
                /*
                duplicaterName = await chkIfNameIsExists(tb_name.Text, passengersrow.passengerId);
                */

                if (HelpClass.validate(requiredControlList, this) && duplicaterName)
                {
                    passengersrow.name = tb_name.Text;
                    passengersrow.notes = tb_notes.Text;
                    //passengersrow.isActive = true;
                    decimal s = await passengersrow.Save(passengersrow);
                    if (s <= 0)
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                    else
                    {
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopUpdate"), animation: ToasterAnimation.FadeIn);
                        Clear();
                        await RefreshPassengersList();
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
        private async void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);

                if (passengersrow.passengerId != 0)
                {
                    decimal s = await passengersrow.Delete(passengersrow.passengerId, MainWindow.userLogin.userId, true);
                    if (s < 0)
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("cannotdelete"), animation: ToasterAnimation.FadeIn);
                    else
                    {
                        passengersrow.passengerId = 0;
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopDelete"), animation: ToasterAnimation.FadeIn);

                        await RefreshPassengersList();
                        await Search();
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


        #endregion

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
    }
}
