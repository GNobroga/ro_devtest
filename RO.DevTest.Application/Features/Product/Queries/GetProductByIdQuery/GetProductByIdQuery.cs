using MediatR;
namespace RO.DevTest.Application.Features.Queries.GetProductByIdQuery;

public class GetProductByIdQuery : IRequest<GetProductByIdResult> {
    public Guid ProductId { get; set; }
}