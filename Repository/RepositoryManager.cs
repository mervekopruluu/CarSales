using Repository.Contracts;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly ApiDbContext _context;
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<IApplicationUserTokenRepository> _applicationUserTokenRepository;
    private readonly Lazy<ICarRepository> _carRepository;

    public RepositoryManager(ApiDbContext context)
    {
        _context = context;
        _applicationUserTokenRepository =
            new Lazy<IApplicationUserTokenRepository>(() => new ApplicationUserTokenRepository(_context));

        _userRepository =
            new Lazy<IUserRepository>(() => new UserRepository(_context));

        _carRepository = new Lazy<ICarRepository>(() => new CarRepository(_context));
    }

    public IUserRepository User => _userRepository.Value;
    public IApplicationUserTokenRepository ApplicationUserTokens => _applicationUserTokenRepository.Value;
    public ICarRepository Car => _carRepository.Value;

    public void Save()
    {
        _context.SaveChanges();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}