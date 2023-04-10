using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Public.Dtos.UserManagement;

public class RegisterModel
{
    [Required]
    [DisplayName("Ad")]
    [StringLength(60)]
    public string Name { get; set; }

    [Required]
    [DisplayName("Soyad")]
    [StringLength(60)]
    public string Surname { get; set; }

    [Required(ErrorMessage = "Kullanıcı Adı zorunlu")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Şifre zorunlu")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Girmiş olduğunuz parola birbiri ile eşleşmiyor.")]
    public string ConfirmPassword { get; set; }
}