
using MediatR;

namespace RO.DevTest.Application.Features.Order.Commands.DeleteOrderCommand;

public class DeleteOrderCommand(Guid orderId) : IRequest<DeleteOrderResult> {
    public Guid OrderId { get; set; } = orderId;
}