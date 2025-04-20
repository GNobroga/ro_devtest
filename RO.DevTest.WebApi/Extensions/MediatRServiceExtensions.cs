using RO.DevTest.Application;

namespace RO.DevTest.WebApi.Extensions;

public static class MediatRServiceExtensions {
    public static IServiceCollection AddMediatRServices(this IServiceCollection services) {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssemblies(
                typeof(ApplicationLayer).Assembly,
                typeof(Program).Assembly
            );
        });
        return services;
    }
}
