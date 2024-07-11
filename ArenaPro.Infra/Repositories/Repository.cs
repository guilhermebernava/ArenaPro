using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using ArenaPro.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ArenaPro.Infra.Repositories;
public class Repository<T> : IRepository<T> where T : Entity
{
    private AppDbContext _dbContext;
    public DbSet<T> dbSet { get; set; }
    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        dbSet = _dbContext.Set<T>();
    }

    public async Task<bool> CreateAsync(T T)
    {
        dbSet.Add(T);
        return await SaveAsync();
    }

    public async Task<bool> DeleteAsync(T T)
    {
        dbSet.Remove(T);
        return await SaveAsync();
    }

    public Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbSet;
        foreach (var includeProperty in includes)
        {
            query = query.Include(includeProperty);
        }
        return query.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbSet;
        foreach (var includeProperty in includes)
        {
            query = query.Include(includeProperty);
        }
        
       return await query.Where(_ => _.Id == id).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateAsync(T T)
    {
        dbSet.Update(T);
        return await SaveAsync();
    }

    public async Task<bool> SaveAsync() => await _dbContext.SaveChangesAsync() == 1;

}
