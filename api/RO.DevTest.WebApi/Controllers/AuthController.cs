using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Application.Features.Auth.Commands.ExchangeTokenCommand;
using RO.DevTest.Application.Features.Auth.Commands.LoginCommand;
using RO.DevTest.Domain.Models;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/auth")]
[OpenApiTags("Auth")]
public class AuthController(IMediator mediator) : Controller {
    private readonly IMediator _mediator = mediator;

    ///[TODO] - CREATE LOGIN HANDLER HERE 
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<PageResult<LoginResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command) {
        var result = await _mediator.Send(command);
        return Ok(ApiResponse<LoginResponse>.FromSuccess(result));
    }

    [HttpPost("exchange-token")]
    [ProducesResponseType(typeof(ApiResponse<PageResult<TokenInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExchangeToken([FromBody] ExchangeTokenCommand request) {
        var result = await _mediator.Send(request);
        return Ok(ApiResponse<TokenInfo>.FromSuccess(result));
    }
}
