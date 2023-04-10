namespace Repository.Contracts;

public interface IRepositoryManager
{
    IUserRepository User { get; }
    IApplicationUserTokenRepository ApplicationUserTokens { get; }
    ICarRepository Car { get; }
    void Save();
    Task SaveAsync();
}