using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Persistence.Repositories;

public class UserRepository(DefaultContext context) : BaseRepository<User>(context), IUserRepository {
    public async Task<bool> ExistsById(string id) => await ExistsBy(u => u.Id.Equals(id));
}
