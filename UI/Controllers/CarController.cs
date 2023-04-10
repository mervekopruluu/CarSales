using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Public.Dtos.Car;

namespace UI.Controllers;

public class CarController : BaseController
{
    public CarController(IConfiguration configuration) : base(configuration)
    {
    }
    
    public async Task<IActionResult> Add(CarDtoForCreate input)
    {
        using var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        using var httpClient = new HttpClient(handler);
        var token = HttpContext.Session.GetString("token");
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        var response = await httpClient.PostAsJsonAsync(ApiUrl + "/cars", input);
        
        var responseJson = await response.Content.ReadAsStringAsync();
        var carResponse = JsonConvert.DeserializeObject<CarDto>(responseJson);
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Buy(BuyCarDto input)
    {
        using var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        using var httpClient = new HttpClient(handler);
        var token = HttpContext.Session.GetString("token");
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        var response =
            await httpClient.PutAsJsonAsync(ApiUrl + "/cars/buy/" + input.Id,
                input.Price);
        
        var responseJson = await response.Content.ReadAsStringAsync();
        var carResponse = JsonConvert.DeserializeObject<CarDto>(responseJson);
        return RedirectToAction("Index", "Home");
    }
}