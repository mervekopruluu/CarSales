using Public.Dtos.UserManagement;

namespace Service.Contracts;

public interface IAuthService
{
    Task<ResponseModel> Register(RegisterModel model);
    Task<ResponseModel> Login(LoginModel model);
}