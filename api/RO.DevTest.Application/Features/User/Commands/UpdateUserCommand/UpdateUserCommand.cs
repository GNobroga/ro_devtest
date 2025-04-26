using MediatR;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Domain.Enums;

namespace RO.DevTest.Application.Features.User.Commands.UpdateUserCommand;

public class UpdateUserCommand(string userId) : IRequest<UserDTO> {

    public string UserId { get; set; } = userId;
    public string? UserName { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PasswordConfirmation { get; set; }
    public UserRoles? Role { get; set; }
}