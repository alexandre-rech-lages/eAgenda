using eAgenda.Infra.Arquivos;
using System;
using System.Windows.Forms;

namespace eAgenda.WinApp
{
    internal static class Program
    {       
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TelaPrincipalForm());

        }

    }
}
