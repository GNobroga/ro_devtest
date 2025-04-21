
using MediatR;
using RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;

namespace RO.DevTest.Application.Features.Order.Commands.CreateOrderCommand;

public class OrderItemDto {
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CreateOrderCommand : IRequest<CreateOrderResult> {
    public string UserId { get; set; } = default!;
    public List<OrderItemDto> Items { get; set; } = default!;
}