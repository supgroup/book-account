using BookAccountApp.ApiClasses;
using BookAccountApp.Classes;
using POS.View.windows;
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
        FileClass fileClassrow = new FileClass();
        IEnumerable<FileClass> fileClassQuery;
        IEnumerable<FileClass> fileClassList;
        public int itemId = 0;
        public string type = "";
        string txtSearch = "";

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
                await Search();



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
            txt_title.Text = MainWindow.resourcemanager.GetString("Docs");
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, MainWindow.resourcemanager.GetString("trSearchHint"));
            tt_close.Content = MainWindow.resourcemanager.GetString("trClose");
            //btn_save.Content = MainWindow.resourcemanager.GetString("trSave");

            dg_files.Columns[0].Header = MainWindow.resourcemanager.GetString("docName");
            dg_files.Columns[1].Header = MainWindow.resourcemanager.GetString("ext");
            dg_files.Columns[2].Header = MainWindow.resourcemanager.GetString("select");
            btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");
            btn_export.Content = MainWindow.resourcemanager.GetString("trExport");
            //tt_refresh.Content = MainWindow.resourcemanager.GetString("trRefresh");
            //tt_report.Content = MainWindow.resourcemanager.GetString("trPdf");
            //tt_excel.Content = MainWindow.resourcemanager.GetString("trExcel");
            //tt_count.Content = MainWindow.resourcemanager.GetString("trCount");

            chk_allSerials.Content = MainWindow.resourcemanager.GetString("selectAll");

        }

        async Task<IEnumerable<FileClass>> RefreshFilesList()
        {

            fileClassList = await fileClassrow.GetbytypeId(type, itemId);

            return fileClassList;
        }
        void RefreshFilesView()
        {

            dg_files.ItemsSource = fileClassQuery;
            //txt_count.Text = dg_items.Items.Count.ToString();
        }
        async Task Search()
        {

            if (fileClassList is null)
                await RefreshFilesList();

            fileClassQuery = fileClassList;
            RefreshFilesView();

        }
        IEnumerable<FileClass> fileClassQueryPage = new List<FileClass>();
        #endregion



        private void Chk_allSerials_Checked(object sender, RoutedEventArgs e)
        {
           
            try
            {
                chk_allSerials.Content = MainWindow.resourcemanager.GetString("trUnSelectAll");
                //isActiveCount = fileClassQuery.Count(c => c.isActive ==true);

                foreach (var s in fileClassQuery)
                {
                   
                        s.isActive = true;
                
                     
                }
                dg_files.ItemsSource = fileClassQuery;
                dg_files.Items.Refresh();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
         
        }

        private void Chk_allSerials_Unchecked(object sender, RoutedEventArgs e)
        {
        
            try
            {
                chk_allSerials.Content = MainWindow.resourcemanager.GetString("trSelectAll");

                foreach (var s in fileClassQuery)
                {

                    s.isActive = false;


                }
                dg_files.ItemsSource = fileClassQuery;
                dg_files.Items.Refresh();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
            
        }

        private void Btn_export_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                List<FileClass> selectedList = fileClassQuery.Where(f => f.isActive == true).ToList();
                if (selectedList.Count > 0)
                {
                    string sourceDirroot = "";
                    string sourceDir = "";
                    if (type == "passenger")
                    {
                        sourceDirroot = Global.rootpassengerFolder;
                    }
                    else if (type == "office")
                    {
                        sourceDirroot = Global.rootofficeFolder;
                    }
                    else if (type == "service")
                    {
                        sourceDirroot = Global.rootservicefilesFolder;
                    }

                    string foldername = itemId.ToString();
                    sourceDir = System.IO.Path.Combine(sourceDirroot, foldername);
                    string sourceFile = "";
                    //

                    System.Windows.Forms.FolderBrowserDialog folderbdialog = new System.Windows.Forms.FolderBrowserDialog();
                   
                    System.Windows.Forms.DialogResult result = folderbdialog.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        string destfolderName = folderbdialog.SelectedPath;
                        foreach (FileClass row in selectedList)
                        {
                            sourceFile = System.IO.Path.Combine(sourceDir, row.fileName + row.extention);
                            string newname = row.fileName + row.extention;
                            string destFile = System.IO.Path.Combine(destfolderName, newname);

                            while (File.Exists(destFile))
                            {
                                newname = row.fileName + "-" + HelpClass.GenerateRandomNo();
                                destFile = System.IO.Path.Combine(destfolderName, newname + row.extention);
                            }
                            File.Copy(sourceFile, destFile);

                        }
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopExport"), animation: ToasterAnimation.FadeIn);
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
                List<FileClass> selectedList = fileClassQuery.Where(f => f.isActive == true).ToList();
                if (selectedList.Count > 0)
                {
                    Window.GetWindow(this).Opacity = 0.2;
                    wd_acceptCancelPopup w = new wd_acceptCancelPopup();
                    w.contentText = MainWindow.resourcemanager.GetString("trMessageBoxDelete");
                    w.ShowDialog();
                    Window.GetWindow(this).Opacity = 1;
 

                    if (w.isOk)
                    {

                        string sourceDirroot = "";
                        string sourceDir = "";
                        if (type == "passenger")
                        {
                            sourceDirroot = Global.rootpassengerFolder;
                        }
                        else if (type == "office")
                        {
                            sourceDirroot = Global.rootofficeFolder;
                        }
                        else if (type == "service")
                        {
                            sourceDirroot = Global.rootservicefilesFolder;
                        }

                        string foldername = itemId.ToString();
                        sourceDir = System.IO.Path.Combine(sourceDirroot, foldername);
                        string sourceFile = "";
                        //
                        FileClass filedelete = new FileClass();
                        decimal res = 0;
                        foreach (FileClass row in selectedList)
                        {
                            sourceFile = System.IO.Path.Combine(sourceDir, row.fileName + row.extention);
                            // string newname = row.fileName + row.extention;
                          
                            if (File.Exists(sourceFile))
                            {
                                // If file found, delete it
                                File.Delete(sourceFile);
                            }
                            res = await fileClassrow.DeletebytypeId(row.fileId, MainWindow.userLogin.userId, true, type);
                        }
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopDelete"), animation: ToasterAnimation.FadeIn);
                        await RefreshFilesList();
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
    }
}
