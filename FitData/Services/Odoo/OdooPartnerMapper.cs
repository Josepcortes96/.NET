using FitData.Entidades;

namespace FitData.Services.Odoo
{
    public static class OdooPartnerMapper
    {
        public static object Map(Usuario u) => new
        {
            name = $"{u.Nombre} {u.Apellido}",
            email = u.Username,
            vat = u.Nif,
            customer_rank = 1
        };
    }
}