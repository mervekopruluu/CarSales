using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ApiDbContext : IdentityDbContext<User>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApiDbContext(DbContextOptions<ApiDbContext> options, IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<ApplicationUserToken> ApplicationUserTokens { get; set; }

    public DbSet<Car> Cars { get; set; }
    public override DbSet<User> Users { get; set; }
 
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUserToken>().ToTable("ApplicationUserTokens");
        builder.Entity<Car>().ToTable("Cars");
        builder.Entity<Car>().Property(x => x.CustomerId).IsRequired(false);
    }

}