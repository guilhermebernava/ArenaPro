using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using System.Linq.Expressions;

public interface ITeamRepository : IRepository<Team>
{
    Task<List<Team>> GetByNameAsync(string name, params Expression<Func<Team, object>>[] includes);
}