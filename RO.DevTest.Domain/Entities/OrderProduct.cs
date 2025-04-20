using System.ComponentModel.DataAnnotations.Schema;
using RO.DevTest.Domain.Abstract;

namespace RO.DevTest.Domain.Entities;

public class OrderProduct : BaseEntity {
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;

    public Guid OrderId { get; set; }
    public Order Order { get; set; } = default!;

    public int Quantity { get; set; } = 1;
}