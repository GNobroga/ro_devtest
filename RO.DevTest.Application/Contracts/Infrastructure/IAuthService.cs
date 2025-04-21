using System.Security.Claims;

namespace RO.DevTest.Application.Contracts.Infrastructure;

public interface IAuthService {
    public string? GetUserId();
    bool IsInRole(string role);

    bool IsAuthenticated();
    ClaimsPrincipal GetPrincipal();
}