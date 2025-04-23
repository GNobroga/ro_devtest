using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Domain.Exception;
namespace RO.DevTest.Infrastructure.Security;

/// <summary>
    /// Serviço responsável por gerar tokens JWT (JSON Web Token) para autenticação.
    /// Implementa a interface <see cref="ITokenService"/> e fornece funcionalidades
    /// para a criação de tokens.
/// </summary>
public class UserTokenService : IUserTokenService {

    private readonly IIdentityAbstractor _identityAbstractor;

    private const int DefaultAccessTokenExpirationHours = 2;
    private const int DefaultRefreshTokenExpirationDays = 2;

    private record TokenResponse(string Token, DateTime IssuedAt, DateTime ExpirationDate);
    private readonly JwtSettings _jwtSettings;

    public UserTokenService(IOptions<JwtSettings> jwtSettings, IIdentityAbstractor identityAbstractor) {
        _jwtSettings = jwtSettings.Value; 
        _identityAbstractor = identityAbstractor;
    }
    
    public TokenInfo GenerateJwtForUser(User user, List<string> roles) {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var accessToken = GenerateAccessToken(user, roles, signingCredentials);
        var refreshToken = GenerateRefreshToken(user, signingCredentials);

        return new TokenInfo(
            accessToken.Token, 
            refreshToken.Token, 
            accessToken.IssuedAt,
            accessToken.ExpirationDate, 
            roles
        );
    }

     private TokenResponse GenerateAccessToken(User user, List<string> roles, SigningCredentials credentials) {
        var expiresAt = DateTime.UtcNow.AddHours(_jwtSettings.AccessTokenExpireHours ?? DefaultAccessTokenExpirationHours);

        JwtSecurityToken jwtSecurityToken = new(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: BuildClaimsForAccessToken(user, roles), 
            expires: expiresAt,
            signingCredentials: credentials
        );  

        string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return new TokenResponse(accessToken, jwtSecurityToken.IssuedAt, expiresAt);
    }

   private TokenResponse GenerateRefreshToken(User user, SigningCredentials credentials) {
        var expiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpireDays ?? DefaultRefreshTokenExpirationDays);

        JwtSecurityToken jwtSecurityToken = new(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: BuildClaimsForRefreshToken(user), 
            expires: expiresAt,
            signingCredentials: credentials
        );

        string refreshToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return new TokenResponse(refreshToken, jwtSecurityToken.IssuedAt, expiresAt);
    }

    private static List<Claim> BuildClaimsForRefreshToken(User user, DateTime? issuedAt = null) {
         List<Claim> claims = [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.DateTime),
            new Claim(JwtClaimsConstants.TokenType, JwtClaimsConstants.ClaimValueTokenTypeRefresh)
        ];
        return claims;
    }

    private static List<Claim> BuildClaimsForAccessToken(User user, List<string> roles) {
        List<Claim> claims = [
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.DateTime),
            new Claim(JwtClaimsConstants.Role, string.Join(" ", roles))
        ];
        return claims;
    }

    public async Task<TokenInfo> ExchangeRefreshTokenAsync(string refreshToken) {
        var tokenValidationResult = await ValidateRefreshTokenAsync(refreshToken);
        if (!tokenValidationResult.IsValid) 
            throw new BadRequestException("Refresh token inválido");

        var sub = tokenValidationResult.Claims[ClaimTypes.NameIdentifier] as string;

        var user = await _identityAbstractor.FindUserByIdAsync(sub!);

        if (user == null)
            throw new BadRequestException("Refresh token inválido");

        var roles = await _identityAbstractor.GetUserRolesAsync(user);

        return GenerateJwtForUser(user, [..roles]);
    }

    private async Task<TokenValidationResult> ValidateRefreshTokenAsync(string refreshToken) {
        var tokenHandler = new JwtSecurityTokenHandler();
        var keyBytes = Encoding.UTF8.GetBytes(_jwtSettings.Key);
        var securityKey = new SymmetricSecurityKey(keyBytes);

        var validationParameters = new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey,
            ValidateIssuer = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwtSettings.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        return await tokenHandler.ValidateTokenAsync(refreshToken, validationParameters);
    }
}