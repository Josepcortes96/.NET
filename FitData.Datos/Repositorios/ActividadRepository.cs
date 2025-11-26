// FitData.Datos/Repositorios/ActividadRepository.cs
using System.Collections.Generic;
using System.Linq;
using FitData.Entidades;

namespace FitData.Datos.Repositorios
{
    public class ActividadRepository
    {
        private readonly FitDataContext _context;

        public ActividadRepository(FitDataContext context)
        {
            _context = context;
        }

        public List<Actividad> GetAll()
        {
            return _context.Actividades.ToList();
        }

        public List<Actividad> GetByEncargado(int idEncargado)
        {
            return _context.Actividades
                           .Where(a => a.IdEncargado.HasValue && a.IdEncargado.Value == idEncargado)
                           .ToList();
        }

        public List<Actividad> GetByMonitor(int idMonitor)
        {
            return _context.Actividades
                .Where(a => a.IdMonitor == idMonitor)
                .ToList();
        }


        public void Add(Actividad a)
        {
            _context.Actividades.Add(a);
            _context.SaveChanges();
        }

        public void Update(Actividad a)
        {
            _context.Actividades.Update(a);
            _context.SaveChanges();
        }

        public void Delete(int idActividad)
        {
            var act = _context.Actividades.FirstOrDefault(a => a.IdActividad == idActividad);
            if (act != null)
            {
                _context.Actividades.Remove(act);
                _context.SaveChanges();
            }
        }
    }
}
