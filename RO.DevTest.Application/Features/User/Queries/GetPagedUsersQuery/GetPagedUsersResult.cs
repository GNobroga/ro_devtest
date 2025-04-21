using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Features.User.Commands.CreateUserCommand;

public record GetPagedUsersResult(string Id, string Username, string Name, string Email) {
    public GetPagedUsersResult(Domain.Entities.User user) : this(user.Id, user.UserName!, user.Name, user.Email!) {}
}