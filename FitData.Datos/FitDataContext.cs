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
        public DbSet<Cliente> Clientes { get; set; }

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
            base.OnModelCreating(modelBuilder);

            // Usuario: tabla y PK (clave ONLY en el root)
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);

            //  Cliente => entidad separada con PK = FK a Usuario
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Cliente>().HasKey(c => c.IdUsuario);

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Usuario)
                .WithOne(u => u.Cliente)   // si Usuario tiene la prop Cliente
                .HasForeignKey<Cliente>(c => c.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);


            // Otras entidades
            modelBuilder.Entity<Actividad>().ToTable("Actividad");
            modelBuilder.Entity<Actividad>().HasKey(a => a.IdActividad);

            modelBuilder.Entity<Horario>().ToTable("Horario");
            modelBuilder.Entity<Horario>().HasKey(h => h.IdHorario);

            modelBuilder.Entity<Reserva>().ToTable("Reserva");
            modelBuilder.Entity<Reserva>().HasKey(r => r.IdReserva);

            modelBuilder.Entity<ListaEspera>().ToTable("ListaEspera");
            modelBuilder.Entity<ListaEspera>().HasKey(l => l.IdLista);
        }

    }
}
