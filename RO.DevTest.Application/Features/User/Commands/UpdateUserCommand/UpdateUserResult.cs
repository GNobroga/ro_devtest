namespace RO.DevTest.Application.Features.User.Commands.UpdateUserCommand;

public record UpdateUserResult(string Id, string UserName, string Name, string Email, List<string> Roles) {
    public UpdateUserResult(Domain.Entities.User user, List<string> roles) : this(user.Id, user.UserName!, user.Name, user.Email!, roles) {}
}