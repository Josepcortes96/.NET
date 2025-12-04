using FitData.Datos;
using FitData.Entidades;
using FitData.Services.Odoo;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;

namespace FitData.Services
{
    public class SincronizacionService
    {
        private readonly FitDataContext _ctx;
        private readonly OdooClient _odoo;

        public SincronizacionService(FitDataContext ctx, OdooClient odoo)
        {
            _ctx = ctx;
            _odoo = odoo;
        }

        public async Task SincronizarAsync()
        {
            try
            {
                
                if (!await _odoo.LoginAsync())
                {
                    MessageBox.Show("No se pudo autenticar con Odoo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine(">>> LOGIN OK. Empezando sincronización...");
                MessageBox.Show("Conexión realizada. Sincronizando…", "Odoo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ==== 2) LEER CLIENTES ====
                var clientes = _ctx.Clientes.Select(c => c.IdUsuario).ToHashSet();

                // ==== 3) LISTA COMPLETA DE USUARIOS ====
                var usuarios = _ctx.Usuarios.AsNoTracking().ToList();

                foreach (var u in usuarios)
                {
                    try
                    {
                        // ======== VERIFICAR DUPLICADOS ANTES DE CREAR ========
                        if (await _odoo.UsuarioExisteAsync(u.Username))
                        {
                            Console.WriteLine($"[SKIP] Usuario '{u.Username}' ya existe en Odoo. Saltado.");
                            continue;
                        }

                        // ======== CLIENTE → PARTNER ========
                        if (clientes.Contains(u.IdUsuario))
                        {
                            var data = OdooPartnerMapper.Map(u);
                            int id = await _odoo.CrearPartnerAsync(data);

                            Console.WriteLine($"[CREADO CLIENTE] {u.Nombre} → PartnerID={id}");
                        }
                        // ======== USUARIO NORMAL ========
                        else
                        {
                            var data = OdooUserMapper.Map(u);
                            int id = await _odoo.CrearUsuarioOdooAsync(data);

                            Console.WriteLine($"[CREADO USUARIO] {u.Nombre} → UserID={id}");
                        }
                    }
                    catch (Exception exUser)
                    {
                        MessageBox.Show(
                            $"Error sincronizando usuario '{u.Nombre}':\n\n{exUser.Message}",
                            "ERROR INDIVIDUAL",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                }

                MessageBox.Show("Sincronización COMPLETADA correctamente.", "Odoo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $">>> EXCEPCIÓN GENERAL <<<\n\nMensaje: {ex.Message}\n\nStackTrace:\n{ex.StackTrace}",
                    "ERROR CRÍTICO",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
