using MediatR;
using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Common.Mappers;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Order.Queries.GetOrderByIdQuery;

public class GetOrderByIdQueryHandler(IOrderRepository repository) : IRequestHandler<GetOrderByIdQuery, OrderDTO> {
   
    private readonly IOrderRepository _repository = repository;
   
    public Task<OrderDTO> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken) {
        var order = _repository.Get(o => o.Id.Equals(request.OrderId), query => {
                return query
                    .Include(o => o.User)
                    .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product);
        });

        if (order == null)         
            throw new EntityNotFoundException("pedido não encontrado");

        return Task.FromResult(OrderMapper.ToDTO(order));
    }
}