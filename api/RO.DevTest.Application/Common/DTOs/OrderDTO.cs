using System.Text.Json.Serialization;
using RO.DevTest.Domain.Enums;

namespace RO.DevTest.Application.DTOs;

public record OrderProductDTO(
    Guid Id,
    string Name,
    decimal Price,
    int Quantity,
    string? ImageUrl
);

public record OrderUserDTO(
    string Id,
    string Name,
    string Email
);

public class OrderDTO(Guid id, DateTime placedAt, DateTime modifiedOn, OrderStatus status, decimal total, OrderUserDTO user, List<OrderProductDTO> items) {

    public Guid Id { get; set; } = id;
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus Status { get; set; } = status;
    public DateTime PlacedAt { get; set; } = placedAt;

    public DateTime ModifiedOn { get; set; } = modifiedOn;

    public decimal Total { get; set; } = total;
    public OrderUserDTO User { get; set; } = user;
    public List<OrderProductDTO> Items  { get; set; } = items;
}