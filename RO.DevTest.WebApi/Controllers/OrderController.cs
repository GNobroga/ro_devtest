using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Application.Features.Order.Commands.CreateOrUpdateOrderCommand;
using RO.DevTest.Domain.Enums;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController(IMediator mediator, IAuthService authService) : ControllerBase {

    private readonly IMediator _mediator = mediator;

    [Authorize(Roles = nameof(UserRoles.Customer))]
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrUpdateOrderCommand request) {
        request.UserId = authService.GetUserId()!;
        var result = await _mediator.Send(request);
        return Ok(result);
    }



}