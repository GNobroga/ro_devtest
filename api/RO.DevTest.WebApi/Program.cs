using RO.DevTest.WebApi.Extensions;

namespace RO.DevTest.WebApi;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddApiServices()
            .AddMediatRServices();

        var app = builder.Build();
        
        app.UseApiSwagger();
        app.UseApiPipeline();
        app.Run();
    }
}
