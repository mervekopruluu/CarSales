using Microsoft.AspNetCore.Mvc;
using Public.Dtos.UserManagement;

namespace UI.Controllers;

public class RegisterController: Controller
{ 
    private readonly ILogger<LoginController> _logger;

    public RegisterController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Register(RegisterModel model)
    {
        return View("Index");
    }
    
}