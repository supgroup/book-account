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
    /// Interaction logic for wd_agentPackages.xaml
    /// </summary>

    public partial class wd_agentPackages : Window
    {
        public int agentID { get; set; }

        List<Packages> allPackagesSource = new List<Packages>();
        List<Packages> allPackages = new List<Packages>();
        List<Packages> selectedPackagesSource = new List<Packages>();
        IEnumerable<Packages> packageQuery;
        List<AgentPackage> selectedPackages = new List<AgentPackage>();

        Packages packageModel = new Packages();
        AgentPackage agentPackageModel = new AgentPackage();

        Packages package = new Packages();

        string txtSearch = "";
        public wd_agentPackages()
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
                HelpClass.StartAwait(grid_agentPackage);

                #region translate
                if (MainWindow.lang.Equals("en"))
                {
                    MainWindow.resourcemanager = new ResourceManager("BookAccountApp.en_file", Assembly.GetExecutingAssembly());
                    grid_agentPackage.FlowDirection = FlowDirection.LeftToRight;
                }
                else
                {
                    MainWindow.resourcemanager = new ResourceManager("BookAccountApp.ar_file", Assembly.GetExecutingAssembly());
                    grid_agentPackage.FlowDirection = FlowDirection.RightToLeft;
                }

                translat();
                #endregion

                allPackagesSource = await packageModel.GetByAgentCountryId(agentID);
                selectedPackagesSource = await packageModel.GetPackagesByAgent(agentID);

                allPackages.AddRange(allPackagesSource);

                //remove selected packages from all packages
                foreach (var i in selectedPackagesSource)
                {
                    //package = await packageModel.GetByID(i.packageId);
                    package = allPackages.Where(p => p.packageId == i.packageId).FirstOrDefault();
                    allPackages.Remove(package);
                }

                dg_allPackages.ItemsSource = allPackages;
                dg_allPackages.SelectedValuePath = "packageId";
                dg_allPackages.DisplayMemberPath = "packageName";

                dg_selectedPackages.ItemsSource = selectedPackagesSource;
                dg_selectedPackages.SelectedValuePath = "packageId";
                dg_selectedPackages.DisplayMemberPath = "packageName";

                HelpClass.EndAwait(grid_agentPackage);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_agentPackage);
                HelpClass.ExceptionMessage(ex, this);
            }

        }

        private void translat()
        {
            MaterialDesignThemes.Wpf.HintAssist.SetHint(txb_searchitems, MainWindow.resourcemanager.GetString("trSearchHint"));

            btn_save.Content = MainWindow.resourcemanager.GetString("trSave");

            dg_allPackages.Columns[0].Header = MainWindow.resourcemanager.GetString("trName");
            dg_selectedPackages.Columns[0].Header = MainWindow.resourcemanager.GetString("trName");

            txt_title.Text = MainWindow.resourcemanager.GetString("trAgentPackages");
            txt_allPackages.Text = MainWindow.resourcemanager.GetString("trAllPackages");
            txt_selectedPackages.Text = MainWindow.resourcemanager.GetString("trAgentPackages");

            tt_search.Content = MainWindow.resourcemanager.GetString("trSearch");
            tt_selectAllPackages.Content = MainWindow.resourcemanager.GetString("trSelectAllItems");
            tt_unselectAll.Content = MainWindow.resourcemanager.GetString("trUnSelectAllItems");
            tt_selectOne.Content = MainWindow.resourcemanager.GetString("trSelectOneItem");
            tt_unselectOne.Content = MainWindow.resourcemanager.GetString("trUnSelectOneItem");
            tt_close.Content = MainWindow.resourcemanager.GetString("trClose");
        }

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

        private void Btn_colse_Click(object sender, RoutedEventArgs e)
        {//close
            //isActive = false;
            this.Close();
        }

        private void Txb_searchitems_TextChanged(object sender, TextChangedEventArgs e)
        {//search
            try
            {
                txtSearch = txb_searchitems.Text;

                packageQuery = allPackages.Where(s => s.packageName.Contains(txtSearch));
                dg_allPackages.ItemsSource = packageQuery;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }

        }

        private void Dg_allPackages_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {//package double click
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
                int x = allPackages.Count;
                for (int i = 0; i < x; i++)
                {
                    dg_allPackages.SelectedIndex = 0;
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
                package = dg_allPackages.SelectedItem as Packages;
                if (package != null)
                {
                    allPackages.Remove(package);
                    selectedPackagesSource.Add(package);

                    dg_allPackages.ItemsSource = allPackages;
                    dg_selectedPackages.ItemsSource = selectedPackagesSource;

                    dg_allPackages.Items.Refresh();
                    dg_selectedPackages.Items.Refresh();
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
                package = dg_selectedPackages.SelectedItem as Packages;
                if (package != null)
                {
                    allPackages.Add(package);

                    selectedPackagesSource.Remove(package);

                    dg_allPackages.ItemsSource = allPackages;
                    dg_selectedPackages.ItemsSource = selectedPackagesSource;

                    dg_allPackages.Items.Refresh();
                    dg_selectedPackages.Items.Refresh();
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
                int x = selectedPackagesSource.Count;
                for (int i = 0; i < x; i++)
                {
                    dg_selectedPackages.SelectedIndex = 0;
                    Btn_unSelectedOne_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Dg_selectedPackages_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {//selected package double click
            try
            {
                Btn_unSelectedOne_Click(null, null);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Dg_selectedPackages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//select selected package

        }

        private async void Btn_save_Click(object sender, RoutedEventArgs e)
        {//save
            try
            {
                HelpClass.StartAwait(grid_agentPackage);

                foreach (var p in selectedPackagesSource)
                {
                    AgentPackage ap = new AgentPackage();
                    ap.agentPackageId = 0;
                    ap.packageId = p.packageId;
                    ap.agentId = agentID;
                    ap.createUserId = MainWindow.userID;
                    selectedPackages.Add(ap);
                }

                decimal s = await agentPackageModel.saveNewList(selectedPackages, agentID);

                //isActive = true;
                this.Close();

                HelpClass.EndAwait(grid_agentPackage);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_agentPackage);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Dg_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            //// Have to do this in the unusual case where the border of the cell gets selected.
            //// and causes a crash 'EditItem is not allowed'
            //e.Cancel = true;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            GC.Collect();
        }
    }
}
