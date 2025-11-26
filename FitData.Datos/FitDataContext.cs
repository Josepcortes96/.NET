using FitData.Entidades;
using Microsoft.EntityFrameworkCore;

namespace FitData.Datos
{
    public class FitDataContext : DbContext
    {
        public FitDataContext() { }

        public FitDataContext(DbContextOptions<FitDataContext> options)
            : base(options) { }

        // TABLAS PRINCIPALES
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<ListaEspera> ListaEsperas { get; set; }

        // TABLAS POR ROL
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<MonitorUsuario> Monitores { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Recepcionista> Recepcionistas { get; set; }
        public DbSet<Encargado> Encargados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=localhost,1433;Database=fitdata;User Id=sa;Password=C4mbiami!;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =============================
            // USUARIO
            // =============================
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);

            // =============================
            // CLIENTE (PK = FK IdUsuario)
            // =============================
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Cliente>().HasKey(c => c.IdUsuario);

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Usuario)
                .WithMany() // EVITA crear ClienteldUsuario
                .HasForeignKey(c => c.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // =============================
            // MONITORUSUARIO
            // =============================
            modelBuilder.Entity<MonitorUsuario>().ToTable("Monitores");
            modelBuilder.Entity<MonitorUsuario>().HasKey(m => m.IdUsuario);

            modelBuilder.Entity<MonitorUsuario>()
                .HasOne(m => m.Usuario)
                .WithMany()
                .HasForeignKey(m => m.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // =============================
            // ADMINISTRADOR
            // =============================
            modelBuilder.Entity<Administrador>().ToTable("Administrador");
            modelBuilder.Entity<Administrador>().HasKey(a => a.IdUsuario);

            modelBuilder.Entity<Administrador>()
                .HasOne(a => a.Usuario)
                .WithMany()
                .HasForeignKey(a => a.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // =============================
            // RECEPCIONISTA
            // =============================
            modelBuilder.Entity<Recepcionista>().ToTable("Recepcionista");
            modelBuilder.Entity<Recepcionista>().HasKey(r => r.IdUsuario);

            modelBuilder.Entity<Recepcionista>()
                .HasOne(r => r.Usuario)
                .WithMany()
                .HasForeignKey(r => r.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // =============================
            // ENCARGADO
            // =============================
            modelBuilder.Entity<Encargado>().ToTable("Encargado");
            modelBuilder.Entity<Encargado>().HasKey(e => e.IdUsuario);

            modelBuilder.Entity<Encargado>()
                .HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // =============================
            // OTRAS TABLAS
            // =============================
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
