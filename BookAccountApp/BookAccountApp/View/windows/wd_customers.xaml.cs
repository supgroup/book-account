using BookAccountApp.ApiClasses;
using BookAccountApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookAccountApp.View.windows
{
    /// <summary>
    /// Interaction logic for wd_customers.xaml
    /// </summary>
    public partial class wd_customers : Window
    {
        List<Customers> allCustomersSource = new List<Customers>();
        List<Customers> allCustomers = new List<Customers>();
        List<Customers> selectedCustomersSource = new List<Customers>();
        IEnumerable<Customers> customerQuery;
        List<AgentCustomer> selectedCustomers = new List<AgentCustomer>();

        Customers customerModel = new Customers();
        AgentCustomer agentCustomerModel = new AgentCustomer();

        Customers customer = new Customers();

        public int agentID { get; set; }

        string txtSearch = "";

        public wd_customers()
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
                HelpClass.StartAwait(grid_customers);

                #region translate
                if (MainWindow.lang.Equals("en"))
                {
                    MainWindow.resourcemanager = new ResourceManager("BookAccountApp.en_file", Assembly.GetExecutingAssembly());
                    grid_customers.FlowDirection = FlowDirection.LeftToRight;
                }
                else
                {
                    MainWindow.resourcemanager = new ResourceManager("BookAccountApp.ar_file", Assembly.GetExecutingAssembly());
                    grid_customers.FlowDirection = FlowDirection.RightToLeft;
                }

                translat();
            #endregion

                allCustomersSource = await customerModel.GetByAgentCountryId(agentID);
                selectedCustomersSource = await customerModel.GetCustomersByAgent(agentID);

                allCustomers.AddRange(allCustomersSource.Where(c => c.isActive == 1));

                //remove selected customers from all customers
                foreach (var i in selectedCustomersSource)
                {
                    customer = allCustomers.Where(c => c.custId == i.custId).FirstOrDefault();
                    allCustomers.Remove(customer);
                }

                dg_allCustomers.ItemsSource = allCustomers;

                dg_selectedCustomers.ItemsSource = selectedCustomersSource;

                HelpClass.EndAwait(grid_customers);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_customers);
                HelpClass.ExceptionMessage(ex, this);
            }

        }

        #region methods
        private void translat()
        {
            MaterialDesignThemes.Wpf.HintAssist.SetHint(txb_searchCustomers, MainWindow.resourcemanager.GetString("trSearchHint"));

            btn_save.Content = MainWindow.resourcemanager.GetString("trSave");

            dg_allCustomers.Columns[0].Header = MainWindow.resourcemanager.GetString("trName");
            dg_selectedCustomers.Columns[0].Header = MainWindow.resourcemanager.GetString("trName");

            txt_title.Text = MainWindow.resourcemanager.GetString("trAgentCustomer");
            txt_all.Text = MainWindow.resourcemanager.GetString("trAllCustomers");
            txt_selected.Text = MainWindow.resourcemanager.GetString("trAgentCustomer");

            tt_search.Content = MainWindow.resourcemanager.GetString("trSearch");
            tt_selectAll.Content = MainWindow.resourcemanager.GetString("trSelectAllItems");
            tt_unselectAll.Content = MainWindow.resourcemanager.GetString("trUnSelectAllItems");
            tt_selectOne.Content = MainWindow.resourcemanager.GetString("trSelectOneItem");
            tt_unselectOne.Content = MainWindow.resourcemanager.GetString("trUnSelectOneItem");
            tt_close.Content = MainWindow.resourcemanager.GetString("trClose");
        }
        #endregion

        #region events
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception ex)
            {
                //SectionData.ExceptionMessage(ex, this);
            }
        }

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
        {
            GC.Collect();
        }

        private void Btn_colse_Click(object sender, RoutedEventArgs e)
        {//close
            //isActive = false;
            this.Close();
        }
      
        private void Dg_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            //// Have to do this in the unusual case where the border of the cell gets selected.
            //// and causes a crash 'EditItem is not allowed'
            //e.Cancel = true;
        }

        #endregion

        #region select
        private void Dg_allCustomers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Btn_selectOne_Click(null, null);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_selectedAll_Click(object sender, RoutedEventArgs e)
        {//select all
            try
            {
                int x = allCustomers.Count;
                for (int i = 0; i < x; i++)
                {
                    dg_allCustomers.SelectedIndex = 0;
                    Btn_selectOne_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_selectOne_Click(object sender, RoutedEventArgs e)
        {//select one
            try
            {
                customer = dg_allCustomers.SelectedItem as Customers;
                if (customer != null)
                {
                    allCustomers.Remove(customer);
                    selectedCustomersSource.Add(customer);

                    dg_allCustomers.ItemsSource = allCustomers;
                    dg_selectedCustomers.ItemsSource = selectedCustomersSource;

                    dg_allCustomers.Items.Refresh();
                    dg_selectedCustomers.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_unSelectedOne_Click(object sender, RoutedEventArgs e)
        {//unselect one
            try
            {
                customer = dg_selectedCustomers.SelectedItem as Customers;
                if (customer != null)
                {
                    allCustomers.Add(customer);

                    selectedCustomersSource.Remove(customer);

                    dg_allCustomers.ItemsSource = allCustomers;
                    dg_selectedCustomers.ItemsSource = selectedCustomersSource;

                    dg_allCustomers.Items.Refresh();
                    dg_selectedCustomers.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_unSelectAll_Click(object sender, RoutedEventArgs e)
        {//unselect all
            try
            {
                int x = selectedCustomersSource.Count;
                for (int i = 0; i < x; i++)
                {
                    dg_selectedCustomers.SelectedIndex = 0;
                    Btn_unSelectedOne_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Dg_selectedPackages_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Btn_unSelectedOne_Click(null, null);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        #endregion

        #region save   search
        private async void Btn_save_Click(object sender, RoutedEventArgs e)
        {//save
            try
            {
                HelpClass.StartAwait(grid_customers);

                foreach (var p in selectedCustomersSource)
                {
                    AgentCustomer ac = new AgentCustomer();
                    ac.agentCustomerId = 0;
                    ac.agentId = agentID;
                    ac.customerId = p.custId;
                    ac.createUserId = MainWindow.userID;
                    selectedCustomers.Add(ac);
                }

                decimal s = await agentCustomerModel.saveNewList(selectedCustomers, agentID);

                //isActive = true;
                this.Close();

                HelpClass.EndAwait(grid_customers);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_customers);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Txb_searchCustomers_TextChanged(object sender, TextChangedEventArgs e)
        {//search
            try
            {
                txtSearch = txb_searchCustomers.Text;

                customerQuery = allCustomers.Where(s => s.custname.Contains(txtSearch) ||
                                                        s.lastName.Contains(txtSearch) ||
                                                        s.custCode.Contains(txtSearch));
                dg_allCustomers.ItemsSource = customerQuery;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        #endregion
    }
}
