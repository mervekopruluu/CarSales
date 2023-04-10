using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers;

public class BaseController : Controller
{
    public string ApiUrl => Configuration.GetValue<string>("ApiUrl");
    private IConfiguration Configuration { get; }

    public BaseController(IConfiguration configuration)
    {
        Configuration = configuration;
        
    }
}