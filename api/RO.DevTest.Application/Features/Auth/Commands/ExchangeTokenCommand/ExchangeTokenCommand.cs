using MediatR;
using RO.DevTest.Application.Contracts.Infrastructure;

namespace RO.DevTest.Application.Features.Auth.Commands.ExchangeTokenCommand;

public class ExchangeTokenCommand : IRequest<TokenInfo> {
    public string RefreshToken { get; set; } = string.Empty;
}
