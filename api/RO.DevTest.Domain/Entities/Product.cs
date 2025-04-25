using RO.DevTest.Domain.Abstract;

namespace RO.DevTest.Domain.Entities;

public class Product : BaseEntity {
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; } 

    public Product() {}
    public Product(Guid id) : base(id) {}

}