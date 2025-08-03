using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OP.Prueba.Application.Interfaces;
using OP.Prueba.Persistence.Repository;

public static class ServiceExtensions
{
    public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("PruebaConnection"),
            b => b.MigrationsAssembly("Op.Prueba.Persistence")));

        services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));
    }
}
