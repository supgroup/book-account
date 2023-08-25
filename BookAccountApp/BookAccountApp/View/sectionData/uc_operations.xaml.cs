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

        Operations operations = new Operations();
        IEnumerable<Operations> operationssQuery;
        IEnumerable<Operations> operationss;
        byte tgl_operationsstate;
        string searchText = "";
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        public static List<string> requiredControlList;
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);

                requiredControlList = new List<string> { "operation" };

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

                Keyboard.Focus(tb_operation);

                await RefreshOperationssList();
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
            //txt_baseInformation.Text = MainWindow.resourcemanager.GetString("trBaseInformation");
            txt_active.Text = MainWindow.resourcemanager.GetString("trActive");
            txt_title.Text = MainWindow.resourcemanager.GetString("operationInfo");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_operation, MainWindow.resourcemanager.GetString("operationNameHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_opStatement, MainWindow.resourcemanager.GetString("opStatementHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_duration, MainWindow.resourcemanager.GetString("durationHint"));            
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_notes, MainWindow.resourcemanager.GetString("trNoteHint"));

            //txt_contactInformation.Text = MainWindow.resourcemanager.GetString("trContactInformation");

            //MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_custlevel, MainWindow.resourcemanager.GetString("trLevelHint"));
            //txt_contactInformation.Text = MainWindow.resourcemanager.GetString("trContactInformation");
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_notes, MainWindow.resourcemanager.GetString("trNoteHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_address, MainWindow.resourcemanager.GetString("trAdressHint"));
            /*
            father  
fatherHint  	
mother  
motherHint  
    */
            //   operationss
           
            dg_operations.Columns[0].Header = MainWindow.resourcemanager.GetString("trNo.");        
            dg_operations.Columns[1].Header = MainWindow.resourcemanager.GetString("operationName");
            dg_operations.Columns[2].Header = MainWindow.resourcemanager.GetString("opStatement");
            dg_operations.Columns[3].Header = MainWindow.resourcemanager.GetString("duration");
          

            //dg_operations.Columns[3].Header = MainWindow.resourcemanager.GetString("trMobile");

            tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");
            tt_report.Content = MainWindow.resourcemanager.GetString("trPdf");
            tt_excel.Content = MainWindow.resourcemanager.GetString("trExcel");
            tt_preview.Content = MainWindow.resourcemanager.GetString("trPreview");
            tt_print.Content = MainWindow.resourcemanager.GetString("trPrint");
            //tt_count.Content = MainWindow.resourcemanager.GetString("trCount");

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
            
                operations = new Operations();
                if (HelpClass.validate(requiredControlList, this))
                {
                    //tb_custCode.Text = await operations.generateCodeNumber("cu");

                    operations.operation = tb_operation.Text;
                    operations.opStatement =cb_opStatement.Text;
                    operations.duration = cb_duration.Text;                   
                    operations.notes = tb_notes.Text;

                    operations.createUserId = MainWindow.userLogin.userId;
                    operations.updateUserId = MainWindow.userLogin.userId;


                    decimal s = await operations.Save(operations);
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
              
                if (operations.operationId > 0)
                {
                    if (HelpClass.validate(requiredControlList, this) && HelpClass.IsValidEmail(this))
                    {
                        operations.operation = tb_operation.Text;
                        operations.opStatement = cb_opStatement.Text;
                        operations.duration = cb_duration.Text;
                        operations.notes = tb_notes.Text;

                        decimal s = await operations.Save(operations);
                        if (s <= 0)
                            Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                        else
                        {
                            Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopUpdate"), animation: ToasterAnimation.FadeIn);

                            //if (isImgPressed)
                            //{
                            //    int operationId = (int)s;
                            //    string b = await operations.uploadImage(imgFileName, Md5Encription.MD5Hash("Inc-m" + operationId.ToString()), operationId);
                            //    operations.image = b;
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
                if (operations.operationId != 0)
                {
                    decimal s = await operations.Delete(operations.operationId, MainWindow.userLogin.userId, true);
                    if (s < 0)
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("cannotdelete"), animation: ToasterAnimation.FadeIn);
                    else
                    {
                        operations.operationId = 0;
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopDelete"), animation: ToasterAnimation.FadeIn);

                        await RefreshOperationssList();
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
        private async Task activate()
        {//activate
            /*
            operations.isActive = 1;
            decimal s = await operations.Save(operations);
            if (s <= 0)
                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
            else
            {
                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopActive"), animation: ToasterAnimation.FadeIn);
                await RefreshOperationssList();
                await Search();
            }
            */
        }
        #endregion
        #region events
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
                /*
                if (operationss is null)
                    await RefreshOperationssList();
                tgl_operationsstate = 1;
                await Search();
                */
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
                if (operationss is null)
                    await RefreshOperationssList();
                tgl_operationsstate = 0;
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
        {
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
        private async void Dg_operations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);

                //selection
                if (dg_operations.SelectedIndex != -1)
                {
                    operations = dg_operations.SelectedItem as Operations;
                    this.DataContext = operations;
                    if (operations != null)
                    {
                        //tb_custCode.Text = operations.custCode;
                        //cb_country.SelectedValue = operations.countryId;
                        this.DataContext = operations;
                        //await getImg();
                        #region delete
                        //if (operations.canDelete)
                        //    btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");
                        //else
                        //{
                        //    if (operations.isActive == 0)
                        //        btn_delete.Content = MainWindow.resourcemanager.GetString("trActive");
                        //    else
                        //        btn_delete.Content = MainWindow.resourcemanager.GetString("trInActive");
                        //}
                        #endregion
                        //HelpClass.getMobile(operations.mobile, cb_areaMobile, tb_mobile);
                        //HelpClass.getPhone(operations.phone, cb_areaPhone, cb_areaPhoneLocal, tb_phone);
                        //HelpClass.getPhone(operations.fax, cb_areaFax, cb_areaFaxLocal, tb_fax);
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
            if (operationss is null)
                await RefreshOperationssList();
          
            searchText = tb_search.Text.ToLower();
            operationssQuery = operationss.Where(s =>
            (s.operationId.ToString().Contains(searchText) ||
            s.operation.ToLower().Contains(searchText) ||
            s.opStatement.ToLower().Contains(searchText) ||
            s.duration.ToLower().Contains(searchText)
            
            ));
            //&& s.isActive == tgl_operationsstate
            //);
            
            RefreshOperationssView();
        }
        async Task<IEnumerable<Operations>> RefreshOperationssList()
        {
            operationss = await operations.GetAll();
            return operationss;
        }
        void RefreshOperationssView()
        {
            dg_operations.ItemsSource = operationssQuery;
            //txt_count.Text = operationssQuery.Count().ToString();
        }
        #endregion

        #region validate - clearValidate - textChange - lostFocus - . . . . 

        void Clear()
        {
            this.DataContext = new Operations();


            // last 
            HelpClass.clearValidate(requiredControlList, this);
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
            addpath = @"\Reports\SectionData\Ar\ArOperation.rdlc";

            //}
            //else
            //{
            //    addpath = @"\Reports\SectionData\En\EnOperationss.rdlc";
            //}
            //D:\myproj\posproject3\BookAccountApp\BookAccountApp\Reports\statisticReports\En\EnBook.rdlc
            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
            //     subTitle = clsReports.ReportTabTitle(firstTitle, secondTitle);
            //  Title = MainWindow.resourcemanagerreport.GetString("trAccountantReport");

            clsReports.OperationReport(operationssQuery, rep, reppath, paramarr);
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
                LocalReportExtensions.PrintToPrinterbyNameAndCopy(rep, FillCombo.getdefaultPrinters(), FillCombo.rep_print_count == null ? short.Parse("1") : short.Parse(FillCombo.rep_print_count));
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
                    saveFileDialog2.Filter = "EXCEL|*.xls;";
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

        }



        #endregion

        private void Btn_exportData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_importData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_addOpStatement_Click(object sender, RoutedEventArgs e)
        {
            /*
           try
           {
               if (sender != null)
                   HelpClass.StartAwait(grid_main);
               Window.GetWindow(this).Opacity = 0.2;
               wd_flight w = new wd_flight();
               w.ShowDialog();
               await FillCombo.fillFlightTable(cb_flight);
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
           */
        }

        private void Btn_addDuration_Click(object sender, RoutedEventArgs e)
        {
            /*
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);
                Window.GetWindow(this).Opacity = 0.2;
                wd_flight w = new wd_flight();
                w.ShowDialog();
                await FillCombo.fillFlightTable(cb_flight);
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
            */
        }
    }
}
