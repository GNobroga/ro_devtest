using FluentValidation;

namespace RO.DevTest.Application.Features.User.Commands.CreateUserCommand;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand> {
    public CreateUserCommandValidator() {
        RuleFor(cpau => cpau.Email)
            .NotEmpty()
            .WithMessage("O campo e-mail precisa ser preenchido");

        RuleFor(cpau => cpau.UserName) 
            .NotEmpty()
            .WithMessage("O campo username precisa ser preenchido")
            .MinimumLength(6)
            .WithMessage("O campo username precisa ter, pelo menos, 3 caracters");

        RuleFor(cpau => cpau.Name) 
            .NotEmpty()
            .WithMessage("O nome name precisa ser preenchido")
            .MinimumLength(3)
            .WithMessage("O name precisa ter no mínimo 3 caracteres");

        RuleFor(cpau => cpau.Email)
            .EmailAddress()
            .WithMessage("O campo email precisa ser um e-mail válido");

        RuleFor(cpau => cpau.Password)
            .MinimumLength(6)
            .WithMessage("O campo senha precisa ter, pelo menos, 6 caracteres");

        RuleFor(cpau => cpau.PasswordConfirmation)
            .NotEmpty()
            .Matches(cpau => cpau.Password)
            .WithMessage("O campo de confirmação de senha deve ser igual ao campo senha");
    }
}
