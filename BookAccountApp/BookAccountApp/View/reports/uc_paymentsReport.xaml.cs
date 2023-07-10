using BookAccountApp.ApiClasses;
using BookAccountApp.Classes;
using BookAccountApp.View.sales;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using Microsoft.Reporting.WinForms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using BookAccountApp.View.windows;

namespace BookAccountApp.View.reports
{
    /// <summary>
    /// Interaction logic for uc_paymentsReport.xaml
    /// </summary>
    public partial class uc_paymentsReport : UserControl
    {
        IEnumerable<PaymentsSts> paymentSts;
        IEnumerable<PaymentsSts> paymentStsQuery;
        Statistics statisticsModel = new Statistics();
        string searchText = "";

        public uc_paymentsReport()
        {
            InitializeComponent();
        }

        private static uc_paymentsReport _instance;
        public static uc_paymentsReport Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_paymentsReport();
                return _instance;
            }
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);

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
            translate();
            #endregion

                if (!MainWindow.userLogin.type.Equals("ag"))
                {
                    dpnl_country.Visibility = Visibility.Visible;
                    bdr_country.Visibility = Visibility.Collapsed;
                    await FillCombo.fillCountriesNames(cb_countries);
                    bdr_agent.Visibility = Visibility.Collapsed;
                }
                else
                {
                    dpnl_country.Visibility = Visibility.Collapsed;
                    bdr_country.Visibility = Visibility.Visible;
                    tb_country.Text = await HelpClass.getCountry(MainWindow.userLogin.countryId.Value);

                    dpnl_agent.Visibility = Visibility.Collapsed;
                    bdr_agent.Visibility = Visibility.Visible;
                    tb_agent.Text = MainWindow.userLogin.AccountName;

                    var typelist = new[] {
                                                new { Text  = MainWindow.userLogin.name+" "+MainWindow.userLogin.lastName ,
                                                      Value = MainWindow.userLogin.userId  }
                                             };
                    cb_agents.DisplayMemberPath = "Text";
                    cb_agents.SelectedValuePath = "Value";
                    cb_agents.ItemsSource = typelist;
                    cb_agents.SelectedIndex = 0;

                }

                await Search();

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {//search
            try
            {
                await Search();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {//unload
            GC.Collect();
        }

        #region methods
        async Task Search()
        {
            if (paymentSts is null)
                await RefreshPaymentSTSList();

            searchText = txt_search.Text.ToLower();
            paymentStsQuery = paymentSts
                .Where(s =>
            (
            s.countryName.ToLower().Contains(searchText)
            ||
            s.agentAccountName.ToString().ToLower().Contains(searchText)
            ||
            s.customerName.ToString().ToLower().Contains(searchText)
            ||
            s.packageName.ToString().ToLower().Contains(searchText)
            )
            &&
            //start date
            (dp_startDate.SelectedDate != null ? s.updateDatePo >= dp_startDate.SelectedDate : true)
            &&
            //end date            
            (dp_endDate.SelectedDate != null ? s.updateDatePo <= dp_endDate.SelectedDate : true)
            &&
            //country
            (cb_countries.SelectedIndex != -1 ? s.countryId == (int)cb_countries.SelectedValue : true)
            &&
            //agent
            (cb_agents.SelectedIndex != -1 ? s.userId == (int)cb_agents.SelectedValue : true)
             &&
            //customer
            (cb_customers.SelectedIndex != -1 ? s.customerId == (int)cb_customers.SelectedValue : true)
              &&
            //package
            (cb_packages.SelectedIndex != -1 ? s.packageId == (int)cb_packages.SelectedValue : true)
            )
            ;
            RefreshpaymentStsView();
        }


        void RefreshpaymentStsView()
        {
            dg_book.ItemsSource = paymentStsQuery;
            txt_count.Text = dg_book.Items.Count.ToString();
            fillColumnChart();
            fillPieChart();
            fillRowChart();
        }
        async Task<IEnumerable<PaymentsSts>> RefreshPaymentSTSList()
        {
            if(!MainWindow.userLogin.type.Equals("ag"))
                paymentSts = await statisticsModel.GetAllPayments();
            else
                paymentSts = await statisticsModel.GetPaymentsByAgentId(MainWindow.userLogin.userId);

            return paymentSts;
        }

        private void translate()
        {
            txt_tabTitle.Text = MainWindow.resourcemanager.GetString("trBook");

            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_countries, MainWindow.resourcemanager.GetString("trCountryHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_country, MainWindow.resourcemanager.GetString("trCountryHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_agents, MainWindow.resourcemanager.GetString("trAgentHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_agent, MainWindow.resourcemanager.GetString("trAgentHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_customers, MainWindow.resourcemanager.GetString("trCustomerHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_packages, MainWindow.resourcemanager.GetString("trPackageHint"));

            chk_allCountries.Content = MainWindow.resourcemanager.GetString("trAll");
            chk_allAgents.Content = MainWindow.resourcemanager.GetString("trAll");
            chk_allCustomers.Content = MainWindow.resourcemanager.GetString("trAll");
            chk_allPackages.Content = MainWindow.resourcemanager.GetString("trAll");

            MaterialDesignThemes.Wpf.HintAssist.SetHint(dp_startDate, MainWindow.resourcemanager.GetString("trStartDateHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(dp_endDate, MainWindow.resourcemanager.GetString("trEndDateHint"));

            MaterialDesignThemes.Wpf.HintAssist.SetHint(txt_search, MainWindow.resourcemanager.GetString("trSearchHint"));

            dg_book.Columns[0].Header = MainWindow.resourcemanager.GetString("trProcess");//Process Num
            dg_book.Columns[1].Header = MainWindow.resourcemanager.GetString("tr_Book");//booked Num 
            dg_book.Columns[2].Header = MainWindow.resourcemanager.GetString("trPackage");
            dg_book.Columns[3].Header = MainWindow.resourcemanager.GetString("trProcessDate");
            dg_book.Columns[4].Header = MainWindow.resourcemanager.GetString("trExpireDate");
            dg_book.Columns[5].Header = MainWindow.resourcemanager.GetString("trAgent");
            dg_book.Columns[6].Header = MainWindow.resourcemanager.GetString("trCustomer");
            dg_book.Columns[7].Header = MainWindow.resourcemanager.GetString("trPrice");
            dg_book.Columns[8].Header = MainWindow.resourcemanager.GetString("trPayments");

            tt_report.Content = MainWindow.resourcemanager.GetString("trPdf");
            tt_print.Content = MainWindow.resourcemanager.GetString("trPrint");
            tt_excel.Content = MainWindow.resourcemanager.GetString("trExcel");
            tt_count.Content = MainWindow.resourcemanager.GetString("trCount");
            tt_refresh.Content = MainWindow.resourcemanager.GetString("trRefresh");
        }
        private void chkAll(string _name, bool isChk)
        {
            List<ComboBox> cbList = new List<ComboBox>();
            List<CheckBox> chkList = new List<CheckBox>();
            switch (_name)
            {
                case "chk_allCountries":
                    cbList = new List<ComboBox>() { cb_countries, cb_agents, cb_customers, cb_packages };
                    chkList = new List<CheckBox>() { chk_allAgents, chk_allCustomers, chk_allPackages };
                    break;
                case "chk_allAgents":
                    cbList = new List<ComboBox>() { cb_agents, cb_customers, cb_packages };
                    chkList = new List<CheckBox>() { chk_allCustomers, chk_allPackages };
                    break;
                case "chk_allCustomers":
                    cbList = new List<ComboBox>() { cb_customers, cb_packages };
                    chkList = new List<CheckBox>() { chk_allPackages };
                    break;
                case "chk_allPackages":
                    cbList = new List<ComboBox>() { cb_packages };
                    chkList = new List<CheckBox>();
                    break;
            }
            foreach (var c in cbList)
            {
                c.IsEnabled = !isChk;
                c.SelectedIndex = -1;
            }
            foreach (var ch in chkList)
            {
                ch.IsEnabled = !isChk;
            }
        }
        #endregion

        #region events
        private async void Btn_refresh_Click(object sender, RoutedEventArgs e)
        {//refresh
            try
            {
                await RefreshPaymentSTSList();

                if (!MainWindow.userLogin.type.Equals("ag"))
                {
                    cb_countries.SelectedIndex = -1;
                    cb_agents.SelectedIndex = -1;
                }
                cb_customers.SelectedIndex = -1;
                cb_packages.SelectedIndex = -1;

                await Search();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Dp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {//date
            try
            {
                await Search();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Cb_countries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//select country
            try
            {
                if (cb_countries.SelectedIndex != -1)
                {
                    dpnl_agent.IsEnabled = true;
                    await FillCombo.fillAgentByCountry(cb_agents, (int)cb_countries.SelectedValue);
                }
                else
                {
                    dpnl_agent.IsEnabled = false;
                    cb_agents.SelectedIndex = -1;
                }
                await Search();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Cb_agents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//select agent
            try
            {
                /*
                if (cb_agents.SelectedIndex != -1)
                {
                    dpnl_customer.IsEnabled = true;
                    if ((int)cb_agents.SelectedValue == 3)
                        await FillCombo.fillCustomer(cb_customers);
                    else
                        await FillCombo.fillCustomerByAgent(cb_customers, (int)cb_agents.SelectedValue);
                }
                else
                {
                    dpnl_customer.IsEnabled = false;
                    cb_customers.SelectedIndex = -1;
                }

                await Search();
                */
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Cb_customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//select customer
            try
            {
                if (cb_customers.SelectedIndex != -1)
                {
                    dpnl_package.IsEnabled = true;
                    cb_packages.ItemsSource = paymentStsQuery.Where(b => b.customerId == (int)cb_customers.SelectedValue).GroupBy(p => p.packageId);
                    cb_packages.SelectedValuePath = "packageId";
                    cb_packages.DisplayMemberPath = "packageName";
                    cb_packages.SelectedIndex = -1;
                    //await FillCombo.fillPackageByCustomer(cb_packages, (int)cb_customers.SelectedValue);
                }
                else
                {
                    dpnl_package.IsEnabled = false;
                    cb_packages.SelectedIndex = -1;
                }
                await Search();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Cb_packages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//select package
            try
            {
                if (cb_packages.SelectedIndex != -1)
                    paymentSts = await statisticsModel.GetPaymentsByAgentId((int)cb_agents.SelectedValue);

                await Search();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Chk_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = ((CheckBox)sender).Name;
                chkAll(name, true);
                await Search();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Chk_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = ((CheckBox)sender).Name;
                chkAll(name, false);
                await Search();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        #endregion

        #region charts
        private void fillColumnChart()
        {
            axcolumn.Labels = new List<string>();
            List<string> names = new List<string>();
            List<int> ids = new List<int>();
            List<int> packIds = new List<int>();

            var tempName = paymentStsQuery.GroupBy(s => new { s.customerId }).Select(s => new
            {
                customerId = s.FirstOrDefault().customerId,
                //customerName = s.FirstOrDefault().customerName + " " + s.FirstOrDefault().customerLastName
                customerName = s.FirstOrDefault().customerCompany
            });
            names.AddRange(tempName.Select(nn => nn.customerName));
            ids.AddRange(tempName.Select(nn => nn.customerId.Value));

            var tempPackage = paymentStsQuery.GroupBy(s => new { s.packageId }).Select(s => new
            {
                packageId = s.FirstOrDefault().packageId,
                packageName = s.FirstOrDefault().packageName
            });
            packIds.AddRange(tempPackage.Select(nn => nn.packageId.Value));
           
            List<PaymentsSts> resultList = new List<PaymentsSts>();

            resultList = paymentStsQuery.GroupBy(x => new { x.customerId, x.packageId }).Select(x => new PaymentsSts
            {
                customerId = x.FirstOrDefault().customerId,
                //customerName = x.FirstOrDefault().customerName + " " + x.FirstOrDefault().customerLastName,
                customerName = x.FirstOrDefault().customerCompany,
                packageId = x.FirstOrDefault().packageId,
                totalnet = x.Sum(g => (decimal)g.totalnet)
            }
            ).ToList();

            SeriesCollection columnChartData = new SeriesCollection();

            int xCount = 6;
            if (names.Count() <= 6)
                xCount = names.Count();

            for (int j = 0; j < packIds.Count; j++)
            {
                List<decimal> paid = new List<decimal>();

                for (int i = 0; i < xCount; i++)
                {
                    axcolumn.Labels.Add(names.ToList().Skip(i).FirstOrDefault());

                    int cusId = ids[i], pId = packIds[j];

                    paid.Add(resultList.Where(nn => nn.packageId == pId && nn.customerId == cusId).Select(nn => nn.totalnet).FirstOrDefault());
                }

                columnChartData.Add(
                new StackedColumnSeries
                {
                    Values = paid.AsChartValues(),
                    DataLabels = true,
                    Title = tempPackage.Select(nn => nn.packageName).Skip(j).FirstOrDefault()
                });
            }
            DataContext = this;
            cartesianChart.Series = columnChartData;
        }

        private void fillPieChart()
        {
            List<string> titles = new List<string>();
            IEnumerable<int> x = null;

            titles.Clear();
            int count = 0;

            var result = paymentStsQuery.GroupBy(s => new { s.customerId }).Select(s => new
            {
                customerId = s.Key.customerId,
                //customerName = s.FirstOrDefault().customerName+" "+ s.FirstOrDefault().customerLastName,
                customerName = s.FirstOrDefault().customerCompany,
                count = s.Count()
            });
            titles.AddRange(result.Select(jj => jj.customerName));

            x = result.Select(m => m.count);

            count = titles.Count();

            SeriesCollection piechartData = new SeriesCollection();

            int xCount = 6;
            if (count < 6) xCount = count;

            for (int i = 0; i < xCount; i++)
            {
                List<decimal> final = new List<decimal>();
                final.Add(x.ToList().Skip(i).FirstOrDefault());
                piechartData.Add(
                 new PieSeries
                 {
                     Values = final.AsChartValues(),
                     Title = titles.Skip(i).FirstOrDefault(),
                     DataLabels = true,
                 }
             );
            }
            if (count > 6)
            {
                List<decimal> final = new List<decimal>();
                final.Add(x.ToList().Skip(6).FirstOrDefault());
                piechartData.Add(
                new PieSeries
                {
                    Values = final.AsChartValues(),
                    Title = MainWindow.resourcemanager.GetString("trOthers"),
                    DataLabels = true,
                }
            );
            }

            chart1.Series = piechartData;
        }
        private void fillRowChart()
        {
            int endYear = DateTime.Now.Year;
            int startYear = endYear - 1;
            int startMonth = DateTime.Now.Month;
            int endMonth = startMonth;
            if (dp_startDate.SelectedDate != null && dp_endDate.SelectedDate != null)
            {
                startYear  = dp_startDate.SelectedDate.Value.Year;
                endYear    = dp_endDate.SelectedDate.Value.Year;
                startMonth = dp_startDate.SelectedDate.Value.Month;
                endMonth   = dp_endDate.SelectedDate.Value.Month;
            }
            MyAxis.Labels = new List<string>();
            List<string> names = new List<string>();
            List<int> ids = new List<int>();
            List<PaymentsSts> resultList = new List<PaymentsSts>();

            SeriesCollection rowChartData = new SeriesCollection();
            var temp = paymentStsQuery.GroupBy(s => new { s.agentId }).Select(s => new
            {
                Date = s.FirstOrDefault().updateDatePo,///////////?????????????????????????
                Name = s.FirstOrDefault().agentName +" "+ s.FirstOrDefault().agentLastName,
                Id = s.FirstOrDefault().agentId
            });
            names.AddRange(temp.Select(nn => nn.Name.ToString()));
            ids.AddRange(temp.Select(nn => nn.Id.Value));

            int xCount = 6;
            if (temp.Count() < 6) xCount = temp.Count();
            for (int i = 0; i < xCount; i++)
            {
                #region
                SeriesCollection columnChartData = new SeriesCollection();
                List<decimal> paySum = new List<decimal>();

                if (endYear - startYear <= 1)
                {
                    for (int year = startYear; year <= endYear; year++)
                    {
                        for (int month = startMonth; month <= 12; month++)
                        {
                            var firstOfThisMonth = new DateTime(year, month, 1);
                            var firstOfNextMonth = firstOfThisMonth.AddMonths(1);
                            var drawSum = paymentStsQuery.ToList().Where(c => c.updateDatePo > firstOfThisMonth && c.updateDatePo <= firstOfNextMonth && c.agentId == ids[i]).Select(c => c.totalnet).Sum();

                            paySum.Add(drawSum);

                            MyAxis.Labels.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month) + "/" + year);

                            if (year == endYear && month == endMonth)
                            {
                                break;
                            }
                            if (month == 12)
                            {
                                startMonth = 1;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    for (int year = startYear; year <= endYear; year++)
                    {
                        var firstOfThisYear = new DateTime(year, 1, 1);
                        var firstOfNextMYear = firstOfThisYear.AddYears(1);
                        var drawSum = paymentStsQuery.ToList().Where(c => c.updateDatePo > firstOfThisYear && c.updateDatePo <= firstOfNextMYear).Select(c => c.totalnet).Sum();

                        paySum.Add(drawSum);
                        MyAxis.Labels.Add(year.ToString());
                    }
                }
                rowChartData.Add(
                new LineSeries
                {
                    Values = paySum.AsChartValues(),
                    //Title = MainWindow.resourcemanager.GetString("trPayments")
                    Title = names[i]

                });
                #endregion
            }
            if (temp.Count() > 6)
            {
                
                #region
                SeriesCollection columnChartData = new SeriesCollection();
                List<decimal> paySum = new List<decimal>();
                decimal drawSum = 0;
                if (endYear - startYear <= 1)
                    {
                        for (int year = startYear; year <= endYear; year++)
                        {
                            for (int month = startMonth; month <= 12; month++)
                            {
                                for (int i = 6; i < temp.Count(); i++)
                                {
                                    var firstOfThisMonth = new DateTime(year, month, 1);
                                    var firstOfNextMonth = firstOfThisMonth.AddMonths(1);
                                    drawSum = paymentStsQuery.ToList().Where(c => c.updateDatePo > firstOfThisMonth && c.updateDatePo <= firstOfNextMonth && c.agentId == ids[i]).Select(c => c.totalnet).Sum();
                                }
                                paySum.Add(drawSum);
                                MyAxis.Labels.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month) + "/" + year);

                                if (year == endYear && month == endMonth)
                                {
                                    break;
                                }
                                if (month == 12)
                                {
                                    startMonth = 1;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int year = startYear; year <= endYear; year++)
                        {
                            for (int i = 6; i < temp.Count(); i++)
                            {
                                var firstOfThisYear = new DateTime(year, 1, 1);
                                var firstOfNextMYear = firstOfThisYear.AddYears(1);
                                drawSum = paymentStsQuery.ToList().Where(c => c.updateDatePo > firstOfThisYear && c.updateDatePo <= firstOfNextMYear).Select(c => c.totalnet).Sum();
                            }
                            paySum.Add(drawSum);
                            MyAxis.Labels.Add(year.ToString());
                        }
                    }


                rowChartData.Add(
                new LineSeries
                {
                    Values = paySum.AsChartValues(),
                    Title = MainWindow.resourcemanager.GetString("trOthers")
                }
                );
                    #endregion

            }
            DataContext = this;
            rowChart.Series = rowChartData;
        }
        #endregion

        #region reports
        ReportCls reportclass = new ReportCls();
        LocalReport rep = new LocalReport();
        SaveFileDialog saveFileDialog = new SaveFileDialog();

        public void BuildReport()
        {

            //string firstTitle = "paymentsReport";
            ////string secondTitle = "";
            ////string subTitle = "";
            //string Title = "";

            List<ReportParameter> paramarr = new List<ReportParameter>();

            string addpath;
            bool isArabic = ReportCls.checkLang();
            if (isArabic)
            {
                addpath = @"\Reports\statisticReports\Ar\ArPayments.rdlc";
            }
            else
            {
                addpath = @"\Reports\statisticReports\En\EnPayments.rdlc";
            }
            //D:\myproj\posproject3\BookAccountApp\BookAccountApp\Reports\statisticReports\En\EnBook.rdlc
            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
            //     subTitle = clsReports.ReportTabTitle(firstTitle, secondTitle);
            //  Title = MainWindow.resourcemanagerreport.GetString("trAccountantReport");

            clsReports.PaymentsReport(paymentStsQuery, rep, reppath, paramarr);
            clsReports.setReportLanguage(paramarr);
            clsReports.Header(paramarr);

            rep.SetParameters(paramarr);

            rep.Refresh();

        }
        private void Btn_pdf_Click(object sender, RoutedEventArgs e)
        {///pdf
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

        private void Btn_print_Click(object sender, RoutedEventArgs e)
        {//print
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
        {//excel
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

        private void Btn_preview_Click(object sender, RoutedEventArgs e)
        {//preview
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

        #endregion

        private async void Dg_book_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
               // HelpClass.StartAwait(grid_main);
               /*
                PaymentsSts payment = dg_book.SelectedItem as PaymentsSts;
                if (payment.packageUserId > 0)
                {
                    PackageUser puModel = new PackageUser();
                    PackageUser packageUser = await puModel.GetByID(payment.packageUserId);

                    Packages pModel = new Packages();
                    Packages package = await pModel.GetByID(payment.packageId.Value);

                    if (packageUser.packageId > 0)
                    {
                        uc_sales.Instance.isPayment = true;
                        uc_payment.Instance.isFirstTime = false;
                        MainWindow.mainWindow.Btn_sales_Click(MainWindow.mainWindow.btn_sales, null);
                        uc_sales.Instance.Window_Loaded(null, null);
                        uc_sales.Instance.Btn_packageUser_Click(uc_sales.Instance.btn_packageUser, null);
                        uc_payment.Instance.cusID = payment.customerId.Value;
                        uc_payment.Instance.packuserID = payment.packageUserId;
                        uc_payment.Instance.discount_ = payment.discountValue;
                        uc_payment.Instance.UserControl_Loaded(null, null);
                    }

                }
                //Clear();
                //ClearPackageUser();

                //HelpClass.EndAwait(grid_main);
                */
            }
            catch (Exception ex)
            {
                //HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
    }
}
