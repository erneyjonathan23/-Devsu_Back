using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OP.Prueba.Domain.Entities;

namespace OP.Prueba.Persistence.Configuration
{
    public class MovimientoConfig : IEntityTypeConfiguration<Movimiento>
    {
        public void Configure(EntityTypeBuilder<Movimiento> builder)
        {
            builder.ToTable("Movimiento");

            builder.HasKey(e => e.MovimientoId).HasName("PK__Movimiento__3214EC078BF374BA");

            builder.Property(e => e.MovimientoId)
                .IsRequired(true)
                .HasColumnType("int")
                .UseIdentityColumn();

            builder.Property(e => e.Fecha)
                .IsRequired(true)
                .HasColumnType("datetime");

            builder.Property(e => e.TipoMovimientoId)
                .IsRequired(true)
                .HasColumnType("int");

            builder.Property(e => e.CuentaId)
                .IsRequired(true)
                .HasColumnType("int");

            builder.Property(e => e.Valor)
                .IsRequired(true)
                .HasColumnType("decimal");

            builder.Property(e => e.Saldo)
                .IsRequired(true)
                .HasColumnType("decimal");

            builder.HasOne(d => d.TipoMovimiento).WithMany()
                .HasForeignKey(d => d.TipoMovimientoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Cuenta).WithMany()
                .HasForeignKey(d => d.CuentaId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
