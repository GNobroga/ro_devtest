using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.User.Commands.CreateUserCommand;
using RO.DevTest.Domain.Models;

namespace RO.DevTest.Application.Features.User.Queries.GetPagedUsersQuery;

public class GetPagedUsersQueryHandler(IUserRepository repository) : IRequestHandler<GetPagedUsersQuery, PageResult<GetPagedUsersResult>> {
    
    private readonly IUserRepository _repository = repository;
    
    public async Task<PageResult<GetPagedUsersResult>> Handle(GetPagedUsersQuery request, CancellationToken cancellationToken) {
       var result = await _repository.GetPagedAndSortedResultsAsync(request.Filter, request.SearchFields);
       return result.Map<GetPagedUsersResult>(user => new(user));
    }
}