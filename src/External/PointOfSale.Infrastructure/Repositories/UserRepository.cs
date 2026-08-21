using Microsoft.EntityFrameworkCore;
using PointOfSale.Model.Repositories;
using PointOfSale.Model.Users;

namespace PointOfSale.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User, UserId>, IUserRepository
{
    
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User> GetByNameAsync(string name)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Name == name);
    }
}