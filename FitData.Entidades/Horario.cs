using System;
namespace FitData.Entidades;

    public class Horario
{
    public int idHorario { get; set; }
    public int idActividad { get; set; }
    public string diaSemana { get; set; }
    public DateTime horaInicio { get; set; }
    public DateTime horaFin { get; set; }
    public int plazasTotales { get; set; }
    public int plazasOcupadas { get; set; }
}
