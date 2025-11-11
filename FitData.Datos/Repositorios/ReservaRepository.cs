using System;
using System.Collections.Generic;
using System.Linq;
using FitData.Entidades;

namespace FitData.Datos.Repositorios
{
    public enum ReservationResult
    {
        Confirmed,
        AddedToWaitingList,
        AlreadyReserved,
        Error
    }

    public class ReservaRepository
    {
        private readonly FitDataContext _context;
        private readonly ListaEsperaRepository _listaRepo;
        private readonly HorarioRepository _horarioRepo;

        public ReservaRepository(FitDataContext context)
        {
            _context = context;
            _listaRepo = new ListaEsperaRepository(context);
            _horarioRepo = new HorarioRepository(context);
        }

        // Obtiene las reservas de un cliente
        public List<Reserva> GetByCliente(int idCliente)
        {
            return _context.Reservas.Where(r => r.IdCliente == idCliente).ToList();
        }

        // Añade reserva: si hay plazas se confirma; si no, se añade a lista de espera
        public ReservationResult TryAddReservation(int idCliente, int idHorario)
        {
            // comprobar si ya tiene reserva para ese horario
            bool yaReservado = _context.Reservas.Any(r => r.IdCliente == idCliente && r.IdHorario == idHorario);
            if (yaReservado) return ReservationResult.AlreadyReserved;

            // obtener horario
            var horario = _context.Horarios.FirstOrDefault(h => h.IdHorario == idHorario);
            if (horario == null) return ReservationResult.Error;

            int limite = horario.PlazasTotales > 0 ? horario.PlazasTotales : 16; // default 16
            if (horario.PlazasOcupadas < limite)
            {
                // Confirmar reserva
                var reserva = new Reserva
                {
                    IdCliente = idCliente,
                    IdHorario = idHorario,
                    FechaReserva = DateTime.UtcNow,
                    Estado = "confirmada"
                };
                _context.Reservas.Add(reserva);
                horario.PlazasOcupadas += 1;
                _context.Horarios.Update(horario);
                _context.SaveChanges();
                return ReservationResult.Confirmed;
            }
            else
            {
                // Añadir a lista de espera
                var item = new ListaEspera
                {
                    IdCliente = idCliente,
                    IdHorario = idHorario,
                    Posicion = GetNextWaitingPosition(idHorario)
                };
                _listaRepo.Add(item);
                return ReservationResult.AddedToWaitingList;
            }
        }

        // Cancela una reserva por idReserva; si hay alguien en lista de espera promueve al primero
        public bool CancelReservation(int idReserva)
        {
            var reserva = _context.Reservas.FirstOrDefault(r => r.IdReserva == idReserva);
            if (reserva == null) return false;

            int horarioId = reserva.IdHorario;
            // eliminar reserva
            _context.Reservas.Remove(reserva);

            // decrementar plazas ocupadas (si >0)
            var horario = _context.Horarios.FirstOrDefault(h => h.IdHorario == horarioId);
            if (horario != null && horario.PlazasOcupadas > 0)
                horario.PlazasOcupadas -= 1;

            _context.SaveChanges();

            // promover primer de lista de espera si existe
            var first = _listaRepo.GetFirstInQueue(horarioId);
            if (first != null)
            {
                // crear reserva para esa persona
                var nueva = new Reserva
                {
                    IdCliente = first.IdCliente,
                    IdHorario = horarioId,
                    FechaReserva = DateTime.UtcNow,
                    Estado = "confirmada"
                };
                _context.Reservas.Add(nueva);

                // actualizar plazas ocupadas
                if (horario != null) horario.PlazasOcupadas += 1;

                // eliminar el item de lista de espera y reordenar posiciones
                _listaRepo.Delete(first.IdLista);
                _listaRepo.ReorderPositions(horarioId);

                _context.SaveChanges();
            }

            return true;
        }

        private int GetNextWaitingPosition(int idHorario)
        {
            var last = _context.ListaEsperas.Where(l => l.IdHorario == idHorario).OrderByDescending(l => l.Posicion).FirstOrDefault();
            return last == null ? 1 : last.Posicion + 1;
        }
    }
}
