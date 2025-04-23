
namespace RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;

public record CreateProductResult {

    public string Id { get; set; } = string.Empty;
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; } 

    public DateTime CreatedOn { get; set; }

    public CreateProductResult(Domain.Entities.Product product) {
        Id = product?.Id.ToString()!;
        Name = product!.Name;
        Price = product.Price;
        Description = product.Description;
        CreatedOn = product.CreatedOn;
    }
}