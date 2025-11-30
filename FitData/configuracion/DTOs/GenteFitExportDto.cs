using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FitData.configuracion.DTOs
{
    [XmlRoot("GenteFitExport", Namespace = "http://example.org/gentefit")]
    public class GenteFitExportDto
    {
        public BatchDto Batch { get; set; } = new BatchDto();
        public UsuariosDto Usuarios { get; set; } = new UsuariosDto();
        public ClientesDto Clientes { get; set; } = new ClientesDto();
    }

    public class BatchDto
    {
        public string BatchId { get; set; } = "";
        public DateTime Date { get; set; }
        public string Origin { get; set; } = "GenteFit";
        public string CenterCode { get; set; } = "";
        public string User { get; set; } = "";
        public string Type { get; set; } = "";
    }

    public class UsuariosDto
    {
        [XmlElement("Usuario")]
        public List<UsuarioDto> Usuario { get; set; } = new List<UsuarioDto>();
    }

    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Nif { get; set; } = "";
        public string Rol { get; set; } = "";
        public string Username { get; set; } = "";
    }

    public class ClientesDto
    {
        [XmlElement("Cliente")]
        public List<ClienteDto> Cliente { get; set; } = new List<ClienteDto>();
    }

    public class ClienteDto
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Nif { get; set; } = "";
        public string Username { get; set; } = "";
        public string Rol { get; set; } = "";
    }
}
