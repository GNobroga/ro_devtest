namespace RO.DevTest.WebApi.Extensions;

public static class CorsExtensions {

    public const string DefaultCorsPolicyName = "AllowAll";
        
     public static IServiceCollection AddCustomCors(this IServiceCollection services) {
        services.AddCors(options => {
            options.AddPolicy(DefaultCorsPolicyName, policy => {
                policy.AllowAnyOrigin()  
                        .AllowAnyMethod()  
                        .AllowAnyHeader(); 
            });
        });
        return services;
    }
}