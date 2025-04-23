using MediatR;
using RO.DevTest.Domain.Models;

namespace RO.DevTest.Application.Features.Queries.GetPagedProductsQuery;

public class GetPagedProductsQuery(PagedFilter filter, params string[] searchFields) : IRequest<PageResult<GetPagedProductsResult>> {
    public string[] SearchFields { get; set; } = searchFields ?? [];
    public PagedFilter Filter { get; set; } = filter;
}