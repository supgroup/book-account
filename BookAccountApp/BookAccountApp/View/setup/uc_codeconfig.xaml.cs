using BookAccountApp.Classes;
using BookAccountApp.View.windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookAccountApp.View.setup
{
    /// <summary>
    /// Interaction logic for uc_selectPos.xaml
    /// </summary>
    public partial class uc_codeconfig : UserControl
    {
        public uc_codeconfig()
        {
            InitializeComponent();
        }
        private static uc_codeconfig _instance;
        public static uc_codeconfig Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_codeconfig();
                return _instance;
            }
        }


 

        public bool isActivated { get; set; }
 
        BrushConverter bc = new BrushConverter();
        public static List<string> requiredControlList;
      
        string deviceCode = "";
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);

                requiredControlList = new List<string> { "hardcode" };

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
               
       

              //  Keyboard.Focus(tb);
 
              //  Clear();

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
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

        private void Tb_validateEmptyLostFocus(object sender, RoutedEventArgs e)
        {
            //    try
            //    {
            //        string name = sender.GetType().Name;
            //        validateEmpty(name, sender);
            //    }
            //    catch (Exception ex)
            //    {
            //       SectionData.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            //    }
        }
        //private void validateEmpty(string name, object sender)
        //{
        //    if (name == "ComboBox")
        //    {
        //        if ((sender as ComboBox).Name == "cb_branch")
        //            validateEmptyComboBox((ComboBox)sender, p_errorBranch, tt_errorBranch, "trEmptyBranchToolTip");
        //        if ((sender as ComboBox).Name == "cb_pos")
        //            validateEmptyComboBox((ComboBox)sender, p_errorBranch, tt_errorBranch, "trErrorEmptyPosToolTip");
        //    }
        //}


        private void Tb_customerCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                HelpClass.validate(requiredControlList, this);
                if (!(string.IsNullOrEmpty(tb_customerCode.Text)))
                {
                    btn_generatecode.IsEnabled = true;
                }
                else
                {
                    btn_generatecode.IsEnabled = false;
                }

            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void validateEmpty_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.validate(requiredControlList, this);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }


    }
}
