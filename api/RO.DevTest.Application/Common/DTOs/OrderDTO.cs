using System.Text.Json.Serialization;
using RO.DevTest.Domain.Enums;

namespace RO.DevTest.Application.DTOs;

public record OrderProductDTO(
    Guid Id,
    string Name,
    decimal Price,
    int Quantity
);

public record OrderUserDTO(
    string Id,
    string Name,
    string Email
);

public class OrderDTO(DateTime placedAt, OrderStatus status, decimal total, OrderUserDTO user, List<OrderProductDTO> items) {

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus Status { get; set; } = status;
    public DateTime PlacedAt { get; set; } = placedAt;

    public decimal Total { get; set; } = total;
    public OrderUserDTO User { get; set; } = user;
    public List<OrderProductDTO> Items  { get; set; } = items;
}