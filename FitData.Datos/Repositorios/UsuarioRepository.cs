using FitData.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using FitData.Datos.Xml;

namespace FitData.Datos.Repositorios
{
    public class UsuarioRepository
    {
        private readonly FitDataContext _context;

        public UsuarioRepository(FitDataContext context)
        {
            _context = context;
        }

        // CREATE
        public void Add(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        // READ ALL
        public List<Usuario> GetAll()
        {
            return _context.Usuarios.ToList();
        }

        // ✅ READ BY ID (nuevo método)
        public Usuario? GetByUsername(string username)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Username == username);
        }

        // UPDATE
        public void Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        // DELETE
        public void Delete(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
        }

        // EXPORT: guardar en XML (sin password)
        public void ExportToXml(string path)
        {
            var list = _context.Usuarios
                .Select(u => new UsuarioXml
                {
                    IdUsuario = u.IdUsuario,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Nif = u.Nif,
                    Rol = u.Rol,
                    Username = u.Username
                })
                .ToList();

            XmlHelper.ExportUsuarios(list, path);
        }

        // IMPORT: leer XML e insertar/actualizar en DB
        // strategy: si IdUsuario > 0 y existe -> update; si no -> insert
        public void ImportFromXml(string path)
        {
            var usuariosXml = XmlHelper.ImportUsuarios(path);
            foreach (var ux in usuariosXml)
            {
                var existing = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == ux.IdUsuario);
                if (existing != null)
                {
                    existing.Nombre = ux.Nombre;
                    existing.Apellido = ux.Apellido;
                    existing.Nif = ux.Nif;
                    existing.Rol = ux.Rol;
                    existing.Username = ux.Username;
                    _context.Usuarios.Update(existing);
                }
                else
                {
                    var nuevo = new Usuario
                    {
                        Nombre = ux.Nombre,
                        Apellido = ux.Apellido,
                        Nif = ux.Nif,
                        Rol = ux.Rol,
                        Username = ux.Username,
                        Password = "" // dejar vacío o asignar un password temporal
                    };
                    _context.Usuarios.Add(nuevo);
                }
            }
            _context.SaveChanges();
        }
     }
}
