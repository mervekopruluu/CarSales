using Entities.Models;

namespace Repository.Contracts;

public interface IUserRepository
{
    Task<User?> GetUser(string userId);
    Task<User> CreateUser(User user);
    User UpdateUser(User user);
    Task DeleteUser(string userId);
}