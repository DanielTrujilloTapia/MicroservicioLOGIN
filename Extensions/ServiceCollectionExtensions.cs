using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microservicio.Login.Api.Aplicacion;
using Microservicio.Login.Api.Persistencia;
using Microsoft.Extensions.DependencyInjection;

namespace Microservicio.Login.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddControllers()
                .AddFluentValidation(cfg =>
                    cfg.RegisterValidatorsFromAssemblyContaining<NuevoLogin>());

            // 👉 Agregar ContextoMongo como Singleton
            services.AddSingleton(sp =>
            {
                var connectionString = configuration["MongoDatabase:ConnectionString"];
                var databaseName = configuration["MongoDatabase:DatabaseName"];
                return new ContextoMongo(connectionString, databaseName);
            });

            // Versión nueva (MediatR v12+):
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(NuevoLogin.Manejador).Assembly));
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ConsultaLogin).Assembly));

            return services;
        }
    }
}
