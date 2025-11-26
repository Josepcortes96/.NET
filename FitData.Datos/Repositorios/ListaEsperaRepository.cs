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

        // Obtener lista ordenada por posición
        public List<ListaEspera> GetByHorario(int idHorario)
        {
            return _context.ListaEsperas
                .Where(l => l.IdHorario == idHorario)
                .OrderBy(l => l.Posicion)
                .ToList();
        }

        // Añadir verificando duplicados + posicion correcta
        public void Add(ListaEspera l)
        {
            //  Evitar duplicados
            bool alreadyExists = _context.ListaEsperas
                .Any(e => e.IdCliente == l.IdCliente && e.IdHorario == l.IdHorario);

            if (alreadyExists)
                throw new InvalidOperationException("El cliente ya está en la lista de espera.");

            //  Asignar posición automática correcta
            int nextPos = _context.ListaEsperas
                .Count(e => e.IdHorario == l.IdHorario) + 1;

            l.Posicion = nextPos;

            _context.ListaEsperas.Add(l);
            _context.SaveChanges();
        }

        // Eliminar entrada
        public void Delete(int idLista)
        {
            var item = _context.ListaEsperas.FirstOrDefault(x => x.IdLista == idLista);
            if (item != null)
            {
                _context.ListaEsperas.Remove(item);
                _context.SaveChanges();
            }
        }

        // Devuelve el primero en la cola (posición 1)
        public ListaEspera GetFirstInQueue(int idHorario)
        {
            return _context.ListaEsperas
                .Where(l => l.IdHorario == idHorario)
                .OrderBy(l => l.Posicion)
                .FirstOrDefault();
        }
        
        // Reordenar posiciones tras borrar uno
        public void ReorderPositions(int idHorario)
        {
            var list = _context.ListaEsperas
                .Where(l => l.IdHorario == idHorario)
                .OrderBy(l => l.Posicion)
                .ToList();

            int pos = 1;
            foreach (var item in list)
            {
                item.Posicion = pos++;
                _context.ListaEsperas.Update(item);
            }
            _context.SaveChanges();
        }

        // Obtener todas las listas donde está un cliente
        public List<ListaEspera> GetAllByCliente(int idCliente)
        {
            return _context.ListaEsperas
                .Where(l => l.IdCliente == idCliente)
                .OrderBy(l => l.IdHorario)
                .ToList();
        }

        // Obtener TODAS las entradas de lista de espera 
        public List<ListaEspera> GetAll()
        {
            return _context.ListaEsperas
                .OrderBy(l => l.IdHorario)
                .ThenBy(l => l.Posicion)
                .ToList();
        }

    }
}
