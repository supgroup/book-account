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

namespace BookAccountApp.View.windows
{
    /// <summary>
    /// Interaction logic for wd_officeSystem.xaml
    /// </summary>
     public partial class wd_officeSystem : Window
    {
        #region variables

        OfficeSystem officeSystemrow = new OfficeSystem();
     public Systems SystemModel = new Systems();
        public Office OfficeModel = new Office();
        IEnumerable<OfficeSystem> officeSystemQuery;
        IEnumerable<OfficeSystem> officeSystemList;
        public static List<string> requiredControlList;
        public string type = "";
        bool tgl_priceState = true;
        string searchText = "";
        BrushConverter bc = new BrushConverter();


        #endregion

        public wd_officeSystem()
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
                await RefreshofficeSystemList();
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

        #region methods
        private void translate()
        {
             
            txt_title.Text = MainWindow.resourcemanager.GetString("officeCommissions");
             
                txt_systemName.Text = type == "systems" ? SystemModel.name : OfficeModel.name;
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, MainWindow.resourcemanager.GetString("trSearchHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_name, MainWindow.resourcemanager.GetString("opStatementHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_notes, MainWindow.resourcemanager.GetString("trNoteHint"));

            btn_save.Content = MainWindow.resourcemanager.GetString("trSave");
            btn_cancel.Content = MainWindow.resourcemanager.GetString("trCancel");
            //btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");


            dg_items.Columns[0].Header = type == "systems" ? MainWindow.resourcemanager.GetString("trOffice") : MainWindow.resourcemanager.GetString("bookSystem");
            dg_items.Columns[1].Header = MainWindow.resourcemanager.GetString("commission");
             


        }
        

        
       
        /*
        void Clear()
        {
            this.DataContext = new Flights();
            // last 
            HelpClass.clearValidate(requiredControlList, this);
        }
        */
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






        #endregion

        #region events
        async Task Search()
        {
            //search
            if (officeSystemList is null)
                await RefreshofficeSystemList();

            //searchText = tb_search.Text.ToLower();
            //officeSystemQuery = officeSystemList.Where(s =>
            //(
            //s.name.ToLower().Contains(searchText) ||
            //s.notes.ToLower().Contains(searchText)

            //) && s.isActive == tgl_operationsstate);
            //&& s.isActive == tgl_operationsstate
            //);
            officeSystemQuery = officeSystemList;
            RefreshOfficeSystemView();
        }
        async Task<IEnumerable<OfficeSystem>> RefreshofficeSystemList()
        {
            if(type== "systems")
            {
                officeSystemList = await officeSystemrow.GetbySysId(SystemModel.systemId);
            }
           
            else if (type == "office")
            {               
   officeSystemList = await officeSystemrow.GetbyOfficeId(OfficeModel.officeId);
            }
        
            return officeSystemList;
        }
        void RefreshOfficeSystemView()
        {
            dg_items.ItemsSource = officeSystemQuery;
        }

        private void Dg_items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//selection
            try
            {
                HelpClass.StartAwait(grid_main);
                //selection
                if (dg_items.SelectedIndex != -1)
                {
                    officeSystemrow = dg_items.SelectedItem as OfficeSystem;
                    //   this.DataContext = officeSystemrow;
                    if (officeSystemrow != null)
                    {
                        this.DataContext = officeSystemrow;
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
      
       

       
        #endregion

        #region repots



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

        private async void Btn_save_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                HelpClass.StartAwait(grid_main);
                //if (HelpClass.validate(requiredControlList, this) && duplicaterName)
                //{
                    
                  int s = await officeSystemrow.updateList(officeSystemQuery.ToList());
                    if (s <= 0)
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                    else
                    {
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopUpdate"), animation: ToasterAnimation.FadeIn);
                        
                        await RefreshofficeSystemList();
                        await Search();
                    }
                //}
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
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
    }
}
