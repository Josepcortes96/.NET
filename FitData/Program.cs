using System;
using System.Windows.Forms;
using FitData.Forms;      
using FitData.Services;
using FitData.Services.Odoo;
using FitData.Datos;
<<<<<<< HEAD
using FitData.Entidades;
using FitData.Datos.Xml; // <= importante
=======
>>>>>>> origin/josep/Producto-4

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

<<<<<<< HEAD
        

            // Ejecuta la aplicación normalmente
            Application.Run(new LoginForm());
=======
            Application.Run(new LoginForm()); 
        }

        public static async Task SincronizarOdooAsync()
        {
            var ctx = new FitDataContext();
            var odoo = new OdooClient("fitdata", "josepcortes6@gmail.com", "fitdata");
            var sync = new SincronizacionService(ctx, odoo);

            await sync.SincronizarAsync();

            MessageBox.Show("Sincronización con Odoo completada correctamente.");
>>>>>>> origin/josep/Producto-4
        }
    }
}