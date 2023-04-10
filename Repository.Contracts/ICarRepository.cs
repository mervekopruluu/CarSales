using Entities.Models;

namespace Repository.Contracts;

public interface ICarRepository
{
    Task<Car?> Get(Guid id);
    Task<Car> Add(Car car);
    Task<Car> Buy(Guid id, string userId, decimal price);
    Task<Car> Sell(Guid id, string userId);
    Task<List<Car>> GetByStatus(InternalStatusEnum status, string? customerId = null);
    Task<List<Car>> SoldList(string salerId);
    Car Update(Car car);
    Task<List<Car>> SoldOutList();
}