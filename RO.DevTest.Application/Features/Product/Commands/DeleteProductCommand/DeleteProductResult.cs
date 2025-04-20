namespace RO.DevTest.Application.Features.Product.Commands.DeleteProductCommand;

public record DeleteProductResult(Guid ProductId, bool IsDeleted);