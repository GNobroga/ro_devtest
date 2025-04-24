using MediatR;
using RO.DevTest.Application.Common.Mappers;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Product.Queries.GetProductByIdQuery;

public class GetProductByIdQueryHandler(IProductRepository repository) : IRequestHandler<GetProductByIdQuery, ProductDTO> {
   
    private readonly IProductRepository _repository = repository;

    public Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) {
        var productId = request.ProductId;

        var product = _repository.Get(product => product.Id == productId) 
            ?? throw new EntityNotFoundException($"Produto com ID {productId} não encontrado.");

        return Task.FromResult(ProductMapper.ToDTO(product));
    }
}