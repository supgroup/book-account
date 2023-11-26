using netoaster;
using BookAccountApp.Classes;
using BookAccountApp.View.setup;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for wd_setupOtherPos.xaml
    /// </summary>
    public partial class wd_setupProg : Window
    {
        public wd_setupProg()
        {
            InitializeComponent();
        }
        public static ResourceManager resourcemanager;
      
   
        
       
        static public string imgFileName = "pic/no-image-icon-125x125.png";
        static public ImageBrush brush = new ImageBrush();

        uc_codeconfig selectcodeconfig;
      
         public static List<string> requiredControlList;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);
                requiredControlList = new List<string> { "name" };
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
                grid_main.Children.Clear();


                grid_main.Children.Add(uc_codeconfig.Instance);
                #endregion





                if (sender != null)
                    HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                if (sender != null)
                    HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }

        }
        private void translate()
        {

            //txt_title.Text = MainWindow.resourcemanager.GetString("airlines");
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, MainWindow.resourcemanager.GetString("trSearchHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_name, MainWindow.resourcemanager.GetString("airlineHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_notes, MainWindow.resourcemanager.GetString("trNoteHint"));

            //btn_add.Content = MainWindow.resourcemanager.GetString("trAdd");
            //btn_update.Content = MainWindow.resourcemanager.GetString("trUpdate");
            //btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");


            //dg_items.Columns[0].Header = MainWindow.resourcemanager.GetString("airline");
            //dg_items.Columns[1].Header = MainWindow.resourcemanager.GetString("trNote");


            //btn_clear.ToolTip = MainWindow.resourcemanager.GetString("trClear");



            //tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");
            ////tt_report.Content = MainWindow.resourcemanager.GetString("trPdf");
            ////tt_print.Content = MainWindow.resourcemanager.GetString("trPrint");
            ////tt_excel.Content = MainWindow.resourcemanager.GetString("trExcel");
            ////tt_preview.Content = MainWindow.resourcemanager.GetString("trPreview");
            //tt_count.Content = MainWindow.resourcemanager.GetString("trCount");

        }
        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                if (sender != null)
                    HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Btn_back_Click(object sender, RoutedEventArgs e)
        {
            //pageIndex--;
            //CallPage(pageIndex, (sender as Button).Tag.ToString());
        }
        private async void Btn_next_Click(object sender, RoutedEventArgs e)
        {
            //isValid = true;
            //// uc_serverConfig
            //if (pageIndex == 0)
            //{
            //    var supsublist = list.Take(2);
            //    foreach (var item in supsublist)
            //    {
            //        if (item.key.Equals("serverUri"))
            //        {
            //            if (string.IsNullOrWhiteSpace(serverConfigInstance.serverUri))
            //            {
            //                item.value = "";
            //                isValid = false;
            //                break;
            //            }
            //            else
            //            {
            //                item.value = serverConfigInstance.serverUri;
            //                bool validUrl = setupConfiguration.validateUrl(item.value);
            //                if (!validUrl)
            //                {
            //                    Toaster.ShowWarning(this, message: "wrong Url", animation: ToasterAnimation.FadeIn);
            //                    isValid = false;
            //                    break;
            //                }
            //            }

            //        }
            //        else if (item.key.Equals("activationkey"))
            //        {
            //            if (string.IsNullOrWhiteSpace(serverConfigInstance.activationkey))
            //            {
            //                item.value = "";
            //                isValid = false;
            //                break;
            //            }
            //            else
            //            {
            //                item.value = serverConfigInstance.activationkey;
            //            }
            //        }
            //    }
            //}
            //else if (pageIndex == 1)
            //{
            //    var supsublist = list.Skip(2).Take(2);
            //    foreach (var item in supsublist)
            //    {
            //        if (item.key.Equals("posId"))
            //        {
            //            if (selectPosInstance.posId.Equals(0))
            //            {
            //                item.value = "";
            //                isValid = false;
            //                break;
            //            }
            //            else
            //            {
            //                item.value = selectPosInstance.posId.ToString();
            //            }
            //        }
            //        else if (item.key.Equals("branchId"))
            //        {
            //            if (selectPosInstance.branchId.Equals(0))
            //            {
            //                item.value = "";
            //                isValid = false;
            //                break;
            //            }
            //            else
            //            {
            //                item.value = selectPosInstance.branchId.ToString();
            //            }
            //        }
            //    }
            //}

            //if (isValid)
            //{
            //    if (pageIndex == 1)
            //{
            //        int res = 0;
            //        string branchName = selectPosInstance.branchName ;
            //        string posName = selectPosInstance.posName;
            //        if (selectPosInstance.isActivated)
            //        {
            //            res = selectPosInstance.posId;
            //        }
            //        else
            //        {
            //            //server INFO
            //            string url = serverConfigInstance.serverUri;
            //            string activationkey = serverConfigInstance.activationkey;


            //            //// pos INFO
            //            int posId = selectPosInstance.posId;
            //            string motherCode = setupConfiguration.GetMotherBoardID();
            //            string hardCode = setupConfiguration.GetHDDSerialNo();
            //            string deviceCode = motherCode + "-" + hardCode;

            //            Global.APIUri = url + "/api/";
            //             res = (int)await setupConfiguration.setPosConfiguration(activationkey, deviceCode, posId);
                       
            //        }

            //        if (res > 0)
            //        {
            //            Properties.Settings.Default.APIUri = Global.APIUri;
            //            Properties.Settings.Default.posId = res.ToString();
            //            Properties.Settings.Default.BranchName = branchName;
            //            Properties.Settings.Default.PosName = posName;
            //            Properties.Settings.Default.Save();
            //            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //            config.AppSettings.Settings.Add("posId", res.ToString());

            //            config.Save(ConfigurationSaveMode.Modified);

            //            // Force a reload of a changed section.
            //            ConfigurationManager.RefreshSection("appSettings");
            //            this.Close();
            //            return;
            //        }
            //        else if (res == -2 || res == -3) // invalid or resrved activation key
            //        {
            //            //uc_serverConfig.Instance.activationkey = "";
            //            pageIndex = 0;
            //            CallPage(0);
            //            Toaster.ShowWarning(Window.GetWindow(this), message: wd_setupOtherPos.resourcemanager.GetString("trErrorWrongActivation"), animation: ToasterAnimation.FadeIn);
            //            return;
            //        }
            //    }
            //if (pageIndex < 1)
            //{
            //    pageIndex++;
            //    CallPage(pageIndex, (sender as Button).Tag.ToString());
            //}
            //}
            //else
            //    Toaster.ShowWarning(Window.GetWindow(this), message: "Should fill form first", animation: ToasterAnimation.FadeIn);

        }
        private void restartApplication()
        {
            System.Diagnostics.Process.Start("pos.exe");
            Application.Current.Shutdown();
        }
        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    Btn_next_Click(btn_next, null);
                }
            }
            catch (Exception ex)
            {
                if (sender != null)
                    HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch //(Exception ex)
            {
                //SectionData.ExceptionMessage(ex, this);
            }
        }
    }
}
