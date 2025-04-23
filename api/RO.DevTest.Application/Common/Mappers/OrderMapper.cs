using System.Security.Cryptography;
using RO.DevTest.Application.DTOs;

namespace RO.DevTest.Application.Common.Mappers;

public static class OrderMapper {

    public static OrderDTO ToDTO(Domain.Entities.Order order) {
        DateTime placedAt = order.CreatedOn;
        UserDTO user = new(order.User.Id, order.User.Name, order.User.Email!);

        List<OrderProductDTO> items = order.OrderProducts.Select(orderProduct => {
            var product = orderProduct.Product;
            var quantity = orderProduct.Quantity;
            return new OrderProductDTO(product.Id, product.Name, product.Price, quantity);
        }).ToList();

        decimal total = items.Aggregate((decimal) 0, (acc, curr) =>  acc + (curr.Price * curr.Quantity));

        return new OrderDTO(placedAt, order.Status, total, user, items);
    }

}