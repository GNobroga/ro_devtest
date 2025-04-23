using System.Collections.Generic;
using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Enums;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Order.Commands.ChangeOrderStatusCommand;

public class ChangeOrderStatusCommandHandler(IOrderRepository repository) : IRequestHandler<ChangeOrderStatusCommand, ChangeOrderStatusResult> {  
    private readonly IOrderRepository _repository = repository;
    public async Task<ChangeOrderStatusResult> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken) {
        
        var order = _repository.Get(o => o.Id.Equals(request.OrderId)) 
            ?? throw new EntityNotFoundException("Pedido não encontrado");

        order.Status = request.Status;

        await _repository.UpdateAsync(order);

        return new ChangeOrderStatusResult(request.Status);
    }
}