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

namespace BookAccountApp.View.windows
{
    /// <summary>
    /// Interaction logic for wd_operations.xaml
    /// </summary>
    public partial class wd_systems : Window
    {
        #region variables

        Systems SystemModel = new Systems();
        //IEnumerable<Operations> operationssQuery;
        //IEnumerable<Operations> operationss;
        public static List<string> requiredControlList;

        bool tgl_priceState = true;
        string searchText = "";
        BrushConverter bc = new BrushConverter();


        #endregion

        public wd_systems()
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
                requiredControlList = new List<string> { "name", "company_commission" };
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

               //await fillcombos();
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

 
            //txt_baseInformation.Text = MainWindow.resourcemanager.GetString("trBaseInformation");
            
            txt_title.Text = MainWindow.resourcemanager.GetString("bookInfo");
             MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_name, MainWindow.resourcemanager.GetString("bookSysNameHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_company_commission, MainWindow.resourcemanager.GetString("companyCommissionHint"));

            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_notes, MainWindow.resourcemanager.GetString("trNoteHint"));

            //MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_opStatement, MainWindow.resourcemanager.GetString("opStatementHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_duration, MainWindow.resourcemanager.GetString("durationHint"));
 
            tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");
            btn_add.Content = MainWindow.resourcemanager.GetString("trAdd");
            btn_update.Content = MainWindow.resourcemanager.GetString("trUpdate");
            btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");

        }




        void Clear()
        {
            this.DataContext = new Systems();
            tb_company_commission.Text = "";
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


        /*
        private async Task<bool> chkIfNameIsExists(string name, int uId)
        {
            bool isValid = true;
            if (operationsList == null)
                await RefreshOperationsList();
            if (operationsList.Any(i => i.name == name && i.operationsId != uId))
                isValid = false;
            if (!isValid)
                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trErrorDuplicateNameToolTip"), animation: ToasterAnimation.FadeIn);
            return isValid;
        }
        */
        #endregion

        #region events

     
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



        #region Add - Update - Delete - Search - Tgl - Clear - DG_SelectionChanged - refresh
        private async void Btn_add_Click(object sender, RoutedEventArgs e)
        {//add
            try
            {
                //Country coumod = new Country();
                //TimeSpan ts = new TimeSpan();
                //ts = await coumod.GetOffsetTime(int.Parse(tb_custname.Text));
                //tb_lastName.Text = ts.ToString();

                HelpClass.StartAwait(grid_main);

                SystemModel = new Systems();
                if (HelpClass.validate(requiredControlList, this))
                {
                    //tb_custCode.Text = await SystemModel.generateCodeNumber("cu");


                    SystemModel.name = tb_name.Text.Trim();
                    SystemModel.company_commission = (tb_company_commission.Text == null || tb_company_commission.Text == "") ? 0 : Convert.ToDecimal(tb_company_commission.Text);
                    SystemModel.notes = tb_notes.Text;

                    SystemModel.createUserId = MainWindow.userLogin.userId;
                    SystemModel.updateUserId = MainWindow.userLogin.userId;


                    decimal s = await SystemModel.Save(SystemModel);
                    if (s <= 0)
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                    else
                    {
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopAdd"), animation: ToasterAnimation.FadeIn);


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
        private async void Btn_update_Click(object sender, RoutedEventArgs e)
        {//update
            try
            {
                HelpClass.StartAwait(grid_main);

                if (SystemModel.systemId > 0)
                {
                    if (HelpClass.validate(requiredControlList, this))
                    {


                        SystemModel.name = tb_name.Text.Trim();
                        SystemModel.company_commission = (tb_company_commission.Text == null || tb_company_commission.Text == "") ? 0 : Convert.ToDecimal(tb_company_commission.Text);
                        SystemModel.notes = tb_notes.Text;
                        SystemModel.updateUserId = MainWindow.userLogin.userId;
                        decimal s = await SystemModel.Save(SystemModel);
                        if (s <= 0)
                            Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                        else
                        {
                            Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopUpdate"), animation: ToasterAnimation.FadeIn);

                            //if (isImgPressed)
                            //{
                            //    int systemId = (int)s;
                            //    string b = await SystemModel.uploadImage(imgFileName, Md5Encription.MD5Hash("Inc-m" + systemId.ToString()), systemId);
                            //    SystemModel.image = b;
                            //    isImgPressed = false;
                            //    if (!b.Equals(""))
                            //    {
                            //        await getImg();
                            //    }
                            //    else
                            //    {
                            //        HelpClass.clearImg(btn_image);
                            //    }
                            //}

                           
                        }
                    }
                }
                else
                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trSelectItemFirst"), animation: ToasterAnimation.FadeIn);

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

                if (SystemModel.systemId != 0)
                {
                    if ((!SystemModel.canDelete) && (SystemModel.isActive == false))
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
                        if (SystemModel.canDelete)
                            w.contentText = MainWindow.resourcemanager.GetString("trMessageBoxDelete");
                        if (!SystemModel.canDelete)
                            w.contentText = MainWindow.resourcemanager.GetString("trMessageBoxDeactivate");
                        w.ShowDialog();
                        Window.GetWindow(this).Opacity = 1;
                        #endregion

                        if (w.isOk)
                        {
                            string popupContent = "";
                            if (SystemModel.canDelete) popupContent = MainWindow.resourcemanager.GetString("trPopDelete");
                            if ((!SystemModel.canDelete) && (SystemModel.isActive == true)) popupContent = MainWindow.resourcemanager.GetString("trPopInActive");

                            var s = await SystemModel.Delete(SystemModel.systemId, MainWindow.userLogin.userId, SystemModel.canDelete);
                            if (s < 0)
                                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                            else
                            {
                                SystemModel.systemId = 0;
                                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopDelete"), animation: ToasterAnimation.FadeIn);

                               
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
        private async Task activate()
        {//activate
            SystemModel.isActive = true;
            var s = await SystemModel.Save(SystemModel);
            if (s <= 0)
                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
            else
            {
                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopActive"), animation: ToasterAnimation.FadeIn);
                
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
        string input;
        decimal _decimal = 0;
        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
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
    }
}
