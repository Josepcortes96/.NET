using System;
using System.Collections.Generic;
using System.IO;
using FitData.Datos.Xml;

namespace FitData
{
    public static class XmlTestHelper
    {
        public static GenteFitExchange CrearEjemploGenteFitExchange()
        {
            return new GenteFitExchange
            {
                InfoLote = new InfoLote
                {
                    IdLote = "GF-LOTE-0001",
                    GeneradoEn = DateTime.Now,
                    SistemaOrigen = "GenteFitApp",
                    Descripcion = "Lote de prueba para comprobar XSD + XML"
                },

                Usuarios = new Usuarios
                {
                    UsuarioList = new List<UsuarioXml>
                    {
                        new UsuarioXml
                        {
                            IdUsuario = 1,
                            Nombre = "María",
                            Apellido = "García",
                            Nif = "12345678A",
                            Rol = "Cliente",
                            Username = "maria",
                            Email = "maria@example.com",
                            Telefono = "+34911223344",
                            FechaNacimiento = new DateTime(1990, 5, 10)
                        }
                    }
                },

                Actividades = new Actividades
                {
                    ActividadList = new List<ActividadXml>
                    {
                        new ActividadXml
                        {
                            IdActividad = "ACT-1001",
                            NombreActividad = "Zumba",
                            Descripcion = "Clase de Zumba de prueba",
                            Intensidad = "Alta",
                            Sala = "Sala A",
                            Plazas = 16
                        }
                    }
                },

                Reservas = new Reservas
                {
                    ReservaList = new List<ReservaXml>
                    {
                        new ReservaXml
                        {
                            IdReserva = "RES-2001",
                            IdUsuario = 1,
                            IdActividad = "ACT-1001",
                            FechaHora = DateTime.Now.AddDays(1),
                            Estado = "confirmada",
                            Notas = "Reserva de prueba"
                        }
                    }
                }
            };
        }

        public static void ProbarExportacion()
        {
            var exchange = CrearEjemploGenteFitExchange();

            string outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Xml");
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            string outputPath = Path.Combine(outputDir, "lote_prueba.xml");

            XmlHelper.ExportExchange(exchange, outputPath, validateBefore: true);
        }
    }
}