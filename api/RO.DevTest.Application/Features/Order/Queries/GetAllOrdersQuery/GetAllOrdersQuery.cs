using MediatR;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Domain.Models;

namespace RO.DevTest.Application.Features.Order.Queries.GetAllOrdersQuery;

public class GetAllOrdersQuery(PagedFilter filter, params string[] searchFields) : IRequest<PageResult<OrderDTO>> {
    public string[] SearchFields { get; set; } = searchFields ?? [];
    public PagedFilter Filter { get; set; } = filter;
}