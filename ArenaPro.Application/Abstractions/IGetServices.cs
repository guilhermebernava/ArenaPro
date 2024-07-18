using ArenaPro.Domain.Entities;
using System.Linq.Expressions;

namespace ArenaPro.Application.Abstractions;
public interface IGetServices<P, R, TEntity> where TEntity : Entity
{
    Task<R> ExecuteAsync(P parameter, params Expression<Func<TEntity, object>>[] includes);
}

public interface IGetServices<R, TEntity>
{
    Task<R> ExecuteAsync(params Expression<Func<TEntity, object>>[] includes);
}