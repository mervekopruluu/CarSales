using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Public.Dtos.Car;
using Service.Contracts;

namespace API.Controllers;

[Authorize]
[Route("api/cars")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public CarController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetCar(Guid id)
    {
        var car = await _serviceManager.CarService.Get(id);
        return Ok(car);
    }

    [HttpGet]
    public async Task<ActionResult> GetCars()
    {
        var cars = await _serviceManager.CarService.GetAll();
        return Ok(cars);
    }

    [HttpPost]
    public async Task<ActionResult> Add(CarDtoForCreate input)
    {
        var carEntity = await _serviceManager.CarService.Add(input);
        return Ok(carEntity);
    }

    [HttpPut]
    public async Task<ActionResult> Update(CarDtoForUpdate input)
    {
        var car = _serviceManager.CarService.Update(input);
        return Ok(car);
    }
    
    [HttpGet("my-cars")]
    public async Task<ActionResult> GetMyCars()
    {
        var cars = await _serviceManager.CarService.SoldList();
        return Ok(cars);
    }
    
    [HttpGet("purchased-cars")]
    public async Task<ActionResult> GetPurchasedCars()
    {
        var cars = await _serviceManager.CarService.PurchasedList();
        return Ok(cars);
    }
    
    [HttpGet("sold-outs")]
    public async Task<ActionResult> GetSoldOuts()
    {
        var cars = await _serviceManager.CarService.SoldOutList();
        return Ok(cars);
    }
    
    [HttpGet("waiting-for-payment-cars")]
    public async Task<ActionResult> GetWaitingForPaymentCars()
    {
        var cars = await _serviceManager.CarService.WaitingForPaymentList();
        return Ok(cars);
    }
    
    [HttpPut("buy/{id}")]
    public async Task<ActionResult> Buy(Guid id, [FromBody] decimal price)
    {
        
        var car = await _serviceManager.CarService.Buy(id, price);
        return Ok(car);
    }
    
    [HttpPut("sell/{id}")]
    public async Task<ActionResult> Sell(Guid id)
    {
        var car = await _serviceManager.CarService.Sell(id);
        return Ok(car);
    }
    
    [HttpPut("pay/{id}")]
    public async Task<ActionResult> Pay(Guid id, [FromBody] decimal price)
    {
        var car = await _serviceManager.CarService.Buy(id, price);
        return Ok(car);
    }
}