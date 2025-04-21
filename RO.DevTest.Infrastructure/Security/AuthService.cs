using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using RO.DevTest.Application.Contracts.Infrastructure;

namespace RO.DevTest.Infrastructure.Security;

public class AuthService(IHttpContextAccessor contextAccessor) : IAuthService {

    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    private ClaimsPrincipal? User => _contextAccessor.HttpContext?.User;

    public ClaimsPrincipal GetPrincipal() => User!;

    public string? GetUserId() => User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public bool IsAuthenticated() => User?.Identity?.IsAuthenticated ?? false;

    public bool IsInRole(string role) => User?.IsInRole(role) ?? false;

}