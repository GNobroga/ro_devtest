using Microsoft.EntityFrameworkCore;
using RO.DevTest.Persistence;
using RO.DevTest.WebApi.Extensions;

namespace RO.DevTest.WebApi;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddApiServices()
            .AddMediatRServices();

            builder.Services.AddCustomCors();

        var app = builder.Build();

        app.MigrateDatabase();
                
        app.UseApiSwagger();
        app.UseApiPipeline();
        app.Run();
    }
}
