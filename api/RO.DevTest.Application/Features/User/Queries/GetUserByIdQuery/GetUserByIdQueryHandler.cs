using MediatR;
using RO.DevTest.Application.Common.Mappers;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.User.Queries.GetUserByIdQuery;

public class GetUserByIdQueryHandler(IUserRepository repository) : IRequestHandler<GetUserByIdQuery, UserDTO> {
    
    private readonly IUserRepository _repository = repository;
    public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) {
        var result = _repository.Get(u => u.Id.Equals(request.UserId)) ??
            throw new EntityNotFoundException("Usuário não encontrado");
        return await Task.FromResult(UserMapper.ToDTO(result));
    }
}