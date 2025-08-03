using Microsoft.EntityFrameworkCore;
using OP.Prueba.Domain.Entities;

namespace OP.Prueba.Identity.Seeds
{
    public static class DefaultTipoGeneral
    {
        public static void Seeds(ModelBuilder builder)
        {
            builder.Entity<TipoGeneral>().HasData(new List<TipoGeneral>()
            {
                // Tipos de cuenta
                new TipoGeneral { TipoGeneralId = 1, TipoCategoriaId = 1, Codigo = "A", Descripcion = "Ahorros" },
                new TipoGeneral { TipoGeneralId = 2, TipoCategoriaId = 1, Codigo = "C", Descripcion = "Corriente" },

                // Estados cuenta
                new TipoGeneral { TipoGeneralId = 3, TipoCategoriaId = 2, Codigo = "ACT", Descripcion = "Activo" },
                new TipoGeneral { TipoGeneralId = 4, TipoCategoriaId = 2, Codigo = "INA", Descripcion = "Inactivo" },

                // Generos
                new TipoGeneral { TipoGeneralId = 5, TipoCategoriaId = 3, Codigo = "M", Descripcion = "Masculino" },
                new TipoGeneral { TipoGeneralId = 6, TipoCategoriaId = 3, Codigo = "F", Descripcion = "Femenino" },

                // Tipos de movimientos
                new TipoGeneral { TipoGeneralId = 7, TipoCategoriaId = 4, Codigo = "CON", Descripcion = "Consignación" },
                new TipoGeneral { TipoGeneralId = 8, TipoCategoriaId = 4, Codigo = "RET", Descripcion = "Retiro" },
            });
        }
    }

}