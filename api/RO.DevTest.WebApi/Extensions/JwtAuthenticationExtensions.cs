using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RO.DevTest.Domain.Models;
using RO.DevTest.Infrastructure.Security;

namespace RO.DevTest.WebApi.Extensions;

public static class JwtAuthenticationExtensions {

    private const string ForbiddenErrorMessage = "O usuário não tem permissões suficientes para acessar este recurso";
    private const string AuthenticationErrorMessage = "Token expirado ou invalido";
    private const string RefreshTokenCannotBeUsedForAuthentication = "Refresh token não pode ser usado para autenticação";

    private static readonly Func<ForbiddenContext, Task> HandleOnForbidden = context => {
        int statusCode = (int) HttpStatusCode.Unauthorized;
        context.Response.StatusCode = statusCode;
        var response = ApiResponse<object>.FromFailure(statusCode, ForbiddenErrorMessage);
        context.Response.WriteAsJsonAsync(response);
        return Task.CompletedTask;
    };

    private static readonly Func<AuthenticationFailedContext, Task> HandleAuthenticationFailed = context => {
        int statusCode = (int) HttpStatusCode.Unauthorized;
        context.Response.StatusCode = statusCode;
        var response = ApiResponse<object>.FromFailure(statusCode, AuthenticationErrorMessage);
        context.Response.WriteAsJsonAsync(response);
        return Task.CompletedTask;
    };

    private static readonly Func<TokenValidatedContext, Task> HandleOnTokenValidated = context => {
        var typeClaim = context.Principal?.FindFirstValue(JwtClaimsConstants.TokenType);
        if (typeClaim == JwtClaimsConstants.ClaimValueTokenTypeRefresh) {
            int statusCode = (int) HttpStatusCode.Unauthorized;
            context.Response.StatusCode = statusCode;
            var response = ApiResponse<object>.FromFailure(statusCode, RefreshTokenCannotBeUsedForAuthentication);
            context.Response.WriteAsJsonAsync(response);
        }

        return Task.CompletedTask;
    };

    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration) {
        var jwtSettings = configuration.GetSection("Jwt");
        var secret = jwtSettings["Key"]!;

        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options => {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                RoleClaimType = ClaimTypes.Role,
            };

            options.Events = new JwtBearerEvents {
               OnForbidden = HandleOnForbidden,
               OnAuthenticationFailed = HandleAuthenticationFailed,
               OnTokenValidated = HandleOnTokenValidated
            };
        });

        return services;
    }


}