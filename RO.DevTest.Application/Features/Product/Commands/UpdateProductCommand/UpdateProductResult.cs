
namespace RO.DevTest.Application.Features.Product.Commands.UpdateProductCommand;

public record UpdateProductResult {

    public string Id { get; set; } = string.Empty;
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; } 

    public DateTime CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }

    public UpdateProductResult(Domain.Entities.Product product) {
        Id = product?.Id.ToString()!;
        Name = product!.Name;
        Price = product.Price;
        Description = product.Description;
        CreatedOn = product.CreatedOn;
        ModifiedOn = product.ModifiedOn;
    }
}