using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository;

public class CarRepository : RepositoryBase<Car>, ICarRepository
{
    public CarRepository(ApiDbContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<Car> Add(Car car) => await Create(car);
    public async Task<Car?> Get(Guid id) => await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<Car> Buy(Guid id, string userId, decimal price)
    {
        var car = await Get(id);
        if (car is null)
            throw new InvalidOperationException("Car not found");

        if (car.SalerId == userId)
            throw new InvalidOperationException("You can't buy your own car");
        if (car.Status == InternalStatusEnum.Sold)
            throw new InvalidOperationException("Car already sold");
        if (car.Status == InternalStatusEnum.PartiallyPaid && car.CustomerId != userId)
            throw new InvalidOperationException("Car already sold to another customer");

        car.CustomerId = userId;
        car.RemainingPayment -= price;
        car.Status = car.RemainingPayment <= 0 ? InternalStatusEnum.Sold : InternalStatusEnum.PartiallyPaid;
        return Update(car);
    }

    public async Task<Car> Sell(Guid id, string userId)
    {
        var car = await Get(id);
        if (car is null)
            throw new InvalidOperationException("Car not found");
        
        if (car.SalerId != userId)
            throw new InvalidOperationException("You can't sell car that you don't own");
        if (car.Status == InternalStatusEnum.Sold)
            throw new InvalidOperationException("Car already sold");

        car.CustomerId = userId;
        car.RemainingPayment = 0;
        car.Status = InternalStatusEnum.Sold;
        return Update(car);
    }

    public async Task<List<Car>> GetByStatus(InternalStatusEnum status, string? customerId = null) =>
        await FindByCondition(x => x.Status == status &&
                                   (customerId == null || x.CustomerId == customerId))
            .Include(x => x.Customer).Include(x => x.Saler).ToListAsync();

    public async Task<List<Car>> SoldList(string salerId) =>
        await FindByCondition(x => x.SalerId == salerId).Include(x => x.Customer).Include(x => x.Saler).ToListAsync();

    public async Task<List<Car>> SoldOutList() => await FindByCondition(x => x.Status == InternalStatusEnum.Sold)
        .Include(x => x.Customer).Include(x => x.Saler).ToListAsync();
}