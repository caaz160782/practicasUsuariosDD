using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Usuarios.Application.Abstractions.Data;
using Usuarios.Application.Abstractions.Email;
using Usuarios.Application.Abstractions.Time;
using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Roles;
using Usuarios.Domain.Usuarios;
using Usuarios.Infrastructure.Abstractions.Data;
using Usuarios.Infrastructure.Abstractions.Email;
using Usuarios.Infrastructure.Abstractions.Time;
using Usuarios.Infrastructure.Repositories;

namespace Usuarios.Infrastructure;

    public static class DependecyInjection
    {
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();

        });

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        services.AddScoped<IUnitOfWork>(
            sp => sp.GetRequiredService<ApplicationDbContext>()
        );

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IRolRepository, RolRepository>();

        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            
        return services;
    }
    }
