
namespace RO.DevTest.Application.Features.Order.Commands.DeleteOrderCommand;

public record DeleteOrderResult(
    Guid OrderId,
    bool Deleted
) {
    public static DeleteOrderResult Failure(Guid orderId) => new DeleteOrderResult(orderId, false);
}