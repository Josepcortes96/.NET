using FitData.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
        public Usuario GetById(int id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
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
    }
}
