using System.Linq.Expressions;

namespace Repository.Contracts;

public interface IRepositoryBase<T> where T : class
{
    IQueryable<T> FindAll();
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    Task<T> Create(T entity);
    T Update(T entity);
    Task Delete(Expression<Func<T, bool>> expression);
}