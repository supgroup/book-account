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
using BookAccountApp.View.sectionData;

namespace BookAccountApp.View.sales
{
    /// <summary>
    /// Interaction logic for uc_salesSyria.xaml
    /// </summary>
    public partial class uc_salesSyria : UserControl
    {
        public uc_salesSyria()
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
        private static uc_salesSyria _instance;
        public static uc_salesSyria Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_salesSyria();
                return _instance;
            }
        }

        ServiceData serviceData = new ServiceData();
        IEnumerable<ServiceData> serviceDatasQuery;
        IEnumerable<ServiceData> serviceDatas;
        bool first = true;
        bool tgl_serviceDatastate=true;
        string searchText = "";
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        public static List<string> requiredControlList;
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);

                requiredControlList = new List<string> { "airline", "passenger" ,"total", "serviceDate" };
      
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

               // Keyboard.Focus(cb_passenger);
             
                //await RefreshServiceDatasList();
                await Search();              
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
        private void translate()
        {
             
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, MainWindow.resourcemanager.GetString("trSearchHint"));
            //txt_baseInformation.Text = MainWindow.resourcemanager.GetString("trBaseInformation");
            txt_active.Text = MainWindow.resourcemanager.GetString("trActive");
            txt_title.Text = MainWindow.resourcemanager.GetString("bookInfoSyr");
            /*
trTaxHint
* */
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_passenger, MainWindow.resourcemanager.GetString("passengerNameHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_system, MainWindow.resourcemanager.GetString("bookSystemHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_ticketNum, MainWindow.resourcemanager.GetString("ticketNumHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_airline, MainWindow.resourcemanager.GetString("airlineFlightHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_office, MainWindow.resourcemanager.GetString("officeNameHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_priceBeforTax, MainWindow.resourcemanager.GetString("priceBeforTaxHint")); 
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_total, MainWindow.resourcemanager.GetString("totalHint"));

            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_notes, MainWindow.resourcemanager.GetString("trNoteHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(dp_fromDateSearch, MainWindow.resourcemanager.GetString("fromDate"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(dp_toDateSearch, MainWindow.resourcemanager.GetString("toDate"));
            txt_exportDocsButton.Text = MainWindow.resourcemanager.GetString("docExport");
            txt_uploadDocsButton.Text = MainWindow.resourcemanager.GetString("docUpload");
         
            dg_serviceData.Columns[0].Header = MainWindow.resourcemanager.GetString("trNo.");
            dg_serviceData.Columns[1].Header = MainWindow.resourcemanager.GetString("passengerName");
            dg_serviceData.Columns[2].Header = MainWindow.resourcemanager.GetString("bookSystem");
            dg_serviceData.Columns[3].Header = MainWindow.resourcemanager.GetString("ticketNum");
            dg_serviceData.Columns[4].Header = MainWindow.resourcemanager.GetString("airlineFlight");
            dg_serviceData.Columns[5].Header = MainWindow.resourcemanager.GetString("officeName");
           dg_serviceData.Columns[6].Header = MainWindow.resourcemanager.GetString("priceBeforTax");
            dg_serviceData.Columns[7].Header = MainWindow.resourcemanager.GetString("total"); 
            dg_serviceData.Columns[8].Header = MainWindow.resourcemanager.GetString("trDate");
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
            
                serviceData = new ServiceData();
                if (HelpClass.validate(requiredControlList, this))
                {
                    //tb_custCode.Text = await serviceData.generateCodeNumber("cu");

                    serviceData.serviceNum = await serviceData.generateCodeNumber("SY");
                    serviceData.passengerId = Convert.ToInt32(cb_passenger.SelectedValue); 
                    serviceData.ticketNum = tb_ticketNum.Text;
                    serviceData.flightId = Convert.ToInt32(cb_airline.SelectedValue);
                    serviceData.officeId = Convert.ToInt32(cb_office.SelectedValue);
                    //serviceData.serviceDate = dp_serviceDate.SelectedDate;
                    serviceData.total =( tb_total.Text==null|| tb_total.Text=="")?0: Convert.ToDecimal(tb_total.Text);
                  serviceData.notes = tb_notes.Text;
                    serviceData.systemType="syr";
                    serviceData.createUserId = MainWindow.userLogin.userId;
                    serviceData.updateUserId = MainWindow.userLogin.userId;
                    // calc comm
                    serviceData.system_commission_ratio = FillCombo.syr_commission;
                    serviceData.system_commission_value = HelpClass.calcPercentage((decimal)serviceData.total,FillCombo.syr_commission);
                   
                    if (serviceData.officeId>0)
                    {
                        serviceData.office_commission_ratio = FillCombo.office_syr_commission;
                        serviceData.office_commission_value = HelpClass.calcPercentage((decimal)serviceData.total, FillCombo.office_syr_commission);

                        serviceData.company_commission_ratio = FillCombo.company_syr_commission - FillCombo.office_syr_commission;
                        serviceData.company_commission_value = HelpClass.calcPercentage((decimal)serviceData.total, (decimal)serviceData.company_commission_ratio);

                    }
                    else
                    {
                        serviceData.office_commission_ratio =0;
                        serviceData.office_commission_value = 0;
                        serviceData.company_commission_ratio = FillCombo.company_syr_commission;
                        serviceData.company_commission_value = HelpClass.calcPercentage((decimal)serviceData.total, FillCombo.company_syr_commission);

                    }
                    serviceData.totalnet = serviceData.total - serviceData.system_commission_value - serviceData.office_commission_value;
                    serviceData.airlinePaid = 0;
                    serviceData.airlineUnpaid = 0;
                    serviceData.officePaid = 0;
                    serviceData.officeUnpaid = serviceData.office_commission_value;
                    serviceData.passengerPaid = 0;
                    serviceData.passengerUnpaid = serviceData.total;//passenger
                    serviceData.systemPaid = 0;
                    serviceData.systemUnpaid = serviceData.system_commission_value;
                    //
                    decimal s = await serviceData.Save(serviceData);
                    if (s <= 0)
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                    else
                    {
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopAdd"), animation: ToasterAnimation.FadeIn);


                        Clear();
                        await RefreshServiceDatasList();
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
                if (serviceData.serviceId>0) { 
                    if (HelpClass.validate(requiredControlList, this))
                {
                    //tb_custCode.Text = await serviceData.generateCodeNumber("cu");

                    serviceData.passengerId = Convert.ToInt32(cb_passenger.SelectedValue); 
                    serviceData.ticketNum = tb_ticketNum.Text;
                    serviceData.flightId = Convert.ToInt32(cb_airline.SelectedValue);
                    serviceData.officeId = Convert.ToInt32(cb_office.SelectedValue);
                    //serviceData.serviceDate = dp_serviceDate.SelectedDate;
                    serviceData.total = Convert.ToDecimal(tb_total.Text);
                  serviceData.notes = tb_notes.Text;
                        serviceData.systemType = "syr";
                        serviceData.createUserId = MainWindow.userLogin.userId;
                    serviceData.updateUserId = MainWindow.userLogin.userId;
                        // calc comm
                        serviceData.system_commission_ratio = FillCombo.syr_commission;
                        serviceData.system_commission_value = HelpClass.calcPercentage((decimal)serviceData.total, FillCombo.syr_commission);

                        if (serviceData.officeId > 0)
                        {
                            serviceData.office_commission_ratio = FillCombo.office_syr_commission;
                            serviceData.office_commission_value = HelpClass.calcPercentage((decimal)serviceData.total, FillCombo.office_syr_commission);

                            serviceData.company_commission_ratio = FillCombo.company_syr_commission - FillCombo.office_syr_commission;
                            serviceData.company_commission_value = HelpClass.calcPercentage((decimal)serviceData.total, (decimal)serviceData.company_commission_ratio);

                        }
                        else
                        {
                            serviceData.office_commission_ratio = 0;
                            serviceData.office_commission_value = 0;
                            serviceData.company_commission_ratio = FillCombo.company_syr_commission;
                            serviceData.company_commission_value = HelpClass.calcPercentage((decimal)serviceData.total, FillCombo.company_syr_commission);

                        }
                        serviceData.totalnet = serviceData.total - serviceData.system_commission_value - serviceData.office_commission_value;
                        serviceData.totalnet = serviceData.total - serviceData.system_commission_value - serviceData.office_commission_value;
                        serviceData.airlinePaid = 0;
                        serviceData.airlineUnpaid = 0;
                        serviceData.officePaid = 0;
                        serviceData.officeUnpaid = serviceData.office_commission_value;
                        serviceData.passengerPaid = 0;
                        serviceData.passengerUnpaid = serviceData.total;//passenger
                        serviceData.systemPaid = 0;
                        serviceData.systemUnpaid = serviceData.system_commission_value;
                        //

                        decimal s = await serviceData.Save(serviceData);
                    if (s <= 0)
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                    else
                    {
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopAdd"), animation: ToasterAnimation.FadeIn);
                        Clear();
                        await RefreshServiceDatasList();
                        await Search();
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
        private async void Btn_delete_Click(object sender, RoutedEventArgs e)
        {//delete
            try
            {
                HelpClass.StartAwait(grid_main);
               
           
                    if (serviceData.serviceId != 0)
                    {
                        if ((!serviceData.canDelete) && (serviceData.isActive == false))
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
                            if (serviceData.canDelete)
                                w.contentText = MainWindow.resourcemanager.GetString("trMessageBoxDelete");
                            if (!serviceData.canDelete)
                                w.contentText = MainWindow.resourcemanager.GetString("trMessageBoxDeactivate");
                            w.ShowDialog();
                            Window.GetWindow(this).Opacity = 1;
                            #endregion

                            if (w.isOk)
                            {
                                string popupContent = "";
                                if (serviceData.canDelete) popupContent = MainWindow.resourcemanager.GetString("trPopDelete");
                                if ((!serviceData.canDelete) && (serviceData.isActive == true)) popupContent = MainWindow.resourcemanager.GetString("trPopInActive");

                                var s = await serviceData.Delete(serviceData.serviceId, MainWindow.userLogin.userId, serviceData.canDelete);
                                if (s < 0)
                                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                                else
                                {
                                    serviceData.serviceId = 0;
                                    Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopDelete"), animation: ToasterAnimation.FadeIn);

                                    await RefreshServiceDatasList();
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
            serviceData.isActive = true;
            var s = await serviceData.Save(serviceData);
            if (s <= 0)
                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
            else
            {
                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopActive"), animation: ToasterAnimation.FadeIn);
                await RefreshServiceDatasList();
                await Search();
            }
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

                if (serviceDatas is null)
                    await RefreshServiceDatasList();
                tgl_serviceDatastate = true;
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
                if (serviceDatas is null)
                    await RefreshServiceDatasList();
                tgl_serviceDatastate = false;
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
        private async void Dg_serviceData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);

                //selection
               
                if (dg_serviceData.SelectedIndex != -1)
                {
                    serviceData = dg_serviceData.SelectedItem as ServiceData;
                  //  this.DataContext = serviceData;
                    if (serviceData != null)
                    {
                        //tb_custCode.Text = serviceData.custCode;
                         cb_passenger.SelectedValue = serviceData.passengerId;
                        cb_airline.SelectedValue = serviceData.flightId;
                        cb_office.SelectedValue = serviceData.officeId;
                        tb_total.Text = HelpClass.DecTostring(serviceData.total);
                        this.DataContext = serviceData;
                        #region delete
                        if (serviceData.canDelete)
                            btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");
                        else
                        {
                            if (serviceData.isActive == false)
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
                dp_toDateSearch.SelectedDate = null;
                dp_fromDateSearch.SelectedDate = null;
                tb_search.Text = "";
                await RefreshServiceDatasList();
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
            if (serviceDatas is null)
                await RefreshServiceDatasList();
          
            searchText = tb_search.Text.ToLower();
            serviceDatasQuery = serviceDatas.Where(s => (searchText == "" ? true :
            (
          (  s.airline==null?false:( s.airline.ToLower().Contains(searchText))) ||
           ( s.passenger == null ? false :( s.passenger.ToLower().Contains(searchText))) ||
             (s.ticketNum == null ? false : (s.ticketNum.ToLower().Contains(searchText)))||
         (  s.officeName == null ? false : (s.officeName.ToLower().Contains(searchText)))             
            )
            //&& 
            //(
            ////start date
            //((dp_fromDateSearch.SelectedDate != null || dp_fromDateSearch.Text != "") ? s.serviceDate == null ? false : (s.serviceDate.Value.Date >= dp_fromDateSearch.SelectedDate.Value.Date) : true)
            //&&
            ////end date
            //((dp_toDateSearch.SelectedDate != null || dp_toDateSearch.Text != "") ? s.serviceDate == null ? false : (s.serviceDate.Value.Date <= dp_toDateSearch.SelectedDate.Value.Date) : true)

            //)
            ) && s.isActive == tgl_serviceDatastate
            );
          
            //);
          
            RefreshServiceDatasView();
        }
        async Task<IEnumerable<ServiceData>> RefreshServiceDatasList()
        {
           
            serviceDatas = await serviceData.GetBy("syr", dp_fromDateSearch.SelectedDate, dp_toDateSearch.SelectedDate);

        //    serviceDatas = serviceDatas.Where(s => s.systemType == "syr").ToList();
            return serviceDatas;
        }
        void RefreshServiceDatasView()
        {

            dg_serviceData.ItemsSource = serviceDatasQuery;
           // txt_count.Text = serviceDatasQuery.Count().ToString();
        }
        public async Task fillcombos()
        {
            await FillCombo.fillPassengers(cb_passenger);
            await FillCombo.fillFlights(cb_airline);
            await FillCombo.fillOffice(cb_office);
            await FillCombo.fillSystems(cb_system);
        }

        #endregion

        #region validate - clearValidate - textChange - lostFocus - . . . . 

        void Clear()
        {
            this.DataContext = new ServiceData();
            cb_passenger.SelectedIndex = -1;
            cb_airline.SelectedIndex = -1;
            cb_office.SelectedIndex = -1;
            //cb_passenger.Text ="";
            //cb_airline.Text = "";
            //cb_office.Text = "";
            tb_total.Text = "";
            // last 
            HelpClass.clearValidate(requiredControlList, this);
        }
        string input;
        decimal _decimal = 0;
        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        { //only  digits
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
            addpath = @"\Reports\Sale\ArSales.rdlc";

            //}
            //else
            //{
            //    addpath = @"\Reports\SectionData\En\EnServiceDatas.rdlc";
            //}
            //D:\myproj\posproject3\BookAccountApp\BookAccountApp\Reports\statisticReports\En\EnBook.rdlc
            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
            //     subTitle = clsReports.ReportTabTitle(firstTitle, secondTitle);
            string title = MainWindow.resourcemanagerreport.GetString("book_sales") + " / " + MainWindow.resourcemanagerreport.GetString("syr");
            paramarr.Add(new ReportParameter("trTitle", title));
            clsReports.SaleReport(serviceDatasQuery, rep, reppath, paramarr);
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
                win_lvc win = new win_lvc(serviceDatasQuery, 6, true);
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

        private async void Btn_addAirline_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);
                Window.GetWindow(this).Opacity = 0.2;
                wd_flightWithoutGrid w = new wd_flightWithoutGrid();
                w.ShowDialog();
                await FillCombo.fillFlights(cb_airline);
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
        
        private async  void Btn_addPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);
                Window.GetWindow(this).Opacity = 0.2;
                wd_passengers w = new wd_passengers();
                w.ShowDialog();
               await FillCombo.fillPassengers(cb_passenger);
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
        private async void Btn_addSystem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);
                Window.GetWindow(this).Opacity = 0.2;
                wd_systems w = new wd_systems();
                w.ShowDialog();
                await FillCombo.fillSystems(cb_system);
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
        private async void Btn_addOffice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);
                Window.GetWindow(this).Opacity = 0.2;
                wd_office w = new wd_office();
                w.ShowDialog();
                 await FillCombo.fillOffice(cb_office);
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

        private void Tb_total_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private async void Dp_fromDateSearch_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                //if (cb_custname.SelectedItem != null)
                //{

                //}
                await RefreshServiceDatasList();
                await Search();

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Dp_toDateSearch_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                //if (cb_custname.SelectedItem != null)
                //{

                //}
                await RefreshServiceDatasList();
                await Search();

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        OpenFileDialog openFileDialog = new OpenFileDialog();
        private async void Btn_uploadDocs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);

                if (serviceData.serviceId > 0)
                {
                    FileClass fcls = new FileClass();
                    string foldername = serviceData.serviceId.ToString();
                    openFileDialog.Multiselect = true;
                    openFileDialog.Title = MainWindow.resourcemanager.GetString("docUpload");
                    if (openFileDialog.ShowDialog() == true)
                    {
                        string dir = System.IO.Path.Combine(Global.rootservicefilesFolder, foldername);
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }
                        // Read the files
                        decimal s = 0;
                        foreach (String file in openFileDialog.FileNames)
                        {
                            // Create a PictureBox.


                            string fName = System.IO.Path.GetFileNameWithoutExtension(file);
                            string fileName = System.IO.Path.GetFileName(file);
                            string ext = System.IO.Path.GetExtension(file);
                            string destpath = System.IO.Path.Combine(dir, fileName);
                            string newname = fName;
                            while (File.Exists(destpath))
                            {
                                newname = fName + "-" + HelpClass.GenerateRandomNo();
                                destpath = System.IO.Path.Combine(dir, newname + ext);
                            }
                            File.Copy(file, destpath);
                            //save todb
                            fcls.fileName = newname;
                            fcls.extention = ext;
                            fcls.folderName = foldername;
                            fcls.tableRowId = serviceData.serviceId;
                            s = await fcls.SaveService(fcls);



                        }
                        if (s <= 0)
                            Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                        else
                        {
                            Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopUpload"), animation: ToasterAnimation.FadeIn);
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

        private void Btn_exportDocs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);
                if (serviceData.serviceId > 0)
                {
                    Window.GetWindow(this).Opacity = 0.2;
                    wd_files w = new wd_files();
                    w.type = "service";
                    w.itemId = serviceData.serviceId;
                    w.ShowDialog();

                    Window.GetWindow(this).Opacity = 1;
                }
                else
                {

                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trSelectItemFirst"), animation: ToasterAnimation.FadeIn);

                }

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
