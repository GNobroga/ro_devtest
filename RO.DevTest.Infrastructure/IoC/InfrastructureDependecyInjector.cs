using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Infrastructure.Abstractions;
using RO.DevTest.Infrastructure.Security;
using RO.DevTest.Persistence;
using RO.DevTest.Persistence.Repositories;

namespace RO.DevTest.Infrastructure.IoC;

public static class InfrastructureDependecyInjector {
    
    /// <summary>
    /// Inject the dependencies of the Infrastructure layer into an
    /// <see cref="IServiceCollection"/>
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to inject the dependencies into
    /// </param>
    /// <returns>
    /// The <see cref="IServiceCollection"/> with dependencies injected
    /// </returns>
    public static IServiceCollection InjectInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration) {
        ConfigureIdentity(services);

        services.AddScoped<IIdentityAbstractor, IdentityAbstractor>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddSingleton<ITokenService, TokenService>();

        services.Configure<JwtSettings>(configuration.GetRequiredSection("Jwt"));
        return services;
    }

    public static void ConfigureIdentity(IServiceCollection services) {
        services.AddDefaultIdentity<User>(options => {
            options.User.RequireUniqueEmail = true; 
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredUniqueChars = 0;
        })
        .AddRoles<IdentityRole>() 
        .AddEntityFrameworkStores<DefaultContext>() 
        .AddDefaultTokenProviders(); 
    }

    
}
