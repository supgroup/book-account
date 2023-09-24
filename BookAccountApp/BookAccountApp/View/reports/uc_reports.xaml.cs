using BookAccountApp.Classes;
using BookAccountApp.View.reports;
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

namespace BookAccountApp.View.reports
{
    /// <summary>
    /// Interaction logic for uc_reports.xaml
    /// </summary>
    public partial class uc_reports : UserControl
    {
        private static uc_reports _instance;
        public static uc_reports Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_reports();
                return _instance;
            }
        }
        public uc_reports()
        {
            InitializeComponent();
        }
        public static List<string> menuList;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_mainGrid);

                menuList = new List<string> { "paymentsSts", "bookSts", "systemTransactions" };

                #region translate
                translate();
                #endregion

                Btn_paymentsSts_Click(btn_paymentsSts, null);

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
            /*
             * financeChange
bookProfits
paySysTrans
             * */
            btn_bookSts.Content = MainWindow.resourcemanager.GetString("bookProfits");
            btn_paymentsSts.Content = MainWindow.resourcemanager.GetString("financeChange");
            btn_systemTransactions.Content = MainWindow.resourcemanager.GetString("paySysTrans");
             
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

        

        private void Btn_paymentsSts_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                colorButtonRefreash(button.Tag.ToString());
                MainWindow.mainWindow.second = button.Tag.ToString();
                MainWindow.mainWindow.setMainPath();
                grid_main.Children.Clear();
                grid_main.Children.Add(uc_paymentsSts.Instance);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Btn_bookSts_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                colorButtonRefreash(button.Tag.ToString());
                MainWindow.mainWindow.second = button.Tag.ToString();
                MainWindow.mainWindow.setMainPath();
                grid_main.Children.Clear();
                grid_main.Children.Add(uc_bookSts.Instance);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

      private void Btn_systemTransactions_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                colorButtonRefreash(button.Tag.ToString());
                MainWindow.mainWindow.second = button.Tag.ToString();
                MainWindow.mainWindow.setMainPath();
                grid_main.Children.Clear();
                uc_systemTransactions ucsys = new uc_systemTransactions();
                grid_main.Children.Add(ucsys);
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
