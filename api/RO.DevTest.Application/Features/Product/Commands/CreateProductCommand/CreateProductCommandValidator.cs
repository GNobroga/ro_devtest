using FluentValidation;

namespace RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand> {
    
    public CreateProductCommandValidator() {
        RuleFor(cpc => cpc.Name)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.")
            .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(cpc => cpc.Price)
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

        RuleFor(cpc => cpc.Description)
            .MaximumLength(255).WithMessage("A descrição deve ter no máximo 255 caracteres.");
    }
}

