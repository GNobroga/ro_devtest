using MediatR;
using Microsoft.AspNetCore.Identity;
using RO.DevTest.Application.Common.Mappers;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Extensions;
using RO.DevTest.Domain.Enums;
using RO.DevTest.Domain.Exception;
namespace RO.DevTest.Application.Features.User.Commands.UpdateUserCommand;

public class UpdateUserCommandHandler(IIdentityAbstractor identityAbstractor) : IRequestHandler<UpdateUserCommand, UserDTO> {
    private readonly IIdentityAbstractor _identityAbstractor = identityAbstractor;
    public async Task<UserDTO> Handle(UpdateUserCommand request, CancellationToken cancellationToken) {
        await request.ThrowIfInvalidCommandAsync(new UpdateUserCommandValidator());

        var user = await _identityAbstractor.FindUserByIdAsync(request.UserId);

        if (user is null) 
            throw new EntityNotFoundException($"Usuário com o ID {request.UserId} não encontrado");

        string email = request.Email!;
   
        if (!string.Equals(user.Email, email, StringComparison.InvariantCultureIgnoreCase)) {
            await ThrowIfEmailAlreadyExistsAsync(email);
        }

        string userName = request.UserName!;

        if (!string.Equals(user.UserName, userName, StringComparison.InvariantCultureIgnoreCase)) {
            await ThrowIfUserNameAlreadyExistsAsync(userName);
        }

        MapToEntity(request, user);

        List<Task> tasks = [];

        var updateUserResult = await _identityAbstractor.UpdateUserAsync(user);
        
        EnsureSuccessOrThrow(updateUserResult);

        if (request.Password is not null) {
            var updateUserPasswordResult = await _identityAbstractor.ResetUserPasswordAsync(user, request.Password);
            EnsureSuccessOrThrow(updateUserPasswordResult);
        }

        if (request.Role is not null) {
           tasks.Add(RemoveRolesAndAddNewRole(user, request.Role.Value));
        }

        await Task.WhenAll(tasks);

        return UserMapper.ToDTO(user);
    }

    private static void EnsureSuccessOrThrow(IdentityResult result) {
        ArgumentNullException.ThrowIfNull(result);

        if (!result.Succeeded) {
            var errors = result.Errors.Select(e => $"{e.Code}: {e.Description}");
            throw new BadRequestException(errors);
        }
    }



    private async Task RemoveRolesAndAddNewRole(Domain.Entities.User user, UserRoles role) {
        await _identityAbstractor.RemoveRolesFromUser(user);
        await _identityAbstractor.AddToRoleAsync(user, role);
    }

    private async Task ThrowIfEmailAlreadyExistsAsync(string email) {
        var normalizedEmail = email.ToLower();
        var existingUser = await _identityAbstractor.FindUserByEmailAsync(normalizedEmail);
 
        if (existingUser is not null)
            throw new ConflictException($"O e-mail '{email}' já está em uso.");
    }

    private async Task ThrowIfUserNameAlreadyExistsAsync(string userName) {
        var normalizedUserName = userName.ToLower();
        var existingUser = await _identityAbstractor.FindUserByUserNameAsync(normalizedUserName);

        if (existingUser is not null)
            throw new ConflictException($"O username '{userName}' já está em uso.");
    }

    private static void MapToEntity(UpdateUserCommand source, Domain.Entities.User target) {
        target.Name = source.Name ?? target.Name;
        target.UserName = source.UserName ?? target.UserName;
        target.Email = source.Email ?? target.Email;
    }

}