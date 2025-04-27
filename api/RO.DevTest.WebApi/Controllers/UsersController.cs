using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Features.User.Commands.CreateUserCommand;
using RO.DevTest.Application.Features.User.Commands.DeleteUserCommand;
using RO.DevTest.Application.Features.User.Commands.UpdateUserCommand;
using RO.DevTest.Application.Features.User.Queries.GetPagedUsersQuery;
using RO.DevTest.Application.Features.User.Queries.GetUserByIdQuery;
using RO.DevTest.Domain.Enums;
using RO.DevTest.Domain.Models;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/user")]
[OpenApiTags("Users")]
public class UsersController(IMediator mediator) : Controller {
    private readonly IMediator _mediator = mediator;

    private static readonly string[] SearchFields = [
        "UserName",
        "Name",
        "Email"
    ];

    [Authorize(Roles = nameof(UserRoles.Admin))]
    [HttpGet("customer")]
    public async Task<IActionResult> GetCustomers([FromQuery] PagedFilter filter) {
        return await GetUsers(filter, UserRoles.Customer);
    }

    [Authorize(Roles = nameof(UserRoles.Admin))]
    [HttpGet("admin")]
    public async Task<IActionResult> GetAdmins([FromQuery] PagedFilter filter) {
       return await GetUsers(filter, UserRoles.Admin);
    } 

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById([FromRoute] string id) {
        var result = await _mediator.Send(new GetUserByIdQuery(id));
        return Ok(ApiResponse<UserDTO>.FromSuccess(result));
    }
    
    private async Task<IActionResult> GetUsers(PagedFilter filter, UserRoles role) {
        var result = await _mediator.Send(new GetPagedUsersQuery(filter, role, SearchFields));
        return Ok(ApiResponse<PageResult<UserDTO>>.FromSuccess(result));
    }

    [HttpPost("admin")]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAdminUser([FromBody] CreateUserCommand request) {
        request.Role = UserRoles.Admin;
        return await CreateUser(request);
    }

    [HttpPost("customer")]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCustomerUser([FromBody] CreateUserCommand request) {
        request.Role = UserRoles.Customer;
        return await CreateUser(request);
    }
   
    private async Task<IActionResult> CreateUser(CreateUserCommand request) {
        UserDTO result = await _mediator.Send(request);
        ApiResponse<UserDTO> response = ApiResponse<UserDTO>.FromSuccess(result, (int) HttpStatusCode.Created);
        return Created(HttpContext.Request.GetDisplayUrl(), response);
    }

    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<UpdateUserCommand>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [HttpPut("admin/{id}")]
    public async Task<IActionResult> UpdateAdminUser([FromRoute] string id, [FromBody] UpdateUserCommand request) {
        request.Role = UserRoles.Admin;
        return await UpdateUser(id, request);
    }

    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<UpdateUserCommand>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [HttpPut("customer/{id}")]
    public async Task<IActionResult> UpdateCustomerUser([FromRoute] string id, [FromBody] UpdateUserCommand request) {
        request.Role = UserRoles.Customer;
        return await UpdateUser(id, request);
    }

    private async Task<IActionResult> UpdateUser(string id, UpdateUserCommand request) {
        request.UserId = id;
        UserDTO result = await _mediator.Send(request);
        return Ok(ApiResponse<UserDTO>.FromSuccess(result));
    }

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<DeleteUserCommand>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUser([FromRoute] string id) {
        DeleteUserCommand command = new(id);
        DeleteUserResult result = await _mediator.Send(command);
        return Ok(ApiResponse<DeleteUserResult>.FromSuccess(result));
    }

}
