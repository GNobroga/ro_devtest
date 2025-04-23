namespace RO.DevTest.Application.Features.User.Commands.DeleteUserCommand;

public record DeleteUserResult(string UserId, bool Deleted) {

    public static DeleteUserResult Failure(string userId) => new(userId, false);
    
}