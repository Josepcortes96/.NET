using System;

namespace FitData.Entidades
{
    public class Horario
    {
        public int IdHorario { get; set; }
        public int IdActividad { get; set; }
        public string DiaSemana { get; set; } = "";
        public TimeSpan HoraInicio { get; set; }     
        public TimeSpan HoraFin { get; set; }        
        public int PlazasTotales { get; set; }
        public int PlazasOcupadas { get; set; }

    }
}