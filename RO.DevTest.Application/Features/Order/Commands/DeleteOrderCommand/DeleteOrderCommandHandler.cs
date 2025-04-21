using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Order.Commands.DeleteOrderCommand;

public class DeleteOrderCommandHandler(IOrderRepository repository) : IRequestHandler<DeleteOrderCommand, DeleteOrderResult> {
    
    private readonly IOrderRepository _repository = repository;
    
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken) {
        var order = _repository.Get(o => o.Id.Equals(request.OrderId));

        if (order is null) 
            return DeleteOrderResult.Failure(request.OrderId);

        await _repository.DeleteAsync(order);
        
        return new DeleteOrderResult(request.OrderId, true);
    }
}