using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OP.Prueba.Domain.Entities;

namespace OP.Prueba.Persistence.Configuration
{
    public class PersonaConfig : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("Persona");

            builder.HasKey(p => p.PersonaId);

            builder.Property(p => p.PersonaId)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Nombres).IsRequired().HasMaxLength(150);
            builder.Property(p => p.GeneroId).IsRequired();
            builder.Property(p => p.Edad).IsRequired();
            builder.Property(p => p.Identificacion).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Direccion).HasMaxLength(250);
            builder.Property(p => p.Telefono).HasMaxLength(50);

            builder.HasOne(p => p.Genero)
                   .WithMany()
                   .HasForeignKey(p => p.GeneroId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}