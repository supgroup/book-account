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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookAccountApp.View.sales
{
    /// <summary>
    /// Interaction logic for uc_sales.xaml
    /// </summary>
    public partial class uc_sales : UserControl
    {
        public bool isPayment = false;
        private static uc_sales _instance;
        public static uc_sales Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_sales();
                return _instance;
            }
        }
        public uc_sales()
        {
            try
            {
                InitializeComponent();
            }
            catch(Exception ex)
            {
                HelpClass.ExceptionMessage(ex ,this );
            }
        }
        public static List<string> menuList;
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_mainGrid);

                menuList = new List<string> { "book", "payments"};

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

                //if (!isPayment)
                //    Btn_sale_Click(btn_sale, null);
                //else
                //{
                //    isPayment = false;
                //    Btn_packageUser_Click(btn_packageUser, null);
                //}
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
            btn_sale.Content = MainWindow.resourcemanager.GetString("trBookings");
            btn_packageUser.Content = MainWindow.resourcemanager.GetString("trPayments");
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
        
        public void Btn_sale_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                colorButtonRefreash(button.Tag.ToString());
                grid_main.Children.Clear();
                grid_main.Children.Add(uc_sale.Instance);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        public void Btn_packageUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                colorButtonRefreash(button.Tag.ToString());
                grid_main.Children.Clear();
                grid_main.Children.Add(uc_payment.Instance);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_posSerials_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                colorButtonRefreash(button.Tag.ToString());
                grid_main.Children.Clear();
                grid_main.Children.Add(uc_posSerials.Instance);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        public void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            // Collect all generations of memory.
            GC.Collect();
        }
    }
}
