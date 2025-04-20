namespace RO.DevTest.Application.Features.Queries.GetProductByIdQuery;

public record GetProductByIdResult(string Name, decimal Price, string Description) {
    public GetProductByIdResult(Domain.Entities.Product entity) : this(entity.Name, entity.Price, entity.Description ?? string.Empty) {}
}