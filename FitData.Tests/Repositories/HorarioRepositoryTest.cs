using FitData.Datos;
using FitData.Datos.Repositorios;
using FitData.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

public class HorarioRepositoryTests
{
    private FitDataContext GetContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<FitDataContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new FitDataContext(options);
    }

    [Fact]
    public void GetByActividad_ShouldReturnCorrectHorarios()
    {
        var ctx = GetContext("HorarioActividadDB");
        var repo = new HorarioRepository(ctx);

        ctx.Horarios.AddRange(
            new Horario { IdActividad = 1, DiaSemana = "Lunes" },
            new Horario { IdActividad = 2, DiaSemana = "Martes" },
            new Horario { IdActividad = 1, DiaSemana = "Miércoles" }
        );
        ctx.SaveChanges();

        var result = repo.GetByActividad(1);

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void Add_ShouldInsertHorario()
    {
        var ctx = GetContext("AddHorarioDB");
        var repo = new HorarioRepository(ctx);

        var h = new Horario
        {
            IdActividad = 1,
            DiaSemana = "Viernes",
            HoraInicio = TimeSpan.Parse("18:00"),
            HoraFin = TimeSpan.Parse("19:00"),
            PlazasTotales = 20
        };

        repo.Add(h);

        Assert.Equal(1, ctx.Horarios.Count());
    }

    [Fact]
    public void Delete_ShouldRemoveHorario()
    {
        var ctx = GetContext("DeleteHorarioDB");
        var repo = new HorarioRepository(ctx);

        ctx.Horarios.Add(new Horario { IdHorario = 50, DiaSemana = "Lunes" });
        ctx.SaveChanges();

        repo.Delete(50);

        Assert.Empty(ctx.Horarios);
    }
}
