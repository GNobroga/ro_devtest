using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;

namespace RO.DevTest.Application.Features.Product.Commands.DeleteProductCommand;

public class DeleteProductCommandHandler(IProductRepository repository) : IRequestHandler<DeleteProductCommand, DeleteProductResult> {
   
    private readonly IProductRepository _repository = repository;
   
    public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken) {
        var product = _repository.Get(product => product.Id == request.ProductId);

        if (product is null) 
            return DeleteProductResult.Failure(request.ProductId);
        
        await _repository.DeleteAsync(product);

        return new DeleteProductResult(request.ProductId, true);
    }
}