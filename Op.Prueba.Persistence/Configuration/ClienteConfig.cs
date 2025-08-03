using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OP.Prueba.Domain.Entities;

namespace OP.Prueba.Persistence.Configuration
{
    public class ClienteConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");

            builder.Property(e => e.Contrasena)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnType("varchar");

            builder.Property(e => e.EstadoId)
                .IsRequired()
                .HasColumnType("int");

            // Relación FK Estado (TipoGeneral)
            builder.HasOne(d => d.Estado)
                .WithMany()
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_TipoGeneral_Estado");

            // Mapeo de herencia TPT (EF Core lo manejará automáticamente si está bien estructurado en DbContext)
        }
    }
}