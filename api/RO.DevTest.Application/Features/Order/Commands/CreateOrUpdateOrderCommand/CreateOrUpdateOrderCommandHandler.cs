using MediatR;
using RO.DevTest.Application.Common.Mappers;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.DTOs;
using RO.DevTest.Application.Extensions;
using RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Domain.Enums;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Order.Commands.CreateOrUpdateOrderCommand;

public class CreateOrUpdateOrderCommandHandler(
    IOrderRepository repository, 
    IOrderProductRepository orderProductRepository,
    IUserRepository userRepository,
    IProductRepository productRepository

) : IRequestHandler<CreateOrUpdateOrderCommand, OrderDTO> {
   
   private readonly IOrderRepository _repository = repository;
   private readonly IOrderProductRepository _orderProductRepository = orderProductRepository;
   private readonly IUserRepository _userRepository = userRepository;
   private readonly IProductRepository _productRepository = productRepository;
   
    public async Task<OrderDTO> Handle(CreateOrUpdateOrderCommand request, CancellationToken cancellationToken) {
        await request.ThrowIfInvalidCommandAsync(new CreateOrUpdateOrderCommandValidator());

        string userId = request.UserId!;
        bool isUpdate = request.IsUpdated;
        bool existsUser = await _userRepository.ExistsById(userId);

        if (!existsUser) 
            throw new EntityNotFoundException("Usuario não existe");

        Domain.Entities.Order order = new() { UserId = userId };

        if (isUpdate) {
            order = _repository.Get(o => o.Id.Equals(request.OrderId)) 
                ?? throw new EntityNotFoundException("Pedido não encontrado");

            if (!OrderStatus.Pending.Equals(order.Status)) 
                throw new BadRequestException("O pedido precisa esta no status de 'pendente' para que possa ser modificado");

            await DeleteAllOrderProducts(order);
        }

        var productIds = request.Items.Select(item => item.ProductId).DistinctBy(id => id);

        await VerifyAllProductsExistAsync(productIds);

        foreach(var item in MergeDuplicateOrderProducts(request.Items)) {
            var product = new Domain.Entities.Product(item.ProductId);
            order.AddOrderProduct(new OrderProduct(item.ProductId, order.Id, item.Quantity));
        }

        await _repository.CreateAsync(order, cancellationToken);

        return OrderMapper.ToDTO(order);
    }

    public async Task DeleteAllOrderProducts(Domain.Entities.Order order) {
        if (order?.OrderProducts is null || order.OrderProducts.Count == 0)
            return;

        var deleteTasks = order.OrderProducts.Select( _orderProductRepository.DeleteAsync);

        await Task.WhenAll(deleteTasks);
    }

    private static List<OrderItemDto> MergeDuplicateOrderProducts(List<OrderItemDto> items) {
        var orderItems = items
            .GroupBy(item => item.ProductId)
            .Select(group => new OrderItemDto {
                ProductId = group.Key,
                Quantity = group.Sum(i => i.Quantity)
            })
            .ToList();

        return orderItems;
    }

    private async Task VerifyAllProductsExistAsync(IEnumerable<Guid> productIds) {
        productIds ??= [];
        var results = await Task.WhenAll(productIds.Select(_productRepository.ExistsById));
        if (results.Any(exists => !exists)) 
            throw new BadRequestException("One or more provided product IDs do not exist.");
    }
}