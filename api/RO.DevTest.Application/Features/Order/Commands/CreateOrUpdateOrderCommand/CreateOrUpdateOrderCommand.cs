
using MediatR;
using RO.DevTest.Application.DTOs;

namespace RO.DevTest.Application.Features.Order.Commands.CreateOrUpdateOrderCommand;

public class OrderItemDto {
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CreateOrUpdateOrderCommand(List<OrderItemDto> items) : IRequest<OrderDTO> {
    public string? UserId { get; set; } = default!;
    public Guid? OrderId { get; set; }
    public List<OrderItemDto> Items { get; set; } = items;
    public bool IsUpdated => OrderId is not null;
}