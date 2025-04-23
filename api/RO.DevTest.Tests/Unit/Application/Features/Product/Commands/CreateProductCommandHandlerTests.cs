
using System.Threading.Tasks;
using Moq;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Tests.Unit.Application.Features.Product.Commands;

public class CreateProductCommandHandlerTests {

    [Fact]
    public async Task GivenCreateProductCommand_WhenHandled_ThenCreatesProduct() {
        CreateProductCommand command = new() {
            Name = "Smartphone",
            Description = "Celular",
            Price = 100
        };

        var mockProductRepository = new Mock<IProductRepository>();
        mockProductRepository.Setup(pr => pr.CreateAsync(It.IsAny<Domain.Entities.Product>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(command.AssignTo());  

        CreateProductCommandHandler handler = new(mockProductRepository.Object);

        var result = await handler.Handle(command, default);
        
        Assert.Equal(command.Name, result.Name);
    }

    [Fact]
    public async Task GivenCreateProductCommandWithInvalidData_WhenHandled_ThenThrowsException() {
        var mockProductRepository = new Mock<IProductRepository>();
        CreateProductCommandHandler handler = new(mockProductRepository.Object);
        await Assert.ThrowsAsync<BadRequestException>(async () => await handler.Handle(new CreateProductCommand(), default));
    }

}