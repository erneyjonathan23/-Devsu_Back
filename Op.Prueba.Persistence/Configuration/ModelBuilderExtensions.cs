using Microsoft.EntityFrameworkCore;
using OP.Prueba.Identity.Seeds;

namespace OP.Prueba.Persistence.Configuration
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            DefaultTipoCategoria.Seeds(builder);
            DefaultTipoGeneral.Seeds(builder);
        }
    }
}
