using MediatR;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Auth.Commands.LoginCommand;

public class LoginCommandHandler(ITokenService tokenService, IIdentityAbstractor identityAbstractor) : IRequestHandler<LoginCommand, LoginResponse> {
    
    private readonly ITokenService _tokenService = tokenService;
    private readonly IIdentityAbstractor _identityAbstractor = identityAbstractor;
    
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken) {
        var user = await _identityAbstractor.FindUserByUserNameAsync(request.Username)
            ?? throw new BadRequestException("Username or password is invalid.");
    
        var signInResult = await _identityAbstractor.PasswordSignInAsync(user, request.Password);

        if (!signInResult.Succeeded) {
            throw new BadRequestException("Username or password is invalid.");
        }

        var userRoles = await _identityAbstractor.GetUserRolesAsync(user);
        TokenInfo tokenInfo = _tokenService.GenerateJwtForUser(user, userRoles.ToList());
        return LoginResponse.FromTokenInfo(tokenInfo);
    }
}
