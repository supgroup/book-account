﻿using BookAccountApp.Classes;
using netoaster;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Resources;
using System.Reflection;

namespace BookAccountApp.View.settings.emailGeneral
{
    /// <summary>
    /// Interaction logic for uc_emailTemplates.xaml
    /// </summary>
    public partial class uc_emailTemplates : UserControl
    {
        public uc_emailTemplates()
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
        private static uc_emailTemplates _instance;
        public static uc_emailTemplates Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_emailTemplates();
                return _instance;
            }
        }
        string savePermission = "emailTemplates_save";
     
        SetValues setValuesModel = new SetValues();
        SetValues setValues = new SetValues();
        IEnumerable<SetValues> setValuessQuery;
        IEnumerable<SetValues> setValuess;

        SettingCls setModel = new SettingCls();
        SettingCls sett = new SettingCls();
        IEnumerable<SettingCls> setQuery;
        IEnumerable<SettingCls> setQueryView;
       
        string searchText = "";
        public static List<string> requiredControlList;


        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            GC.Collect();
        }
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);
                requiredControlList = new List<string> { "name", };
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


                Keyboard.Focus(tb_title);
                
                 Search();
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
            txt_title.Text = MainWindow.resourcemanager.GetString("trEmailTemplates");

            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, MainWindow.resourcemanager.GetString("trSearchHint"));

            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_title, MainWindow.resourcemanager.GetString("trTitle") + "...");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_text1, MainWindow.resourcemanager.GetString("trText1") + "...");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_text2, MainWindow.resourcemanager.GetString("trText2") + "...");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_link1text, MainWindow.resourcemanager.GetString("trLinkText1") + "...");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_link1url, MainWindow.resourcemanager.GetString("trUrlLink1") + "...");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_link2text, MainWindow.resourcemanager.GetString("trLinkText2") + "...");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_link2url, MainWindow.resourcemanager.GetString("trUrlLink2") + "...");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_link3text, MainWindow.resourcemanager.GetString("trLinkText3") + "...");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_link3url, MainWindow.resourcemanager.GetString("trUrlLink3") + "...");

            btn_refresh.ToolTip = MainWindow.resourcemanager.GetString("trRefresh");
            btn_clear.ToolTip = MainWindow.resourcemanager.GetString("trClear");


            tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");
            tt_refresh.Content = MainWindow.resourcemanager.GetString("trRefresh");

            txt_infoTitle.Text = MainWindow.resourcemanager.GetString("trTitle");
            txt_infoBody.Text = MainWindow.resourcemanager.GetString("trBody");
            txt_infoEmailSupport.Text = MainWindow.resourcemanager.GetString("trEmailSupport");

            btn_save.Content = MainWindow.resourcemanager.GetString("trSave");

            dg_setValues.Columns[0].Header = MainWindow.resourcemanager.GetString("trName");
        }
        #region Add - Update - Delete - Search - Tgl - Clear - DG_SelectionChanged - refresh
        private async void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);



                //write here Mr.Naji

                /////
                decimal msg = 0;
                    setValues = setValuessQuery.Where(x => x.notes == "title").FirstOrDefault();
                    setValues.value = tb_title.Text;
                    msg += await setValuesModel.SaveValueByNotes(setValues);
                    //

                    setValues = setValuessQuery.Where(x => x.notes == "text1").FirstOrDefault();
                    setValues.value = tb_text1.Text;

                    msg += await setValuesModel.SaveValueByNotes(setValues);
                    setValues = setValuessQuery.Where(x => x.notes == "text2").FirstOrDefault();
                    setValues.value = tb_text2.Text;
                    msg += await setValuesModel.SaveValueByNotes(setValues);
                    setValues = setValuessQuery.Where(x => x.notes == "link1text").FirstOrDefault();
                    setValues.value = tb_link1text.Text;
                    msg += await setValuesModel.SaveValueByNotes(setValues);
                    setValues = setValuessQuery.Where(x => x.notes == "link2text").FirstOrDefault();
                    setValues.value = tb_link2text.Text;
                    msg += await setValuesModel.SaveValueByNotes(setValues);
                    setValues = setValuessQuery.Where(x => x.notes == "link3text").FirstOrDefault();
                    setValues.value = tb_link3text.Text;
                    msg += await setValuesModel.SaveValueByNotes(setValues);
                    setValues = setValuessQuery.Where(x => x.notes == "link1url").FirstOrDefault();
                    setValues.value = tb_link1url.Text;
                    msg += await setValuesModel.SaveValueByNotes(setValues);
                    setValues = setValuessQuery.Where(x => x.notes == "link2url").FirstOrDefault();
                    setValues.value = tb_link2url.Text;
                    msg += await setValuesModel.SaveValueByNotes(setValues);
                    setValues = setValuessQuery.Where(x => x.notes == "link3url").FirstOrDefault();
                    setValues.value = tb_link3url.Text;
                    msg += await setValuesModel.SaveValueByNotes(setValues);

              
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
        #region events
        void Search()
        {
            if (setQuery is null)
                RefreshSettingsList();
       

            searchText = tb_search.Text.ToLower();
            if(searchText.Trim() !="" && searchText.Trim()!=null)
            {
                setQuery = setQuery.Where(s => (s.name.ToLower().Contains(searchText)));

                RefreshSetttingsView();
            }
            
        }
        private async void Tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);

                Search();

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
        private void Btn_clear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Dg_setValues_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);

                if (dg_setValues.SelectedIndex != -1)
                {
                    if (dg_setValues.SelectedIndex != -1)
                    {
                        sett = dg_setValues.SelectedItem as SettingCls;
                        setValuessQuery = await setValuesModel.GetBySetName(sett.name);

                        //List<SettingCls> settLst = await setModel.GetAll();
                        //SettingCls setting = settLst.Where(s => s.settingId == sett.settingId).FirstOrDefault();
                        //setValuessQuery = await setValuesModel.GetBySetName(setting.name);

                        tb_title.Text = setValuessQuery.Where(x => x.notes == "title").FirstOrDefault() is null ? ""
                        : setValuessQuery.Where(x => x.notes == "title").FirstOrDefault().value.ToString();
                        tb_text1.Text = setValuessQuery.Where(x => x.notes == "text1").FirstOrDefault() is null ? ""
                           : setValuessQuery.Where(x => x.notes == "text1").FirstOrDefault().value.ToString();
                        tb_text2.Text = setValuessQuery.Where(x => x.notes == "text2").FirstOrDefault() is null ? ""
                        : setValuessQuery.Where(x => x.notes == "text2").FirstOrDefault().value.ToString();
                        tb_link1text.Text = setValuessQuery.Where(x => x.notes == "link1text").FirstOrDefault() is null ? ""
                        : setValuessQuery.Where(x => x.notes == "link1text").FirstOrDefault().value.ToString();

                        tb_link2text.Text = setValuessQuery.Where(x => x.notes == "link2text").FirstOrDefault() is null ? ""
                         : setValuessQuery.Where(x => x.notes == "link2text").FirstOrDefault().value.ToString();
                        tb_link3text.Text = setValuessQuery.Where(x => x.notes == "link3text").FirstOrDefault() is null ? ""
                        : setValuessQuery.Where(x => x.notes == "link3text").FirstOrDefault().value.ToString();


                        tb_link1url.Text = setValuessQuery.Where(x => x.notes == "link1url").FirstOrDefault() is null ? ""
                             : setValuessQuery.Where(x => x.notes == "link1url").FirstOrDefault().value.ToString();
                        tb_link2url.Text = setValuessQuery.Where(x => x.notes == "link2url").FirstOrDefault() is null ? ""
                               : setValuessQuery.Where(x => x.notes == "link2url").FirstOrDefault().value.ToString();

                        tb_link3url.Text = setValuessQuery.Where(x => x.notes == "link3url").FirstOrDefault() is null ? ""
                               : setValuessQuery.Where(x => x.notes == "link3url").FirstOrDefault().value.ToString();

                        this.DataContext = setValues;
                    }

                }

                if (setValues != null)
                {
                    //btn_addRange.IsEnabled = true;


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

      
        #endregion

        #region Refresh & Search
        async void RefreshSettingsList()
        {
            setQuery = await setModel.GetByNotes("emailtemp");
            foreach (SettingCls row in setQuery)
            {
                switch (row.name)
                {
                    case "pur_email_temp":
                        row.trName = MainWindow.resourcemanager.GetString("trPurchasesEmailTemplate");
                        break;
                    case "pur_order_email_temp":
                        row.trName = MainWindow.resourcemanager.GetString("trPurchaseOrdersEmailTemplate");
                        break;
                    case "sale_email_temp":
                        row.trName = MainWindow.resourcemanager.GetString("trSalesEmailTemplate");
                        break;
                    case "sale_order_email_temp":
                        row.trName = MainWindow.resourcemanager.GetString("trSalesOrdersEmailTemplate");
                        break;
                    case "quotation_email_temp":
                        row.trName = MainWindow.resourcemanager.GetString("trQuotationsEmailTemplate");
                        break;
                    case "required_email_temp":
                        row.trName = MainWindow.resourcemanager.GetString("trRequirementsEmailTemplate");
                        break;
                    case "upgrade_email_temp":
                        row.trName = MainWindow.resourcemanager.GetString("trBookingEmailTemplate");
                        break;
                    default:
                        row.trName = row.name;
                        break;

                }
            }

            dg_setValues.ItemsSource = setQuery;

        }
        void RefreshSetttingsView()
        {
            //setQuery.ToList()[0].name = MainWindow.resourcemanager.GetString("trPurchaseOrdersEmailTemplate");
            //setQuery.ToList()[1].name = MainWindow.resourcemanager.GetString("trSalesEmailTemplate");
            //setQuery.ToList()[2].name = MainWindow.resourcemanager.GetString("trSalesOrdersEmailTemplate");
            //setQuery.ToList()[3].name = MainWindow.resourcemanager.GetString("trQuotationsEmailTemplate");
            //setQuery.ToList()[4].name = MainWindow.resourcemanager.GetString("trRequirementsEmailTemplate");
            //setQuery.ToList()[5].name = MainWindow.resourcemanager.GetString("trPurchasesEmailTemplate");
            dg_setValues.ItemsSource = setQuery;
        }
        private async void Btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);

                RefreshSettingsList();
                // RefreshSetttingsView();

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

        #region validate - clearValidate - textChange - lostFocus - . . . . 

         
    void Clear()
    {
         
        dg_setValues.SelectedIndex = -1;
        tb_title.Text =
                tb_text1.Text =
                tb_text2.Text =
                tb_link1text.Text =
                tb_link2text.Text =
                tb_link3text.Text =
                tb_link1url.Text =
                tb_link2url.Text =
                tb_link3url.Text = "";
                this.DataContext = new SetValues();
        HelpClass.clearValidate(requiredControlList, this);

    }
    
        string input;
        decimal _decimal = 0;
        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {


                //only  digits
                TextBox textBox = sender as TextBox;
                HelpClass.InputJustNumber(ref textBox);
                if (textBox.Tag.ToString() == "int")
                {
                    Regex regex = new Regex("[^0-9]");
                    e.Handled = regex.IsMatch(e.Text);
                }
                else if (textBox.Tag.ToString() == "decimal")
                {
                    input = e.Text;
                    e.Handled = !decimal.TryParse(textBox.Text + input, out _decimal);

                }
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

        #region report
        /*
        //report  parameters
        ReportCls reportclass = new ReportCls();
        LocalReport rep = new LocalReport();
        SaveFileDialog saveFileDialog = new SaveFileDialog();

        // end report parameters
        public void BuildReport()
        {

            List<ReportParameter> paramarr = new List<ReportParameter>();

            string addpath;
            bool isArabic = ReportCls.checkLang();
            if (isArabic)
            {
                addpath = @"\Reports\SectionData\hallDivide\Ar\ArTables.rdlc";
            }
            else
            {
                addpath = @"\Reports\SectionData\hallDivide\En\EnTables.rdlc";
            }
            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);

            clsReports.tablesReport(tablesQuery.ToList(), rep, reppath, paramarr);
            clsReports.setReportLanguage(paramarr);
            clsReports.Header(paramarr);

            rep.SetParameters(paramarr);

            rep.Refresh();

        }

        private void Btn_pdf_Click(object sender, RoutedEventArgs e)
        {
            //pdf
            try
            {

                HelpClass.StartAwait(grid_main);

                if (MainWindow.groupObject.HasPermissionAction(basicsPermission, MainWindow.groupObjects, "report") )
                {
                    #region
                    BuildReport();

                    saveFileDialog.Filter = "PDF|*.pdf;";

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string filepath = saveFileDialog.FileName;
                        LocalReportExtensions.ExportToPDF(rep, filepath);
                    }
                    #endregion
                }
                else
                    Toaster.ShowInfo(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trdontHavePermission"), animation: ToasterAnimation.FadeIn);

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
            try
            {

                HelpClass.StartAwait(grid_main);
                if (MainWindow.groupObject.HasPermissionAction(basicsPermission, MainWindow.groupObjects, "report") )
                {
                    #region
                    BuildReport();
                    LocalReportExtensions.PrintToPrinterbyNameAndCopy(rep, FillCombo.rep_printer_name, FillCombo.rep_print_count == null ? short.Parse("1") : short.Parse(FillCombo.rep_print_count));
                    #endregion
                }
                else
                    Toaster.ShowInfo(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trdontHavePermission"), animation: ToasterAnimation.FadeIn);


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
            try
            {

                HelpClass.StartAwait(grid_main);

                if (MainWindow.groupObject.HasPermissionAction(basicsPermission, MainWindow.groupObjects, "report") )
                {
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
                }
                else
                    Toaster.ShowInfo(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trdontHavePermission"), animation: ToasterAnimation.FadeIn);


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
            try
            {

                HelpClass.StartAwait(grid_main);
                if (MainWindow.groupObject.HasPermissionAction(basicsPermission, MainWindow.groupObjects, "report") )
                {
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
                }
                else
                    Toaster.ShowInfo(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trdontHavePermission"), animation: ToasterAnimation.FadeIn);


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


    }
}
