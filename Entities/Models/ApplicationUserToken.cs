using Microsoft.AspNetCore.Identity;

namespace Entities.Models;

public class ApplicationUserToken : IdentityUserToken<string>
{
    public DateTime ExpireDate { get; set; }
}