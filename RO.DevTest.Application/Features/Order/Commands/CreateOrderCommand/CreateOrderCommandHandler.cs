using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Extensions;
using RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Order.Commands.CreateOrderCommand;

public class CreateOrderCommandHandler(
    IOrderRepository repository, 
    IOrderProductRepository orderProductRepository,
    IUserRepository userRepository,
    IProductRepository productRepository

) : IRequestHandler<CreateOrderCommand, CreateOrderResult> {
   
   private readonly IOrderRepository _repository = repository;
   private readonly IOrderProductRepository _orderProductRepository = orderProductRepository;
   private readonly IUserRepository _userRepository = userRepository;
   private readonly IProductRepository _productRepository = productRepository;
   
    public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken) {
        await request.ThrowIfInvalidCommandAsync(new CreateOrderCommandValidator());

        string userId = request.UserId;
        bool existsUser = await _userRepository.ExistsById(userId);

        if (!existsUser) 
            throw new EntityNotFoundException("Usuario não existe");

        var order = new Domain.Entities.Order() {
            UserId = userId
        };

        await VerifyAllProductsExistAsync(request.Items.Select(item => item.ProductId));

        foreach(var item in request.Items) {
            var product = new Domain.Entities.Product(item.ProductId);
            order.AddOrderProduct(new OrderProduct(item.ProductId, order.Id, item.Quantity));
        }

        await _repository.CreateAsync(order);

        order.Products.ForEach(product => Console.WriteLine(product.Name));

        return CreateOrderResult.FromOrder(order);
    }

    private async Task VerifyAllProductsExistAsync(IEnumerable<Guid> productIds) {
        productIds ??= [];
        var results = await Task.WhenAll(productIds.Select(_productRepository.ExistsById));
        if (results.Any(exists => !exists)) 
            throw new BadRequestException("One or more provided product IDs do not exist.");
    }
}