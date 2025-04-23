using System.Text.Json.Serialization;
using RO.DevTest.Domain.Enums;

namespace RO.DevTest.Application.Features.Order.Commands.ChangeOrderStatusCommand;
public record ChangeOrderStatusResult {

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus NewStatus { get; set; }

    public ChangeOrderStatusResult(OrderStatus status) {
        NewStatus = status;
    }
}