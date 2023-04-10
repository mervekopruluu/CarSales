using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Public.Dtos.UserManagement;
using UI.Models;

namespace UI.Controllers;

public class LoginController : BaseController
{
    private readonly ILogger<LoginController> _logger;

    public LoginController(IConfiguration configuration, ILogger<LoginController> logger)
        : base(configuration)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<ActionResult> Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            using var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            using var HttpClient = new HttpClient(handler);
            var response = await HttpClient.PostAsJsonAsync(ApiUrl + "/users/Login", model);

            var responseJson = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonConvert.DeserializeObject<ResponseModel>(responseJson);

            if (!loginResponse.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, loginResponse.Message);
                return View("Index", model);
            }

            var token = loginResponse.TokenInfo.Token;
            HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            HttpContext.Session.SetString("token", token);

            return RedirectToAction("Index", "Home");
        }

        return View("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}