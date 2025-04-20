using MediatR;

namespace RO.DevTest.Application.Features.User.Commands.DeleteUserCommand;
using RO.DevTest.Application.Contracts.Infrastructure;

public class DeleteUserCommandHandler(IIdentityAbstractor identityAbstractor) : IRequestHandler<DeleteUserCommand, DeleteUserResult> {
    private readonly IIdentityAbstractor _identityAbstractor = identityAbstractor;
    
   public async Task<DeleteUserResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken) {
        var user = await _identityAbstractor.FindUserByIdAsync(request.UserId);

        if (user is null)
            return DeleteUserResult.Failure(request.UserId); 

        var deleteSucceeded = await DeleteUserAsync(user);

        return new DeleteUserResult(request.UserId, deleteSucceeded);
    }

    private async Task<bool> DeleteUserAsync(Domain.Entities.User user) {
        var result = await _identityAbstractor.DeleteUser(user);
        return result.Succeeded;
    }
}