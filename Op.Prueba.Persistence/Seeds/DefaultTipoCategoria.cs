using Microsoft.EntityFrameworkCore;
using OP.Prueba.Domain.Entities;

namespace OP.Prueba.Identity.Seeds
{
    public static class DefaultTipoCategoria
    {
        public static void Seeds(ModelBuilder builder)
        {
            builder.Entity<TipoCategoria>().HasData(new List<TipoCategoria>() {

                new TipoCategoria { TipoCategoriaId = 1, Nombre = "Tipos de cuenta" },
                new TipoCategoria { TipoCategoriaId = 2, Nombre = "Estados" },
                new TipoCategoria { TipoCategoriaId = 3, Nombre = "Generos" },
                new TipoCategoria { TipoCategoriaId = 4, Nombre = "Tipos de movimientos" },
            });
        }
    }

}
