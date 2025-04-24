using RO.DevTest.Application.DTOs;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Domain.Enums;

namespace RO.DevTest.Application.Contracts.Persistance.Repositories;

public interface IOrderRepository : IBaseRepository<Order> { 
   Task<OrderSummaryDTO> GetOrderSummaryByPeriodAsync(DateOnly startDate, DateOnly endDate, OrderStatus? status);
}
