using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Features.Order.Commands.ChangeOrderStatusCommand;
using RO.DevTest.Application.Features.Order.Commands.CreateOrUpdateOrderCommand;
using RO.DevTest.Application.Features.Order.Commands.DeleteOrderCommand;
using RO.DevTest.Application.Features.Order.Queries.GetAllOrdersQuery;
using RO.DevTest.Application.Features.Order.Queries.GetOrdersByUserQuery;
using RO.DevTest.Application.Features.Order.Queries.GetOrderSummaryByPeriodQuery;
using RO.DevTest.Domain.Enums;
using RO.DevTest.Domain.Models;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController(IMediator mediator, IAuthService authService) : ControllerBase {

    private readonly IMediator _mediator = mediator;

    private static readonly string[] SearchFields = [
      "User.Email",
      "User.Name",
      "User.Id"
    ];

    [HttpGet("summary")]
    public async Task<IActionResult> GetOrderSummary(GetOrderSummaryByPeriodQuery request) {
        var result = await _mediator.Send(request);
        return Ok(ApiResponse<OrderSummaryDTO>.FromSuccess(result));
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetOrdersByUser([FromQuery] PagedFilter filter) {
        var userId = authService.GetUserId()!;
        var query = new GetOrdersByUserQuery(userId, filter, SearchFields);
        var result = await _mediator.Send(query);
        return Ok(ApiResponse<PageResult<OrderDTO>>.FromSuccess(result));
    }


    [HttpGet]
    public async Task<IActionResult> GetOrders([FromQuery] PagedFilter filter) {
        var query = new GetAllOrdersQuery(filter, SearchFields);
        var result = await _mediator.Send(query);
        return Ok(ApiResponse<PageResult<OrderDTO>>.FromSuccess(result));
    }

    [Authorize(Roles = nameof(UserRoles.Customer))]
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrUpdateOrderCommand request) {
        request.UserId = authService.GetUserId()!;
        var result = await _mediator.Send(request);
        return Created(HttpContext.Request.GetDisplayUrl(), ApiResponse<OrderDTO>.FromSuccess(result));
    }

    [Authorize(Roles = nameof(UserRoles.Admin))]
    [HttpPut("{id}/status")]
    public async Task<IActionResult> ChangeOrderStatus([FromRoute] Guid id, ChangeOrderStatusCommand request) {
       request.OrderId = id;
       var result = await _mediator.Send(request);
       return Ok(ApiResponse<ChangeOrderStatusResult>.FromSuccess(result));
    }

    [Authorize]
    [HttpPost("{id}")]
    public async Task<IActionResult> UpdateOrder([FromRoute] Guid id, CreateOrUpdateOrderCommand request) {
        request.OrderId = id;
        var result = await _mediator.Send(request);
        return Ok(ApiResponse<OrderDTO>.FromSuccess(result));
    }

    [Authorize(Roles = nameof(UserRoles.Admin))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder([FromRoute] Guid id) {
        DeleteOrderCommand command = new(id);
        var result = await _mediator.Send(command);
        return Ok(ApiResponse<DeleteOrderResult>.FromSuccess(result));
    }


}