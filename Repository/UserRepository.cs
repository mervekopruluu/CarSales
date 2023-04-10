using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(ApiDbContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<User?> GetUser(string userId) => await FindByCondition(u => u.Id == userId).FirstOrDefaultAsync();

    public async Task<User> CreateUser(User user) => await Create(user);

    public User UpdateUser(User user) => Update(user);

    public async Task DeleteUser(string userId) => await Delete(x => x.Id == userId);
}