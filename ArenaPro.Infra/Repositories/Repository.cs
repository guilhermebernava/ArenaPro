using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using ArenaPro.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace ArenaPro.Infra.Repositories;
public class Repository<T> : IRepository<T> where T : Entity
{
    public readonly AppDbContext _context;
    private readonly ILogger<Repository<T>> _logger;
    public DbSet<T> dbSet { get; set; }

    public Repository(AppDbContext dbContext, ILogger<Repository<T>> logger)
    {
        _context = dbContext;
        dbSet = _context.Set<T>();
        _logger = logger;
    }

    public async virtual Task<bool> CreateAsync(T T)
    {
        dbSet.Add(T);
        var logMessage = _getPropertiesLogMessage(T);
        _logger.LogInformation($"Created entity with properties: {logMessage}");
        return await SaveAsync();
    }

    public async Task<bool> DeleteAsync(T T)
    {
        T.Delete();
        dbSet.Update(T);
        var logMessage = _getPropertiesLogMessage(T);
        _logger.LogInformation($"Deleted entity with properties: {logMessage}");
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
        var logMessage = _getPropertiesLogMessage(T);
        _logger.LogInformation($"Updated entity with properties: {logMessage}");
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

    private string _getPropertiesLogMessage(T entity)
    {
        var properties = typeof(T).GetProperties();
        var propertyValues = properties.Select(p => $"{p.Name}: {p.GetValue(entity)}");
        return string.Join(", ", propertyValues);
    }
}
