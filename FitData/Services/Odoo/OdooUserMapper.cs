
using FitData.Entidades;

namespace FitData.Services.Odoo
{
    public static class OdooUserMapper
    {
        public static object Map(Usuario u) => new
        {
            name = $"{u.Nombre} {u.Apellido}",
            login = u.Username,
            password = u.Password
        };
    }
}
