using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Forza_Mods_AIO
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (!Application.OpenForms.Cast<Form>().Any(form => form.Name == "Error"))
            {
                Form error = new TabForms.PopupForms.Error(e);
                error.StartPosition = FormStartPosition.CenterParent;
                error.Show();
            }
            else
            {
                Form error = Application.OpenForms["error"];
                error.Focus();
            }
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (!Application.OpenForms.Cast<Form>().Any(form => form.Name == "Error"))
            {
                Form error = new TabForms.PopupForms.Error(e);
                error.StartPosition = FormStartPosition.CenterParent;
                error.Show();
            }
            else
            {
                Form error = Application.OpenForms["error"];
                error.Focus();
            }
        }
    }
}
