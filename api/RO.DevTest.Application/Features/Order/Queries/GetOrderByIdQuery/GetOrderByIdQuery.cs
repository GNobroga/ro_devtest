using MediatR;
using RO.DevTest.Application.DTOs;

namespace RO.DevTest.Application.Features.Order.Queries.GetOrderByIdQuery;

public class GetOrderByIdQuery(Guid id) : IRequest<OrderDTO> {
    public Guid OrderId { get; set; } = id;
} 