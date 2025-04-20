using MediatR;

namespace RO.DevTest.Application.Features.Product.Commands.DeleteProductCommand;

public class DeleteProductCommand : IRequest<DeleteProductResult> {
    public Guid ProductId { get; set; }
}