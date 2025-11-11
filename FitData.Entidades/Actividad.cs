namespace FitData.Entidades
{
    public class Actividad
    {
        public int IdActividad { get; set; }
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string NivelIntensidad { get; set; } = "";
        public string Sala { get; set; } = "";
        public int IdMonitor { get; set; }
        public int? IdEncargado { get; set; }
    }
}
