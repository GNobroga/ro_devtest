namespace RO.DevTest.WebApi.Extensions;

public static class SwaggerExtensions {
    public static void UseApiSwagger(this WebApplication app) {
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
