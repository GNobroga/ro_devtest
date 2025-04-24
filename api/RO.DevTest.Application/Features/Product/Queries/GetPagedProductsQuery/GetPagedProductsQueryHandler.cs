using MediatR;
using RO.DevTest.Application.Common.Mappers;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Domain.Models;

namespace RO.DevTest.Application.Features.Product.Queries.GetPagedProductsQuery;

public class GetPagedProductsQueryHandler(IProductRepository repository) : IRequestHandler<GetPagedProductsQuery, PageResult<ProductDTO>> {
    
    private readonly IProductRepository _repository = repository;
    public async Task<PageResult<ProductDTO>> Handle(GetPagedProductsQuery request, CancellationToken cancellationToken){
        PagedFilter filter = request.Filter;
        string[] searchFields = request.SearchFields;
        PageResult<Domain.Entities.Product> pageResultProducts = await _repository.GetPagedAndSortedResultsAsync(filter, searchFields);
        return pageResultProducts.Map(ProductMapper.ToDTO);
    }
}