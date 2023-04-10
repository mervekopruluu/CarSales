using System.ComponentModel.DataAnnotations;

namespace Public.Dtos.UserManagement;

public class LoginModel
{
    [Required(ErrorMessage = "Kullanıcı Adı zorunlu")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Şifre zorunlu")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}