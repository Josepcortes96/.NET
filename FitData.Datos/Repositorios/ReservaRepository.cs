using System;
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

            if (reserva.IdCliente <= 0) 
                throw new ArgumentException("IdCliente no válido.");
            if (reserva.IdHorario <= 0) 
                throw new ArgumentException("IdHorario no válido.");

            if (reserva.FechaReserva == default)
                reserva.FechaReserva = DateTime.Now;

            if (string.IsNullOrWhiteSpace(reserva.Estado))
                reserva.Estado = "confirmada";

            var horario = _context.Horarios.Find(reserva.IdHorario);
            if (horario == null)
                throw new InvalidOperationException($"Horario {reserva.IdHorario} no existe.");

            var cliente = _context.Clientes.Find(reserva.IdCliente);
            if (cliente == null)
                throw new InvalidOperationException($"Cliente {reserva.IdCliente} no existe.");

            // -----------------------------
            // SI HAY PLAZAS => RESERVA NORMAL
            // -----------------------------
            if (horario.PlazasOcupadas < horario.PlazasTotales)
            {
                using var tx = _context.Database.BeginTransaction();
                try
                {
                    _context.Reservas.Add(reserva);
                    horario.PlazasOcupadas++;

                    _context.SaveChanges();
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw new InvalidOperationException("Error al guardar la reserva: " + ex.Message);
                }

                return;
            }

            // -----------------------------
            // SI NO HAY PLAZAS => LISTA ESPERA
            // -----------------------------
            var listaRepo = new ListaEsperaRepository(_context);
            int pos = listaRepo.GetByHorario(reserva.IdHorario).Count + 1;

            var entry = new ListaEspera
            {
                IdCliente = reserva.IdCliente,
                IdHorario = reserva.IdHorario,
                Posicion = pos
            };

            listaRepo.Add(entry);

            throw new InvalidOperationException(
                $"Actividad llena. Cliente añadido a lista de espera. Posición {pos}."
            );
        }


        public List<Reserva> GetByCliente(int idCliente)
        {
            return _context.Reservas
                .Where(r => r.IdCliente == idCliente)
                .ToList();
        }

        // ---------------------------------
        // CANCELACIÓN: LIBERA PLAZA Y
        // AUTOINVITA AL SIGUIENTE EN LISTA
        // ---------------------------------
        public void Cancelar(int idReserva)
        {
            var reserva = _context.Reservas.FirstOrDefault(r => r.IdReserva == idReserva);
            if (reserva == null) return;

            var horario = _context.Horarios.FirstOrDefault(h => h.IdHorario == reserva.IdHorario);
            if (horario == null) return;

            using var tx = _context.Database.BeginTransaction();
            try
            {
                reserva.Estado = "cancelada";
                horario.PlazasOcupadas--;

                _context.SaveChanges();

                // mover lista de espera
                var listaRepo = new ListaEsperaRepository(_context);
                var primero = listaRepo.GetFirstInQueue(horario.IdHorario);

                if (primero != null)
                {
                    // crear reserva nueva automática
                    var nueva = new Reserva
                    {
                        IdCliente = primero.IdCliente,
                        IdHorario = horario.IdHorario,
                        FechaReserva = DateTime.Now,
                        Estado = "confirmada"
                    };

                    _context.Reservas.Add(nueva);
                    horario.PlazasOcupadas++;

                    // borrar de espera
                    listaRepo.Delete(primero.IdLista);
                    listaRepo.ReorderPositions(horario.IdHorario);

                    _context.SaveChanges();
                }

                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }
    }
}
