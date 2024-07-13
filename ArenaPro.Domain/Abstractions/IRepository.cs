using ArenaPro.Domain.Entities;
using System.Linq.Expressions;

namespace ArenaPro.Domain.Abstractions;
public interface IRepository<T> where T : Entity
{
    Task<bool> CreateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
    Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<bool> SaveAsync();
    IQueryable<T> AddIncludes(params Expression<Func<T, object>>[] includes);
}
