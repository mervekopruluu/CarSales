using Entities.Models;
using Public.Dtos.Car;

namespace Service.Contracts;

public interface ICarService
{
    Task<Car?> Get(Guid id);
    Task<List<CarDto>> GetAll();
    Task<CarDto> Buy(Guid id, decimal price);
    Task<CarDto> Sell(Guid id);
    Task<List<CarDto>> SoldList(); 
    Task<List<CarDto>> PurchasedList();
    Task<List<CarDto>> WaitingForPaymentList();
    Task<CarDto> Add(CarDtoForCreate car);
    CarDto Update(CarDtoForUpdate car);
    Task<List<CarDto>> SoldOutList();
}