using System.Collections.Generic;
using System.Linq;
using FitData.Entidades;

namespace FitData.Datos.Repositorios
{
    public class ListaEsperaRepository
    {
        private readonly FitDataContext _context;

        public ListaEsperaRepository(FitDataContext context)
        {
            _context = context;
        }

        public List<ListaEspera> GetByHorario(int idHorario)
        {
            return _context.ListaEsperas
                           .Where(l => l.IdHorario == idHorario)
                           .OrderBy(l => l.Posicion)
                           .ToList();
        }

        public void Add(ListaEspera l)
        {
            _context.ListaEsperas.Add(l);
            _context.SaveChanges();
        }

        public void Delete(int idLista)
        {
            var item = _context.ListaEsperas.FirstOrDefault(x => x.IdLista == idLista);
            if (item != null)
            {
                _context.ListaEsperas.Remove(item);
                _context.SaveChanges();
            }
        }

        // Obtener primer elemento en la fila (posición 1)
        public ListaEspera GetFirstInQueue(int idHorario)
        {
            return _context.ListaEsperas.Where(l => l.IdHorario == idHorario)
                                       .OrderBy(l => l.Posicion)
                                       .FirstOrDefault();
        }
         
        // Reasigna posiciones empezando en 1 para un horario concreto
        public void ReorderPositions(int idHorario)
        {
            var list = _context.ListaEsperas.Where(l => l.IdHorario == idHorario).OrderBy(l => l.Posicion).ToList();
            int pos = 1;
            foreach (var item in list)
            {
                item.Posicion = pos++;
                _context.ListaEsperas.Update(item);
            }
            _context.SaveChanges();
        }

        // Obtener todas las entradas donde el cliente está en lista de espera
        public List<ListaEspera> GetAllByCliente(int idCliente)
        {
            return _context.ListaEsperas.Where(l => l.IdCliente == idCliente).OrderBy(l => l.IdHorario).ToList();
        }
    }
}
