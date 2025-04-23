namespace RO.DevTest.Application.Features.Product.Commands.DeleteProductCommand;

public record DeleteProductResult(Guid ProductId, bool IsDeleted) {

    public static DeleteProductResult Failure(Guid productId) => new(productId, false);
}