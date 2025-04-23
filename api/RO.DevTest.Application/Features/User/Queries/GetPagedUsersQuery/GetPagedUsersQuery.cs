using MediatR;
using RO.DevTest.Application.Features.User.Commands.CreateUserCommand;
using RO.DevTest.Domain.Models;

namespace RO.DevTest.Application.Features.User.Queries.GetPagedUsersQuery;

public class GetPagedUsersQuery(PagedFilter filter, params string[] searchFields) : IRequest<PageResult<GetPagedUsersResult>> {
    public string[] SearchFields { get; set; } = searchFields ?? [];
    public PagedFilter Filter { get; set; } = filter;
}