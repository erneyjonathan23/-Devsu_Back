using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Op.Prueba.Application.Interfaces;
using Op.Prueba.Application.Services;
using OP.Prueba.Application.Behaviours;
using OP.Prueba.Application.Interfaces;
using OP.Prueba.Application.Services;
using System.Reflection;

namespace OP.Prueba.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            #region Services
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IReportService, ReportService>();
            #endregion
        }
    }
}