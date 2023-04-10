using Microsoft.AspNetCore.Mvc;
using Public.Dtos.UserManagement;
using Service.Contracts;

namespace API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public UserController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                var responseModel = await _serviceManager.AuthService.Register(model);
                return Ok(responseModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var responseModel = await _serviceManager.AuthService.Login(model);
                return Ok(responseModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}