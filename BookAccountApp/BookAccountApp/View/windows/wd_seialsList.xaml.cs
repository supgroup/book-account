using BookAccountApp.ApiClasses;
using BookAccountApp.Classes;
using Microsoft.Reporting.WinForms;
using Microsoft.Win32;
using netoaster;
using System;
using System.Collections.Generic;
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
using System.IO;
using BookAccountApp.ApiClasses;
using System.Text.RegularExpressions;

namespace BookAccountApp.View.windows
{
    /// <summary>
    /// Interaction logic for wd_seialsList.xaml
    /// </summary>
    public partial class wd_seialsList : Window
    {
        PosSerials posSerialModel = new PosSerials();
        IEnumerable<PosSerials> posSerialsQuery;
        IEnumerable<PosSerials> posSerials;
        public int packageUserID = 0;
        string txtSearch = "";
        //print
        ReportCls reportclass = new ReportCls();
        LocalReport rep = new LocalReport();
        SaveFileDialog saveFileDialog = new SaveFileDialog();

        PackageUser pu = new PackageUser();
        PackageUser puModel = new PackageUser();
        Packages p = new Packages();
        Packages pModel = new Packages();
        int isActiveCount = 0;

        int itemsPerPage = 20;
        private int pageCount;

        public wd_seialsList()
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
            btns = new Button[] { btn_firstPage, btn_prevPage, btn_activePage, btn_nextPage, btn_lastPage };

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
            translat();
            #endregion

            #region fill page count combo
            var typelist = new[] {
                new { Text = "20"    , Value = "20" },
                new { Text = "50"    , Value = "50" },
                new { Text = "100"   , Value = "100"}
                    };
            cb_itemPerPage.DisplayMemberPath = "Text";
            cb_itemPerPage.SelectedValuePath = "Value";
            cb_itemPerPage.ItemsSource = typelist;
            #endregion

            chk_allSerials.IsChecked = false;
            posSerialsQuery = await RefreshList();

            isActiveCount = posSerials.Count(s => s.isActive == 1);

            pu = await puModel.GetByID(packageUserID);
            p = await pModel.GetByID(pu.packageId.Value);

            cb_itemPerPage.SelectedIndex = 0;

            Tb_search_TextChanged(tb_search, null);
             
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

        private async void Btn_save_Click(object sender, RoutedEventArgs e)
        {//save
            try
            {
                HelpClass.StartAwait(grid_main);

                if ((isActiveCount <= p.posCount) || (p.posCount == -1))
                {
                   List< PosSerialsUpdate> uplist = new List<PosSerialsUpdate>();
                    //posSerialsQueryPage
                   
                    int x = posSerialsQueryPage.Count();
                    List<PosSerials> activeposSerialsQuery = posSerialsQuery.Where(X => X.isActive == 1).ToList();
                    foreach (PosSerials srow in activeposSerialsQuery)
                    {

                        PosSerialsUpdate uprow = new PosSerialsUpdate();
                        uprow.serialId =srow.serialId ;
                        uprow.isActive = srow.isActive;
                       // if (srow.isActive == 1)
                            uplist.Add(uprow);
                    }
                    

                   int res = await posSerialModel.UpdateList(uplist.ToList(), MainWindow.userID,  packageUserID);
             
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
                posSerialsQuery = posSerialsQuery.Where(s => s.serial.Contains(txtSearch)
                || (s.posName    != null ? s.posName.Contains(txtSearch)    : false)
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

                posSerialsQuery = await RefreshList();

                /*
                 RefreshView();
                 */
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        #endregion

        #region events
        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    Btn_save_Click(null, null);
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
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, MainWindow.resourcemanager.GetString("trSearchHint"));
            tt_close.Content = MainWindow.resourcemanager.GetString("trClose");
            btn_save.Content = MainWindow.resourcemanager.GetString("trSave");

            dg_serials.Columns[0].Header = MainWindow.resourcemanager.GetString("trSerialNum");
            dg_serials.Columns[1].Header = MainWindow.resourcemanager.GetString("trBranch");
            dg_serials.Columns[2].Header = MainWindow.resourcemanager.GetString("trPOS");
            dg_serials.Columns[3].Header = MainWindow.resourcemanager.GetString("trIsActive");

            tt_refresh.Content = MainWindow.resourcemanager.GetString("trRefresh");
            tt_report.Content = MainWindow.resourcemanager.GetString("trPdf");
            tt_excel.Content = MainWindow.resourcemanager.GetString("trExcel");
            tt_count.Content = MainWindow.resourcemanager.GetString("trCount");

            chk_allSerials.Content = MainWindow.resourcemanager.GetString("trSelectAll");

        }

        async Task<IEnumerable<PosSerials>> RefreshList()
        {
            posSerials = await posSerialModel.GetSerialAndPosInfo(packageUserID);
            return posSerials;
        }
        IEnumerable<PosSerials> posSerialsQueryPage = new List<PosSerials>();
        #endregion
        #region Pagination Y
        Pagination pagination = new Pagination();
        Button[] btns;
        public int pageIndex = 1;
        void refreshGrid()
        {
            if (btns is null)
                btns = new Button[] { btn_firstPage, btn_prevPage, btn_activePage, btn_nextPage, btn_lastPage };
            posSerialsQueryPage = pagination.refrishPagination(posSerialsQuery, pageIndex, btns, itemsPerPage);
            foreach (PosSerials srow in posSerialsQuery)
            {
                foreach (PosSerials grow in posSerialsQueryPage)
                {
                    if (grow.serialId == srow.serialId)

                        srow.isActive = grow.isActive;
                }
            }
            dg_serials.ItemsSource = posSerialsQueryPage;

        }

        private void Tb_pageNumberSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);

                if (tb_pageNumberSearch.Text.Equals(""))
                {
                    pageIndex = 1;
                }
                else if (((posSerialsQuery.Count() - 1) / itemsPerPage) + 1 < int.Parse(tb_pageNumberSearch.Text))
                {
                    pageIndex = ((posSerialsQuery.Count() - 1) / itemsPerPage) + 1;
                }
                else
                {
                    pageIndex = int.Parse(tb_pageNumberSearch.Text);
                }

                #region
                
                posSerialsQuery = posSerialsQuery.Where(s => s.serial.Contains(txtSearch)
                || (s.posName != null ? s.posName.Contains(txtSearch) : false)
                || (s.branchName != null ? s.branchName.Contains(txtSearch) : false));
                txt_count.Text = posSerialsQuery.Count().ToString();

                refreshGrid();
                #endregion
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
    
        private void Btn_firstPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);

                pageIndex = 1;
                #region
                posSerialsQuery = posSerialsQuery.Where(s => s.serial.Contains(txtSearch)
                || (s.posName != null ? s.posName.Contains(txtSearch) : false)
                || (s.branchName != null ? s.branchName.Contains(txtSearch) : false));
                txt_count.Text = posSerialsQuery.Count().ToString();
                refreshGrid();
                #endregion
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
        private void Btn_prevPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);

                pageIndex = int.Parse(btn_prevPage.Content.ToString());
                #region
                posSerialsQuery = posSerialsQuery.Where(s => s.serial.Contains(txtSearch)
                || (s.posName != null ? s.posName.Contains(txtSearch) : false)
                || (s.branchName != null ? s.branchName.Contains(txtSearch) : false));
                txt_count.Text = posSerialsQuery.Count().ToString();
                refreshGrid();
                #endregion
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
        private void Btn_activePage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);

                pageIndex = int.Parse(btn_activePage.Content.ToString());
                #region
                posSerialsQuery = posSerialsQuery.Where(s => s.serial.Contains(txtSearch)
                || (s.posName != null ? s.posName.Contains(txtSearch) : false)
                || (s.branchName != null ? s.branchName.Contains(txtSearch) : false));
                refreshGrid();
                #endregion
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
        private void Btn_nextPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);

                pageIndex = int.Parse(btn_nextPage.Content.ToString());
                #region
                posSerialsQuery = posSerialsQuery.Where(s => s.serial.Contains(txtSearch)
                || (s.posName != null ? s.posName.Contains(txtSearch) : false)
                || (s.branchName != null ? s.branchName.Contains(txtSearch) : false));
                txt_count.Text = posSerialsQuery.Count().ToString();
                refreshGrid();
                #endregion
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
        private void Btn_lastPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);

                //posSerialsQuery = items.Where(x => x.isActive == tglCategoryState);
                pageIndex = ((posSerialsQuery.Count() - 1) / itemsPerPage) + 1;
                #region
                posSerialsQuery = posSerialsQuery.Where(s => s.serial.Contains(txtSearch)
                || (s.posName != null ? s.posName.Contains(txtSearch) : false)
                || (s.branchName != null ? s.branchName.Contains(txtSearch) : false));
                txt_count.Text = posSerialsQuery.Count().ToString();
                refreshGrid();
                #endregion
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
        #endregion
        #region reports

        //ReportCls reportclass = new ReportCls();

        public async void  BuildReport()
        {

            List<ReportParameter> paramarr = new List<ReportParameter>();

            string addpath = "";
            string firstTitle = "Serials";
            string secondTitle = "";
            string subTitle = "";
            string Title = "";

            bool isArabic = ReportCls.checkLang();
            //if (isArabic)
            //{
            //addpath = @"\Reports\StatisticReport\Accounts\Paymetns\Ar\ArVendor.rdlc";
            //secondTitle = "vendors";
            //}
            //else
            //{
            //addpath = @"\Reports\StatisticReport\Accounts\Paymetns\En\Vendor.rdlc";
            //secondTitle = "vendors";
            //}
            addpath = @"\Reports\Sale\Book\Serials\En\serials.rdlc";
         
            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
            PackageUser puModel = new PackageUser();
            Customers cuModel = new Customers();
            Users agModel = new Users();

            puModel = await puModel.GetByID(packageUserID);
            cuModel = await cuModel.GetByID((int)puModel.customerId);
            agModel= await agModel.GetByID((int)puModel.userId);
            string serkey = puModel.packageSaleCode;
            string agentname = "";
            if (agModel.userId==3)
            {
                agentname = "SupClouds";
            }
            else
            {
                 agentname = agModel.name + " " + agModel.lastName;
            }
          

        

            //ReportCls.checkLang();
            //subTitle = clsReports.ReportTabTitle(firstTitle, secondTitle);
            Title = "Serials";
            paramarr.Add(new ReportParameter("trTitle", Title));

            paramarr.Add(new ReportParameter("Agent", agentname));
            paramarr.Add(new ReportParameter("Customer", cuModel.custname +" "+ cuModel.lastName));
            paramarr.Add(new ReportParameter("serverKey", serkey));

            List<PosSerials> repserialList = new List<PosSerials>();

            repserialList = await posSerialModel.GetByPackUserId(packageUserID);
            clsReports.serialsReport(repserialList.Where(s=>s.isActive==1), rep, reppath, paramarr);
            //clsReports.setReportLanguage(paramarr);
            //clsReports.Header(paramarr);

            rep.SetParameters(paramarr);

            rep.Refresh();
        }
        private void Btn_pdf_Click(object sender, RoutedEventArgs e)
        {//pdf
            try
            {
                HelpClass.StartAwait(grid_main);

                #region
                BuildReport();

                saveFileDialog.Filter = "PDF|*.pdf;";

                if (saveFileDialog.ShowDialog() == true)
                {
                    string filepath = saveFileDialog.FileName;
                   LocalReportExtensions.ExportToPDF(rep, filepath);
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

                PackageUser puModel = new PackageUser();
                PackageUser pu = await puModel.GetByID(packageUserID);
                string customer = pu.customerName +" "+pu.customerLastName;
                string package = pu.packageName;
                string key = pu.packageSaleCode;
                string agent = pu.userName + " " + pu.userLastName;
                string s = "";
                #region
                Thread t1 = new Thread(() =>
                {
                    BuildReport();
                    this.Dispatcher.Invoke(() =>
                    {
                        saveFileDialog.Filter = "EXCEL|*.xls;";
                        if (saveFileDialog.ShowDialog() == true)
                        {
                            string filepath = saveFileDialog.FileName;
                            LocalReportExtensions.ExportToExcel(rep, filepath);
                        }
                    });
                });
                t1.Start();
                #endregion

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        #endregion

        private void Chk_allSerials_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                chk_allSerials.Content = MainWindow.resourcemanager.GetString("trUnSelectAll");
                isActiveCount = posSerialsQueryPage.Count(c => c.isActive == 1);

                foreach (var s in posSerialsQueryPage)
                {
                    if ((isActiveCount <= p.posCount) || (p.posCount == -1))
                    {
                        s.isActive = 1;
                        dg_serials.ItemsSource = posSerialsQueryPage;
                        dg_serials.Items.Refresh();
                    }
                }
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
           
                foreach (var s in posSerialsQueryPage)
                {
                    s.isActive = 0;
                    dg_serials.ItemsSource = posSerialsQueryPage;
                    dg_serials.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Cb_itemPerPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //try
            //{
            itemsPerPage = int.Parse(cb_itemPerPage.SelectedValue.ToString());
            pageCount = posSerialsQuery.Count() / itemsPerPage;
            pageIndex = 1;
            Tb_search_TextChanged(tb_search, null);
            /*
            RefreshView();
            */
            //}
            //catch (Exception ex)
            //{
            //    HelpClass.ExceptionMessage(ex, this);
            //}
        }

    }
}
