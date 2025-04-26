
using System.Linq.Expressions;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RO.DevTest.Application.Common.Mappers;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Domain.Enums;
using RO.DevTest.Domain.Models;

namespace RO.DevTest.Application.Features.User.Queries.GetPagedUsersQuery;

public class GetPagedUsersQueryHandler(IUserRepository repository) : IRequestHandler<GetPagedUsersQuery, PageResult<UserDTO>> {
    
    private readonly IUserRepository _repository = repository;

    private IdentityDbContext<Domain.Entities.User> Context => (IdentityDbContext<Domain.Entities.User>) _repository.GetContext();
    public async Task<PageResult<UserDTO>> Handle(GetPagedUsersQuery request, CancellationToken cancellationToken) {
        var roleName = request.Role.ToString();
        var adminRoleId = Context.Roles
            .Where(r => r.Name == roleName)
            .Select(r => r.Id)
            .FirstOrDefault();

        var userIdsWithAdminRole = Context.UserRoles
            .Where(ur => ur.RoleId == adminRoleId)
            .Select(ur => ur.UserId);

       Expression<Func<Domain.Entities.User, bool>> predicate = u => userIdsWithAdminRole.Contains(u.Id);


        var result = await _repository.GetPagedAndSortedResultsAsync(request.Filter, request.SearchFields, predicate);

        return result.Map(UserMapper.ToDTO);
    }
}