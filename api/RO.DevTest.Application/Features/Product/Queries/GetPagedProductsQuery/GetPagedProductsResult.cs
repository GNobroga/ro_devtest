namespace RO.DevTest.Application.Features.Queries.GetPagedProductsQuery;

public record GetPagedProductsResult(string Name, decimal Price, string Description) {
    public GetPagedProductsResult(Domain.Entities.Product entity) : this(entity.Name, entity.Price, entity.Description ?? string.Empty) {}
}