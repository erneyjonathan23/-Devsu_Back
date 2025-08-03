using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OP.Prueba.Domain.Entities;
using System.Reflection.Emit;

namespace OP.Prueba.Persistence.Configuration
{
    public class CuentaConfig : IEntityTypeConfiguration<Cuenta>
    {
        public void Configure(EntityTypeBuilder<Cuenta> builder)
        {
            builder.ToTable("Cuenta");

            builder.HasKey(e => e.CuentaId);

            builder.Property(e => e.CuentaId)
                .IsRequired()
                .UseIdentityColumn();

            builder.Property(e => e.NumeroCuenta)
                .HasMaxLength(150)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(e => e.ClienteId)
                .IsRequired();

            builder.Property(e => e.TipoCuentaId)
                .IsRequired();

            builder.Property(e => e.SaldoInicial)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.EstadoId)
            .IsRequired();

            builder
                .HasOne(c => c.Cliente)
                .WithMany()
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.TipoCuenta)
                .WithMany()
                .HasForeignKey(c => c.TipoCuentaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.Estado)
                .WithMany()
                .HasForeignKey(c => c.EstadoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}