using Microsoft.EntityFrameworkCore;
using OP.Prueba.Application.Interfaces;
using OP.Prueba.Domain.Common;
using OP.Prueba.Domain.Entities;
using OP.Prueba.Persistence.Configuration;

public class ApplicationDbContext : DbContext
{
    private readonly IDateTimeService _dateTime;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        _dateTime = dateTime;
    }

    public DbSet<Persona> Personas { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Cuenta> Cuentas { get; set; }
    public DbSet<TipoCategoria> TipoCategorias { get; set; }
    public DbSet<TipoGeneral> TiposGenerales { get; set; }
    public DbSet<Movimiento> Movimientos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Herencia TPT: Persona -> Cliente
        modelBuilder.Entity<Persona>().ToTable("Persona");
        modelBuilder.Entity<Cliente>().ToTable("Cliente");

        // Seed Data (Tipos generales, categorias, etc.)
        modelBuilder.Seed();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-E6IA8RD;Initial Catalog=BancoDB;Integrated Security=True;MultipleActiveResultSets=true;Encrypt=False;");
        }

        optionsBuilder.EnableSensitiveDataLogging();
    }
}
