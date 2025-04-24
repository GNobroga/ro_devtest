using System.Security.Cryptography;
using RO.DevTest.Application.DTOs;

namespace RO.DevTest.Application.Common.Mappers;

public static class OrderMapper {

    public static OrderDTO ToDTO(Domain.Entities.Order order) {
        var user = new UserDTO(order.User.Id, order.User.Name, order.User.Email!);

        var items = order.OrderProducts
            .Select(op => new OrderProductDTO(
                op.Product.Id,
                op.Product.Name,
                op.Product.Price,
                op.Quantity))
            .ToList();

        var total = items.Sum(item => item.Price * item.Quantity);

        return new OrderDTO(order.CreatedOn, order.Status, total, user, items);
    }


}