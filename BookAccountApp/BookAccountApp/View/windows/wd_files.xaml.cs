using BookAccountApp.ApiClasses;
using BookAccountApp.Classes;
using Microsoft.Reporting.WinForms;
using netoaster;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
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
using netoaster;
using System.IO;

namespace BookAccountApp.View.windows
{
    /// <summary>
    /// Interaction logic for wd_files.xaml
    /// </summary>
    public partial class wd_files : Window
    {
        FileClass posSerialModel = new FileClass();
        IEnumerable<FileClass> fileClassQuery;
        IEnumerable<FileClass> fileClass;
        public int packageUserID = 0;
        string txtSearch = "";
     public string type = "";
        //print
        ReportCls reportclass = new ReportCls();
        LocalReport rep = new LocalReport();
        SaveFileDialog saveFileDialog = new SaveFileDialog();


        public wd_files()
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

                #region translate
                if (MainWindow.lang.Equals("en"))
                {
                    grid_main.FlowDirection = FlowDirection.LeftToRight;
                }
                else
                {
                    grid_main.FlowDirection = FlowDirection.RightToLeft;
                }
                translat();
                #endregion

                

                chk_allSerials.IsChecked = false;
                fileClassQuery = await RefreshList();



                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        #region save - search - close - refresh
        private void Btn_colse_Click(object sender, RoutedEventArgs e)
        {//close
            this.Close();
        }
        /*
        private async void Btn_save_Click(object sender, RoutedEventArgs e)
        {//save
            try
            {
                HelpClass.StartAwait(grid_main);

                if ((isActiveCount <= p.posCount) || (p.posCount == -1))
                {
                    List<FileClassUpdate> uplist = new List<FileClassUpdate>();
                    //fileClassQueryPage

                    int x = fileClassQueryPage.Count();
                    List<FileClass> activefileClassQuery = fileClassQuery.Where(X => X.isActive == 1).ToList();
                    foreach (FileClass srow in activefileClassQuery)
                    {

                        FileClassUpdate uprow = new FileClassUpdate();
                        uprow.serialId = srow.serialId;
                        uprow.isActive = srow.isActive;
                        // if (srow.isActive == 1)
                        uplist.Add(uprow);
                    }


                    int res = await posSerialModel.UpdateList(uplist.ToList(), MainWindow.userID, packageUserID);

                    if (res > 0)
                    {
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopSave"), animation: ToasterAnimation.FadeIn);
                        this.Close();
                    }
                    else
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                }
                else
                {
                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trIsActiveCountPopError"), animation: ToasterAnimation.FadeIn);
                }

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {//search
            try
            {
                txtSearch = tb_search.Text;
                fileClassQuery = fileClassQuery.Where(s => s.serial.Contains(txtSearch)
                || (s.posName != null ? s.posName.Contains(txtSearch) : false)
                || (s.branchName != null ? s.branchName.Contains(txtSearch) : false));

                refreshGrid();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Btn_refresh_Click(object sender, RoutedEventArgs e)
        {//refresh
            try
            {

                HelpClass.StartAwait(grid_main);

                fileClassQuery = await RefreshList();

               
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
            */

        #endregion

#region events
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
        {//unload
            GC.Collect();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {//mouse down
            try
            {
                DragMove();
            }
            catch (Exception ex)
            {
            }
        }

        private void TextBox_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            (sender as TextBox).IsReadOnly = false;
        }

        private void TextBox_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            (sender as TextBox).IsReadOnly = true;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.C && Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                (sender as TextBox).IsReadOnly = false;
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            (sender as TextBox).IsReadOnly = true;
        }

        #endregion

        #region methods
        private void translat()
        {
            txt_title.Text = MainWindow.resourcemanager.GetString("trSerials");
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, MainWindow.resourcemanager.GetString("trSearchHint"));
            tt_close.Content = MainWindow.resourcemanager.GetString("trClose");
            //btn_save.Content = MainWindow.resourcemanager.GetString("trSave");

            //dg_serials.Columns[0].Header = MainWindow.resourcemanager.GetString("trSerialNum");
            //dg_serials.Columns[1].Header = MainWindow.resourcemanager.GetString("trBranch");
            //dg_serials.Columns[2].Header = MainWindow.resourcemanager.GetString("trPOS");
            //dg_serials.Columns[3].Header = MainWindow.resourcemanager.GetString("trIsActive");

            //tt_refresh.Content = MainWindow.resourcemanager.GetString("trRefresh");
            //tt_report.Content = MainWindow.resourcemanager.GetString("trPdf");
            //tt_excel.Content = MainWindow.resourcemanager.GetString("trExcel");
            //tt_count.Content = MainWindow.resourcemanager.GetString("trCount");

            chk_allSerials.Content = MainWindow.resourcemanager.GetString("trSelectAll");

        }

        async Task<IEnumerable<FileClass>> RefreshList()
        {
            //fileClass = await posSerialModel.GetSerialAndPosInfo(packageUserID);
            return fileClass;
        }
        IEnumerable<FileClass> fileClassQueryPage = new List<FileClass>();
        #endregion
        
        

        private void Chk_allSerials_Checked(object sender, RoutedEventArgs e)
        {
            /*
            try
            {
                chk_allSerials.Content = MainWindow.resourcemanager.GetString("trUnSelectAll");
                isActiveCount = fileClassQueryPage.Count(c => c.isActive == 1);

                foreach (var s in fileClassQueryPage)
                {
                    if ((isActiveCount <= p.posCount) || (p.posCount == -1))
                    {
                        s.isActive = 1;
                        dg_serials.ItemsSource = fileClassQueryPage;
                        dg_serials.Items.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
            */
        }

        private void Chk_allSerials_Unchecked(object sender, RoutedEventArgs e)
        {
            /*
            try
            {
                chk_allSerials.Content = MainWindow.resourcemanager.GetString("trSelectAll");

                foreach (var s in fileClassQueryPage)
                {
                    s.isActive = 0;
                    dg_serials.ItemsSource = fileClassQueryPage;
                    dg_serials.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
            */
        }

        private void Btn_export_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
