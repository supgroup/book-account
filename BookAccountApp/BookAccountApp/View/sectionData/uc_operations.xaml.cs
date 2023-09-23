using BookAccountApp.ApiClasses;
using BookAccountApp.Classes;
using BookAccountApp.View.windows;
using Microsoft.Win32;
using netoaster;
using POS.View.windows;
using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Reporting.WinForms;

namespace BookAccountApp.View.sectionData
{
    /// <summary>
    /// Interaction logic for uc_operations.xaml
    /// </summary>
    public partial class uc_operations : UserControl
    {
        public uc_operations()
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
        private static uc_operations _instance;
        public static uc_operations Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_operations();
                return _instance;
            }
        }

        Systems SystemModel = new Systems();
        IEnumerable<Systems> SystemsQuery;
        IEnumerable<Systems> SystemsList;
        bool first = true;
        bool tgl_operationsstate;
        string searchText = "";
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        public static List<string> requiredControlList;
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);

                requiredControlList = new List<string> { "name", "company_commission" };

                #region translate
                //if (MainWindow.lang.Equals("en"))
                //{
                //    MainWindow.resourcemanager = new ResourceManager("BookAccountApp.en_file", Assembly.GetExecutingAssembly());
                //    grid_main.FlowDirection = FlowDirection.LeftToRight;
                //}
                //else
                //{
                //    MainWindow.resourcemanager = new ResourceManager("BookAccountApp.ar_file", Assembly.GetExecutingAssembly());
                //    grid_main.FlowDirection = FlowDirection.RightToLeft;
                //}
                translate();
                #endregion

                //await FillCombo.fillCountries(cb_areaMobile);
                //await FillCombo.fillCountries(cb_areaPhone);
                //await FillCombo.fillCountries(cb_areaFax);
                //await FillCombo.fillCountriesNames(cb_country);
                //FillCombo.fillAgentLevel(cb_custlevel);
          
                Keyboard.Focus(tb_name);

                await RefreshOperationssList();
                await Search();
              
                Clear();
               // fillcombos();
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
            //bookInfo bookSysNameHint companyCommissionHint bookSystem officeCommissions

            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, MainWindow.resourcemanager.GetString("trSearchHint"));
            //txt_baseInformation.Text = MainWindow.resourcemanager.GetString("trBaseInformation");
            txt_active.Text = MainWindow.resourcemanager.GetString("trActive");
            txt_title.Text = MainWindow.resourcemanager.GetString("bookInfo"); 
       txt_officeSystemButton.Text = MainWindow.resourcemanager.GetString("officeCommissions");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_name, MainWindow.resourcemanager.GetString("bookSysNameHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_company_commission, MainWindow.resourcemanager.GetString("companyCommissionHint"));
                     
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_notes, MainWindow.resourcemanager.GetString("trNoteHint"));
 
            //   SystemsList
           
            dg_operations.Columns[0].Header = MainWindow.resourcemanager.GetString("trNo.");        
            dg_operations.Columns[1].Header = MainWindow.resourcemanager.GetString("bookSystem");
            dg_operations.Columns[2].Header = MainWindow.resourcemanager.GetString("companyCommission");
            dg_operations.Columns[3].Header = MainWindow.resourcemanager.GetString("trNote");
            tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");
            tt_report.Content = MainWindow.resourcemanager.GetString("trPdf");
            tt_excel.Content = MainWindow.resourcemanager.GetString("trExcel");
            tt_preview.Content = MainWindow.resourcemanager.GetString("trPreview");
            tt_print.Content = MainWindow.resourcemanager.GetString("trPrint");
            btn_add.Content = MainWindow.resourcemanager.GetString("trAdd");
            btn_update.Content = MainWindow.resourcemanager.GetString("trUpdate");
            btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");

        }
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
                        await RefreshOperationssList();
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
             
                if (SystemModel.systemId > 0)
                {
                    if (HelpClass.validate(requiredControlList, this)  )
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

                            await RefreshOperationssList();
                            await Search();
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

                                    await RefreshOperationssList();
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
        private async Task activate()
        {//activate
            SystemModel.isActive = true;
            var s = await SystemModel.Save(SystemModel);
            if (s <= 0)
                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
            else
            {
                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopActive"), animation: ToasterAnimation.FadeIn);
                await RefreshOperationssList();
                await Search();
            }
        }
        #endregion
        #region events
        public async Task fillcombos()
        {
            //await FillCombo.fillStatementsTable(cb_opStatement);
            //await FillCombo.fillDurationsTable(cb_duration);
           
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

                if (SystemsList is null)
                    await RefreshOperationssList();
                tgl_operationsstate = true;
                if (first)
                {
                    first = false;
                }
                else
                {
                    await Search();

                }

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
                if (SystemsList is null)
                    await RefreshOperationssList();
                tgl_operationsstate = false;
                await Search();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Btn_clear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                Clear();
                await fillcombos();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Dg_operations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
              
                //selection
                if (dg_operations.SelectedIndex != -1)
                {
                    SystemModel = dg_operations.SelectedItem as Systems;
             
                    if (SystemModel != null)
                    {
                        //tb_custCode.Text = SystemModel.custCode;
                        //cb_country.SelectedValue = SystemModel.countryId;
                 
                        this.DataContext = SystemModel;
                        tb_company_commission.Text = HelpClass.DecTostring(SystemModel.company_commission);
                        #region delete
                        if (SystemModel.canDelete)
                            btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");
                        else
                        {
                            if (SystemModel.isActive == false)
                                btn_delete.Content = MainWindow.resourcemanager.GetString("trActive");
                            else
                                btn_delete.Content = MainWindow.resourcemanager.GetString("trInActive");
                        }
                        #endregion
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
        private async void Btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {//refresh

                HelpClass.StartAwait(grid_main);
                await RefreshOperationssList();
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

        #region Refresh & Search
        async Task Search()
        {
            //search
            if (SystemsList is null)
                await RefreshOperationssList();
          
            searchText = tb_search.Text.ToLower();
            SystemsQuery = SystemsList.Where(s =>
            ( 
            s.name.ToLower().Contains(searchText) ||
            s.notes.ToLower().Contains(searchText)  
            
            ) && s.isActive == tgl_operationsstate);
            //&& s.isActive == tgl_operationsstate
            //);
            
            RefreshOperationssView();
        }
        async Task<IEnumerable<Systems>> RefreshOperationssList()
        {
            SystemsList = await SystemModel.GetAll();
            return SystemsList;
        }
        void RefreshOperationssView()
        {
            dg_operations.ItemsSource = SystemsQuery;
            //txt_count.Text = SystemsQuery.Count().ToString();
        }
        #endregion

        #region validate - clearValidate - textChange - lostFocus - . . . . 

        void Clear()
        {
            this.DataContext = new Systems();
            tb_company_commission.Text = "";
            // last 
            HelpClass.clearValidate(requiredControlList, this);
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
                HelpClass.validate(requiredControlList, this);
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
                HelpClass.validate(requiredControlList, this);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        #endregion



        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            // Collect all generations of memory.
            GC.Collect();
        }

        #region reports

        ReportCls reportclass = new ReportCls();
        LocalReport rep = new LocalReport();
        SaveFileDialog saveFileDialog2 = new SaveFileDialog();

        public void BuildReport()
        {

            //string firstTitle = "paymentsReport";
            ////string secondTitle = "";
            ////string subTitle = "";
            //string Title = "";
           
            List<ReportParameter> paramarr = new List<ReportParameter>();

            string addpath;
            bool isArabic = ReportCls.checkLang();
            //if (isArabic)
            //{
            addpath = @"\Reports\SectionData\Ar\ArSystems.rdlc";

            //}
            //else
            //{
            //    addpath = @"\Reports\SectionData\En\EnOperationss.rdlc";
            //}
            //D:\myproj\posproject3\BookAccountApp\BookAccountApp\Reports\statisticReports\En\EnBook.rdlc
            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
            //     subTitle = clsReports.ReportTabTitle(firstTitle, secondTitle);
            //  Title = MainWindow.resourcemanagerreport.GetString("trAccountantReport");

            clsReports.SystemReport(SystemsQuery, rep, reppath, paramarr);
            clsReports.setReportLanguage(paramarr);
            clsReports.Header(paramarr);

            rep.SetParameters(paramarr);

            rep.Refresh();
           
        }

        private void Btn_pdf_Click(object sender, RoutedEventArgs e)
        {

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

        private void Btn_preview_Click(object sender, RoutedEventArgs e)
        {

            //preview
            try
            {
                HelpClass.StartAwait(grid_main);

                #region
                Window.GetWindow(this).Opacity = 0.2;

                string pdfpath = "";
                //
                pdfpath = @"\Thumb\report\temp.pdf";
                pdfpath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, pdfpath);

                BuildReport();

                LocalReportExtensions.ExportToPDF(rep, pdfpath);
                wd_previewPdf w = new wd_previewPdf();
                w.pdfPath = pdfpath;
                if (!string.IsNullOrEmpty(w.pdfPath))
                {
                    w.ShowDialog();
                    w.wb_pdfWebViewer.Dispose();


                }
                Window.GetWindow(this).Opacity = 1;
                #endregion

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }

        }

        private void Btn_print_Click(object sender, RoutedEventArgs e)
        {

            //print
            try
            {
                HelpClass.StartAwait(grid_main);

                #region
                BuildReport();
                LocalReportExtensions.PrintToPrinterbyNameAndCopy(rep, FillCombo.rep_printer_name, FillCombo.rep_print_count == null ? short.Parse("1") : short.Parse(FillCombo.rep_print_count));
                #endregion

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }

        }

        private void Btn_exportToExcel_Click(object sender, RoutedEventArgs e)
        {

            //excel
            try
            {
                HelpClass.StartAwait(grid_main);

                #region
                //Thread t1 = new Thread(() =>
                //{
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


                //});
                //t1.Start();
                #endregion

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }

        }

        private void Btn_pieChart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);

                //if (FillCombo.groupObject.HasPermissionAction(basicsPermission, FillCombo.groupObjects, "report"))
                //{
                #region
                Window.GetWindow(this).Opacity = 0.2;
                win_lvc win = new win_lvc(SystemsQuery, 4);
                win.ShowDialog();
                Window.GetWindow(this).Opacity = 1;
                #endregion
                //}
                //else
                //    Toaster.ShowInfo(Window.GetWindow(this), message: AppSettings.resourcemanager.GetString("trdontHavePermission"), animation: ToasterAnimation.FadeIn);

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                Window.GetWindow(this).Opacity = 1;
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }


        #endregion

        private void Btn_exportData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_importData_Click(object sender, RoutedEventArgs e)
        {

        }
        /*
        private async void Btn_addOpStatement_Click(object sender, RoutedEventArgs e)
        {
            
           try
           {
               if (sender != null)
                   HelpClass.StartAwait(grid_main);
               Window.GetWindow(this).Opacity = 0.2;
               wd_statements w = new wd_statements();
               w.ShowDialog();
               await FillCombo.fillStatementsTable(cb_opStatement);
               Window.GetWindow(this).Opacity = 1;

               if (sender != null)
                   HelpClass.EndAwait(grid_main);
           }
           catch (Exception ex)
           {
               Window.GetWindow(this).Opacity = 1;
               if (sender != null)
                   HelpClass.EndAwait(grid_main);
               HelpClass.ExceptionMessage(ex, this);
           }
            
        }

        private async void Btn_addDuration_Click(object sender, RoutedEventArgs e)
        {
          
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);
                Window.GetWindow(this).Opacity = 0.2;
                wd_durations  w = new wd_durations();
                w.ShowDialog();
                await FillCombo.fillDurationsTable(cb_duration);
                Window.GetWindow(this).Opacity = 1;

                if (sender != null)
                    HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                Window.GetWindow(this).Opacity = 1;
                if (sender != null)
                    HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
             
        }
        */
        private void Btn_officeSystem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);
                if (SystemModel.systemId > 0)
                {
              

                Window.GetWindow(this).Opacity = 0.2;
                wd_officeSystem w = new wd_officeSystem();
                    w.type = "systems";
                    w.SystemModel = SystemModel;
                w.ShowDialog();
                Window.GetWindow(this).Opacity = 1;
                }
                else
                {

                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trSelectItemFirst"), animation: ToasterAnimation.FadeIn);

                }
                //

                if (sender != null)
                    HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                Window.GetWindow(this).Opacity = 1;
                if (sender != null)
                    HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
    }
}
