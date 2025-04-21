
using MediatR;

namespace RO.DevTest.Application.Features.Order.Commands.CreateOrUpdateOrderCommand;

public class OrderItemDto {
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CreateOrUpdateOrderCommand(string userId, List<OrderItemDto> items) : IRequest<CreateOrUpdateOrderResult> {
    public string UserId { get; set; } = userId;
    public Guid? OrderId { get; set; }
    public List<OrderItemDto> Items { get; set; } = items;
    public bool IsUpdated => OrderId is not null;
}