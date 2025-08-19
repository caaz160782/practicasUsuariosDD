using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Application;

public static class DependecyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssembly(typeof(DependecyInjection).Assembly);
        });

        services.AddTransient<NombreUsuarioService>();
        services.AddValidatorsFromAssembly(typeof(DependecyInjection).Assembly);
        return services;
    }
}