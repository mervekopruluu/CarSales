using System.Security.Claims;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Public.Dtos.Car;
using Repository.Contracts;
using Service.Contracts;

namespace Service;

public class CarService : ICarService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CarService(IRepositoryManager repositoryManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Car?> Get(Guid id) => await _repositoryManager.Car.Get(id);

    public async Task<List<CarDto>> GetAll() =>
        _mapper.Map<List<CarDto>>(await _repositoryManager.Car.GetByStatus(InternalStatusEnum.ReadyForSale));

    public async Task<CarDto> Buy(Guid id, decimal price) =>
        _mapper.Map<CarDto>(await _repositoryManager.Car.Buy(id, GetCurrentUserId(), price));

    public async Task<CarDto> Sell(Guid id) =>
        _mapper.Map<CarDto>(await _repositoryManager.Car.Sell(id, GetCurrentUserId()));

    public async Task<List<CarDto>> SoldList() =>
        _mapper.Map<List<CarDto>>(await _repositoryManager.Car.SoldList(GetCurrentUserId()));

    private string? GetCurrentUserId() =>
        _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public async Task<List<CarDto>> PurchasedList() => _mapper.Map<List<CarDto>>(
        await _repositoryManager.Car.GetByStatus(InternalStatusEnum.Sold, GetCurrentUserId()));

    public async Task<List<CarDto>> WaitingForPaymentList() => _mapper.Map<List<CarDto>>(
        await _repositoryManager.Car.GetByStatus(InternalStatusEnum.PartiallyPaid, GetCurrentUserId()));

    public async Task<CarDto> Add(CarDtoForCreate input)
    {
        var currentUserId = GetCurrentUserId();
        var car = _mapper.Map<Car>(input);
        car.SalerId = currentUserId;
        var result = await _repositoryManager.Car.Add(car);
        await _repositoryManager.SaveAsync();
        return _mapper.Map<CarDto>(result);
    }

    public CarDto Update(CarDtoForUpdate input)
    {
        var car = _mapper.Map<Car>(input);
        return _mapper.Map<CarDto>(_repositoryManager.Car.Update(car));
    }

    public async Task<List<CarDto>> SoldOutList() =>
        _mapper.Map<List<CarDto>>(await _repositoryManager.Car.SoldOutList());
}