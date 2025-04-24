using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Extensions;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Order.Queries.GetOrderSummaryByPeriodQuery;

public class GetOrderSummaryByPeriodQueryHandler(IOrderRepository repository) : IRequestHandler<GetOrderSummaryByPeriodQuery, OrderSummaryDTO> {

    private readonly IOrderRepository _repository = repository;
    public async Task<OrderSummaryDTO> Handle(GetOrderSummaryByPeriodQuery request, CancellationToken cancellationToken) {
        await request.ThrowIfInvalidCommandAsync(new GetOrderSummaryByPeriodQueryValidator());

        var startDate = request.StartDate!.Value;
        var endDate = request.EndDate!.Value;

        if (startDate > endDate) 
            throw new BadRequestException("A data de inicio não pode ser maior que a data final");
        
        Domain.Enums.OrderStatus? status =  request.Status;
        return await _repository.GetOrderSummaryByPeriodAsync(startDate, endDate, status);
    }
}