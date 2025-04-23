using System.Linq.Expressions;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Contracts.Persistance.Repositories;

public interface IProductRepository : IBaseRepository<Product> {
    public Task<bool> ExistsById(Guid id);
}