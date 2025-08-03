using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OP.Prueba.Domain.Entities;

namespace OP.Prueba.Persistence.Configuration
{
    public class TipoCategoriaConfig : IEntityTypeConfiguration<TipoCategoria>
    {
        public void Configure(EntityTypeBuilder<TipoCategoria> builder)
        {
            builder.ToTable("TipoCategoria");

            builder.HasKey(e => e.TipoCategoriaId)
                   .HasName("PK_TipoCategoria");

            builder.Property(e => e.TipoCategoriaId)
                   .IsRequired()
                   .HasColumnType("int")
                   .UseIdentityColumn();

            builder.Property(e => e.Nombre)
                   .HasMaxLength(250)
                   .IsRequired()
                   .HasColumnType("nvarchar");

            builder.HasMany(tc => tc.TiposGenerales)
                   .WithOne(tg => tg.TipoCategoria)
                   .HasForeignKey(tg => tg.TipoCategoriaId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}