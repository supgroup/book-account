using BookAccountApp.Classes;
using BookAccountApp.View.bookSales;
using BookAccountApp.View.sales;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookAccountApp.View.accounting
{
    /// <summary>
    /// Interaction logic for uc_accounting.xaml
    /// </summary>
    public partial class uc_accounting : UserControl
    {
        private static uc_accounting _instance;
        public static uc_accounting Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_accounting();
                return _instance;
            }
        }
        public uc_accounting()
        {
            InitializeComponent();
        }
        public static List<string> menuList;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_mainGrid);

                menuList = new List<string> { "payment", "received", };

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

                Btn_payment_Click(btn_payment, null);

                HelpClass.EndAwait(grid_mainGrid);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_mainGrid);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void translate()
        {
            btn_payment.Content = MainWindow.resourcemanager.GetString("trPayments");
            btn_received.Content = MainWindow.resourcemanager.GetString("trReceived");
            //btn_office.Content = MainWindow.resourcemanager.GetString("trOffices");
            //btn_passengers.Content = MainWindow.resourcemanager.GetString("passengers");
            //btn_flights.Content = MainWindow.resourcemanager.GetString("flights");
            //btn_operations.Content = MainWindow.resourcemanager.GetString("operations");
            //btn_customers.Content = MainWindow.resourcemanager.GetString("trCustomers");
        }

        void colorButtonRefreash(string str)
        {
            foreach (Button button in FindControls.FindVisualChildren<Button>(this))
            {
                if (button.Tag != null)
                {
                    foreach (var item in menuList)
                    {
                        if (item == button.Tag.ToString())
                        {
                            if (item == str)
                                button.Background = Application.Current.Resources["MainColor"] as SolidColorBrush;
                            else
                                button.Background = Application.Current.Resources["SecondColor"] as SolidColorBrush;

                        }
                    }
                }
            }
        }




        private void Btn_payment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                colorButtonRefreash(button.Tag.ToString());
                grid_main.Children.Clear();
                uc_payment ucsy = new uc_payment();
                grid_main.Children.Add(ucsy);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Btn_received_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                colorButtonRefreash(button.Tag.ToString());
                grid_main.Children.Clear();
                uc_received ucso = new uc_received();
                grid_main.Children.Add(ucso);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }


        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            // Collect all generations of memory.
            GC.Collect();
        }


    }
}
