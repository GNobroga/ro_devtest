using RO.DevTest.Infrastructure.IoC;
using RO.DevTest.Persistence.IoC;
using RO.DevTest.WebApi.Middlewares;

namespace RO.DevTest.WebApi.Extensions;
public static class ApiServiceExtensions {
    public static IServiceCollection AddApiServices(this WebApplicationBuilder builder) {
        IServiceCollection services = builder.Services;
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddScoped<ApiExceptionMiddleware>();

        services.InjectPersistenceDependencies(builder.Configuration)
            .InjectInfrastructureDependencies(builder.Configuration);
            
        return services;
    }
}
