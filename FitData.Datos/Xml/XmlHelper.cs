using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FitData.Datos.Xml
{
    /// <summary>
    /// Xml helper robusto:
    /// - Serializa/Deserializa el objeto GenteFitExchange
    /// - Intenta localizar el XSD en varias rutas
    /// - Si no encuentra el XSD, no falla: lo registra y continúa (no valida)
    /// - Soporta recurso embebido como fallback
    /// </summary>
    public static class XmlHelper
    {
        private const string DefaultXsdFileName = "gentefit_exchange.xsd";

        // Public helpers (Usuarios simple)
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

        // ----------------------------
        // Export / Import del lote completo
        // ----------------------------
        public static void ExportExchange(GenteFitExchange exchange, string path, bool validateBefore = true)
        {
            // Asegurar carpeta existente
            var dir = Path.GetDirectoryName(path) ?? ".";
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            // Serializa primero (siempre)
            var serializer = new XmlSerializer(typeof(GenteFitExchange));
            using (var stream = File.Create(path))
            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 }))
            {
                serializer.Serialize(writer, exchange);
            }

            // Intentar validar; si no puede encontrarse XSD => registrar y continuar
            if (validateBefore)
            {
                try
                {
                    string? xsdPath = LocateXsdOrExtract(DefaultXsdFileName);
                    if (xsdPath == null)
                    {
                        // Registrar advertencia (no lanzar)
                        WriteLog($"WARNING: XSD '{DefaultXsdFileName}' not found; skipping validation. Expected file copied into Xml/ folder?");
                    }
                    else
                    {
                        ValidateXmlWithXsd(path, xsdPath);
                        WriteLog($"Validation OK for '{path}' using XSD '{xsdPath}'.");
                    }
                }
                catch (Exception ex)
                {
                    // Registrar excepción completa, pero no lanzar: el XML ya fue generado.
                    WriteLog("Validation error (caught): " + ex.ToString());
                }
            }
        }

        public static GenteFitExchange ImportExchange(string path, bool validateFirst = true)
        {
            if (validateFirst)
            {
                try
                {
                    string? xsdPath = LocateXsdOrExtract(DefaultXsdFileName);
                    if (xsdPath != null) ValidateXmlWithXsd(path, xsdPath);
                    else WriteLog($"WARNING: XSD not found; skipping validation for import '{path}'.");
                }
                catch (Exception ex)
                {
                    WriteLog("Import validation error (caught): " + ex.ToString());
                }
            }

            var serializer = new XmlSerializer(typeof(GenteFitExchange));
            using var stream = File.OpenRead(path);
            return (GenteFitExchange)serializer.Deserialize(stream)!;
        }

        // ----------------------------
        // VALIDACIÓN XSD
        // ----------------------------
        private static void ValidateXmlWithXsd(string xmlPath, string xsdPath)
        {
            if (!File.Exists(xmlPath)) throw new FileNotFoundException("XML file not found", xmlPath);
            if (!File.Exists(xsdPath)) throw new FileNotFoundException("XSD file not found", xsdPath);

            var settings = new XmlReaderSettings();
            settings.Schemas.Add(null, xsdPath);
            settings.ValidationType = ValidationType.Schema;

            var errors = new List<string>();
            settings.ValidationEventHandler += (sender, e) =>
            {
                errors.Add($"{e.Severity}: {e.Message}");
            };

            using (var reader = XmlReader.Create(xmlPath, settings))
            {
                while (reader.Read()) { }
            }

            if (errors.Count > 0)
            {
                var msg = $"XML validation failed. Errors:\n{string.Join("\n", errors)}";
                throw new InvalidOperationException(msg);
            }
        }

        // ----------------------------
        // Localización robusta del XSD
        // - Comprueba múltiples rutas probables
        // - Si no hay archivo en disco, intenta extraer recurso embebido a un temp y devolver la ruta temporal
        // - Si aún no hay nada, devuelve null
        // ----------------------------
        private static string? LocateXsdOrExtract(string xsdFileName)
        {
            var triedPaths = new List<string>();

            // 1) ruta junto al ejecutable: <AppDomain.BaseDirectory>/Xml/
            var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
            triedPaths.Add(Path.Combine(baseDir, "Xml", xsdFileName));

            // 2) ruta CurrentDirectory/Xml
            triedPaths.Add(Path.Combine(Directory.GetCurrentDirectory(), "Xml", xsdFileName));

            // 3) ruta relativa para entornos dev (subir niveles)
            triedPaths.Add(Path.Combine(baseDir, "..", "..", "FitData.Datos", "Xml", xsdFileName));
            triedPaths.Add(Path.Combine(baseDir, "..", "FitData.Datos", "Xml", xsdFileName));
            triedPaths.Add(Path.Combine(baseDir, "FitData.Datos", "Xml", xsdFileName));

            // 4) ruta de la DLL FitData.Datos (si está cargada)
            try
            {
                var asm = Assembly.Load("FitData.Datos");
                if (asm != null)
                {
                    var asmPath = Path.GetDirectoryName(asm.Location) ?? "";
                    triedPaths.Add(Path.Combine(asmPath, "Xml", xsdFileName));
                }
            }
            catch { /* ignore */ }

            // Normalizar y buscar la primera existente
            foreach (var p in triedPaths.Distinct())
            {
                try
                {
                    var full = Path.GetFullPath(p);
                    if (File.Exists(full)) return full;
                }
                catch { /* ignore path issues */ }
            }

            // Si no existe en disco: buscar recurso embebido en los ensamblados (FitData.Datos y este assembly)
            var assembliesToCheck = new[] { Assembly.GetExecutingAssembly() };
            try
            {
                assembliesToCheck = assembliesToCheck.Concat(new[] { Assembly.Load("FitData.Datos") }).ToArray();
            }
            catch { /* ignore if not present */ }

            foreach (var asm in assembliesToCheck)
            {
                try
                {
                    var resourceName = asm.GetManifestResourceNames()
                                          .FirstOrDefault(n => n.EndsWith(xsdFileName, StringComparison.OrdinalIgnoreCase));
                    if (resourceName != null)
                    {
                        // extraer a temp
                        var tempDir = Path.Combine(Path.GetTempPath(), "GenteFitXml");
                        Directory.CreateDirectory(tempDir);
                        var outPath = Path.Combine(tempDir, xsdFileName);
                        using (var rs = asm.GetManifestResourceStream(resourceName))
                        using (var fs = File.Create(outPath))
                        {
                            rs!.CopyTo(fs);
                        }
                        return outPath;
                    }
                }
                catch { /* ignore per-assembly errors */ }
            }

            // Nada encontrado: escribir log con rutas probadas para depuración y devolver null
            try
            {
                WriteLog("XSD not found. Tried paths:\n" + string.Join("\n", triedPaths.Distinct().Select(p => {
                    try { return Path.GetFullPath(p); } catch { return p; }
                })));
            }
            catch { /* ignore logging error */ }

            return null;
        }

        // ----------------------------
        // Simple logging dentro de Xml/diagnostico_xsd_paths.txt
        // ----------------------------
        private static void WriteLog(string message)
        {
            try
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
                var xmlDir = Path.Combine(baseDir, "Xml");
                Directory.CreateDirectory(xmlDir);
                var logPath = Path.Combine(xmlDir, "diagnostico_xsd_paths.txt");
                var sb = new StringBuilder();
                sb.AppendLine($"[{DateTime.Now}] {message}");
                File.AppendAllText(logPath, sb.ToString());
            }
            catch { /* best-effort logging; ignore failures */ }
        }
    }
}