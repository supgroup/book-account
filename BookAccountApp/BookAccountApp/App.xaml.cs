using BookAccountApp.Classes;
using BookAccountApp.View.windows;
using POS.View.windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using BookAccountApp.ApiClasses;
namespace BookAccountApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                ProgramDetailsCls prgdt = new ProgramDetailsCls();
                string result = await prgdt.CheckAvailable();
                if (result!="ok")
                {
                    wd_setupProg setupwin = new wd_setupProg();
                    setupwin.isExtende = false;
                    setupwin.reason = result;
                    setupwin.Show();
                }
                else
                {
                    wd_logIn logIn = new wd_logIn();
                    // MainWindow main = new MainWindow();
                    logIn.Show();
                }
           

            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

    }
}
