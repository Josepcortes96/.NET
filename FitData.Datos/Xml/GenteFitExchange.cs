using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FitData.Datos.Xml
{
    [XmlRoot("GenteFitExchange")]
    public class GenteFitExchange
    {
        [XmlElement("InfoLote")]
        public InfoLote InfoLote { get; set; } = new InfoLote();

        [XmlElement("Usuarios")]
        public Usuarios Usuarios { get; set; } = new Usuarios();

        [XmlElement("Actividades")]
        public Actividades Actividades { get; set; } = new Actividades();

        [XmlElement("Reservas")]
        public Reservas Reservas { get; set; } = new Reservas();
    }

    public class InfoLote
    {
        [XmlElement("IdLote")]
        public string IdLote { get; set; } = "";

        [XmlElement("GeneradoEn")]
        public DateTime GeneradoEn { get; set; } = DateTime.UtcNow;

        [XmlElement("SistemaOrigen")]
        public string SistemaOrigen { get; set; } = "GenteFitApp";

        [XmlElement("Descripcion")]
        public string Descripcion { get; set; } = "";
    }

    public class Usuarios
    {
        [XmlElement("Usuario")]
        public List<UsuarioXml> UsuarioList { get; set; } = new List<UsuarioXml>();
    }

    public class Actividades
    {
        [XmlElement("Actividad")]
        public List<ActividadXml> ActividadList { get; set; } = new List<ActividadXml>();
    }

    public class ActividadXml
    {
        [XmlElement("IdActividad")]
        public string IdActividad { get; set; } = "";

        [XmlElement("NombreActividad")]
        public string NombreActividad { get; set; } = "";

        [XmlElement("Descripcion")]
        public string Descripcion { get; set; } = "";

        [XmlElement("Intensidad")]
        public string Intensidad { get; set; } = "";

        [XmlElement("Sala")]
        public string Sala { get; set; } = "";

        [XmlElement("Plazas")]
        public int Plazas { get; set; } = 0;
    }

    public class Reservas
    {
        [XmlElement("Reserva")]
        public List<ReservaXml> ReservaList { get; set; } = new List<ReservaXml>();
    }

    public class ReservaXml
    {
        [XmlElement("IdReserva")]
        public string IdReserva { get; set; } = "";

        [XmlElement("IdUsuario")]
        public int IdUsuario { get; set; }

        [XmlElement("IdActividad")]
        public string IdActividad { get; set; } = "";

        [XmlElement("FechaHora")]
        public DateTime FechaHora { get; set; } = DateTime.UtcNow;

        [XmlElement("Estado")]
        public string Estado { get; set; } = "";

        [XmlElement("Notas")]
        public string Notas { get; set; } = "";
    }
}
