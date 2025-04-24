using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Domain.Enums;
using RO.DevTest.Persistence;
using RO.DevTest.Persistence.Repositories;


public class OrderRepository(DefaultContext context) : BaseRepository<Order>(context), IOrderRepository {
    public async Task<OrderSummaryDTO> GetOrderSummaryByPeriodAsync(DateOnly startDate, DateOnly endDate, OrderStatus? status) {
        var baseQuery = Entities.Where(o =>  
            DateOnly.FromDateTime(o.CreatedOn) >= startDate && 
            DateOnly.FromDateTime(o.CreatedOn) <= endDate &&
           (!status.HasValue || o.Status == status.Value)
        ).AsNoTracking();

        var baseProductsQuery = baseQuery.Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .SelectMany(o => o.OrderProducts);

        var countOrders = await baseQuery.CountAsync();

        var countCustomer = await Context.Users
            .Where(u => Context.UserRoles
                .Any(ur => ur.UserId == u.Id && 
                        Context.Roles.Any(r => r.Id == ur.RoleId && r.Name == "Customer")))
            .CountAsync();

        var revenue = await baseProductsQuery
            .Select(o => o.Product.Price * o.Quantity)
            .SumAsync();

        var productDtos = baseProductsQuery
            .Select(o => new OrderSummaryProductDTO(o.ProductId, o.Product.Name, o.Product.Price * o.Quantity));

        return new OrderSummaryDTO(countOrders, countCustomer, revenue, await productDtos.ToListAsync());
    }
}