namespace FitData.Entidades;
{
    public class Actividad
{
    public int idActividad { get; set; }
    public string nombre { get; set; }       // e.g., "Zumba"
    public string descripcion { get; set; }
    public string nivelIntensidad { get; set; } // e.g., "Bajo", "Medio", "Alto"
    public string sala { get; set; }         // e.g., "Sala 1"
    public int idMonitor { get; set; }
    public string idEncargado { get; set; }
}
}