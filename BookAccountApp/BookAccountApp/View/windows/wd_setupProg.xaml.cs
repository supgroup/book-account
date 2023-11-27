using netoaster;
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

using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Media;

using BookAccountApp.ApiClasses;
using Newtonsoft.Json;
 

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


        public bool isActivated { get; set; }

        BrushConverter bc = new BrushConverter();
        public static List<string> requiredControlList;
        public int isFirstActive = 0;
        public bool isExtende = false;
        public string reason = "";
        string deviceCode = "";
        ProgramDetailsCls programdetailModel = new ProgramDetailsCls();
        List<ProgramDetailsCls> programdetailList = new List<ProgramDetailsCls>();
        ActivateModel activeModel = new ActivateModel();
        CodeCls codclass = new CodeCls();

       // static public string imgFileName = "pic/no-image-icon-125x125.png";
        static public ImageBrush brush = new ImageBrush();

        //uc_codeconfig selectcodeconfig;
       
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);
                //  requiredControlList = new List<string> { "name" };
                #region translate
                MainWindow.lang = "ar";
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
                // grid_mainWindow.FlowDirection = FlowDirection.RightToLeft;
                //}
                if (reason == "ok") {
                    txt_message.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF008F00"));
                }
           translate();
                Keyboard.Focus(tb_customerCode);

                //  Clear();
                programdetailList = await programdetailModel.GetAll();
           //     string res = await programdetailModel.CheckAvailable();

                deviceCode = programdetailModel.getHardCode();
                tb_customerCode.Text = deviceCode;
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

            txt_title.Text = MainWindow.resourcemanager.GetString("activeData");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_customerCode, MainWindow.resourcemanager.GetString("deviceserialHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_activeCode, MainWindow.resourcemanager.GetString("activationCodeHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_notes, MainWindow.resourcemanager.GetString("trNoteHint"));

            btn_activate.Content = MainWindow.resourcemanager.GetString("confirmActivate");
            btn_cancel.Content = MainWindow.resourcemanager.GetString("cancel");
            txt_message.Text = reasonconverter(reason);
        }

        public string reasonconverter(string reason)
        {
            string trans = "";
            if (reason== "notbegin")
            {
                trans=MainWindow.resourcemanager.GetString("notbegin");
            }
            else if (reason == "expired")
            {
                trans= MainWindow.resourcemanager.GetString("expired");
            }
            else if (reason == "wrongcode")
            {
                trans= MainWindow.resourcemanager.GetString("wrongcode");
            }
            else if (reason == "ok")
            {
                trans = MainWindow.resourcemanager.GetString("copyActivated");
            }

            return trans;
               
        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isExtende == false)
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    this.Close();
                }
               // 
                
            }
            catch (Exception ex)
            {
                if (sender != null)
                    HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
      
    
        private void restartApplication()
        {
            System.Diagnostics.Process.Start("pos.exe");
            Application.Current.Shutdown();
        }


        private void Tb_customerCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                HelpClass.validate(requiredControlList, this);
                if (!(string.IsNullOrEmpty(tb_customerCode.Text)))
                {
                    btn_activate.IsEnabled = true;
                }
                else
                {
                    btn_activate.IsEnabled = false;
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
        private async void Btn_activate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                string result = "";
                if (!string.IsNullOrEmpty(tb_activeCode.Text))
                {
                    DateTime now = DateTime.Now;
                    string activeKey = tb_activeCode.Text.Trim();
                    // string orginalkeydec = CodeCls.FinalDecode(tb_activeCode.Text);                   
                    //  ActivateModel activemode = JsonConvert.DeserializeObject<ActivateModel>(orginalkeydec, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });
                    List<ProgramDetailsCls> pList = programdetailList.Where(x => x.activateCode == activeKey).ToList();
                    if (pList==null|| pList.Count()==0)
                    {                   
                    ActivateModel activemode = codclass.convertToModel(activeKey);
                    if (activemode.customerHardCode == deviceCode)
                    {
                        //  MessageBox.Show("ok");
                        if (activemode.expireDate.Value >= now)
                        {
                            //   MessageBox.Show("ok");
                            int cont = await programdetailModel.GetCountDetailList(programdetailList);
                            if ((cont == 0 && activemode.startDate > now))
                            {
                                //   MessageBox.Show("تاريخ بدايةالصلاحية لم يبدا بعد");
                                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("notbegin"), animation: ToasterAnimation.FadeIn);

                            }
                            else
                            {

                               // MessageBox.Show("ok");
                                if (cont == 0 && activemode.startDate <= now)
                                {
                                    //save record
                                    programdetailModel = new ProgramDetailsCls();
                                    programdetailModel.isCurrent = 1;
                                    programdetailModel.state = "active";
                                    programdetailModel.activateCode = activeKey;

                                    decimal s = await programdetailModel.Save(programdetailModel);
                                    if (s <= 0)
                                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                                    else
                                    {
                                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("activatedsucsessful"), animation: ToasterAnimation.FadeIn);
                                        await Task.Delay(2000);
                                        Application.Current.Shutdown();
                                    }
                                }
                                else if (cont > 0 )
                                {
                                    result = await programdetailModel.CheckmodelAvailable(activemode);
                                    if (result== "notbegin")
                                    {
                                        // add and iscurrent=2

                                        programdetailModel = new ProgramDetailsCls();
                                        programdetailModel.isCurrent = 2;
                                        programdetailModel.state = "active";
                                        programdetailModel.activateCode = activeKey;
                                        decimal s = await programdetailModel.resetallCurrent2();
                                          s = await programdetailModel.Save(programdetailModel);
                                        if (s <= 0)
                                            Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                                        else
                                        {
                                            Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("activatedsucsessful"), animation: ToasterAnimation.FadeIn);
                                            await Task.Delay(2000);
                                            Application.Current.Shutdown();
                                        }
                                    }
                                    else if (result=="ok")
                                    {
                                        //add and is current =1 

                                        programdetailModel = new ProgramDetailsCls();
                                        programdetailModel.isCurrent = 1;
                                        programdetailModel.state = "active";
                                        programdetailModel.activateCode = activeKey;
                                        decimal s=await programdetailModel.resetallCurrent();
                                         s = await programdetailModel.Save(programdetailModel);
                                        if (s <= 0)
                                            Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                                        else
                                        {
                                            Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("activatedsucsessful"), animation: ToasterAnimation.FadeIn);
                                            await Task.Delay(2000);
                                            Application.Current.Shutdown();
                                        }
                                    }
                                }
                                
                                //save record
                                //   await programdetailModel.Save(programdetailModel);

                            }
                        }
                        else
                        {
                         //   MessageBox.Show("النسخة منتهية");
                            Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("expired"), animation: ToasterAnimation.FadeIn);

                        }
                    }
                    else
                    {
                       // MessageBox.Show("الرمز غير صحيح");
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("wrongcode"), animation: ToasterAnimation.FadeIn);
                    }
                    }
                    else
                    {
                       
                        //الرمز مستخدم مسبقا
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("codeExist"), animation: ToasterAnimation.FadeIn);

                    }
                }
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                if (sender != null)
                    HelpClass.EndAwait(grid_main);

               
                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("wrongcode"), animation: ToasterAnimation.FadeIn);

               // MessageBox.Show("الرمز غير صحيح");
               // HelpClass.ExceptionMessage(ex, this);
            }
            txt_message.Text = "";
        }

    }
}
