using MediatR;
using RO.DevTest.Domain.Enums;

namespace RO.DevTest.Application.Features.Order.Commands.ChangeOrderStatusCommand;
public class ChangeOrderStatusCommand : IRequest<ChangeOrderStatusResult> {
    public Guid? OrderId { get; set; } = default!;
    public OrderStatus Status { get; set; }
}