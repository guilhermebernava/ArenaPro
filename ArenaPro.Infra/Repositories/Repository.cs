using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using ArenaPro.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ArenaPro.Infra.Repositories;
public class Repository<T> : IRepository<T> where T : Entity
{
    public readonly AppDbContext _context;
    public DbSet<T> dbSet { get; set; }
    public Repository(AppDbContext dbContext)
    {
        _context = dbContext;
        dbSet = _context.Set<T>();
    }

    public async virtual Task<bool> CreateAsync(T T)
    {
        dbSet.Add(T);
        return await SaveAsync();
    }

    public async Task<bool> DeleteAsync(T T)
    {
        T.Delete();
        dbSet.Update(T);
        return await SaveAsync();
    }

    public Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        var query = AddIncludes(includes);
        return query.Where(_ =>  _.DeletedAt == null).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        var query = AddIncludes(includes);
        return await query.Where(_ => _.Id == id && _.DeletedAt == null).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateAsync(T T)
    {
        dbSet.Update(T);
        return await SaveAsync();
    }

    public async Task<bool> SaveAsync()
    {
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }


    public IQueryable<T> AddIncludes(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = dbSet;
        foreach (var includeProperty in includes)
        {
            query = query.Include(includeProperty);
        }
        return query;
    }
}
