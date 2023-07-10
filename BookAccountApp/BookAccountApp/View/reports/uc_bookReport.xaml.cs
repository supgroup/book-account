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
using netoaster;
using BookAccountApp.View.windows;

namespace BookAccountApp.View.reports
{
    /// <summary>
    /// Interaction logic for uc_bookReport.xaml
    /// </summary>
    public partial class uc_bookReport : UserControl
    {
        
        IEnumerable<BookSts> bookSts;
        IEnumerable<BookSts> bookStsQuery;
        Statistics statisticsModel = new Statistics();
        string searchText = "";
        private static uc_bookReport _instance;
        public static uc_bookReport Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_bookReport();
                return _instance;
            }
        }
        public uc_bookReport()
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
                                            new { Text  = MainWindow.userLogin.name+" "+ MainWindow.userLogin.lastName,
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
            if (bookSts is null)
                await RefreshBookSTSList();

            searchText = txt_search.Text.ToLower();
            bookStsQuery = bookSts
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
            RefreshbookStsView();
        }

        void RefreshbookStsView()
        {
            dg_book.ItemsSource = bookStsQuery;
            txt_count.Text = bookStsQuery.Count().ToString();
            fillColumnChart();
            fillPieChart();
            fillRowChart();
        }
        async Task<IEnumerable<BookSts>> RefreshBookSTSList()
        {
            if (!MainWindow.userLogin.type.Equals("ag"))
                bookSts = await statisticsModel.GetAllBooks();
            else
                bookSts = await statisticsModel.GetByAgentId(MainWindow.userLogin.userId);

            return bookSts;
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

            dg_book.Columns[0].Header = MainWindow.resourcemanager.GetString("trNO");
            dg_book.Columns[1].Header = MainWindow.resourcemanager.GetString("trBookDate");
            dg_book.Columns[2].Header = MainWindow.resourcemanager.GetString("trUpgradeDate");
            dg_book.Columns[3].Header = MainWindow.resourcemanager.GetString("trPackage");
            dg_book.Columns[4].Header = MainWindow.resourcemanager.GetString("trAgent");
            dg_book.Columns[5].Header = MainWindow.resourcemanager.GetString("trCustomer");
            dg_book.Columns[6].Header = MainWindow.resourcemanager.GetString("trPrice");
           
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
                await RefreshBookSTSList();

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
                    //cb_agents.IsEnabled = false;
                    cb_agents.SelectedIndex = -1;
                    dpnl_agent.IsEnabled = false;
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
                    cb_packages.ItemsSource = bookStsQuery.Where(b => b.customerId == (int)cb_customers.SelectedValue).GroupBy(p => p.packageId);
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
                    bookSts = await statisticsModel.GetByAgentId((int)cb_agents.SelectedValue);

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
        private async void Dg_book_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //HelpClass.StartAwait(grid_main);

                BookSts book = dg_book.SelectedItem as BookSts;
                /*
                if (book.packageUserId > 0)
                {

                    PackageUser puModel = new PackageUser();
                    PackageUser packageUser = await puModel.GetByID(book.packageUserId);

                    Packages pModel = new Packages();
                    Packages package = await pModel.GetByID(book.packageId.Value);

                    if (packageUser.packageId > 0)
                    {
                        MainWindow.mainWindow.Btn_sales_Click(MainWindow.mainWindow.btn_sales, null);
                        uc_sales.Instance.Window_Loaded(null, null);
                        uc_sales.Instance.Btn_sale_Click(uc_sales.Instance.btn_sale, null);
                        uc_sale.Instance.UserControl_Loaded(null, null);
                        uc_sale.Instance.package = package;

                        uc_sale.Instance.oldCustomerId = packageUser.customerId.Value;
                        uc_sale.Instance.oldAgentId = packageUser.userId.Value;
                        uc_sale.Instance.oldPackageId = packageUser.packageId.Value;
                        uc_sale.Instance.oldCountryPackageId = packageUser.countryPackageId.Value;
                        uc_sale.Instance.packuser = packageUser;
                        uc_sale.Instance.btn_serials.IsEnabled = true;
                        uc_sale.Instance.tb_activationCode.Text = packageUser.packageSaleCode;
                        uc_sale.Instance.isOnline = packageUser.isOnlineServer.Value;
                        HelpClass.clearValidate(uc_sale.requiredControlList, this);
                    }
                }
                */
                //Clear();
                //ClearPackageUser();

                //HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                //HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        #endregion

        #region charts
        private void fillColumnChart()
        {
            axcolumn.Labels = new List<string>();
            List<string> names = new List<string>();
            List<int> countLst = new List<int>();

            var result = bookStsQuery.GroupBy(s => s.packageId).Select(s => new
            {
                packageId = s.Key,
                count = s.Count()
            });

            var tempName = bookStsQuery.GroupBy(s => s.packageName).Select(s => new
            {
                packageName = s.Key
            });
            names.AddRange(tempName.Select(nn => nn.packageName));

            countLst.AddRange(result.Select(nn => nn.count));

            SeriesCollection columnChartData = new SeriesCollection();
            List<int> cS = new List<int>();

            List<string> titles = new List<string>()
            {
               MainWindow.resourcemanager.GetString("trPackages")
            };
            int x = 6;
            if (names.Count() <= 6) x = names.Count();

            for (int i = 0; i < x; i++)
            {
                cS.Add(countLst.ToList().Skip(i).FirstOrDefault());
                axcolumn.Labels.Add(names.ToList().Skip(i).FirstOrDefault());
            }

            if (names.Count() > 6)
            {
                int countSum = 0;
                for (int i = 6; i < names.Count(); i++)
                    countSum = countSum + countLst.ToList().Skip(i).FirstOrDefault();

                if (countSum != 0)
                    cS.Add(countSum);

                axcolumn.Labels.Add(MainWindow.resourcemanager.GetString("trOthers"));
            }

            columnChartData.Add(
            new StackedColumnSeries
            {
                Values = cS.AsChartValues(),
                Title = titles[0],
                DataLabels = true,
            });

            DataContext = this;
            cartesianChart.Series = columnChartData;
        }

        private void fillPieChart()
        {
            List<string> titles = new List<string>();
            IEnumerable<int> x = null;

            titles.Clear();
            var temp = bookStsQuery;
            int count = 0;

            var titleTemp = temp.GroupBy(m => m.agentName+" "+m.agentLastName);
            titles.AddRange(titleTemp.Select(jj => jj.Key));

            var result = temp.GroupBy(s => new { s.agentId , s.packageId}).Select(s => new
            {
                agentId = s.Key.agentId,
                count = s.Count()
            });
            
            var result1 = result.GroupBy(s => s.agentId).Select(m => new {count = m.Count() });

            x = result1.Select(m => m.count);

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
            MyAxis.Labels = new List<string>();
            List<string> names = new List<string>();
            List<int> ids = new List<int>();
            List<int> otherIds = new List<int>();

            List<BookSts> resultList = new List<BookSts>();
            SeriesCollection rowChartData = new SeriesCollection();

           
                var tempName = bookStsQuery.GroupBy(s => new { s.customerId }).Select(s => new
                {
                    id = s.Key,
                    //name = s.FirstOrDefault().customerName+" "+s.FirstOrDefault().customerLastName
                    name = s.FirstOrDefault().customerCompany

                });
                names.AddRange(tempName.Select(nn => nn.name.ToString()));
                ids.AddRange(tempName.Select(mm => mm.id.customerId.Value));

            //LineSeries[] ls = new LineSeries[names.Count];
            int x = 6;
            if (names.Count() < 6) x = names.Count();
            for (int i = 0; i < x; i++)
            {

                drawPoints(names[i], ids[i], rowChartData, 'n', otherIds);
            }
            //others
            if (names.Count() > 6)
            {
                for (int i = names.Count - x; i < names.Count; i++)
                    otherIds.Add(ids[i]);
                drawPoints(MainWindow.resourcemanager.GetString("trOthers"), 0, rowChartData, 'o', otherIds);
            }
            //rowChartData.AddRange(ls);
            DataContext = this;
            rowChart.Series = rowChartData;

        }

        private void drawPoints(string name, int id, SeriesCollection rowChartData, char ch, List<int> otherIds)
        {
            int endYear = DateTime.Now.Year;
            int startYear = endYear - 1;
            int startMonth = DateTime.Now.Month;
            int endMonth = startMonth;
            if (dp_startDate.SelectedDate != null && dp_endDate.SelectedDate != null)
            {
                startYear = dp_startDate.SelectedDate.Value.Year;
                endYear = dp_endDate.SelectedDate.Value.Year;
                startMonth = dp_startDate.SelectedDate.Value.Month;
                endMonth = dp_endDate.SelectedDate.Value.Month;
            }
            SeriesCollection columnChartData = new SeriesCollection();
            List<int> countLst = new List<int>();

            if (endYear - startYear <= 1)
            {
                for (int year = startYear; year <= endYear; year++)
                {
                    for (int month = startMonth; month <= 12; month++)
                    {
                        var firstOfThisMonth = new DateTime(year, month, 1);
                        var firstOfNextMonth = firstOfThisMonth.AddMonths(1);

                        if (ch == 'n')
                        {
                            var drawProfit = bookStsQuery.ToList().Where(c => c.bookDate > firstOfThisMonth && c.bookDate <= firstOfNextMonth && c.customerId.Value == id)
                                                             //.Select(b => b.customerName+" "+b.customerLastName).Count();
                                                             .Select(b => b.customerCompany).Count();

                            countLst.Add(drawProfit);
                        }
                        else if (ch == 'o')
                        {
                            int sum = 0;
                            for (int i = 0; i < otherIds.Count; i++)
                            {
                                var drawCount = bookStsQuery.ToList().Where(c => c.bookDate > firstOfThisMonth && c.bookDate <= firstOfNextMonth && c.customerId.Value == otherIds[i])
                                                            //.Select(b => b.customerName + " " + b.customerLastName).Count();
                                                            .Select(b => b.customerCompany).Count();

                                sum = sum + drawCount;
                            }
                            countLst.Add(sum);
                        }
                       
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
                 
                    if (ch == 'n')
                    {
                        var drawCount= bookStsQuery.ToList().Where(c => c.bookDate > firstOfThisYear && c.bookDate <= firstOfNextMYear && c.customerId.Value == id)
                                                        //.Select(b => b.customerName + " " + b.customerLastName).Count();
                                                        .Select(b => b.customerCompany).Count();

                        countLst.Add(drawCount);
                    }
                    else if (ch == 'o')
                    {
                        int sum = 0;
                        for (int i = 0; i < otherIds.Count; i++)
                        {
                            var drawCount = bookStsQuery.ToList().Where(c => c.bookDate > firstOfThisYear && c.bookDate <= firstOfNextMYear && c.customerId.Value == id)
                                                        //.Select(b => b.customerName + " " + b.customerLastName).Count();
                                                        .Select(b => b.customerCompany).Count();


                            sum = sum + drawCount;
                        }
                        countLst.Add(sum);
                    }
                  
                    MyAxis.Labels.Add(year.ToString());
                }
            }

            rowChartData.Add(
                        new LineSeries
                        {
                            Values = countLst.AsChartValues(),
                            Title = name
                        });

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
                addpath = @"\Reports\statisticReports\Ar\ArBook.rdlc";
            }
            else
            {
                addpath = @"\Reports\statisticReports\En\EnBook.rdlc";
            }
            //D:\myproj\posproject3\BookAccountApp\BookAccountApp\Reports\statisticReports\En\EnBook.rdlc
            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
       //     subTitle = clsReports.ReportTabTitle(firstTitle, secondTitle);
          //  Title = MainWindow.resourcemanagerreport.GetString("trAccountantReport");
         
            clsReports.BookReport(bookStsQuery, rep, reppath, paramarr);
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
                LocalReportExtensions.PrintToPrinterbyNameAndCopy(rep, FillCombo.getdefaultPrinters(), FillCombo.rep_print_count == null ? short.Parse("1") : short.Parse(FillCombo.rep_print_count)) ;
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


    }
}
