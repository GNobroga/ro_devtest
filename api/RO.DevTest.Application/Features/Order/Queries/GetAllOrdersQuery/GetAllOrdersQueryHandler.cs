using System.Linq.Expressions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RO.DevTest.Application.Common.Mappers;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Features.Order.Queries.GetAllOrdersQuery;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Domain.Models;

namespace RO.DevTest.Application.Features.Order.Commands.DeleteOrderCommand;

public class GetAllOrdersQueryHandler(IOrderRepository repository) : IRequestHandler<GetAllOrdersQuery, PageResult<OrderDTO>> {
    
    private readonly IOrderRepository _repository = repository;

    public async Task<PageResult<OrderDTO>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken) {
        var filter = request.Filter;
        var searchFields = request.SearchFields ?? Enumerable.Empty<string>();

        var result = await _repository.GetPagedAndSortedResultsAsync(
            filter: filter, 
            properties: searchFields.ToArray(), 
            query => {
                return query.Include(o => o.User)
                        .Include(o => o.OrderProducts)
                        .ThenInclude(op => op.Product);
            }
        );
        
        return result.Map(OrderMapper.ToDTO);
    }
}