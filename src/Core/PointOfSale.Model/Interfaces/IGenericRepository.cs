using System.Linq.Expressions;

namespace PointOfSale.Model.Repositories;

public interface IGenericRepository<T, TId> where T : class
{
    Task<T?> GetByIdAsync(TId id, params Expression<Func<T, object>>[] includes);
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(TId id);
    
}