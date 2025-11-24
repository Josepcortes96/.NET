using FitData.Datos;
using FitData.Datos.Repositorios;
using FitData.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class ActividadRepositoryTests
{
    private FitDataContext GetContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<FitDataContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new FitDataContext(options);
    }

    [Fact]
    public void Add_ShouldInsertActividad()
    {
        var ctx = GetContext("AddActividadDB");
        var repo = new ActividadRepository(ctx);

        var act = new Actividad
        {
            Nombre = "Zumba",
            Descripcion = "Clase de baile",
            NivelIntensidad = "Media",
            Sala = "Sala 1",
            IdMonitor = 5,
            IdEncargado = 3
        };

        repo.Add(act);

        Assert.Equal(1, ctx.Actividades.Count());
    }

    [Fact]
    public void GetByMonitor_ShouldReturnCorrectItems()
    {
        var ctx = GetContext("GetByMonitorDB");
        var repo = new ActividadRepository(ctx);

        ctx.Actividades.AddRange(
            new Actividad { IdMonitor = 1, Nombre = "Yoga" },
            new Actividad { IdMonitor = 2, Nombre = "Zumba" },
            new Actividad { IdMonitor = 1, Nombre = "Pilates" }
        );
        ctx.SaveChanges();

        var result = repo.GetByMonitor(1);

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void Delete_ShouldRemoveActividad()
    {
        var ctx = GetContext("DeleteActividadDB");
        var repo = new ActividadRepository(ctx);

        ctx.Actividades.Add(new Actividad { IdActividad = 10, Nombre = "Crossfit" });
        ctx.SaveChanges();

        repo.Delete(10);

        Assert.Empty(ctx.Actividades);
    }
}
