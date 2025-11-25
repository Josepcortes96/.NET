using System;
namespace FitData.Entidades;

    public class Reserva
{
    public int IdReserva { get; set; }
    public int IdCliente { get; set; }
    public int IdHorario { get; set; }
    public DateTime FechaReserva { get; set; }
    public string Estado { get; set; } = ""; // e.g. "confirmada", "cancelada"
}
