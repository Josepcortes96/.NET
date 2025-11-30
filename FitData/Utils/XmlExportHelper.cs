using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FitData.Utils
{
    public static class XmlExportHelper
    {
        /// <summary>
        /// Serializa un objeto T a XML (UTF-8) y opcionalmente valida el XML resultante contra un XSD.
        /// Lanza excepciones si ocurre algún error.
        /// </summary>
        public static void SerializeToXml<T>(T obj, string filePath, string xsdPath = null)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath));

            var xmlSerializer = new XmlSerializer(typeof(T));

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "http://example.org/gentefit");

            var settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = System.Text.Encoding.UTF8
            };

            // Serializar a fichero
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            using (var writer = XmlWriter.Create(fs, settings))
            {
                xmlSerializer.Serialize(writer, obj, ns);
            }

            // Validar si se proporcionó XSD
            if (!string.IsNullOrEmpty(xsdPath))
            {
                ValidateXmlAgainstXsd(filePath, xsdPath);
            }
        }

        /// <summary>
        /// Valida un XML (archivo) contra un XSD (archivo). Lanza excepciones en errores.
        /// </summary>
        public static void ValidateXmlAgainstXsd(string xmlFilePath, string xsdFilePath)
        {
            if (!File.Exists(xmlFilePath))
                throw new FileNotFoundException("XML no encontrado: " + xmlFilePath);
            if (!File.Exists(xsdFilePath))
                throw new FileNotFoundException("XSD no encontrado: " + xsdFilePath);

            var schemas = new XmlSchemaSet();
            schemas.Add("http://example.org/gentefit", xsdFilePath);

            var settings = new XmlReaderSettings { Schemas = schemas, ValidationType = ValidationType.Schema };

            settings.ValidationEventHandler += (sender, e) =>
            {
                // lanzar una excepción para que el llamador pueda capturar y mostrar
                throw new Exception($"XML Validation error: {e.Message}");
            };

            using (var reader = XmlReader.Create(xmlFilePath, settings))
            {
                while (reader.Read()) { /* leer todo para forzar validación */ }
            }
        }
    }
}
