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
    /// Interaction logic for uc_passengers.xaml
    /// </summary>
    public partial class uc_passengers : UserControl
    {
        public uc_passengers()
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
        private static uc_passengers _instance;
        public static uc_passengers Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_passengers();
                return _instance;
            }
        }

        Passengers passenger = new Passengers();
        IEnumerable<Passengers> passengersQuery;
        IEnumerable<Passengers> passengers;
        bool first = true;
        bool tgl_passengerstate=true;
        string searchText = "";
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        public static List<string> requiredControlList;
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);
             
                requiredControlList = new List<string> { "name", "lastName"};

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

                await RefreshPassengersList();
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
            txt_title.Text = MainWindow.resourcemanager.GetString("passenger");
            txt_exportDocsButton.Text = MainWindow.resourcemanager.GetString("docExport");
            txt_uploadDocsButton.Text = MainWindow.resourcemanager.GetString("docUpload");
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_custCode, MainWindow.resourcemanager.GetString("trCodeHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_name, MainWindow.resourcemanager.GetString("trNameHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_lastName, MainWindow.resourcemanager.GetString("trLastNameHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_father, MainWindow.resourcemanager.GetString("fatherHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_mother, MainWindow.resourcemanager.GetString("motherHint"));
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
         //   passengers
            dg_passenger.Columns[0].Header = MainWindow.resourcemanager.GetString("trNo.");
            dg_passenger.Columns[1].Header = MainWindow.resourcemanager.GetString("trName");
            dg_passenger.Columns[2].Header = MainWindow.resourcemanager.GetString("trLastName");
            dg_passenger.Columns[3].Header = MainWindow.resourcemanager.GetString("father");
            dg_passenger.Columns[4].Header = MainWindow.resourcemanager.GetString("mother");
        

            //dg_passenger.Columns[3].Header = MainWindow.resourcemanager.GetString("trMobile");

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
              
                passenger = new Passengers();
                if (HelpClass.validate(requiredControlList, this) )
                {
                    //tb_custCode.Text = await passenger.generateCodeNumber("cu");
          
                    passenger.name = tb_name.Text;
                    passenger.lastName = tb_lastName.Text.Trim();
                     
                     
                 
                    passenger.father = tb_father.Text.Trim();
                    passenger.mother = tb_mother.Text;
                    passenger.notes = tb_notes.Text;
                 
                    passenger.createUserId = MainWindow.userLogin.userId;
                    passenger.updateUserId = MainWindow.userLogin.userId;
         

                    decimal s = await passenger.Save(passenger);
                    if (s <= 0)
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                    else
                    {
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopAdd"), animation: ToasterAnimation.FadeIn);
 

                        Clear();
                        await RefreshPassengersList();
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
               
                if (passenger.passengerId > 0)
                {
                    if (HelpClass.validate(requiredControlList, this) && HelpClass.IsValidEmail(this))
                    {
                        //passenger.custname = tb_custname.Text;
                        passenger.name = tb_name.Text;
                        passenger.lastName = tb_lastName.Text;
                        passenger.father = tb_father.Text;
                        passenger.mother = tb_mother.Text;
                        passenger.notes = tb_notes.Text;
                        passenger.updateUserId = MainWindow.userLogin.userId;
                        //passenger.countryId = Convert.ToInt32(cb_country.SelectedValue);
                        //passenger.email = tb_email.Text;
                        //passenger.mobile = cb_areaMobile.Text + "-" + tb_mobile.Text; ;
                        //if (!tb_phone.Text.Equals(""))
                        //    passenger.phone = cb_areaPhone.Text + "-" + cb_areaPhoneLocal.Text + "-" + tb_phone.Text;
                        //if (!tb_fax.Text.Equals(""))
                        //    passenger.fax = cb_areaFax.Text + "-" + cb_areaFaxLocal.Text + "-" + tb_fax.Text;
                        //if (cb_custlevel.SelectedValue != null)
                        //    passenger.custlevel = cb_custlevel.SelectedValue.ToString();
                        //passenger.company = tb_company.Text.Trim();
                        //passenger.address = tb_address.Text;

                        //passenger.createUserId = MainWindow.userLogin.userId;

                        //passenger.balance = 0;

                        decimal s = await passenger.Save(passenger);
                        if (s <= 0)
                            Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                        else
                        {
                            Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopUpdate"), animation: ToasterAnimation.FadeIn);

                            //if (isImgPressed)
                            //{
                            //    int passengerId = (int)s;
                            //    string b = await passenger.uploadImage(imgFileName, Md5Encription.MD5Hash("Inc-m" + passengerId.ToString()), passengerId);
                            //    passenger.image = b;
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

                            await RefreshPassengersList();
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
                if (passenger.passengerId != 0)
                    if (passenger.passengerId != 0)
                    {
                        if ((!passenger.canDelete) && (passenger.isActive == false))
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
                            if (passenger.canDelete)
                                w.contentText = MainWindow.resourcemanager.GetString("trMessageBoxDelete");
                            if (!passenger.canDelete)
                                w.contentText = MainWindow.resourcemanager.GetString("trMessageBoxDeactivate");
                            w.ShowDialog();
                            Window.GetWindow(this).Opacity = 1;
                            #endregion

                            if (w.isOk)
                            {
                                string popupContent = "";
                                if (passenger.canDelete) popupContent = MainWindow.resourcemanager.GetString("trPopDelete");
                                if ((!passenger.canDelete) && (passenger.isActive == true)) popupContent = MainWindow.resourcemanager.GetString("trPopInActive");

                                var s = await passenger.Delete(passenger.passengerId, MainWindow.userLogin.userId, passenger.canDelete);
                                if (s < 0)
                                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                                else
                                {
                                    passenger.passengerId = 0;
                                    Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopDelete"), animation: ToasterAnimation.FadeIn);

                                    await RefreshPassengersList();
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
            passenger.isActive = true;
            var s = await passenger.Save(passenger);
            if (s <= 0)
                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
            else
            {
                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopActive"), animation: ToasterAnimation.FadeIn);
                await RefreshPassengersList();
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

                if (passengers is null)
                    await RefreshPassengersList();
                tgl_passengerstate = true;
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
                if (passengers is null)
                    await RefreshPassengersList();
                tgl_passengerstate = false;
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
        private async void Dg_passenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
              
                //selection
                if (dg_passenger.SelectedIndex != -1)
                {
                    passenger = dg_passenger.SelectedItem as Passengers;
                    this.DataContext = passenger;
                    if (passenger != null)
                    {
                        //tb_custCode.Text = passenger.custCode;
                        //cb_country.SelectedValue = passenger.countryId;
                        this.DataContext = passenger;
                        #region delete
                        if (passenger.canDelete)
                            btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");
                        else
                        {
                            if (passenger.isActive == false)
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
                await RefreshPassengersList();
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
            if (passengers is null)
                await RefreshPassengersList();
          
            searchText = tb_search.Text.ToLower();
            passengersQuery = passengers.Where(s =>
            (s.passengerId.ToString().Contains(searchText) ||
            s.name.ToLower().Contains(searchText) ||
            s.lastName.ToLower().Contains(searchText) ||
            s.father.ToLower().Contains(searchText)
            ||
            s.mother.ToLower().Contains(searchText)
            ) && s.isActive == tgl_passengerstate);
            //&& s.isActive == tgl_passengerstate
            //);

            RefreshPassengersView();
        }
        async Task<IEnumerable<Passengers>> RefreshPassengersList()
        {
            passengers = await passenger.GetAll();
            return passengers;
        }
        void RefreshPassengersView()
        {
            dg_passenger.ItemsSource = passengersQuery;
            //txt_count.Text = passengersQuery.Count().ToString();
        }
        #endregion

        #region validate - clearValidate - textChange - lostFocus - . . . . 

        void Clear()
        {
            this.DataContext = new Passengers();


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
                addpath = @"\Reports\SectionData\Ar\ArPassenger.rdlc";

            //}
            //else
            //{
            //    addpath = @"\Reports\SectionData\En\EnPassengers.rdlc";
            //}
            //D:\myproj\posproject3\BookAccountApp\BookAccountApp\Reports\statisticReports\En\EnBook.rdlc
            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
            //     subTitle = clsReports.ReportTabTitle(firstTitle, secondTitle);
            //  Title = MainWindow.resourcemanagerreport.GetString("trAccountantReport");

            clsReports.PassengersReport(passengersQuery, rep, reppath, paramarr);
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
        {//pie
            try
            {
                HelpClass.StartAwait(grid_main);

                //if (FillCombo.groupObject.HasPermissionAction(basicsPermission, FillCombo.groupObjects, "report"))
                //{
                    #region
                    Window.GetWindow(this).Opacity = 0.2;
                    win_lvc win = new win_lvc(passengersQuery, 1, false);
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
        OpenFileDialog openFileDialog = new OpenFileDialog();
        private async void Btn_uploadDocs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);

                if (passenger.passengerId > 0)
                {
                    FileClass fcls = new FileClass();
                    string foldername = passenger.passengerId.ToString();
                    openFileDialog.Multiselect = true;
                    openFileDialog.Title = MainWindow.resourcemanager.GetString("docUpload");
                    if (openFileDialog.ShowDialog() == true)
                    {
                        string dir = System.IO.Path.Combine(Global.rootpassengerFolder, foldername);
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
                            fcls.tableRowId = passenger.passengerId;
                            s = await fcls.SavePassenger(fcls);



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
                if (passenger.passengerId > 0)
                {
                    Window.GetWindow(this).Opacity = 0.2;
                    wd_files w = new wd_files();
                    w.type = "passenger";
                    w.itemId = passenger.passengerId;
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
