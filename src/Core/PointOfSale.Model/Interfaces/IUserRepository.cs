using PointOfSale.Model.Users;

namespace PointOfSale.Model.Repositories;

public interface IUserRepository : IGenericRepository<User, UserId>
{
    Task<User> GetByNameAsync(string name);
}