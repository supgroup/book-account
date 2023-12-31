﻿using BookAccountApp.ApiClasses;
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

namespace BookAccountApp.View.bookSales
{
    /// <summary>
    /// Interaction logic for uc_salesSoto.xaml
    /// </summary>
    public partial class uc_salesSoto : UserControl
    {
        public uc_salesSoto()
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
        private static uc_salesSoto _instance;
        public static uc_salesSoto Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_salesSoto();
                return _instance;
            }
        }

        ServiceData serviceData = new ServiceData();
        Systems SystemModel = new Systems();
        IEnumerable<ServiceData> serviceDatasQuery;
        IEnumerable<ServiceData> serviceDatas;
        PaySides paysideModel = new PaySides();
        PayOp payOpModel = new PayOp();
        bool first = true;
        bool tb_syChange = false;
        bool tgl_serviceDatastate = true;
        string searchText = "";
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        public static List<string> requiredControlList;
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);

                requiredControlList = new List<string> { "airline", "passenger", "total", "system", "paid", "currency" };

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

                Keyboard.Focus(cb_passenger);

              //  await RefreshServiceDatasList();
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
            // txt_active.Text = MainWindow.resourcemanager.GetString("trActive");
            //txt_active.Text = MainWindow.resourcemanager.GetString("trDraft");        
            txt_title.Text = MainWindow.resourcemanager.GetString("bookInfoSoto");

            chk_draft.Content = MainWindow.resourcemanager.GetString("notDone");
            chk_confirmed.Content = MainWindow.resourcemanager.GetString("done");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_passenger, MainWindow.resourcemanager.GetString("passengerNameHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_system, MainWindow.resourcemanager.GetString("bookSystemHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_ticketNum, MainWindow.resourcemanager.GetString("ticketNumHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_airline, MainWindow.resourcemanager.GetString("airlineFlightHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_office, MainWindow.resourcemanager.GetString("officeNameHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_priceBeforTax, MainWindow.resourcemanager.GetString("priceBeforTaxUSDHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_charge, MainWindow.resourcemanager.GetString("trTaxUSDHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_total, MainWindow.resourcemanager.GetString("totalUSDHint"));

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
            dg_serviceData.Columns[8].Header = MainWindow.resourcemanager.GetString("exchangePrice");
            dg_serviceData.Columns[9].Header = MainWindow.resourcemanager.GetString("currency");
            dg_serviceData.Columns[10].Header = MainWindow.resourcemanager.GetString("trDate");

            tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");
            tt_report.Content = MainWindow.resourcemanager.GetString("trPdf");
            tt_excel.Content = MainWindow.resourcemanager.GetString("trExcel");
            tt_preview.Content = MainWindow.resourcemanager.GetString("trPreview");
            tt_print.Content = MainWindow.resourcemanager.GetString("trPrint");
            //tt_count.Content = MainWindow.resourcemanager.GetString("trCount");

            btn_add.Content = MainWindow.resourcemanager.GetString("trAdd");
            btn_update.Content = MainWindow.resourcemanager.GetString("trUpdate");
            btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_paid, MainWindow.resourcemanager.GetString("PaidAmount"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_currency, MainWindow.resourcemanager.GetString("currencyHint"));

            btn_send.Content = MainWindow.resourcemanager.GetString("sending");
            //
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_priceBeforTaxSY, MainWindow.resourcemanager.GetString("priceBeforTaxSYHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_chargeSY, MainWindow.resourcemanager.GetString("trTaxSYHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_totalSY, MainWindow.resourcemanager.GetString("totalSYHint"));
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
                    int res = await prepareModel("add");
                    serviceData.state = "draft";
                    //
                    if (res == 0)
                    {

                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("msgpaid"), animation: ToasterAnimation.FadeIn);
                    }
                    else
                    {
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
        private async void Btn_update_Click(object sender, RoutedEventArgs e)
        {//update
            try
            {
                HelpClass.StartAwait(grid_main);
                if (serviceData.serviceId > 0)
                {
                    if (HelpClass.validate(requiredControlList, this))
                    {
                        //tb_custCode.Text = await serviceData.generateCodeNumber("cu");
                       await prepareModel("update");
                        serviceData.state = "draft";
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
        private async void Btn_send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                if (serviceData.serviceId > 0)
                {
                    if (HelpClass.validate(requiredControlList, this))
                    {
                        int res = await prepareModel("send");
                        serviceData.state = "confirmd";
                        //
                        if (res == 0)
                        {

                            Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("msgpaid"), animation: ToasterAnimation.FadeIn);
                        }
                        else if (res == -1)
                        {
                            //balance less than total
                            Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("msgpaySysBalance"), animation: ToasterAnimation.FadeIn);


                        }                         
                        else
                        {
                            decimal s = await serviceData.Save(serviceData);
                            if (s <= 0)
                                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                            else
                            {
                                //add payop
                                s = await AddPayRecords();
                                //
                                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopAdd"), animation: ToasterAnimation.FadeIn);
                                Clear();
                                await RefreshServiceDatasList();
                                await Search();
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
        private async Task<int> prepareModel(string process)
        {
            int res = 0;
            if (process == "add")
            {
                serviceData.serviceNum = await serviceData.generateCodeNumber("SOTO");
            }
            serviceData.systemType = "soto";
            serviceData.passengerId = Convert.ToInt32(cb_passenger.SelectedValue);
            serviceData.ticketNum = tb_ticketNum.Text;
            serviceData.flightId = Convert.ToInt32(cb_airline.SelectedValue);
            serviceData.officeId = Convert.ToInt32(cb_office.SelectedValue);
            serviceData.systemId = Convert.ToInt32(cb_system.SelectedValue);

            serviceData.exchangeId = FillCombo.ExchangeModel.exchangeId;
            serviceData.syValue = FillCombo.exchangeValue;
            serviceData.currency = cb_currency.SelectedValue == null ? "syp" : cb_currency.SelectedValue.ToString();//
            serviceData.paid = (tb_paid.Text == null || tb_paid.Text == "") ? 0 : Convert.ToDecimal(tb_paid.Text);//

            //serviceData.serviceDate = dp_serviceDate.SelectedDate;
            serviceData.total = (tb_total.Text == null || tb_total.Text == "") ? 0 : Convert.ToDecimal(tb_total.Text);
            serviceData.total = (tb_total.Text == null || tb_total.Text == "") ? 0 : Convert.ToDecimal(tb_total.Text);
            serviceData.totalSY = (tb_totalSY.Text == null || tb_totalSY.Text == "") ? 0 : Convert.ToDecimal(tb_totalSY.Text);
            serviceData.priceBeforTaxSY = (tb_priceBeforTaxSY.Text == null || tb_priceBeforTaxSY.Text == "") ? 0 : Convert.ToDecimal(tb_priceBeforTaxSY.Text);
            serviceData.tax_valueSY = (tb_chargeSY.Text == null || tb_chargeSY.Text == "") ? 0 : Convert.ToDecimal(tb_chargeSY.Text);

            int cmpres = HelpClass.ComparePaid(serviceData);
            //  if (serviceData.paid <= serviceData.total)
            if (cmpres == -1 || cmpres == 0)
            {
                res = 1;
                if (process == "send")//////////////////////////
                {
                    paysideModel = await paysideModel.GetByCode(serviceData.systemType);
                    serviceData.paysideId = paysideModel.paysideId;
                    if (paysideModel.balance < serviceData.total)
                    {
                        return -1;
                    }
                }
                serviceData.priceBeforTax = (tb_priceBeforTax.Text == null || tb_priceBeforTax.Text == "") ? 0 : Convert.ToDecimal(tb_priceBeforTax.Text);
                serviceData.tax_value = (tb_charge.Text == null || tb_charge.Text == "") ? 0 : Convert.ToDecimal(tb_charge.Text);

                serviceData.notes = tb_notes.Text;

                serviceData.createUserId = MainWindow.userLogin.userId;
                serviceData.updateUserId = MainWindow.userLogin.userId;
                // calc comm
                //     serviceData.system_commission_ratio = FillCombo.syr_commission;

                //serviceData.system_commission_value = HelpClass.calcPercentage((decimal)serviceData.total,FillCombo.syr_commission);
                SystemModel = FillCombo.SystemsList.ToList().Where(x => x.systemId == serviceData.systemId).FirstOrDefault();

                if (serviceData.officeId > 0)
                {
                    OfficeSystem OfficeSystemModel = new OfficeSystem();
                    OfficeSystemModel = await OfficeSystemModel.GetByOfficeSysId((int)serviceData.officeId, (int)serviceData.systemId);
                    serviceData.osId = OfficeSystemModel.osId;
                    serviceData.office_commission_ratio = OfficeSystemModel.office_commission;
                    serviceData.office_commission_value = HelpClass.calcPercentage((decimal)serviceData.priceBeforTax, (decimal)OfficeSystemModel.office_commission);

                    serviceData.commitionRatio = SystemModel.company_commission - OfficeSystemModel.office_commission;
                    serviceData.commitionValue = HelpClass.calcPercentage((decimal)serviceData.priceBeforTax, (decimal)serviceData.commitionRatio);
                    serviceData.profit = serviceData.company_commission_value;
                }
                else
                {
                    serviceData.commitionRatio = SystemModel.company_commission;
                    serviceData.commitionValue = HelpClass.calcPercentage((decimal)serviceData.priceBeforTax, (decimal)serviceData.commitionRatio);
                    serviceData.office_commission_ratio = 0;
                    serviceData.office_commission_value = 0;
                }
                serviceData.company_commission_ratio = SystemModel.company_commission;
                serviceData.company_commission_value = HelpClass.calcPercentage((decimal)serviceData.priceBeforTax, (decimal)SystemModel.company_commission);

                serviceData.totalnet = serviceData.priceBeforTax - serviceData.company_commission_value - serviceData.office_commission_value;

                serviceData.profit = serviceData.commitionValue;
                serviceData.profitSY = serviceData.commitionValue * FillCombo.exchangeValue;
                serviceData.airlinePaid = 0;
                serviceData.airlineUnpaid = 0;
                serviceData.officePaid = 0;
                serviceData.officeUnpaid = serviceData.office_commission_value;
                serviceData.passengerPaid = 0;
                serviceData.passengerUnpaid = serviceData.total;//passenger
                serviceData.systemPaid = 0;
                serviceData.systemUnpaid = serviceData.system_commission_value;
            }
            return res;
        }
        private async Task<int> AddPayRecords()
        {
            int res = 0;
            res = await payOpModel.updateSideBalance(serviceData.systemType, -(decimal)serviceData.total);
            res = await payOpModel.AddSideRecord(serviceData, paysideModel);
            res = await payOpModel.AddCompanyCommRecord(serviceData);
            res = await payOpModel.AddServiceRecord(serviceData);
            res = await payOpModel.AddPaidRecord(serviceData);
            if (serviceData.officeId > 0)
            {
                res = await payOpModel.AddOfficeCommRecord(serviceData);
            }

            return res;
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
        //private async void Tgl_isActive_Checked(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        HelpClass.StartAwait(grid_main);
        //        txt_active.Text = MainWindow.resourcemanager.GetString("trDraft");
        //        if (serviceDatas is null)
        //            await RefreshServiceDatasList();
        //        tgl_serviceDatastate = true;
        //        if (first)
        //        {
        //            first = false;
        //        }
        //        else
        //        {
        //            enableEdit();
        //            await Search();

        //        }

        //        HelpClass.EndAwait(grid_main);
        //    }
        //    catch (Exception ex)
        //    {
        //        HelpClass.EndAwait(grid_main);
        //        HelpClass.ExceptionMessage(ex, this);
        //    }
        //}
        //private async void Tgl_isActive_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        HelpClass.StartAwait(grid_main);
        //        txt_active.Text = MainWindow.resourcemanager.GetString("trConfirmed");
        //        disableEdit();
        //        if (serviceDatas is null)
        //            await RefreshServiceDatasList();
        //        tgl_serviceDatastate = false;
        //        await Search();
        //        HelpClass.EndAwait(grid_main);
        //    }
        //    catch (Exception ex)
        //    {

        //        HelpClass.EndAwait(grid_main);
        //        HelpClass.ExceptionMessage(ex, this);
        //    }
        //}
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
                HelpClass.clearValidate(requiredControlList, this);
                //selection

                if (dg_serviceData.SelectedIndex != -1)
                {
                    serviceData = dg_serviceData.SelectedItem as ServiceData;
                    //  this.DataContext = serviceData;
                    if (serviceData != null)
                    {
                        cb_passenger.SelectedValue = serviceData.passengerId;
                        cb_currency.SelectedValue = serviceData.currency;
                        cb_office.SelectedValue = serviceData.officeId;
                        cb_system.SelectedValue = serviceData.systemId;
                        cb_airline.SelectedValue = serviceData.flightId;
                        tb_total.Text = HelpClass.DecTostring(serviceData.total);
                        tb_priceBeforTax.Text = HelpClass.DecTostring(serviceData.priceBeforTax);
                        tb_charge.Text = HelpClass.DecTostring(serviceData.tax_value);
                        tb_paid.Text = HelpClass.DecTostring(serviceData.paid);

                        tb_totalSY.Text = HelpClass.DecTostring(serviceData.totalSY);
                        tb_priceBeforTaxSY.Text = HelpClass.DecTostring(serviceData.priceBeforTaxSY);
                        tb_chargeSY.Text = HelpClass.DecTostring(serviceData.tax_valueSY);
                        this.DataContext = serviceData;
                        if (tgl_serviceDatastate && MainWindow.userLogin.isAdmin.Value)
                        {
                            btn_send.IsEnabled = true;
                        }
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
            serviceDatasQuery = serviceDatas.Where(s =>( searchText == "" ? true :
            (
          (s.airline == null ? false : (s.airline.ToLower().Contains(searchText))) ||
           (s.passenger == null ? false : (s.passenger.ToLower().Contains(searchText))) ||
             (s.ticketNum == null ? false : (s.ticketNum.ToLower().Contains(searchText))) ||
         (s.officeName == null ? false : (s.officeName.ToLower().Contains(searchText)))
            )
            //&& (
            ////start date
            //((dp_fromDateSearch.SelectedDate != null || dp_fromDateSearch.Text != "") ? s.serviceDate == null ? false : (s.serviceDate.Value.Date >= dp_fromDateSearch.SelectedDate.Value.Date) : true)
            //&&
            ////end date
            //((dp_toDateSearch.SelectedDate != null || dp_toDateSearch.Text != "") ? s.serviceDate == null ? false : (s.serviceDate.Value.Date <= dp_toDateSearch.SelectedDate.Value.Date) : true)

            //)
            ) &&
             (tgl_serviceDatastate == true ? (s.state == "draft") : (s.state == "confirmd"))
           // s.isActive == tgl_serviceDatastate
            );

            //);

            RefreshServiceDatasView();
        }
        async Task<IEnumerable<ServiceData>> RefreshServiceDatasList()
        {
            serviceDatas = await serviceData.GetBy("soto", dp_fromDateSearch.SelectedDate, dp_toDateSearch.SelectedDate);
          //  serviceDatas = serviceDatas.Where(s=>s.systemType=="soto").ToList();
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
            //await FillCombo.fillFlightsBySysId(cb_airline, Convert.ToInt32(cb_system.SelectedValue));
            await FillCombo.fillOffice(cb_office);
            await FillCombo.fillSystems(cb_system);
            FillCombo.fillCurrency(cb_currency);

        }

        #endregion

        #region validate - clearValidate - textChange - lostFocus - . . . . 

        void Clear()
        {
            this.DataContext = new ServiceData();
            cb_passenger.SelectedIndex = -1;
            cb_airline.SelectedIndex = -1;
            cb_office.SelectedIndex = -1;
            cb_system.SelectedIndex = -1;
            //cb_currency.SelectedIndex = -1;
            cb_currency.SelectedValue = "syp";
            tb_total.Text = "";
            tb_priceBeforTax.Text = "";
            tb_charge.Text = "";
            tb_paid.Text = "";
            btn_send.IsEnabled = false;

            tb_totalSY.Text = "";
            tb_priceBeforTaxSY.Text = "";
            tb_chargeSY.Text = "";
            // last 
            HelpClass.clearValidate(requiredControlList, this);
        }
        void disableEdit()
        {
            chk_confirmed.IsEnabled = false;
            chk_confirmed.IsChecked = true;
            chk_draft.IsEnabled = true;
            chk_draft.IsChecked = false;
            //
            cb_passenger.IsEnabled = false;
            btn_addPassenger.IsEnabled = false;
            cb_system.IsEnabled = false;
            btn_addSystem.IsEnabled = false;
            tb_ticketNum.IsEnabled = false;
            cb_airline.IsEnabled = false;
            btn_addAirline.IsEnabled = false;
            cb_office.IsEnabled = false;
            btn_addOffice.IsEnabled = false;
            tb_priceBeforTax.IsEnabled = false;
            tb_charge.IsEnabled = false;
            tb_total.IsEnabled = false;
            tb_paid.IsEnabled = false;
            cb_currency.IsEnabled = false;
            tb_notes.IsEnabled = false;
            btn_uploadDocs.IsEnabled = false;
            btn_exportDocs.IsEnabled = false;
            btn_send.IsEnabled = false;
            btn_add.IsEnabled = false;
            btn_update.IsEnabled = false;
            btn_delete.IsEnabled = false;

            tb_priceBeforTaxSY.IsEnabled = false;
            tb_chargeSY.IsEnabled = false;
            tb_totalSY.IsEnabled = false;
            Clear();

        }
        void enableEdit()
        {
            chk_confirmed.IsEnabled = true;
            chk_confirmed.IsChecked = false;
            chk_draft.IsEnabled = false;
            chk_draft.IsChecked = true;
            //
            cb_passenger.IsEnabled = true;
            btn_addPassenger.IsEnabled = true;
            cb_system.IsEnabled = true;
            btn_addSystem.IsEnabled = true;
            tb_ticketNum.IsEnabled = true;
            cb_airline.IsEnabled = true;
            btn_addAirline.IsEnabled = true;
            cb_office.IsEnabled = true;
            btn_addOffice.IsEnabled = true;
            tb_priceBeforTax.IsEnabled = true;
            tb_charge.IsEnabled = true;
            tb_total.IsEnabled = true;
            tb_paid.IsEnabled = true;
            cb_currency.IsEnabled = true;
            tb_notes.IsEnabled = true;
            //btn_uploadDocs.IsEnabled = true;
            //btn_exportDocs.IsEnabled = true;
            btn_send.IsEnabled = true;
            btn_add.IsEnabled = true;
            btn_update.IsEnabled = true;
            btn_delete.IsEnabled = true;
            
            tb_priceBeforTaxSY.IsEnabled = true;
            tb_chargeSY.IsEnabled = true;
            tb_totalSY.IsEnabled = true;
            Clear();

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
            string title = MainWindow.resourcemanagerreport.GetString("book_sales") + " / " + MainWindow.resourcemanagerreport.GetString("soto");

            paramarr.Add(new ReportParameter("trTitle", title));
            string reptype= tgl_serviceDatastate ? MainWindow.resourcemanager.GetString("notDone") : MainWindow.resourcemanager.GetString("done");
            paramarr.Add(new ReportParameter("repType", reptype));
          
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
                win_lvc win = new win_lvc(serviceDatasQuery, 6, false);
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

 
       

        private  async void Btn_addAirline_Click(object sender, RoutedEventArgs e)
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


     

        private async void Btn_addPassenger_Click(object sender, RoutedEventArgs e)
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
        //private async void Btn_addOperation_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (sender != null)
        //            HelpClass.StartAwait(grid_main);
        //        Window.GetWindow(this).Opacity = 0.2;
        //        wd_operations w = new wd_operations();
        //        w.ShowDialog();
        //        //await FillCombo.fillOperations(cb_operation);
        //        Window.GetWindow(this).Opacity = 1;

        //        if (sender != null)
        //            HelpClass.EndAwait(grid_main);
        //    }
        //    catch (Exception ex)
        //    {
        //        Window.GetWindow(this).Opacity = 1;
        //        if (sender != null)
        //            HelpClass.EndAwait(grid_main);
        //        HelpClass.ExceptionMessage(ex, this);
        //    }
        //}

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

        private async void Cb_system_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (cb_system.SelectedItem != null)
                {
                    //await FillCombo.fillFlightsBySysId(cb_airline, Convert.ToInt32(cb_system.SelectedValue));
                }

            }
            catch (Exception ex)
            {

                HelpClass.ExceptionMessage(ex, this);
            }
        }
        #region events exchange
        private async void Btn_exchange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window.GetWindow(this).Opacity = 0.2;
                wd_rateSyrSoto w = new wd_rateSyrSoto();
                w.ShowDialog();
                Window.GetWindow(this).Opacity = 1;
                await RefreshServiceDatasList();
                await Search();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Tb_priceBeforTax_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                if (tb_syChange == false)
                {

                    //  HelpClass.validate(requiredControlList, this);
                    decimal Taxusd = (tb_priceBeforTax.Text == null || tb_priceBeforTax.Text == "") ? 0 : Convert.ToDecimal(tb_priceBeforTax.Text);
                    ////tb_priceBeforTaxSY.Text = HelpClass.DecTostring( Taxusd * FillCombo.exchangeValue);
                    if (serviceData.serviceId > 0)
                    {
                        tb_priceBeforTaxSY.Text = HelpClass.DecTostring(Taxusd * serviceData.syValue);
                    }
                    else
                    {
                        tb_priceBeforTaxSY.Text = HelpClass.DecTostring(Taxusd * FillCombo.exchangeValue);
                    }
                }

            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Tb_priceBeforTaxSY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (tb_syChange)
                {
                    //   HelpClass.validate(requiredControlList, this);
                    decimal Taxsy = (tb_priceBeforTaxSY.Text == null || tb_priceBeforTaxSY.Text == "") ? 0 : Convert.ToDecimal(tb_priceBeforTaxSY.Text);
                    //  tb_priceBeforTax.Text = HelpClass.DecTostring(Taxsy/FillCombo.exchangeValue);
                    if (Taxsy != 0)
                    {
                        if (serviceData.serviceId > 0)
                        {
                            tb_priceBeforTax.Text = HelpClass.DecTostring(Taxsy / serviceData.syValue);
                        }
                        else
                        {
                            tb_priceBeforTax.Text = HelpClass.DecTostring(Taxsy / FillCombo.exchangeValue);
                        }
                    }
                    else
                    {
                        tb_priceBeforTax.Text = "0";
                    }
                }


            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Tb_syp_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_syChange = true;
        }

        private void Tb_usd_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_syChange = false;
        }

        private void Tb_decimal_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.validate(requiredControlList, this);


                TextBox txtbox = sender as TextBox;
                decimal decval = (txtbox.Text == null || txtbox.Text == "") ? 0 : Convert.ToDecimal(txtbox.Text);

                txtbox.Text = HelpClass.DecTostring(decval);


            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Tb_charge_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (tb_syChange == false)
                {

                    //  HelpClass.validate(requiredControlList, this);
                    decimal usd = (tb_charge.Text == null || tb_charge.Text == "") ? 0 : Convert.ToDecimal(tb_charge.Text);
                    ////tb_priceBeforTaxSY.Text = HelpClass.DecTostring( Taxusd * FillCombo.exchangeValue);
                    if (serviceData.serviceId > 0)
                    {
                        tb_chargeSY.Text = HelpClass.DecTostring(usd * serviceData.syValue);
                    }
                    else
                    {
                        tb_chargeSY.Text = HelpClass.DecTostring(usd * FillCombo.exchangeValue);
                    }
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Tb_chargeSY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (tb_syChange)
                {
                    //   HelpClass.validate(requiredControlList, this);
                    decimal sy = (tb_chargeSY.Text == null || tb_chargeSY.Text == "") ? 0 : Convert.ToDecimal(tb_chargeSY.Text);
                    //  tb_priceBeforTax.Text = HelpClass.DecTostring(Taxsy/FillCombo.exchangeValue);
                    if (sy != 0)
                    {
                        if (serviceData.serviceId > 0)
                        {
                            tb_charge.Text = HelpClass.DecTostring(sy / serviceData.syValue);
                        }
                        else
                        {
                            tb_charge.Text = HelpClass.DecTostring(sy / FillCombo.exchangeValue);
                        }
                    }
                    else
                    {
                        tb_charge.Text = "0";
                    }
                }


            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Tb_total_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (tb_syChange == false)
                {

                    //  HelpClass.validate(requiredControlList, this);
                    decimal usd = (tb_total.Text == null || tb_total.Text == "") ? 0 : Convert.ToDecimal(tb_total.Text);
                    if (serviceData.serviceId > 0)
                    {
                        tb_totalSY.Text = HelpClass.DecTostring(usd * serviceData.syValue);
                    }
                    else
                    {
                        tb_totalSY.Text = HelpClass.DecTostring(usd * FillCombo.exchangeValue);
                    }
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Tb_totalSY_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (tb_syChange)
                {
                    //   HelpClass.validate(requiredControlList, this);
                    decimal sy = (tb_totalSY.Text == null || tb_totalSY.Text == "") ? 0 : Convert.ToDecimal(tb_totalSY.Text);
                    //  tb_priceBeforTax.Text = HelpClass.DecTostring(Taxsy/FillCombo.exchangeValue);
                    if (sy != 0)
                    {
                        if (serviceData.serviceId > 0)
                        {
                            tb_total.Text = HelpClass.DecTostring(sy / serviceData.syValue);
                        }
                        else
                        {
                            tb_total.Text = HelpClass.DecTostring(sy / FillCombo.exchangeValue);
                        }
                    }
                    else
                    {
                        tb_charge.Text = "0";
                    }
                }


            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Chk_confirmed_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                //txt_active.Text = MainWindow.resourcemanager.GetString("trConfirmed");
                disableEdit();

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

        private void Chk_confirmed_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private async void Chk_draft_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);

                if (serviceDatas is null)
                    await RefreshServiceDatasList();
                tgl_serviceDatastate = true;
                //txt_active.Text = MainWindow.resourcemanager.GetString("trDraft");


                if (first)
                {
                    first = false;
                }
                else
                {

                    enableEdit();
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

        private void Chk_draft_Unchecked(object sender, RoutedEventArgs e)
        {

        }
        #endregion

    }
}
