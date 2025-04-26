using MediatR;
using RO.DevTest.Application.DTOs;

namespace RO.DevTest.Application.Features.User.Queries.GetUserByIdQuery;

public class GetUserByIdQuery(string id) : IRequest<UserDTO> {
    public string  UserId { get; set; } = id;
}