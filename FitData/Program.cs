using System;
using System.Windows.Forms;
using FitData.Forms;
using FitData.Datos.Repositorios;
using FitData.Datos;
using FitData.Entidades;

namespace FitData
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
