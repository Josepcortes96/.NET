using System;
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

        public List<Horario> GetAll()
        {
            return _context.Horarios.ToList();
        }

        public List<Horario> GetByActividad(int idActividad)
        {
            return _context.Horarios.Where(h => h.IdActividad == idActividad).ToList();
        }

        // Nuevo: obtener horarios de una actividad para un día concreto (date.Date comparado con HoraInicio.Date)
        public List<Horario> GetByActividadAndDate(int idActividad, DateTime date)
        {
            var d = date.Date;
            return _context.Horarios
                .Where(h => h.IdActividad == idActividad && h.HoraInicio.Date == d)
                .OrderBy(h => h.HoraInicio)
                .ToList();
        }

        public void Add(Horario h)
        {
            if (h.PlazasTotales == 0) h.PlazasTotales = 16;
            _context.Horarios.Add(h);
            _context.SaveChanges();
        }

        public void Update(Horario h)
        {
            _context.Horarios.Update(h);
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
