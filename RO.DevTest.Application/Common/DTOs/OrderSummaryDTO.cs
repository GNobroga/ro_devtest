namespace RO.DevTest.Application.DTOs;

public record ProductDTO(
    Guid Id,
    string Name,
    decimal Total
);
public record OrderSummaryDTO(
    long TotalOrders,
    decimal Revenue,
    List<ProductDTO> Products
);