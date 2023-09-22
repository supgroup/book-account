using BookAccountApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
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
using BookAccountApp.ApiClasses;
namespace BookAccountApp.View.windows
{
    /// <summary>
    /// Interaction logic for wd_invoicesList.xaml
    /// </summary>
    public partial class wd_invoicesList : Window
    {
        public wd_invoicesList()
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

        #region variables
      
        public bool isActive;
        List<ServiceData> allInvoicesSource = new List<ServiceData>();
        List<ServiceData> allInvoices = new List<ServiceData>();
        public List<ServiceData> selectedInvoices = new List<ServiceData>();
        ServiceData invoiceModel = new ServiceData();
        ServiceData invoice = new ServiceData();

        List<PayOp> allCashtransfersSource = new List<PayOp>();
        List<PayOp> allCashtransfers = new List<PayOp>();
        public List<PayOp> selectedCashtansfers = new List<PayOp>();
        PayOp cashTransfer = new PayOp();
        public int selectedId = 0;
        public string opType = "";
        public string side = "";
        //  public int agentId = 0, userId = 0, shippingCompanyId = 0;
        public decimal sum = 0;
        //public string invType;
       
        #endregion

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {

                #region translate
                //if (AppSettings.lang.Equals("en"))
                //{
                //    MainWindow.resourcemanager = new ResourceManager("BookAccountApp.en_file", Assembly.GetExecutingAssembly());
                //    grid_main.FlowDirection = FlowDirection.LeftToRight;
                //}
                //else
                //{
                //    MainWindow.resourcemanager = new ResourceManager("BookAccountApp.ar_file", Assembly.GetExecutingAssembly());
                grid_main.FlowDirection = FlowDirection.RightToLeft;
                //}
                translate();
                #endregion
                if (side=="office" && opType=="p")
                {
                    allCashtransfersSource = await cashTransfer.GetUnpaidOffice(selectedId);
                }
                else if (side == "system" && opType == "d")
                {
                    allCashtransfersSource = await cashTransfer.GetUnpaidSystem(selectedId);
                }
                else if (side == "passenger" && opType == "d")
                {
                    allCashtransfersSource = await cashTransfer.GetUnpaidPassenger(selectedId);
                }
                else if (side == "office" && opType == "d")
                {
                    allCashtransfersSource = await cashTransfer.GetUnpaidBookOffice(selectedId);
                }
                allCashtransfers.AddRange(allCashtransfersSource);
                //if (side == "office" && opType == "p")
                //{
                    lst_allInvoices.Columns[0].Visibility = Visibility.Collapsed;
                    lst_allInvoices.Columns[1].Visibility = Visibility.Collapsed;
                    lst_allInvoices.Columns[2].Visibility = Visibility.Visible;
                    lst_allInvoices.Columns[3].Visibility = Visibility.Visible;

                    lst_selectedInvoices.Columns[0].Visibility = Visibility.Collapsed;
                    lst_selectedInvoices.Columns[1].Visibility = Visibility.Collapsed;
                    lst_selectedInvoices.Columns[2].Visibility = Visibility.Visible;
                    lst_selectedInvoices.Columns[3].Visibility = Visibility.Visible;

                    lst_allInvoices.ItemsSource = allCashtransfers;
                    lst_allInvoices.SelectedValuePath = "payOpId";
                    lst_allInvoices.DisplayMemberPath = "serviceNum";

                    lst_selectedInvoices.ItemsSource = selectedCashtansfers;
                    lst_selectedInvoices.SelectedValuePath = "payOpId";
                    lst_selectedInvoices.DisplayMemberPath = "serviceNum";
                //}
                //else if (side == "system" && opType == "d")
                //{
                    
                //}
                /*
                if (agentId != 0)
                    allInvoicesSource = await invoiceModel.getAgentInvoices(MainWindow.branchID.Value, agentId, invType);
                else if (userId != 0 && invType.Equals("pay"))
                    allCashtransfersSource = await cashTransfer.getNotPaidAgentCommission(userId);
                else if (userId != 0 && invType.Equals("feed"))
                    allCashtransfersSource = await cashTransfer.getNotPaidShortage(userId);
                else if (shippingCompanyId != 0 && invType.Equals("feed"))
                    allInvoicesSource = await invoiceModel.getShipCompanyInvoices(shippingCompanyId, invType);
                else if (shippingCompanyId != 0 && invType.Equals("pay"))
                    //allInvoicesSource = await invoiceModel.getNotPaidComInvoices( shippingCompanyId );
                    allCashtransfersSource = await cashTransfer.getNotPaidDeliverCashes(shippingCompanyId);

                allInvoices.AddRange(allInvoicesSource);
                allCashtransfers.AddRange(allCashtransfersSource);

                #region fill lists
                if (agentId != 0 || (shippingCompanyId != 0 && invType.Equals("feed")))
                {

                    lst_allInvoices.Columns[0].Visibility = Visibility.Visible;
                    lst_allInvoices.Columns[1].Visibility = Visibility.Visible;
                    lst_allInvoices.Columns[2].Visibility = Visibility.Collapsed;
                    lst_allInvoices.Columns[3].Visibility = Visibility.Collapsed;

                    lst_selectedInvoices.Columns[0].Visibility = Visibility.Visible;
                    lst_selectedInvoices.Columns[1].Visibility = Visibility.Visible;
                    lst_selectedInvoices.Columns[2].Visibility = Visibility.Collapsed;
                    lst_selectedInvoices.Columns[3].Visibility = Visibility.Collapsed;

                    lst_allInvoices.ItemsSource = allInvoices;
                    lst_allInvoices.SelectedValuePath = "invNumber";
                    lst_allInvoices.DisplayMemberPath = "invoiceId";

                    lst_selectedInvoices.ItemsSource = selectedInvoices;
                    lst_selectedInvoices.SelectedValuePath = "invNumber";
                    lst_selectedInvoices.DisplayMemberPath = "invoiceId";
                }
                else if (userId != 0 || (shippingCompanyId != 0 && invType.Equals("pay")))
                {
                    lst_allInvoices.Columns[0].Visibility = Visibility.Collapsed;
                    lst_allInvoices.Columns[1].Visibility = Visibility.Collapsed;
                    lst_allInvoices.Columns[2].Visibility = Visibility.Visible;
                    lst_allInvoices.Columns[3].Visibility = Visibility.Visible;

                    lst_selectedInvoices.Columns[0].Visibility = Visibility.Collapsed;
                    lst_selectedInvoices.Columns[1].Visibility = Visibility.Collapsed;
                    lst_selectedInvoices.Columns[2].Visibility = Visibility.Visible;
                    lst_selectedInvoices.Columns[3].Visibility = Visibility.Visible;

                    lst_allInvoices.ItemsSource = allCashtransfers;
                    lst_allInvoices.SelectedValuePath = "cashTransId";
                    lst_allInvoices.DisplayMemberPath = "transNum";

                    lst_selectedInvoices.ItemsSource = selectedCashtansfers;
                    lst_selectedInvoices.SelectedValuePath = "cashTransId";
                    lst_selectedInvoices.DisplayMemberPath = "transNum";
                }
                #endregion
                */
            }
            catch (Exception ex)
            {  
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void translate()
        {
            //   trBookNum officeCommission     theCommission  
            MaterialDesignThemes.Wpf.HintAssist.SetHint(txb_search, MainWindow.resourcemanager.GetString("trSearchHint"));
            btn_save.Content = MainWindow.resourcemanager.GetString("trSave");
            txt_sum.Text = MainWindow.resourcemanager.GetString("trSum");
            tt_selectAllItem.Content = MainWindow.resourcemanager.GetString("trSelectAllItems");
            tt_unselectAllItem.Content = MainWindow.resourcemanager.GetString("trUnSelectAllItems");
            tt_selectItem.Content = MainWindow.resourcemanager.GetString("trSelectOneItem");
            tt_unselectItem.Content = MainWindow.resourcemanager.GetString("trUnSelectOneItem");
            if (side == "office" && opType == "p")
            {
            
                txt_invoice.Text = MainWindow.resourcemanager.GetString("oneofficeCommissions");
              
                txt_invoices.Text = MainWindow.resourcemanager.GetString("Commissions");

                txt_selectedInvoices.Text = MainWindow.resourcemanager.GetString("selectedCommissions");

                lst_allInvoices.Columns[2].Header = MainWindow.resourcemanager.GetString("trBookNum");
                lst_allInvoices.Columns[3].Header = MainWindow.resourcemanager.GetString("theCommission");
                lst_selectedInvoices.Columns[2].Header = MainWindow.resourcemanager.GetString("trBookNum");
                lst_selectedInvoices.Columns[3].Header = MainWindow.resourcemanager.GetString("theCommission");             
            }
            else if (side == "system" && opType == "d")
            {
                txt_selectedInvoices.Text = MainWindow.resourcemanager.GetString("selectedCommissions");
                txt_invoice.Text = MainWindow.resourcemanager.GetString("companyCommissions");
                txt_invoices.Text = MainWindow.resourcemanager.GetString("Commissions");
               
                lst_allInvoices.Columns[2].Header = MainWindow.resourcemanager.GetString("trBookNum");
                lst_allInvoices.Columns[3].Header = MainWindow.resourcemanager.GetString("theCommission");
                lst_selectedInvoices.Columns[2].Header = MainWindow.resourcemanager.GetString("trBookNum");
                lst_selectedInvoices.Columns[3].Header = MainWindow.resourcemanager.GetString("theCommission");
            }
            else if (side == "passenger" && opType == "d")
            {
                //books
                //selectedBooks
                //recieveDeserved
                txt_selectedInvoices.Text = MainWindow.resourcemanager.GetString("selectedBooks");
                txt_invoice.Text = MainWindow.resourcemanager.GetString("recieveDeserved");
                txt_invoices.Text = MainWindow.resourcemanager.GetString("books");

                lst_allInvoices.Columns[2].Header = MainWindow.resourcemanager.GetString("trBookNum");
                lst_allInvoices.Columns[3].Header = MainWindow.resourcemanager.GetString("total");
                lst_selectedInvoices.Columns[2].Header = MainWindow.resourcemanager.GetString("trBookNum");
                lst_selectedInvoices.Columns[3].Header = MainWindow.resourcemanager.GetString("total");
            }
            else if (side == "office" && opType == "d")
            {
                //books
                //selectedBooks
                //recieveDeserved
                txt_selectedInvoices.Text = MainWindow.resourcemanager.GetString("selectedBooks");
                txt_invoice.Text = MainWindow.resourcemanager.GetString("recieveDeserved");
                txt_invoices.Text = MainWindow.resourcemanager.GetString("books");

                lst_allInvoices.Columns[2].Header = MainWindow.resourcemanager.GetString("trBookNum");
                lst_allInvoices.Columns[3].Header = MainWindow.resourcemanager.GetString("total");
                lst_selectedInvoices.Columns[2].Header = MainWindow.resourcemanager.GetString("trBookNum");
                lst_selectedInvoices.Columns[3].Header = MainWindow.resourcemanager.GetString("total");
            }
            /*
            MaterialDesignThemes.Wpf.HintAssist.SetHint(txb_search, MainWindow.resourcemanager.GetString("trSearchHint"));
            txt_invoice.Text = MainWindow.resourcemanager.GetString("trInvoices");
            btn_save.Content = MainWindow.resourcemanager.GetString("trSave");
            txt_invoices.Text = MainWindow.resourcemanager.GetString("trInvoices");
            txt_selectedInvoices.Text = MainWindow.resourcemanager.GetString("trSelectedInvoices");

            lst_allInvoices.Columns[0].Header = MainWindow.resourcemanager.GetString("trInvoiceNumber");
            lst_allInvoices.Columns[1].Header = MainWindow.resourcemanager.GetString("trTotal");
            lst_allInvoices.Columns[2].Header = MainWindow.resourcemanager.GetString("trProcessNumTooltip");
            lst_allInvoices.Columns[3].Header = MainWindow.resourcemanager.GetString("trTotal");

            lst_selectedInvoices.Columns[0].Header = MainWindow.resourcemanager.GetString("trInvoiceNumber");
            lst_selectedInvoices.Columns[1].Header = MainWindow.resourcemanager.GetString("trTotal");
            lst_selectedInvoices.Columns[0].Header = MainWindow.resourcemanager.GetString("trProcessNumTooltip");
            lst_selectedInvoices.Columns[1].Header = MainWindow.resourcemanager.GetString("trTotal");

            txt_sum.Text = MainWindow.resourcemanager.GetString("trSum");
            tb_moneyIcon.Text = AppSettings.Currency;

            tt_selectAllItem.Content = MainWindow.resourcemanager.GetString("trSelectAllItems");
            tt_unselectAllItem.Content = MainWindow.resourcemanager.GetString("trUnSelectAllItems");
            tt_selectItem.Content = MainWindow.resourcemanager.GetString("trSelectOneItem");
            tt_unselectItem.Content = MainWindow.resourcemanager.GetString("trUnSelectOneItem");
            */
        }
        #region events
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
        private void Btn_colse_Click(object sender, RoutedEventArgs e)
        {
           
            isActive = false;
            this.Close();
             
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch //(Exception ex)
            {
                //HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Grid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            //// Have to do this in the unusual case where the border of the cell gets selected.
            //// and causes a crash 'EditItem is not allowed'
            e.Cancel = true;
        }
        private void Txb_search_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            try
            {

                lst_allInvoices.ItemsSource = allCashtransfers.Where(x => (x.serviceNum.ToLower().Contains(txb_search.Text.ToLower())) ||
                                                                     (x.cash.ToString().ToLower().Contains(txb_search.Text.ToLower())));
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
            
        }
        #endregion
        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {//save
             
            isActive = true;
            this.Close();
                  
        }
        #region selection
        private void Lst_allInvoices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Btn_selectedInvoice_Click(null, null);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Lst_selectedInvoices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Btn_unSelectedInvoice_Click(null, null);
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
                //if (side == "office")
                //{
                    int x = allCashtransfers.Count;
                    for (int i = 0; i < x; i++)
                    {
                        lst_allInvoices.SelectedIndex = 0;
                        Btn_selectedInvoice_Click(null, null);
                    }
                //}
                /*
                    if (agentId != 0 || (shippingCompanyId != 0 && invType.Equals("feed")))
                {
                    int x = allInvoices.Count;
                    for (int i = 0; i < x; i++)
                    {
                        lst_allInvoices.SelectedIndex = 0;
                        Btn_selectedInvoice_Click(null, null);
                    }
                }
                else if (userId != 0 || (shippingCompanyId != 0 && invType.Equals("pay")))
                {
                    int x = allCashtransfers.Count;
                    for (int i = 0; i < x; i++)
                    {
                        lst_allInvoices.SelectedIndex = 0;
                        Btn_selectedInvoice_Click(null, null);
                    }
                }
                */
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
            
        }
        private void Btn_selectedInvoice_Click(object sender, RoutedEventArgs e)
        {//select one
            try
            {
                 decimal x = 0;
                //if (side == "office"){
                    cashTransfer = lst_allInvoices.SelectedItem as PayOp;
                    if (cashTransfer != null)
                    {
                        allCashtransfers.Remove(cashTransfer);

                        selectedCashtansfers.Add(cashTransfer);

                        lst_allInvoices.ItemsSource = allCashtransfers;
                        lst_selectedInvoices.ItemsSource = selectedCashtansfers;

                        lst_allInvoices.Items.Refresh();
                        lst_selectedInvoices.Items.Refresh();

                        x = cashTransfer.deserved.Value;
                    }
                //}
                /*
                if (agentId != 0 || (shippingCompanyId != 0 && invType.Equals("feed")))
                {
                    invoice = lst_allInvoices.SelectedItem as ServiceData;
                    if (invoice != null)
                    {
                        allInvoices.Remove(invoice);

                        selectedInvoices.Add(invoice);

                        lst_allInvoices.ItemsSource = allInvoices;
                        lst_selectedInvoices.ItemsSource = selectedInvoices;

                        lst_allInvoices.Items.Refresh();
                        lst_selectedInvoices.Items.Refresh();

                        x = invoice.deserved.Value;
                    }
                }
                else if (userId != 0 || (shippingCompanyId != 0 && invType.Equals("pay")))
                {
                    cashTransfer = lst_allInvoices.SelectedItem as PayOp;
                    if (cashTransfer != null)
                    {
                        allCashtransfers.Remove(cashTransfer);

                        selectedCashtansfers.Add(cashTransfer);

                        lst_allInvoices.ItemsSource = allCashtransfers;
                        lst_selectedInvoices.ItemsSource = selectedCashtansfers;

                        lst_allInvoices.Items.Refresh();
                        lst_selectedInvoices.Items.Refresh();

                        x = cashTransfer.cash.Value;
                    }
                }
                */
                sum += x;

                tb_sum.Text = " " + HelpClass.DecTostring(sum) + " ";

              
                    
            }
            catch (Exception ex)
            {
               
                    
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Btn_unSelectedInvoice_Click(object sender, RoutedEventArgs e)
        {//unselect one
          
            try
            {
                decimal x = 0;
                //if (side == "office")
                //{
                    cashTransfer = lst_selectedInvoices.SelectedItem as PayOp;

                    if (cashTransfer != null)
                    {
                        selectedCashtansfers.Remove(cashTransfer);

                        allCashtransfers.Add(cashTransfer);

                        lst_allInvoices.ItemsSource = allCashtransfers;
                        lst_selectedInvoices.ItemsSource = selectedCashtansfers;

                        lst_allInvoices.Items.Refresh();
                        lst_selectedInvoices.Items.Refresh();

                        x = cashTransfer.deserved.Value;
                    }
                //}
                /*
                if (agentId != 0 || (shippingCompanyId != 0 && invType.Equals("feed")))
                {
                    invoice = lst_selectedInvoices.SelectedItem as ServiceData;

                    if (invoice != null)
                    {
                        selectedInvoices.Remove(invoice);

                        allInvoices.Add(invoice);

                        lst_allInvoices.ItemsSource = allInvoices;
                        lst_selectedInvoices.ItemsSource = selectedInvoices;

                        lst_allInvoices.Items.Refresh();
                        lst_selectedInvoices.Items.Refresh();

                        x = invoice.deserved.Value;
                    }
                }
                if (userId != 0 || (shippingCompanyId != 0 && invType.Equals("pay")))
                {
                    cashTransfer = lst_selectedInvoices.SelectedItem as PayOp;

                    if (cashTransfer != null)
                    {
                        selectedCashtansfers.Remove(cashTransfer);

                        allCashtransfers.Add(cashTransfer);

                        lst_allInvoices.ItemsSource = allCashtransfers;
                        lst_selectedInvoices.ItemsSource = selectedCashtansfers;

                        lst_allInvoices.Items.Refresh();
                        lst_selectedInvoices.Items.Refresh();

                        x = cashTransfer.deserved.Value;
                    }
                }
                */
                sum -= x;

                tb_sum.Text = " " + HelpClass.DecTostring(sum) + " ";

               
                    
            }
            catch (Exception ex)
            {
               
                    

                HelpClass.ExceptionMessage(ex, this);
            }
           
        }
        private void Btn_unSelectedAll_Click(object sender, RoutedEventArgs e)
        {//unselect all
            
            try
            {
                //if (side == "office")
                //{
                    int x = selectedCashtansfers.Count;
                    for (int i = 0; i < x; i++)
                    {
                        lst_selectedInvoices.SelectedIndex = 0;
                        Btn_unSelectedInvoice_Click(null, null);
                    }

                //}
                /*
                    if (agentId != 0 || (shippingCompanyId != 0 && invType.Equals("feed")))
                {
                    int x = selectedInvoices.Count;
                    for (int i = 0; i < x; i++)
                    {
                        lst_selectedInvoices.SelectedIndex = 0;
                        Btn_unSelectedInvoice_Click(null, null);
                    }
                }
                else if (userId != 0 || (shippingCompanyId != 0 && invType.Equals("pay")))
                {
                    int x = selectedCashtansfers.Count;
                    for (int i = 0; i < x; i++)
                    {
                        lst_selectedInvoices.SelectedIndex = 0;
                        Btn_unSelectedInvoice_Click(null, null);
                    }
                }
                */
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
           
        }
        #endregion
    }
}
