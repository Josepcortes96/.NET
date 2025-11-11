using System.Collections.Generic;
using System.Linq;
using FitData.Entidades;

namespace FitData.Datos.Repositorios
{
    public class HorarioRepository
    {
        private readonly FitDataContext _context;

        public HorarioRepository(FitDataContext context)
        {
            _context = context;
        }

        public List<Horario> GetByActividad(int idActividad)
        {
            return _context.Horarios
                .Where(h => h.IdActividad == idActividad)
                .ToList();
        }

        public void Add(Horario horario)
        {
            _context.Horarios.Add(horario);
            _context.SaveChanges();
        }

        public void Update(Horario horario)
        {
            _context.Horarios.Update(horario);
            _context.SaveChanges();
        }

        public void Delete(int idHorario)
        {
            var h = _context.Horarios.FirstOrDefault(x => x.IdHorario == idHorario);
            if (h != null)
            {
                _context.Horarios.Remove(h);
                _context.SaveChanges();
            }
        }
    }
}