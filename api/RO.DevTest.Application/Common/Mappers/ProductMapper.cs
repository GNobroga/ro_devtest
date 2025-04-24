using RO.DevTest.Application.DTOs;

namespace RO.DevTest.Application.Common.Mappers;

public static class ProductMapper {
    public static ProductDTO ToDTO(Domain.Entities.Product product) {
        return new ProductDTO(product.Id, product.Name, product.Price, product.Description);
    }
}