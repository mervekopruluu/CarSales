using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repository.Contracts;
using Service.Contracts;

namespace Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthService> _authService;
    private readonly Lazy<ICarService> _carService;

    public ServiceManager(IConfiguration config,
        SignInManager<User> signInManager,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IRepositoryManager repositoryManager,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _authService = new Lazy<IAuthService>(() =>
            new AuthService(config, signInManager, userManager, roleManager, repositoryManager));

        _carService = new Lazy<ICarService>(() => new CarService(repositoryManager, mapper, httpContextAccessor));
    }

    public IAuthService AuthService => _authService.Value;
    public ICarService CarService => _carService.Value;
}