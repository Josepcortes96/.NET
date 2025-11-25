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
            if (reserva == null) throw new ArgumentNullException(nameof(reserva));

            // Validaciones básicas
            if (reserva.IdCliente <= 0) throw new ArgumentException("IdCliente no válido.");
            if (reserva.IdHorario <= 0) throw new ArgumentException("IdHorario no válido.");
            if (reserva.FechaReserva == default) reserva.FechaReserva = DateTime.Now;
            if (string.IsNullOrWhiteSpace(reserva.Estado)) reserva.Estado = "confirmada";

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // comprobar existencia cliente y horario
                var cliente = _context.Clientes.Find(reserva.IdCliente);
                if (cliente == null)
                    throw new InvalidOperationException($"Cliente {reserva.IdCliente} no existe en la tabla Cliente.");

                var horario = _context.Horarios.Find(reserva.IdHorario);
                if (horario == null)
                    throw new InvalidOperationException($"Horario {reserva.IdHorario} no existe.");

                // comprobar plazas disponibles aquí si lo deseas (evitar overbooking)
                if (horario.PlazasOcupadas >= horario.PlazasTotales)
                    throw new InvalidOperationException("No hay plazas disponibles para este horario.");

                // Añadir reserva y actualizar plazas en la misma transacción
                _context.Reservas.Add(reserva);
                horario.PlazasOcupadas++;
                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                transaction.Rollback();
                var inner = ex.InnerException?.Message ?? ex.Message;
                System.Diagnostics.Debug.WriteLine("DbUpdateException en ReservaRepository.Add: " + inner);
                // Re-lanzamos una excepción más descriptiva para que la UI la muestre
                throw new InvalidOperationException("Error al guardar la reserva en la base de datos: " + inner, ex);
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
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