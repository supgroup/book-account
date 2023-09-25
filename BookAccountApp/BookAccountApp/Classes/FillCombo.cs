using BookAccountApp.ApiClasses;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Drawing.Printing;
using System.Threading;
using System.Windows;
using System.Threading.Tasks;
using System;

namespace BookAccountApp.Classes
{
    public class FillCombo
    {
        /// <summary>
        /// Packages
        /// </summary>
        //static Packages package = new Packages();
        //static IEnumerable<Packages> packages;

        //static PackageUser packageUser = new PackageUser();
        //static IEnumerable<PackageUser> packageUsers;

        //static IEnumerable<Packages> agentPackages;

        //static CountryPackageDate cpd = new CountryPackageDate();
        //static IEnumerable<CountryPackageDate> countryPackageDates;
        //static public async Task fillPackageUser(ComboBox combo)
        //{
        //    packageUsers = await packageUser.GetAll();
        //    combo.ItemsSource = packageUsers.Where(x => x.isActive == 1);
        //    combo.SelectedValuePath = "packageUserId";
        //    combo.DisplayMemberPath = "packageSaleCode";
        //}
        /*
        static public async Task fillBookNum(ComboBox combo , int customerId)
        {
            packageUsers = await packageUser.GetByCustomerId(customerId);
            foreach (var i in packageUsers)
            {
                i.packageNumber = i.packageNumber + "     " + i.packageName;
            }
            combo.ItemsSource = packageUsers.Where(x => x.isActive == 1 );
            combo.SelectedValuePath = "packageUserId";
            combo.DisplayMemberPath = "packageNumber";
            //combo.SelectedIndex = -1;
        }
        */
        /*
        static public async Task fillBookNumAgent(ComboBox combo, int customerId , int agentId)
        {
            packageUsers = await packageUser.GetByCustomerId(customerId);
            packageUsers = packageUsers.Where(p => p.userId == agentId);
            foreach (var i in packageUsers)
            {
                i.packageNumber = i.packageNumber + "     " + i.packageName;
            }
            combo.ItemsSource = packageUsers.Where(x => x.isActive == 1);
            combo.SelectedValuePath = "packageUserId";
            combo.DisplayMemberPath = "packageNumber";
            //combo.SelectedIndex = -1;
        }
        */
        /*
        static public async Task fillPackage(ComboBox combo)
        {
            packages = await package.GetAll();
            combo.ItemsSource = packages.Where(x => x.isActive == 1);
            combo.SelectedValuePath = "packageId";
            combo.DisplayMemberPath = "packageName";
            combo.SelectedIndex = -1;
        }
        */
        /*
        static public async Task fillPackageByCustomer(ComboBox combo , int customerId)
        {
            packages = await package.GetByCustomerCountry(customerId);
            combo.ItemsSource = packages.Where(x => x.isActive == 1 );
            combo.SelectedValuePath = "packageId";
            combo.DisplayMemberPath = "packageName";
            combo.SelectedIndex = -1;
        }
        */
        /*
        static public void fillRegion( )
        {
            MainWindow.Region = countrynum.Where(C=> C.isDefault==1).FirstOrDefault();
           
        }
        */
        /*
        static public async Task fillPeriod(ComboBox combo, int customerId, int packageId)
        {
            countryPackageDates = await cpd.GetByCustomerPackId(customerId , packageId);

            foreach (CountryPackageDate cpd in countryPackageDates)
            {
                string period = "";

                period = HelpClass.getPeriod(cpd);

                cpd.notes = period + "       " + cpd.price + " " + cpd.currency;
            }
            combo.ItemsSource = countryPackageDates.Where(x => x.isActive == 1);
            combo.SelectedValuePath = "Id";
            combo.DisplayMemberPath = "notes";
            combo.SelectedIndex = -1;
        }
        */
        /*
        static public async Task fillAgentPackage(ComboBox combo , int agentId)
        {
            agentPackages = await package.GetPackagesByAgent(agentId);
            combo.ItemsSource = agentPackages.Where(p => p.isActive == 1);
            combo.SelectedValuePath = "packageId";
            combo.DisplayMemberPath = "packageName";
        }
        */
        /// <summary>
        /// Programs
        /// </summary>
        /// 
        /*
        static Programs program = new Programs();
        static IEnumerable<Programs> programs;
        static public async Task fillProgram(ComboBox combo)
        {
            programs = await program.GetAll();
            combo.ItemsSource = programs.Where(x =>  x.isActive == 1);
            combo.SelectedValuePath = "programId";
            combo.DisplayMemberPath = "name";
        }
        */
        /*
        /// <summary>
        /// Version
        /// </summary>
        static Versions version = new Versions();
        static IEnumerable<Versions> versions;
        static public async Task fillVersion(ComboBox combo)
        {
            versions = await version.GetAll();
            combo.ItemsSource = versions.Where(x => x.isActive == 1);
            combo.SelectedValuePath = "verId";
            combo.DisplayMemberPath = "name";
        }

        static public async Task fillVersionByProgram(ComboBox combo, int programId)
        {
            versions = await version.GetAll();
            combo.ItemsSource = versions.Where(x => x.programId == programId &&  x.isActive == 1);
            combo.SelectedValuePath = "verId";
            combo.DisplayMemberPath = "name";
        }
        */
        /// <summary>
        /// User & Agent 
        /// </summary>
        static Users user = new Users();
        static IEnumerable<Users> users;
        static public void fillCurrency (ComboBox combo)
        {
           
            //    cb_invlang
            // replangList = await setvalueModel.GetBySetName("report_lang");

            var CurrencyList = new[] {
                new { Text = "SYP" , Value = "syp" },
                new { Text ="$" , Value = "usd" },
          
                 };
            combo.DisplayMemberPath = "Text";
            combo.SelectedValuePath = "Value";
            combo.ItemsSource = CurrencyList;
 
        }
      
        static public async Task fillAgent(ComboBox combo)
        {
            users = await user.GetAll();
            combo.ItemsSource = users.Where(x => x.isActive == true && x.type == "ag");
            combo.SelectedValuePath = "userId";
            combo.DisplayMemberPath = "name";
        }

        static public async Task fillAgentByCountry(ComboBox combo, int countryID)
        {
            users = await user.GetAll();
            foreach (var a in users)
            {
                if (a.userId == 3)
                    a.name = "Supclouds";
                else
                    a.name = a.name + " " + a.lastName;
            }
            combo.ItemsSource = users.Where(x => x.isActive == true && x.type == "ag" && x.countryId == countryID);
            combo.SelectedValuePath = "userId";
            combo.DisplayMemberPath = "name";
        }
        /*
        /// <summary>
        /// Customer
        /// </summary>
        static Customers customer = new Customers();
        static IEnumerable<Customers> customers;
        static public async Task fillCustomer(ComboBox combo)
        {
            customers = await customer.GetAll();
            //foreach (var c in customers)
            //{
            //    c.custname = c.custCode + " " + c.custname + " " + c.lastName;
            //}
            combo.ItemsSource = customers.Where(x => x.isActive == 1);
            combo.SelectedValuePath = "custId";
            //combo.DisplayMemberPath = "custname";
            combo.DisplayMemberPath = "company";
        }
        static public async Task fillCustomerByAgent(ComboBox combo , int agentID)
        {
            customers = await customer.GetCustomersByAgent(agentID);
            //foreach(var c in customers)
            //{
            //    c.custname = c.custCode + " " + c.custname + " " + c.lastName;
            //}
            combo.ItemsSource = customers.Where(x => x.isActive == 1);
            combo.SelectedValuePath = "custId";
            //combo.DisplayMemberPath = "custname";
            combo.DisplayMemberPath = "company";
        }
        */
        #region Countries
        /// <summary>
        /// area code methods
        /// </summary>
        /// <returns></returns>
        /// 
        //phone 
        public static IEnumerable<Country> countrynum;
        public static IEnumerable<City> citynum;
        public static IEnumerable<City> citynumofcountry;
        public static Country countrycodes = new Country();
        public static City cityCodes = new City();

        static public async Task<IEnumerable<Country>> RefreshCountry()
        {
            countrynum = await countrycodes.GetAll();
            countrynum = countrynum.Where(x => x.countryId == 9).ToList();
            return countrynum;
        }
        static public async Task<IEnumerable<City>> RefreshCity()
        {
            citynum = await cityCodes.Get();
            return citynum;
        }
        static public async Task fillCountries(ComboBox combo)
        {
            if (countrynum is null)
                await RefreshCountry();

            combo.ItemsSource = countrynum.ToList();
            combo.SelectedValuePath = "countryId";
            combo.DisplayMemberPath = "code";
        }
        static public async Task fillCountriesNames(ComboBox combo)
        {
            if (countrynum is null)
                await RefreshCountry();

            combo.ItemsSource = countrynum.ToList();
            combo.SelectedValuePath = "countryId";
            combo.DisplayMemberPath = "name";
        }
        static public async Task fillCountriesLocal(ComboBox combo, int countryid, Border border)
        {
            if (citynum is null)
                await RefreshCity();
            FillCombo.citynumofcountry = FillCombo.citynum.Where(b => b.countryId == countryid).OrderBy(b => b.cityCode).ToList();
            combo.ItemsSource = FillCombo.citynumofcountry;
            combo.SelectedValuePath = "cityId";
            combo.DisplayMemberPath = "cityCode";
            if (FillCombo.citynumofcountry.Count() > 0)
                border.Visibility = Visibility.Visible;
            else
                border.Visibility = Visibility.Collapsed;
        }
        static public async Task fillCountriesLocal(ComboBox combo, int countryid)
        {
            if (citynum is null)
                await RefreshCity();
            FillCombo.citynumofcountry = FillCombo.citynum.Where(b => b.countryId == countryid).OrderBy(b => b.cityCode).ToList();
            combo.ItemsSource = FillCombo.citynumofcountry;
            combo.SelectedValuePath = "cityId";
            combo.DisplayMemberPath = "cityCode";

        }
        #endregion

        #region fill user type
        static public void fillUserType(ComboBox combo)
        {
            var typelist = new[] {
                new { Text = MainWindow.resourcemanager.GetString("trAdmin")       , Value = "ad" },
                new { Text = MainWindow.resourcemanager.GetString("trEmployee")    , Value = "us" },
                 };
            combo.DisplayMemberPath = "Text";
            combo.SelectedValuePath = "Value";
            combo.ItemsSource = typelist;

        }

        #endregion

        #region fill package month
        static public void fillPackageMonth(ComboBox combo)
        {
            var typelist = new[] {
                new { Text = MainWindow.resourcemanager.GetString("trOneMonth")    , Value = "1" },
                new { Text = MainWindow.resourcemanager.GetString("trThreeMonth")  , Value = "3" },
                new { Text = MainWindow.resourcemanager.GetString("trSixMonth")    , Value = "6" },
                new { Text = MainWindow.resourcemanager.GetString("trTwelveMonth") , Value = "12" },
                new { Text = MainWindow.resourcemanager.GetString("trUnLimited")   , Value = "0" },
                 };
            combo.DisplayMemberPath = "Text";
            combo.SelectedValuePath = "Value";
            combo.ItemsSource = typelist;

        }

        #endregion

        #region fill Agent Level
        static public void fillAgentLevel(ComboBox combo)
        {
            var typelist = new[] {
                new { Text = MainWindow.resourcemanager.GetString("trVip")       , Value = "Vip" },
                new { Text = MainWindow.resourcemanager.GetString("trNormal")   , Value = "Normal" },
                 };
            combo.DisplayMemberPath = "Text";
            combo.SelectedValuePath = "Value";
            combo.ItemsSource = typelist;

        }
        #endregion

        #region fill booked
        static public void fillBooked(ComboBox combo)
        {
            var typelist = new[] {
                new { Text = MainWindow.resourcemanager.GetString("trAll")      , Value = "all" },
                new { Text = MainWindow.resourcemanager.GetString("trBooked")   , Value = "true" },
                new { Text = MainWindow.resourcemanager.GetString("trUnBooked") , Value = "false" },
                 };
            combo.DisplayMemberPath = "Text";
            combo.SelectedValuePath = "Value";
            combo.ItemsSource = typelist;

        }

        #endregion

        #region fill server state
        static public void fillServerState(ComboBox combo)
        {
            var typelist = new[] {
                new { Text = MainWindow.resourcemanager.GetString("trOnline")    , Value = "True" },
                new { Text = MainWindow.resourcemanager.GetString("trOffline")   , Value = "False" }
                 };
            combo.DisplayMemberPath = "Text";
            combo.SelectedValuePath = "Value";
            combo.ItemsSource = typelist;

        }

        #endregion

        #region fill offline activation 
        static public void fillOfflineActivation(ComboBox combo)
        {
            var typelist = new[] {
                new { Text = MainWindow.resourcemanager.GetString("trExtend")    , Value = "rn" },
                new { Text = MainWindow.resourcemanager.GetString("trUpgrade")   , Value = "up" }
                 };
            combo.DisplayMemberPath = "Text";
            combo.SelectedValuePath = "Value";
            combo.ItemsSource = typelist;

        }

        #endregion

        #region Report
        public static string sale_copy_count = "1";

        public static string print_on_save_sale;

        public static string email_on_save_sale;

        public static string rep_printer_name;


        public static string rep_print_count = "1";
        public static string docPapersize;

        static public string getdefaultPrinters()
        {

            PrinterSettings settings = new PrinterSettings();
            string defaultPrinterName = settings.PrinterName;


            return defaultPrinterName;
        }
     
        static public string getsettingPrinter()
        {

            string printer = "";
            if (string.IsNullOrEmpty(rep_printer_name))
            {
                printer = getdefaultPrinters();
            }
            else
            {
                printer = rep_printer_name;
            }
            return printer;
        }
        public static async Task Getprintparameter()
        {

            List<SetValues> printList = new List<SetValues>();
            printList = await valueModel.GetBySetvalNote("print");
            /*
             sale_copy_count = printList.Where(X => X.name == "sale_copy_count").FirstOrDefault().value;

             pur_copy_count = printList.Where(X => X.name == "pur_copy_count").FirstOrDefault().value;
             print_on_save_pur = printList.Where(X => X.name == "print_on_save_pur").FirstOrDefault().value;
            email_on_save_pur = printList.Where(X => X.name == "email_on_save_pur").FirstOrDefault().value;

            sale_copy_count = printList.Where(X => X.name == "sale_copy_count").FirstOrDefault().value;

            pur_copy_count = printList.Where(X => X.name == "pur_copy_count").FirstOrDefault().value;

            rep_print_count = printList.Where(X => X.name == "rep_copy_count").FirstOrDefault().value;

            Allow_print_inv_count = printList.Where(X => X.name == "Allow_print_inv_count").FirstOrDefault().value;
             */
            print_on_save_sale = printList.Where(X => X.name == "print_on_save_sale").FirstOrDefault().value;
            email_on_save_sale = printList.Where(X => X.name == "email_on_save_sale").FirstOrDefault().value;
            if (print_on_save_sale == null)
            {
                print_on_save_sale = "0";
            }
            if (email_on_save_sale == null)
            {
                email_on_save_sale = "0";
            }
        }

        public static void fillcb_repname(ComboBox combo)
        {
            List<string> printersList = new List<string>();
            printersList = getsystemPrinters();
            combo.ItemsSource = printersList;
            //cb_repname.DisplayMemberPath = "name";
            //cb_repname.SelectedValuePath = "name";

            combo.SelectedValue =rep_printer_name;
        }
        static public void fillcb_docpapersize(ComboBox combo)
        {
            var sizelist = new[] {
                new { Text = "A4", Value = "A4" },
                new { Text ="A5" , Value = "A5" }
                 };
            combo.DisplayMemberPath = "Text";
            combo.SelectedValuePath = "Value";
            combo.ItemsSource = sizelist;
            combo.SelectedValue = docPapersize;

        }
        public static List<string> getsystemPrinters()
        {
            List<string> printerList = new List<string>();

            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {

                string printername = (string)PrinterSettings.InstalledPrinters[i];

                printerList.Add(printername);

            }
            return printerList;
        }
        static public void FillSideCombo(ComboBox COMBO)
        {
            #region fill deposit to combo
            var list = new[] {
  new { Text = MainWindow.resourcemanager.GetString("trAccounting")  , Value = "accounting" },
            new { Text = MainWindow.resourcemanager.GetString("trSales")  , Value = "sales" },


             };
            COMBO.DisplayMemberPath = "Text";
            COMBO.SelectedValuePath = "Value";
            COMBO.ItemsSource = list;
            #endregion

        }

        static SettingCls setModel = new SettingCls();
        static SetValues valueModel = new SetValues();
        public static List<SettingCls> settingsCls;
        public static List<SetValues> settingsValues;
        public static List<Country> regionList;
        static int nameId, addressId, emailId, mobileId, phoneId, faxId, logoId, taxId;
        public static string logoImage;
        public static string companyName;
        public static string Email;
        public static string Fax;
        public static string Mobile;
        public static string Address;
        public static string Phone;
        public static decimal syr_commission;
        public static decimal soto_commission;
        public static decimal office_syr_commission;
        public static decimal office_soto_commission;
        public static decimal company_syr_commission;
        public static decimal company_soto_commission;
        public static decimal exchangeValue;
        public static string rep_copy_count = "1";

        public static async Task<int> loading_getDefaultSystemInfo()
        {
            try
            {
                  settingsCls = await setModel.GetAll();
                 settingsValues = await valueModel.GetAll();
                SettingCls set = new SettingCls();
                SetValues setV = new SetValues();
                List<char> charsToRemove = new List<char>() { '@', '_', ',', '.', '-' };
                regionList = await MainWindow.Region.GetAllRegion();
                MainWindow.Region = regionList.Where(x => x.name == "Syria").FirstOrDefault();
                #region get company name

                //get company name
                set = settingsCls.Where(s => s.name == "com_name").FirstOrDefault<SettingCls>();
                nameId = set.settingId;
                setV = settingsValues.Where(i => i.settingId == nameId).FirstOrDefault();
                if (setV != null)
                    companyName = setV.value;


                #endregion

                #region  get company address

                //get company address
                set = settingsCls.Where(s => s.name == "com_address").FirstOrDefault<SettingCls>();
                addressId = set.settingId;
                setV = settingsValues.Where(i => i.settingId == addressId).FirstOrDefault();
                if (setV != null)
                    Address = setV.value;

                #endregion

                #region  get company email

                //get company email
                set = settingsCls.Where(s => s.name == "com_email").FirstOrDefault<SettingCls>();
                emailId = set.settingId;
                setV = settingsValues.Where(i => i.settingId == emailId).FirstOrDefault();
                if (setV != null)
                    Email = setV.value;

                #endregion

                #region  get company mobile

                //get company mobile
                set = settingsCls.Where(s => s.name == "com_mobile").FirstOrDefault<SettingCls>();
                mobileId = set.settingId;
                setV = settingsValues.Where(i => i.settingId == mobileId).FirstOrDefault();
                if (setV != null)
                {
                    charsToRemove.ForEach(x => setV.value = setV.value.Replace(x.ToString(), String.Empty));
                    Mobile = setV.value;
                }

                #endregion

                #region  get company phone

                //get company phone
                set = settingsCls.Where(s => s.name == "com_phone").FirstOrDefault<SettingCls>();
                phoneId = set.settingId;
                setV = settingsValues.Where(i => i.settingId == phoneId).FirstOrDefault();
                if (setV != null)
                {
                    charsToRemove.ForEach(x => setV.value = setV.value.Replace(x.ToString(), String.Empty));
                    Phone = setV.value;
                }

                #endregion

                #region  get company fax

                //get company fax
                set = settingsCls.Where(s => s.name == "com_fax").FirstOrDefault<SettingCls>();
                faxId = set.settingId;
                setV = settingsValues.Where(i => i.settingId == faxId).FirstOrDefault();
                if (setV != null)
                {
                    charsToRemove.ForEach(x => setV.value = setV.value.Replace(x.ToString(), String.Empty));
                    Fax = setV.value;
                }

                #endregion

                #region   get company logo
                //get company logo
                set = settingsCls.Where(s => s.name == "com_logo").FirstOrDefault<SettingCls>();
                logoId = set.settingId;
                setV = settingsValues.Where(i => i.settingId == logoId).FirstOrDefault();
                if (setV != null)
                {
                    logoImage = setV.value;
                    //await setV.getImg(logoImage);
                }


                #endregion
                #region  get accuracy

                //get company fax
                set = settingsCls.Where(s => s.name == "accuracy").FirstOrDefault<SettingCls>();
                var accuId = set.settingId;
                setV = settingsValues.Where(i => i.settingId == accuId).FirstOrDefault();
                if (setV != null)
                {

                    MainWindow.accuracy = setV.value;
                }

                #endregion
                #region  get date

                //get company fax
                set = settingsCls.Where(s => s.name == "dateForm").FirstOrDefault<SettingCls>();
                var dateId = set.settingId;
                setV = settingsValues.Where(i => i.settingId == dateId).FirstOrDefault();
                if (setV != null)
                {

                    MainWindow.dateFormat = setV.value;
                }

                #endregion
                #region  get commetion

                //get syr_commission
                set = settingsCls.Where(s => s.name == "syr_commission").FirstOrDefault<SettingCls>();
                // var commesionId = set.settingId;
                setV = settingsValues.Where(i => i.settingId == set.settingId).FirstOrDefault();
                if (setV != null)
                {
                    if (setV.value != null && setV.value != "")
                    {
                        syr_commission = Convert.ToDecimal(setV.value);
                    }
                    else
                    {
                        syr_commission = 0;
                    }

                }
                //soto_commission
                set = settingsCls.Where(s => s.name == "soto_commission").FirstOrDefault<SettingCls>();

                setV = settingsValues.Where(i => i.settingId == set.settingId).FirstOrDefault();
                if (setV != null)
                {
                    if (setV.value != null && setV.value != "")
                    {
                        soto_commission = Convert.ToDecimal(setV.value);
                    }
                    else
                    {
                        soto_commission = 0;
                    }

                }
                //office_syr_commission
                set = settingsCls.Where(s => s.name == "office_syr_commission").FirstOrDefault<SettingCls>();

                setV = settingsValues.Where(i => i.settingId == set.settingId).FirstOrDefault();
                if (setV != null)
                {
                    if (setV.value != null && setV.value != "")
                    {
                        office_syr_commission = Convert.ToDecimal(setV.value);
                    }
                    else
                    {
                        office_syr_commission = 0;
                    }

                }
                //office_soto_commission
                set = settingsCls.Where(s => s.name == "office_soto_commission").FirstOrDefault<SettingCls>();

                setV = settingsValues.Where(i => i.settingId == set.settingId).FirstOrDefault();
                if (setV != null)
                {
                    if (setV.value != null && setV.value != "")
                    {
                        office_soto_commission = Convert.ToDecimal(setV.value);
                    }
                    else
                    {
                        office_soto_commission = 0;
                    }

                }
                //company_syr_commission
                set = settingsCls.Where(s => s.name == "company_syr_commission").FirstOrDefault<SettingCls>();

                setV = settingsValues.Where(i => i.settingId == set.settingId).FirstOrDefault();
                if (setV != null)
                {
                    if (setV.value != null && setV.value != "")
                    {
                        company_syr_commission = Convert.ToDecimal(setV.value);
                    }
                    else
                    {
                        company_syr_commission = 0;
                    }

                }
                //company_soto_commission
                set = settingsCls.Where(s => s.name == "company_soto_commission").FirstOrDefault<SettingCls>();

                setV = settingsValues.Where(i => i.settingId == set.settingId).FirstOrDefault();
                if (setV != null)
                {
                    if (setV.value != null && setV.value != "")
                    {
                        company_soto_commission = Convert.ToDecimal(setV.value);
                    }
                    else
                    {
                        company_soto_commission = 0;
                    }

                }
                //

                #endregion
                 
                #region getExchange
               await getExchange();
                #endregion
                #region  get docPapersize

                //get company fax
                set = settingsCls.Where(s => s.name == "docPapersize").FirstOrDefault<SettingCls>();
               var docId = set.settingId;
                setV = settingsValues.Where(i => i.settingId == docId).FirstOrDefault();
                if (setV != null)
                {
                    if (string.IsNullOrEmpty(setV.value))
                    {
                        docPapersize = "A4";
                    }
                    else
                    {
                        docPapersize = setV.value;
                    }
                  
                }
                else
                {
                    docPapersize = "A4";
                }

                #endregion
                #region get repcopy count

                //get company name
                set = settingsCls.Where(s => s.name == "rep_copy_count").FirstOrDefault<SettingCls>();
                nameId = set.settingId;
                setV = settingsValues.Where(i => i.settingId == nameId).FirstOrDefault();
                if (setV != null)
                {
                    if (!string.IsNullOrEmpty(setV.value))
                    {
                        rep_copy_count = setV.value;
                    }
                    else
                    {
                        rep_copy_count = "1";
                    }

                }
                else
                {
                    rep_copy_count = "1";
                }
                #endregion

                #region get rep_printer_name

                //get company name
                set = settingsCls.Where(s => s.name == "rep_printer_name").FirstOrDefault<SettingCls>();
                nameId = set.settingId;
                setV = settingsValues.Where(i => i.settingId == nameId).FirstOrDefault();
                if (setV != null)
                {
                    if (!string.IsNullOrEmpty(setV.value))
                    {
                        rep_printer_name = setV.value;
                    }
                    else
                    {
                        rep_printer_name = getdefaultPrinters();
                    }

                }
                else
                {
                    rep_printer_name = getdefaultPrinters();
                }
                #endregion
                return 1;
            }
            catch (Exception)
            { return 0; }
            //foreach (var item in loadingList)
            //{
            //    if (item.key.Equals("loading_getDefaultSystemInfo"))
            //    {
            //        item.value = true;
            //        break;
            //    }
            //}

        }

        public static string AgentNameConv(Users userModel)
        {
            if (userModel.userId == 3)
                return "Supclouds";
            else
                return userModel.name + " " + userModel.lastName;
        }
        public static string DateConvert(DateTime? date)
        {
            DateTime datetemp;

            if (date is DateTime)
                date = (DateTime)date;
            else return date.ToString();

            datetemp = DateTime.Parse(date.ToString());
            switch (MainWindow.dateFormat)
            {
                case "ShortDatePattern":
                    return datetemp.ToString(@"dd/MM/yyyy");
                case "LongDatePattern":
                    return datetemp.ToString(@"dddd, MMMM d, yyyy");
                case "MonthDayPattern":
                    return datetemp.ToString(@"MMMM dd");
                case "YearMonthPattern":
                    return datetemp.ToString(@"MMMM yyyy");
                default:
                    return datetemp.ToString(@"dd/MM/yyyy");
            }

        }


        public static string serverActiveConv(int? isActive)
        {
            string value = "";
            if (isActive == 1)
                value = MainWindow.resourcemanagerreport.GetString("trValid");
            else
                value = MainWindow.resourcemanagerreport.GetString("trInValid");

            return value;
        }
        /*
        public static string PeriodConv(CountryPackageDate CountryPackageDateModel)
        {
            string period = "";
            int monthCount = int.Parse(CountryPackageDateModel.monthCount.ToString());
            bool islimitDate = bool.Parse(CountryPackageDateModel.islimitDate.ToString());
            if (!islimitDate)
                period = MainWindow.resourcemanager.GetString("trUnLimited");
            else
            {
                switch (monthCount)
                {
                    case 1: period = MainWindow.resourcemanagerreport.GetString("trOneMonth"); break;
                    case 3: period = MainWindow.resourcemanagerreport.GetString("trThreeMonth"); break;
                    case 6: period = MainWindow.resourcemanagerreport.GetString("trSixMonth"); break;
                    case 12: period = MainWindow.resourcemanagerreport.GetString("trTwelveMonth"); break;
                }
            }
            return period;
        }



        public static string serverActiveationTypeConv(bool? isOnlineServer)
        {
            string value = "";
            if (isOnlineServer == true)
                value = MainWindow.resourcemanagerreport.GetString("trOnline");
            else
                value = MainWindow.resourcemanagerreport.GetString("trOffline");

            return value;
        }
        */
        public static string UnlimitedConvert(int count)
        {
            string value = "";
            if (count == -1)
                value = MainWindow.resourcemanagerreport.GetString("trUnLimited");
            else
                value = count.ToString();
            return value;
        }
        #endregion
        static FlightTable FlightTableModel = new FlightTable();
        static IEnumerable<FlightTable> FlightTableList;
        static public async Task fillFlightTable(ComboBox combo)
        {
            FlightTableList = await FlightTableModel.GetAll();
            combo.ItemsSource = FlightTableList;
            combo.SelectedValuePath = "flightTableId";
            combo.DisplayMemberPath = "name";
            combo.SelectedIndex = -1;


        }
        static PaySides PaySidesModel = new PaySides();
        public static IEnumerable<PaySides> PaySidesList;
        static public async Task fillpaySide(ComboBox combo, string type)
        {
            PaySidesList = await PaySidesModel.GetAll();
            PaySidesList = PaySidesList.Where(s => s.notes.Contains(type)&& s.isActive==true).ToList();
            combo.ItemsSource = PaySidesList;
            // combo.SelectedValuePath = "paysideId";
            combo.SelectedValuePath = "code";
            combo.DisplayMemberPath = "sideAr";
            combo.SelectedIndex = -1;


        }
        
        public static IEnumerable<PaySides> PaySidesSysList;
        static public async Task fillpaySideSys(ComboBox combo, string type)
        {
            PaySidesSysList = await PaySidesModel.GetAll();
            PaySidesSysList = PaySidesList.Where(s => s.notes.Contains(type) && s.isActive == true && (s.code=="syr"|| s.code == "soto")).ToList();
            combo.ItemsSource = PaySidesSysList;
            // combo.SelectedValuePath = "paysideId";
            combo.SelectedValuePath = "code";
            combo.DisplayMemberPath = "sideAr";
           // combo.SelectedIndex = -1;


        }
        public static IEnumerable<PaySides> PaySidesRepList;
        static public async Task fillpaySidereport(ComboBox combo)
        {
            PaySidesRepList = await PaySidesModel.GetAll();
            PaySidesRepList = PaySidesRepList.Where(s => s.code!="syr" && s.code!="soto").ToList();
            combo.ItemsSource = PaySidesRepList;
            // combo.SelectedValuePath = "paysideId";
            combo.SelectedValuePath = "code";
            combo.DisplayMemberPath = "sideAr";
            combo.SelectedIndex = -1;
        }
        static FromTable FromTableModel = new FromTable();
        static IEnumerable<FromTable> FromTableList;
        static public async Task fillFromTable(ComboBox combo)
        {
            FromTableList = await FromTableModel.GetAll();
            combo.ItemsSource = FromTableList;
            combo.SelectedValuePath = "fromTableId";
            combo.DisplayMemberPath = "name";
            combo.SelectedIndex = -1;


        }
        static ToTable ToTableModel = new ToTable();
        static IEnumerable<ToTable> ToTableList;
        static public async Task fillToTable(ComboBox combo)
        {
            ToTableList = await ToTableModel.GetAll();
            combo.ItemsSource = ToTableList;
            combo.SelectedValuePath = "toTableId";
            combo.DisplayMemberPath = "name";
            combo.SelectedIndex = -1;


        }
        static DurationsTable DurationsTableModel = new DurationsTable();
        static IEnumerable<DurationsTable> DurationsTableList;
        static public async Task fillDurationsTable(ComboBox combo)
        {
            DurationsTableList = await DurationsTableModel.GetAll();
            combo.ItemsSource = DurationsTableList;
            combo.SelectedValuePath = "durationId";
            combo.DisplayMemberPath = "name";
            combo.SelectedIndex = -1;


        }
        static StatementsTable StatementsTableModel = new StatementsTable();
        static IEnumerable<StatementsTable> StatementsTableList;
        static public async Task fillStatementsTable(ComboBox combo)
        {
            StatementsTableList = await StatementsTableModel.GetAll();
            combo.ItemsSource = StatementsTableList;
            combo.SelectedValuePath = "opStatementId";
            combo.DisplayMemberPath = "name";
            combo.SelectedIndex = -1;


        }
        static Passengers PassengerModel = new Passengers();
        static IEnumerable<Passengers> PassengersList;
        static public async Task fillPassengers(ComboBox combo)
        {
            PassengersList = await PassengerModel.GetAll();
            combo.ItemsSource = PassengersList;
            combo.SelectedValuePath = "passengerId";
            combo.DisplayMemberPath = "fullName";
            combo.SelectedIndex = -1;


        }
        static Flights FlightsModel = new Flights();
        static IEnumerable<Flights> FlightsList;
        static public async Task fillFlights(ComboBox combo)
        {
            FlightsList = await FlightsModel.GetAll();
            combo.ItemsSource = FlightsList;
            combo.SelectedValuePath = "flightId";
            combo.DisplayMemberPath = "airlineflightTable";
            combo.SelectedIndex = -1;


        }
        static public async Task fillFlightsBySysId(ComboBox combo,int systemId)
        {
            FlightsList = await FlightsModel.GetbySysId(systemId);
            combo.ItemsSource = FlightsList;
            combo.SelectedValuePath = "flightId";
            combo.DisplayMemberPath = "airlineflightTable";
            combo.SelectedIndex = -1;


        }
        static Office OfficeModel = new Office();
        static IEnumerable<Office> OfficeList;
        static public async Task fillOffice(ComboBox combo)
        {
            OfficeList = await OfficeModel.GetAll();
            combo.ItemsSource = OfficeList;
            combo.SelectedValuePath = "officeId";
            combo.DisplayMemberPath = "name";
            combo.SelectedIndex = -1;


        }
        static Systems SystemsModel = new Systems();
        public static IEnumerable<Systems> SystemsList;
        static public async Task fillSystems(ComboBox combo)
        {
            SystemsList = await SystemsModel.GetAll();
            combo.ItemsSource = SystemsList;
            combo.SelectedValuePath = "systemId";
            combo.DisplayMemberPath = "name";
            combo.SelectedIndex = -1;


        }
   
        public static Exchange ExchangeModel = new Exchange();
        static public async Task getExchange()
        {
            ExchangeModel = await ExchangeModel.Getlast();
            if (ExchangeModel is null)
            {
                ExchangeModel = new Exchange();
                exchangeValue = 0;
            }
            else
            {
                exchangeValue = (decimal)ExchangeModel.syValue;
            }
        }
    }
}
