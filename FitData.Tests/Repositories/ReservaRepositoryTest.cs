using FitData.Datos;
using FitData.Datos.Repositorios;
using FitData.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

public class ReservaRepositoryTests
{
    private FitDataContext GetContext(string dbName)
    {
        var opts = new DbContextOptionsBuilder<FitDataContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        return new FitDataContext(opts);
    }

    [Fact]
    public void Add_ShouldIncreasePlazas()
    {
        var ctx = GetContext("ReservaAddDB");
        var repo = new ReservaRepository(ctx);

        ctx.Clientes.Add(new Cliente { IdUsuario = 5 });
        ctx.Horarios.Add(new Horario
        {
            IdHorario = 1,
            IdActividad = 2,
            PlazasTotales = 5,
            PlazasOcupadas = 0
        });
        ctx.SaveChanges();

        var reserva = new Reserva
        {
            IdCliente = 5,
            IdHorario = 1,
            FechaReserva = DateTime.Now
        };

        repo.Add(reserva);

        var h = ctx.Horarios.First();
        Assert.Equal(1, h.PlazasOcupadas);
    }

    [Fact]
    public void Add_ShouldThrow_WhenNoPlazas()
    {
        var ctx = GetContext("ReservaFullDB");
        var repo = new ReservaRepository(ctx);

        ctx.Clientes.Add(new Cliente { IdUsuario = 1 });
        ctx.Horarios.Add(new Horario
        {
            IdHorario = 10,
            PlazasTotales = 1,
            PlazasOcupadas = 1
        });
        ctx.SaveChanges();

        var reserva = new Reserva { IdCliente = 1, IdHorario = 10 };

        Assert.Throws<InvalidOperationException>(() => repo.Add(reserva));
    }

    [Fact]
    public void Cancelar_ShouldSetEstadoCancelada()
    {
        var ctx = GetContext("ReservaCancelDB");
        var repo = new ReservaRepository(ctx);

        ctx.Reservas.Add(new Reserva { IdReserva = 99, Estado = "confirmada" });
        ctx.SaveChanges();

        repo.Cancelar(99);

        Assert.Equal("cancelada", ctx.Reservas.First().Estado);
    }
}
