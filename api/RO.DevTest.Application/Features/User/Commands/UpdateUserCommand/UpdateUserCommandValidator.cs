using FluentValidation;

namespace RO.DevTest.Application.Features.User.Commands.UpdateUserCommand;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand> {

    public UpdateUserCommandValidator() {
       RuleFor(cpau => cpau.Email)
        .EmailAddress()
        .WithMessage("O campo e-mail precisa ser um e-mail válido")
        .When(cpau => !string.IsNullOrWhiteSpace(cpau.Email)); 

        RuleFor(cpau => cpau.UserName) 
            .NotEmpty()
            .WithMessage("O campo userName precisa ser preenchido")
            .MinimumLength(6)
            .WithMessage("O campo userName precisa ter, pelo menos, 3 caracters")
            .When(cpau => !string.IsNullOrWhiteSpace(cpau.UserName));

        RuleFor(cpau => cpau.Password)
            .MinimumLength(6)
            .WithMessage("O campo senha precisa ter, pelo menos, 6 caracteres")
            .When(cpau => !string.IsNullOrWhiteSpace(cpau.Password)); 

        RuleFor(cpau => cpau.PasswordConfirmation)
            .NotEmpty()
            .WithMessage("O campo de confirmação não pode ser vazio")
            .Matches(cpau => cpau.Password)
            .WithMessage("O campo de confirmação de senha deve ser igual ao campo senha")
            .When(cpau => !string.IsNullOrWhiteSpace(cpau.Password)); 

    }
}