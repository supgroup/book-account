﻿using BookAccountApp.Classes;
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

namespace BookAccountApp.View.settings.printerSetting
{
    /// <summary>
    /// Interaction logic for uc_printerSetting.xaml
    /// </summary>
    public partial class uc_printerSetting : UserControl
    {
        public uc_printerSetting()
        {
            InitializeComponent();
        }
        private static uc_printerSetting _instance;
        public static uc_printerSetting Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_printerSetting();
                return _instance;
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            // Collect all generations of memory.
            GC.Collect();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);
                //MainWindow.mainWindow.initializationMainTrack(this.Tag.ToString(), 1);

                #region translate
                if (MainWindow.lang.Equals("en"))
                {
                    //MainWindow.resourcemanager = new ResourceManager("POS.en_file", Assembly.GetExecutingAssembly());
                    grid_main.FlowDirection = FlowDirection.LeftToRight;
                }
                else
                {
                    //MainWindow.resourcemanager = new ResourceManager("POS.ar_file", Assembly.GetExecutingAssembly());
                    grid_main.FlowDirection = FlowDirection.RightToLeft;
                }

                translate();
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
            //txt_comInfo.Text = MainWindow.resourcemanager.GetString("trComInfo");
            //txt_comHint.Text = MainWindow.resourcemanager.GetString("trSettingHint");
        }


        private void Btn_printerSetting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                /*
                Window.GetWindow(this).Opacity = 0.2;
                wd_printerSetting w = new wd_printerSetting();
                w.ShowDialog();
                Window.GetWindow(this).Opacity = 1;
                */
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_copyCount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                /*
                Window.GetWindow(this).Opacity = 0.2;
                wd_copyCount w = new wd_copyCount();
                w.ShowDialog();
                Window.GetWindow(this).Opacity = 1;
                */
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

    }
}
