using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly ApiDbContext _context;

    protected RepositoryBase(ApiDbContext repositoryContext)
    {
        _context = repositoryContext;
    }

    public async Task<T> Create(T entity) => (await _context.Set<T>().AddAsync(entity)).Entity;

    public async Task Delete(Expression<Func<T, bool>> expression)
    {
        var entity = await _context.Set<T>().Where(expression).FirstOrDefaultAsync();
        _context.Set<T>().Remove(entity ?? throw new InvalidOperationException(nameof(entity)));
    }

    public IQueryable<T> FindAll() => _context.Set<T>();
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => _context.Set<T>().Where(expression);
    public T Update(T entity) => _context.Set<T>().Update(entity).Entity;
}