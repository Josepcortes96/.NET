namespace FitData.Entidades
{
    /// <summary>
    /// Representa una actividad disponible en GenteFit.
    /// </summary>
    public class Actividad
    {
        /// <summary>
        /// Identificador �nico de la actividad.
        /// </summary>
        public int IdActividad { get; set; }

        /// <summary>
        /// Nombre de la actividad (por ejemplo: Zumba, Yoga, Spinning...).
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Descripci�n de la actividad.
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// Nivel de intensidad de la actividad (Baja, Media, Alta...).
        /// </summary>
        public string NivelIntensidad { get; set; } = string.Empty;

        /// <summary>
        /// Sala o ubicaci�n donde se imparte la actividad (por ejemplo: Sala 1, Sala 2...).
        /// </summary>
        public string Sala { get; set; } = string.Empty;

        /// <summary>
        /// Identificador del monitor responsable de impartir la actividad.
        /// Clave for�nea hacia la tabla Monitores.
        /// </summary>
        public int IdMonitor { get; set; }

        /// <summary>
        /// Identificador del encargado que gestiona esta actividad.
        /// Clave for�nea hacia la tabla Encargado.
        /// Es nullable porque puede que a�n no est� asignado.
        /// </summary>
        public int? IdEncargado { get; set; }

        public override string ToString()
        {
            return Nombre ?? string.Empty;
        }
    }
}