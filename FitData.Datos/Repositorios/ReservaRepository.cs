using System.Collections.Generic;
using System.Linq;
using FitData.Entidades;

namespace FitData.Datos.Repositorios
{
    public class ReservaRepository
    {
        private readonly FitDataContext _context;

        public ReservaRepository(FitDataContext context)
        {
            _context = context;
        }

        public void Add(Reserva reserva)
        {
            _context.Reservas.Add(reserva);
            _context.SaveChanges();
        }

        public List<Reserva> GetByCliente(int idCliente)
        {
            return _context.Reservas
                .Where(r => r.IdCliente == idCliente)
                .ToList();
        }

        public void Cancelar(int idReserva)
        {
            var reserva = _context.Reservas.FirstOrDefault(r => r.IdReserva == idReserva);
            if (reserva != null)
            {
                reserva.Estado = "cancelada";
                _context.SaveChanges();
            }
        }
    }
}