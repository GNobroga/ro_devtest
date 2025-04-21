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
        AddRepositories(services);
        AddServices(services);
        AddSettings(services, configuration);
        return services;
    }

    public static void AddSettings(this IServiceCollection services, IConfiguration configuration) {
        services.Configure<JwtSettings>(configuration.GetRequiredSection("Jwt"));
    }

    public static void AddServices(this IServiceCollection services) {
        services.AddScoped<IIdentityAbstractor, IdentityAbstractor>();
        services.AddSingleton<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();
    }
    
    public static void AddRepositories(IServiceCollection services) {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrderProductRepository, OrderProductRepository>();
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
