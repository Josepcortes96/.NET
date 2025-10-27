using System;
namespace FitData.Entidades;

    public class Reserva
{
    public int idReserva { get; set; }
    public int idCliente { get; set; }
    public int idHorario { get; set; }
    public DateTime fechaReserva { get; set; }
    public string estado { get; set; } // "confirmada" o "cancelada"
}
