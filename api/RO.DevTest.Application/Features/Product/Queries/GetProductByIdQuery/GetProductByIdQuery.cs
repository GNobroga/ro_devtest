using MediatR;
namespace RO.DevTest.Application.Features.Queries.GetProductByIdQuery;

public class GetProductByIdQuery(Guid productId) : IRequest<GetProductByIdResult> {
    public Guid ProductId { get; set; } = productId;
}