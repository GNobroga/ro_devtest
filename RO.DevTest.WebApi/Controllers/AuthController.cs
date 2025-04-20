using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Application.Features.Auth.Commands.LoginCommand;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/auth")]
[OpenApiTags("Auth")]
public class AuthController(IMediator mediator) : Controller {
    private readonly IMediator _mediator = mediator;

    ///[TODO] - CREATE LOGIN HANDLER HERE 
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginCommand command) {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
