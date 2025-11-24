using FitData.Datos;
using FitData.Datos.Repositorios;
using FitData.Entidades;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;

public class ListaEsperaRepositoryTests
{
    private FitDataContext GetContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<FitDataContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        return new FitDataContext(options);
    }

    [Fact]
    public void Add_ShouldInsertInListaEspera()
    {
        var ctx = GetContext("ListaAddDB");
        var repo = new ListaEsperaRepository(ctx);

        repo.Add(new ListaEspera { IdHorario = 1, IdCliente = 2, Posicion = 1 });

        Assert.Single(ctx.ListaEsperas);
    }

    [Fact]
    public void GetByHorario_ShouldReturnOrdered()
    {
        var ctx = GetContext("ListaHorarioDB");
        var repo = new ListaEsperaRepository(ctx);

        ctx.ListaEsperas.AddRange(
            new ListaEspera { IdHorario = 10, Posicion = 3 },
            new ListaEspera { IdHorario = 10, Posicion = 1 },
            new ListaEspera { IdHorario = 10, Posicion = 2 }
        );
        ctx.SaveChanges();

        var result = repo.GetByHorario(10);

        Assert.Equal(1, result[0].Posicion);
        Assert.Equal(2, result[1].Posicion);
    }

    [Fact]
    public void ReorderPositions_ShouldFixPositions()
    {
        var ctx = GetContext("ListaReorderDB");
        var repo = new ListaEsperaRepository(ctx);

        ctx.ListaEsperas.AddRange(
            new ListaEspera { IdLista = 1, IdHorario = 5, Posicion = 5 },
            new ListaEspera { IdLista = 2, IdHorario = 5, Posicion = 9 }
        );
        ctx.SaveChanges();

        repo.ReorderPositions(5);

        var items = ctx.ListaEsperas.OrderBy(x => x.Posicion).ToList();
        Assert.Equal(1, items[0].Posicion);
        Assert.Equal(2, items[1].Posicion);
    }
}
