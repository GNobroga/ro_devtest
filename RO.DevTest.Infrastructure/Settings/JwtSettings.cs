namespace RO.DevTest.Infrastructure.Settings;

public class JwtSettings {
    public string Key { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public int? AccessTokenExpireHours { get; set; }
    public int? RefreshTokenExpireDays { get; set; }
}