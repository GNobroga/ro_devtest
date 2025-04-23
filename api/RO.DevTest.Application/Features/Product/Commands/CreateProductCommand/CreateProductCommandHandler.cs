using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Extensions;

namespace RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;

public class CreateProductCommandHandler(IProductRepository repository) : IRequestHandler<CreateProductCommand, CreateProductResult> {
    private readonly IProductRepository _repository = repository;
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken) {
        await request.ThrowIfInvalidCommandAsync(new CreateProductCommandValidator());

        var product = request.AssignTo();

        var createdProduct = await _repository.CreateAsync(product, cancellationToken);
        
        return new CreateProductResult(createdProduct);
    }
}
