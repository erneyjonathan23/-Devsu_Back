using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;
using OP.Prueba.Application;
using OP.Prueba.Identity;
using OP.Prueba.Persistence;
using OP.Prueba.Common;

namespace OP.Prueba.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        public static void BaseRegister(this IServiceCollection services, IConfiguration configuration, IHostBuilder hostBuilder)
        {
            services.RegisterLog(configuration, hostBuilder);
            services.AddApplicationLayer();
            services.AddSharedInfraestructure(configuration);
            services.AddPersistenceInfraestructure(configuration);
            services.AddApiVersioningExtension();
        }

        public static void RegisterLog(this IServiceCollection services, IConfiguration configuration, IHostBuilder hostBuilder)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                .WriteTo.File("../Logs/log.txt", restrictedToMinimumLevel: LogEventLevel.Information, rollingInterval: RollingInterval.Day)
                .WriteTo.File("../Logs/logError.txt", restrictedToMinimumLevel: LogEventLevel.Error, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddLogging(static loggingBuilder =>
            {
                loggingBuilder.AddSerilog(dispose: true);
            });
        }
    }
}