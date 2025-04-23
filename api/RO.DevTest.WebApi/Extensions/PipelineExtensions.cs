using RO.DevTest.WebApi.Middlewares;

namespace RO.DevTest.WebApi.Extensions;

public static class PipelineExtensions {
    public static void UseApiPipeline(this WebApplication app) {
        app.UseMiddleware<ApiExceptionMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers(); 
    }
}
