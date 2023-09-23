using BookAccountApp.Classes;
using BookAccountApp.ApiClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Drawing.Printing;
using netoaster;

namespace BookAccountApp.View.windows
{
    /// <summary>
    /// Interaction logic for wd_reportPrinterSetting.xaml
    /// </summary>
    public partial class wd_reportCopyCountSetting : Window
    {

        public wd_reportCopyCountSetting()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            { HelpClass.ExceptionMessage(ex, this); }
        }


        BrushConverter bc = new BrushConverter();







        /*
        public void fillcb_saleInvPaperSize()
        {

            cb_saleInvPaperSize.ItemsSource = papersizeList.Where(x => x.printfor.Contains("sal"));
            // var paperList = papersizeList.Where(x => x.printfor.Contains("sal"));
            cb_saleInvPaperSize.DisplayMemberPath = "paperSize1";
            cb_saleInvPaperSize.SelectedValuePath = "sizeId";
            if (salepapersizeId > 0)
            {
                cb_saleInvPaperSize.SelectedValue = salepapersizeId;
            }


        }
        */
        public static List<string> requiredControlList;
        private void refreshWindow()
        {

            //FillCombo.fillcb_repname(cb_repname);
            //FillCombo.fillcb_docpapersize(cb_docpapersize);

            tb_repPrintCount.Text = FillCombo.rep_copy_count;
        }
        private async Task<decimal> Saverep_copy_count()
        {

                SettingCls set = new SettingCls();
        SetValues setV = new SetValues();
        set = FillCombo.settingsCls.Where(s => s.name == "rep_copy_count").FirstOrDefault();
               int nameId = set.settingId;
        setV = FillCombo.settingsValues.Where(i => i.settingId == nameId).FirstOrDefault();
        setV.value = tb_repPrintCount.Text.ToString();
                decimal res = await setV.Save(setV);
            if (res>0)
            {
                FillCombo.rep_copy_count = setV.value;
            }
            return res;
    }
   
    
        private void Btn_colse_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);
                requiredControlList = new List<string> { "repPrintCount" };
                #region translate

                grid_main.FlowDirection = FlowDirection.RightToLeft;

                translate();
                #endregion

                  refreshWindow();

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
            
            txt_title.Text = MainWindow.resourcemanager.GetString("copyCount");  
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_repPrintCount, MainWindow.resourcemanager.GetString("copyCount") +"...");//
             //MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_saleInvPaperSize, MainWindow.resourcemanager.GetString("trInvoicesPaperSize") + "...");
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_docpapersize, MainWindow.resourcemanager.GetString("trDocPaperSize") + "...");

            btn_save.Content = MainWindow.resourcemanager.GetString("trSave");
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
            { HelpClass.ExceptionMessage(ex, this); }
        }

        List<SettingCls> set = new List<SettingCls>();

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                this.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
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



        private async void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);
                if (HelpClass.validate(requiredControlList, this)&& int.Parse(tb_repPrintCount.Text)>0)
                {
                    decimal msg = 0;
                    msg = await Saverep_copy_count();

                    //  refreshWindow();

                    // await MainWindow.getPrintersNames();
                    if (msg > 0)
                    {
                        await FillCombo.loading_getDefaultSystemInfo();
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopSave"), animation: ToasterAnimation.FadeIn);
                        await Task.Delay(500);
                        this.Close();
                    }
                    else
                    {
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                    }
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

        //private void Tb_validateEmptyLostFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        string name = sender.GetType().Name;
        //        validateEmpty(name, sender);
        //    }
        //    catch (Exception ex)
        //    { HelpClass.ExceptionMessage(ex, this); }
        //}
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
        //private void validateEmpty(string name, object sender)
        //{
        //    try
        //    {
        //        if (name == "TextBox")
        //        {
        //            if ((sender as TextBox).Name == "tb_purCopyCount")
        //                SectionData.validateEmptyTextBox((TextBox)sender, p_errorPurCopyCount, tt_errorPurCopyCount, "trEmptyError");
        //            else if ((sender as TextBox).Name == "tb_saleCopyCount")
        //                SectionData.validateEmptyTextBox((TextBox)sender, p_errorSaleCopyCount, tt_errorSaleCopyCount, "trEmptyError");
        //            else if ((sender as TextBox).Name == "tb_repPrintCount")
        //                HelpClass.validateEmptyTextBox((TextBox)sender, p_errorRepPrintCount, tt_errorRepPrintCount, "trEmptyError");
        //            else if ((sender as TextBox).Name == "tb_directEntry")
        //                SectionData.validateEmptyTextBox((TextBox)sender, p_errorDirectEntry, tt_errorDirectEntry, "trEmptyError");
        //        }
        //    }
        //    catch (Exception ex)
        //    { HelpClass.ExceptionMessage(ex, this); }
        //}


        private void Tb_count_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = sender as TextBox;
                if (textBox == null)
                {
                    return;
                }

             if (textBox.Name == "tb_repPrintCount")
                {
                    if (int.TryParse(textBox.Text, out _numRepPrintCount))
                        numRepPrintCount = int.Parse(textBox.Text);
                }
              

            }
            catch (Exception ex)
            { HelpClass.ExceptionMessage(ex, this); }
        }

        private void Tb_count_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Regex regex = new Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            }
            catch (Exception ex)
            { HelpClass.ExceptionMessage(ex, this); }
        }
        private void Tb_PreventSpaces(object sender, KeyEventArgs e)
        {
            try
            {
                e.Handled = e.Key == Key.Space;
            }
            catch (Exception ex)
            { HelpClass.ExceptionMessage(ex, this); }
        }
        #region NumericCount

        private int _numRepPrintCount = 1;
        public int numRepPrintCount
        {
            get { return _numRepPrintCount; }
            set
            {
                _numRepPrintCount = value;
                tb_repPrintCount.Text = value.ToString();
            }
        }

       
        

        private void Btn_countUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                 if (button.Tag.ToString() == "repPrintCount")
                    numRepPrintCount++;
                
            }
            catch (Exception ex)
            { HelpClass.ExceptionMessage(ex, this); }
        }

        private void Btn_countDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;

                if (button.Tag.ToString() == "repPrintCount" && numRepPrintCount > 1)
                    numRepPrintCount--;
                 
            }
            catch (Exception ex)
            { HelpClass.ExceptionMessage(ex, this); }
        }
        #endregion
    }
}
