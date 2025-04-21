using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Persistence;
using RO.DevTest.Persistence.Repositories;


public class OrderRepository(DefaultContext context) : BaseRepository<Order>(context), IOrderRepository {}