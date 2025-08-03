using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OP.Prueba.Domain.Entities;

namespace OP.Prueba.Persistence.Configuration
{
    public class TipoGeneralConfig : IEntityTypeConfiguration<TipoGeneral>
    {
        public void Configure(EntityTypeBuilder<TipoGeneral> builder)
        {
            builder.ToTable("TipoGeneral");

            builder.HasKey(e => e.TipoGeneralId);

            builder.Property(e => e.TipoGeneralId)
                .UseIdentityColumn();

            builder.Property(e => e.Codigo)
                .HasMaxLength(450)
                .IsRequired();

            builder.Property(e => e.Descripcion)
                .HasMaxLength(450)
                .IsRequired();

            builder.HasOne(d => d.TipoCategoria)
               .WithMany(p => p.TiposGenerales)
               .HasForeignKey(d => d.TipoCategoriaId)
               .OnDelete(DeleteBehavior.Restrict); // <-- Evitas nulos forzadamente
        }
    }
}
