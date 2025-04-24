using MediatR;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Domain.Enums;

namespace RO.DevTest.Application.Features.Order.Queries.GetOrderSummaryByPeriodQuery;

public class GetOrderSummaryByPeriodQuery : IRequest<OrderSummaryDTO> {
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public OrderStatus? Status { get; set; }
}