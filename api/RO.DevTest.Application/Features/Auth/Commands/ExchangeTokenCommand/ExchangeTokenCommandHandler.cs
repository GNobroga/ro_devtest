using MediatR;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Auth.Commands.ExchangeTokenCommand;

public class ExchangeTokenCommandHandler(IUserTokenService userTokenService) : IRequestHandler<ExchangeTokenCommand, TokenInfo> {
    public async Task<TokenInfo> Handle(ExchangeTokenCommand request, CancellationToken cancellationToken) {
       string refreshToken = request.RefreshToken;
       if (string.IsNullOrWhiteSpace(refreshToken)) 
            throw new BadRequestException("Refresh token é obrigatorio");

       return await userTokenService.ExchangeRefreshTokenAsync(refreshToken);
    }
}