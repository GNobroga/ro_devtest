using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Queries.GetProductByIdQuery;

public class GetProductByIdQueryHandler(IProductRepository repository) : IRequestHandler<GetProductByIdQuery, GetProductByIdResult> {
   
    private readonly IProductRepository _repository = repository;

    public Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) {
        var productId = request.ProductId;

        var product = _repository.Get(product => product.Id == productId) 
            ?? throw new EntityNotFoundException($"Produto com ID {productId} não encontrado.");

        return Task.FromResult(new GetProductByIdResult(product));
    }
}