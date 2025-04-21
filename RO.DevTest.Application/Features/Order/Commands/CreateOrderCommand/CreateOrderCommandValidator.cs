using FluentValidation;

namespace RO.DevTest.Application.Features.Order.Commands.CreateOrderCommand;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand> {
    public CreateOrderCommandValidator() {
        RuleFor(o => o.Items)
            .NotEmpty()
            .WithMessage("É necessário ter pelo menos um item no pedido")
            .ForEach(item => {
                item.Must(i => i?.ProductId is not null)
                    .WithMessage("O id do item não pode ser nulo");

                item.Must(i => i.Quantity > 0)
                    .WithMessage("A quantidade de cada item deve ser maior que zero");
            });
    }
}