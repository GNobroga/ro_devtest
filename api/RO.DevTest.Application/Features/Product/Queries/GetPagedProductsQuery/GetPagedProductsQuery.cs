using MediatR;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Domain.Models;

namespace RO.DevTest.Application.Features.Product.Queries.GetPagedProductsQuery;

public class GetPagedProductsQuery(PagedFilter filter, params string[] searchFields) : IRequest<PageResult<ProductDTO>> {
    public string[] SearchFields { get; set; } = searchFields ?? [];
    public PagedFilter Filter { get; set; } = filter;
}