using Entities.Models;

namespace Repository;

public interface IApplicationUserTokenRepository 
{
    Task<ApplicationUserToken?> Get(string userId);
    Task<ApplicationUserToken> Add(ApplicationUserToken userToken);
    ApplicationUserToken Update(ApplicationUserToken userToken);
    void Delete(string userId);
}