// FitData.Datos/Repositorios/HorarioRepository.cs
using System;
using System.Collections.Generic;
using System.Linq;
using FitData.Entidades;

namespace FitData.Datos.Repositorios
{
    public class HorarioRepository
    {
        private readonly FitDataContext _ctx;

        public HorarioRepository(FitDataContext ctx)
        {
            _ctx = ctx;
        }

        public List<Horario> GetAll()
        {
            return _ctx.Horarios.ToList();
        }

        public Horario? GetById(int id)
        {
            return _ctx.Horarios.FirstOrDefault(h => h.IdHorario == id);
        }

        public List<Horario> GetByActividad(int idActividad)
        {
            return _ctx.Horarios.Where(h => h.IdActividad == idActividad).ToList();
        }

        /// <summary>
        /// Devuelve horarios de una actividad filtrando por fecha (intenta usar DiaSemana si está rellenado).
        /// Si no hay coincidencias por día, devuelve todos los horarios de la actividad para evitar "lista vacía".
        /// </summary>
        public List<Horario> GetByActividadAndDate(int idActividad, DateTime date)
        {
            var q = _ctx.Horarios.Where(h => h.IdActividad == idActividad);
            var list = q.ToList();

            // Normaliza nombre del día (ej: "lunes" o "Monday" según cultura)
            var dayName = date.ToString("dddd", System.Globalization.CultureInfo.CurrentCulture).Trim();

            var filtered = list.Where(h =>
            {
                if (string.IsNullOrWhiteSpace(h.DiaSemana)) return true;
                return string.Equals(h.DiaSemana.Trim(), dayName, StringComparison.OrdinalIgnoreCase)
                       || string.Equals(h.DiaSemana.Trim(), date.ToString("dddd", System.Globalization.CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase);
            }).ToList();

            return filtered.Count > 0 ? filtered : list;
        }

        public void Add(Horario h)
        {
            _ctx.Horarios.Add(h);
            _ctx.SaveChanges();
        }

        public void Update(Horario h)
        {
            _ctx.Horarios.Update(h);
            _ctx.SaveChanges();
        }

        public void Delete(int idHorario)
        {
            var h = _ctx.Horarios.FirstOrDefault(x => x.IdHorario == idHorario);
            if (h != null)
            {
                _ctx.Horarios.Remove(h);
                _ctx.SaveChanges();
            }
        }
    }
}
