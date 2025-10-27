using FitData.Entidades;
using Microsoft.EntityFrameworkCore;

namespace FitData.Datos;

public class FitDataContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=fitdata;User Id=sa;Password=C4mbiami!;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>().ToTable("Usuario");
        modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);
    }
}
