using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RO.DevTest.Application.Common.Mappers;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Domain.Models;

namespace RO.DevTest.Application.Features.Order.Queries.GetOrdersByUserQuery;

public class GetOrdersByUserQueryHandler(IOrderRepository repository) : IRequestHandler<GetOrdersByUserQuery, PageResult<OrderDTO>> {

    private readonly IOrderRepository _repository = repository;

    public async Task<PageResult<OrderDTO>> Handle(GetOrdersByUserQuery request, CancellationToken cancellationToken) {
        PagedFilter filter = request.Filter;
        IEnumerable<string> searchFields = request.SearchFields ?? [];

        var pageResult = await _repository.GetPagedAndSortedResultsAsync(
            filter: filter,
            properties: searchFields,
            query => {
                return query.Include(o => o.User)
                        .Include(o => o.OrderProducts)
                        .ThenInclude(op => op.Product);
            }
        );

       return pageResult.Map(OrderMapper.ToDTO);
    }
}