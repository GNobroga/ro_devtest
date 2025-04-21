using MediatR;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Domain.Models;

namespace RO.DevTest.Application.Features.Order.Queries.GetOrdersByUserQuery;

public class GetOrdersByUserQuery(string userId, PagedFilter filter, IEnumerable<string> searchFields) : IRequest<PageResult<OrderDTO>> {
    public string UserId { get; set; } = userId;

    public PagedFilter Filter { get; set; } = filter;

    public IEnumerable<string>? SearchFields { get; set; } = searchFields;
}