using System.Text.Json.Serialization;
using RO.DevTest.Application.Contracts.Infrastructure;

namespace RO.DevTest.Application.Features.Auth.Commands.LoginCommand;

public record LoginResponse {
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AccessToken { get; set; } = null;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RefreshToken { get; set; } = null;
    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpirationDate { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<string>? Roles { get; set; } = null;

    public static LoginResponse FromTokenInfo(TokenInfo tokenInfo) => new() {
        AccessToken = tokenInfo.AccessToken,
        RefreshToken = tokenInfo.RefreshToken,
        IssuedAt = tokenInfo.IssuedAt,
        ExpirationDate = tokenInfo.ExpirationDate,
        Roles = tokenInfo.Roles
    };
}
