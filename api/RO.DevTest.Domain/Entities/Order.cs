using Microsoft.EntityFrameworkCore.Migrations.Operations;
using RO.DevTest.Domain.Abstract;
using RO.DevTest.Domain.Enums;

namespace RO.DevTest.Domain.Entities;

public class Order : BaseEntity {
    
    public string UserId { get; set; } = default!;

    public User User { get; set; } = default!;

    public OrderStatus Status { get; set;} = OrderStatus.Pending;

    public ICollection<OrderProduct> OrderProducts { get; set;} = default!;

    public List<Product> Products => OrderProducts is null ? [] : OrderProducts.Select(item => item.Product).ToList();

    public void AddOrderProduct(OrderProduct item) {
        OrderProducts ??= [];
        OrderProducts.Add(item);
    }
}