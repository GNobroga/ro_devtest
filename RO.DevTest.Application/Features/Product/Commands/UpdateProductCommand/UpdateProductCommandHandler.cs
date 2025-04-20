using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Extensions;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Product.Commands.UpdateProductCommand;

public class UpdateProductCommandHandler(IProductRepository repository) : IRequestHandler<UpdateProductCommand, UpdateProductResult> {
    private readonly IProductRepository _repository = repository;
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken) {
        await request.ThrowIfInvalidCommandAsync(new UpdateProductCommandValidator());
        
        Guid productId = request.ProductId;

        var product = _repository.Get(p => p.Id == productId)
            ?? throw new EntityNotFoundException($"Produto com ID {productId} não encontrado.");


        MapToEntity(request, product);
        
        await _repository.UpdateAsync(product);

        return new UpdateProductResult(product);
    }

    private static void MapToEntity(UpdateProductCommand source, Domain.Entities.Product target) {
        var product = source.AssignTo();
        target.Name = product.Name;
        target.Price = product.Price;
        target.Description = product.Description;
    } 
}
