using MediatR;
using Microsoft.AspNetCore.Mvc;
using RO.DevTest.Application.Features.Order.Commands.CreateOrderCommand;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController(IMediator mediator) : ControllerBase {

    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderCommand request) {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}