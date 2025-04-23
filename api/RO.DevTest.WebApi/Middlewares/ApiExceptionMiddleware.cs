using System.Net;
using RO.DevTest.Domain.Exception;
using RO.DevTest.Domain.Models;

namespace RO.DevTest.WebApi.Middlewares;

public class ApiExceptionMiddleware : IMiddleware {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        try {
            await next(context);
        } catch (Exception ex) {
            await HandleExceptionAsync(ex, context.Response);
        }
    }

    private static async Task HandleExceptionAsync(Exception ex, HttpResponse response) {
        var (statusCode, errors) = GetErrorDetails(ex);
        response.StatusCode = statusCode;
        await response.WriteAsJsonAsync(ApiResponse<object>.FromFailure(statusCode: statusCode, [..errors]));
    }

    private static (int StatusCode, List<string> Errors) GetErrorDetails(Exception ex) {
        List<string> errors = [ex.Message];
        int statusCode = (int) HttpStatusCode.InternalServerError;

         if (ex is ApiException apiException) {
            errors = apiException.Errors ?? ["Invalid request"];
            statusCode = (int) apiException.StatusCode;
        }

        return (statusCode, errors);
    }
}