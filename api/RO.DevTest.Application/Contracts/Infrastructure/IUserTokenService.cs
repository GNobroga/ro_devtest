using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Contracts.Infrastructure;

public record TokenInfo(
    string AccessToken,
    string RefreshToken,
    DateTime IssuedAt,
    DateTime ExpirationDate,
    List<string> Roles
) {}

public interface IUserTokenService {
    TokenInfo GenerateJwtForUser(User user, List<string> roles);
    Task<TokenInfo> ExchangeRefreshTokenAsync(string refreshToken);
}
