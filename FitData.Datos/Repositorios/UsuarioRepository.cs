using FitData.Entidades;
using Microsoft.EntityFrameworkCore;

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

        // READ
        public List<Usuario> GetAll()
        {
            return _context.Usuarios.ToList();
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