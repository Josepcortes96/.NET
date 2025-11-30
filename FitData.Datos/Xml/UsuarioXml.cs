using System;
using System.Xml.Serialization;

namespace FitData.Datos.Xml
{
    public class UsuarioXml
    {
        [XmlElement("IdUsuario")]
        public int IdUsuario { get; set; }

        [XmlElement("Nombre")]
        public string Nombre { get; set; } = "";

        [XmlElement("Apellido")]
        public string Apellido { get; set; } = "";

        [XmlElement("Nif")]
        public string Nif { get; set; } = "";

        [XmlElement("Rol")]
        public string Rol { get; set; } = "";

        [XmlElement("Username")]
        public string Username { get; set; } = "";

        [XmlElement("Email")]
        public string Email { get; set; } = "";

        [XmlElement("Telefono")]
        public string Telefono { get; set; } = "";

        // Fecha como DateTime nullable. Serializa como "YYYY-MM-DD" si DataType="date" se usa al serializar.
        [XmlElement("FechaNacimiento", DataType = "date", IsNullable = true)]
        public DateTime? FechaNacimiento { get; set; } = null;

        [XmlIgnore]
        public string Password { get; set; } = "";
    }
}