
using System.Text.Json.Serialization;
using RO.DevTest.Domain.Enums;

namespace RO.DevTest.Application.Features.Order.Commands.CreateOrUpdateOrderCommand;
public record OrderProductDTO(
    Guid Id,
    string Name,
    decimal Price,
    int Quantity
);

public record UserDTO(
    string Id,
    string Name,
    string Email
);

public class CreateOrUpdateOrderResult(DateTime placedAt, OrderStatus status, decimal total, UserDTO user, List<OrderProductDTO> items) {

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus Status { get; set; } = status;
    public DateTime PlacedAt { get; set; } = placedAt;

    public decimal Total { get; set; } = total;
    public UserDTO User { get; set; } = user;
    public List<OrderProductDTO> Items  { get; set; } = items;

    public static CreateOrUpdateOrderResult FromOrder(Domain.Entities.Order order) {
        DateTime placedAt = order.CreatedOn;
        UserDTO user = new(order.User.Id, order.User.Name, order.User.Email!);

        List<OrderProductDTO> items = order.OrderProducts.Select(orderProduct => {
            var product = orderProduct.Product;
            var quantity = orderProduct.Quantity;
            return new OrderProductDTO(product.Id, product.Name, product.Price, quantity);
        }).ToList();

        decimal total = items.Aggregate((decimal) 0, (acc, curr) =>  acc + (curr.Price * curr.Quantity));
  
        return new CreateOrUpdateOrderResult(placedAt, order.Status, total, user, items);
    }

   
}