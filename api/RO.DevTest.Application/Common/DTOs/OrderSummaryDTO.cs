namespace RO.DevTest.Application.DTOs;

public record OrderSummaryProductDTO(
    Guid Id,
    string Name,
    decimal Total
);
public record OrderSummaryDTO(
    long TotalOrders,
    long TotalClients,
    decimal Revenue,
    List<OrderSummaryProductDTO> Products
) {
}