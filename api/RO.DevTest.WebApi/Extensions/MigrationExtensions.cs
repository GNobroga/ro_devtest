using Microsoft.EntityFrameworkCore;
using RO.DevTest.Persistence;

namespace RO.DevTest.WebApi.Extensions;

public static class MigrationExtensions {

    public static void MigrateDatabase(this IApplicationBuilder app) {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DefaultContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        try {
            logger.LogInformation("Iniciando migração do banco de dados...");
            dbContext.Database.Migrate();
            logger.LogInformation("Migrações aplicadas com sucesso.");
        }
        catch (Exception ex) {
            logger.LogError(ex, "Erro ao aplicar as migrações do banco de dados.");
            throw;  
        }
    }
}