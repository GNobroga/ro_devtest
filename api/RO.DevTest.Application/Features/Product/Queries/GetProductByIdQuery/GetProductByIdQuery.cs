using MediatR;
using RO.DevTest.Application.DTOs;
namespace RO.DevTest.Application.Features.Product.Queries.GetProductByIdQuery;

public class GetProductByIdQuery(Guid productId) : IRequest<ProductDTO> {
    public Guid ProductId { get; set; } = productId;
}