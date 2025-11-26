using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace FitData.Datos.Xml
{
    internal class XmlHelper
    {
        public static void ExportUsuarios(List<UsuarioXml> usuarios, string path)
        {
            var serializer = new XmlSerializer(typeof(List<UsuarioXml>), new XmlRootAttribute("Usuarios"));
            using var stream = File.Create(path);
            serializer.Serialize(stream, usuarios);
        }

        public static List<UsuarioXml> ImportUsuarios(string path)
        {
            var serializer = new XmlSerializer(typeof(List<UsuarioXml>), new XmlRootAttribute("Usuarios"));
            using var stream = File.OpenRead(path);
            return (List<UsuarioXml>)serializer.Deserialize(stream)!;
        }
    }
}
