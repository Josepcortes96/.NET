using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FitData.Datos.Xml
{
    internal class UsuarioXml
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

        // NO incluimos Password por seguridad. 
    
    }
}
