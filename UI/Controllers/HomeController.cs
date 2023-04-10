using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Public.Dtos.Car;

namespace UI.Controllers;

public class HomeController : BaseController
{
    public async Task<IActionResult> Index()
    {
        using var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        using var httpClient = new HttpClient(handler);
        var url = ApiUrl + "/cars/";
        var token = HttpContext.Session.GetString("token");
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        var response = await httpClient.GetFromJsonAsync<List<CarDto>>(url);
        return View(response);
    }

    public async Task<IActionResult> SoldOuts()
    {
        using var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        using var httpClient = new HttpClient(handler);
        var url = ApiUrl + "/cars/sold-outs";
        var token = HttpContext.Session.GetString("token");
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        var response = await httpClient.GetFromJsonAsync<List<CarDto>>(url);
        return View(response);
    }

    public async Task<IActionResult> MyCars()
    {
        using var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        using var httpClient = new HttpClient(handler);
        var url = ApiUrl + "/cars/my-cars";
        var token = HttpContext.Session.GetString("token");
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        var response = await httpClient.GetFromJsonAsync<List<CarDto>>(url);
        return View(response);
    }

    public HomeController(IConfiguration configuration) : base(configuration)
    {
    }
}