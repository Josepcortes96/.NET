// FitData.Datos/FitDataContext.cs
using FitData.Entidades;
using Microsoft.EntityFrameworkCore;

namespace FitData.Datos
{
    public class FitDataContext : DbContext
    {
        public FitDataContext()
        {
        }

        public FitDataContext(DbContextOptions<FitDataContext> options) : base(options)
        {
        }

        // --> Asegúrate de que todas estas propiedades existen:
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<ListaEspera> ListaEsperas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Si ya usas inyección de dependencias, podrías omitir esto.
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost,1433;Database=fitdata;User Id=sa;Password=C4mbiami!;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeos básicos - ajusta nombres de tablas si tu BD usa otros nombres
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);

            modelBuilder.Entity<Actividad>().ToTable("Actividad");
            modelBuilder.Entity<Actividad>().HasKey(a => a.IdActividad);

            modelBuilder.Entity<Horario>().ToTable("Horario");
            modelBuilder.Entity<Horario>().HasKey(h => h.IdHorario);

            modelBuilder.Entity<Reserva>().ToTable("Reserva");
            modelBuilder.Entity<Reserva>().HasKey(r => r.IdReserva);

            modelBuilder.Entity<ListaEspera>().ToTable("ListaEspera");
            modelBuilder.Entity<ListaEspera>().HasKey(l => l.IdLista);

            // Puedes añadir relaciones si lo deseas (FKs), ejemplo:
            // modelBuilder.Entity<Actividad>()
            //     .HasOne<Usuario>() // si quieres configurar navegación
            //     .WithMany()
            //     .HasForeignKey(a => a.IdEncargado)
            //     .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
