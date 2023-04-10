using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ApplicationUserTokenRepository : RepositoryBase<ApplicationUserToken>, IApplicationUserTokenRepository
{
    public ApplicationUserTokenRepository(ApiDbContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<ApplicationUserToken?> Get(string userId) =>
        await FindByCondition(aut => aut.UserId == userId).FirstOrDefaultAsync();

    public async Task<ApplicationUserToken> Add(ApplicationUserToken userToken) => await Create(userToken);

    public async void Delete(string userId) => await Delete(x => x.UserId == userId);
}