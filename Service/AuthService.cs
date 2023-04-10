using Entities.Models;
using Library;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Public.Dtos.UserManagement;
using Repository.Contracts;
using Service.Contracts;

namespace Service;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly IRepositoryManager _repositoryManager;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthService(IConfiguration config,
        SignInManager<User> signInManager,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IRepositoryManager repositoryManager)
    {
        _config = config;
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _repositoryManager = repositoryManager;
    }

    public async Task<ResponseModel> Register(RegisterModel model)
    {
        var existsUser = await _userManager.FindByNameAsync(model.Username);

        if (existsUser != null)
        {
            return new ResponseModel
            {
                Message = "Kullanıcı zaten var.",
                IsSuccess = false
            };
        }

        var user = new User()
        {
            UserName = model.Username,
            FirstName = model.Name,
            LastName = model.Surname,
        };

        var result = await _userManager.CreateAsync(user, model.Password.Trim());

        if (!result.Succeeded)
            return new ResponseModel
            {
                IsSuccess = false,
                Message = $"Kullanıcı oluşturulurken bir hata oluştu: {result.Errors.FirstOrDefault()?.Description}"
            };

        var roleExists = await _roleManager.RoleExistsAsync(_config["Roles:User"]);

        if (!roleExists)
        {
            var role = new IdentityRole(_config["Roles:User"])
            {
                NormalizedName = _config["Roles:User"]
            };

            _roleManager.CreateAsync(role).Wait();
        }

        _userManager.AddToRoleAsync(user, _config["Roles:User"]).Wait();

        return new ResponseModel()
        {
            Message = "Kullanıcı başarılı şekilde oluşturuldu.",
            IsSuccess = true
        };
    }

    public async Task<ResponseModel> Login(LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        
        if (user == null)
        {
            return new ResponseModel
            {
                Message = "Kullanıcı adı bulunamadı",
                IsSuccess = false
            };
        }
        
        var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

        if (signInResult.Succeeded == false)
        {
            return new ResponseModel
            {
                Message = "Kullanıcı adı veya şifre hatalı.",
                IsSuccess = false
            };
        }

        var accessTokenGenerator = new AccessTokenGenerator(_repositoryManager, _config, user);
        var userToken = await accessTokenGenerator.GetToken();

        return new ResponseModel()
        {
            Message = "Kullanıcı başarılı şekilde giriş yaptı.",
            IsSuccess = true,
            TokenInfo = new TokenInfoDto
            {
                Token = userToken.Value,
                ExpireDate = userToken.ExpireDate
            }
        };
    }
}