using MediatR;

namespace RO.DevTest.Application.Features.User.Commands.DeleteUserCommand;

public class DeleteUserCommand(string userId) : IRequest<DeleteUserResult> {
    public string UserId { get; set; } = userId;
}