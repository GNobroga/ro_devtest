using MediatR;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Auth.Commands.LoginCommand;

public class LoginCommandHandler(IUserTokenService userTokenService, IIdentityAbstractor identityAbstractor) : IRequestHandler<LoginCommand, LoginResponse> {
    
    private readonly IUserTokenService _userTokenService = userTokenService;
    private readonly IIdentityAbstractor _identityAbstractor = identityAbstractor;
    
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken) {
        var user = await _identityAbstractor.FindUserByUserNameAsync(request.Username)
            ?? throw new BadRequestException("Usuário ou senha inválido");
    
        var signInResult = await _identityAbstractor.PasswordSignInAsync(user, request.Password);

        if (!signInResult.Succeeded) {
            throw new BadRequestException("Usuário ou senha inválido");
        }

        var userRoles = await _identityAbstractor.GetUserRolesAsync(user);
        TokenInfo tokenInfo = _userTokenService.GenerateJwtForUser(user, userRoles.ToList());
        return LoginResponse.FromTokenInfo(tokenInfo);
    }
}
