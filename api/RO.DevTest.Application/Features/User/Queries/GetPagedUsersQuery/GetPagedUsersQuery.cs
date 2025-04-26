using MediatR;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Features.User.Commands.CreateUserCommand;
using RO.DevTest.Domain.Enums;
using RO.DevTest.Domain.Models;

namespace RO.DevTest.Application.Features.User.Queries.GetPagedUsersQuery;

public class GetPagedUsersQuery(PagedFilter filter, UserRoles role, params string[] searchFields) : IRequest<PageResult<UserDTO>> {

    public UserRoles Role { get; set; } = role;
    public string[] SearchFields { get; set; } = searchFields ?? [];
    public PagedFilter Filter { get; set; } = filter;
}