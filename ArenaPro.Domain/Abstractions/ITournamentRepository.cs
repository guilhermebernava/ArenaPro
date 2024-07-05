using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using System.Linq.Expressions;

public interface ITournamentRepository : IRepository<Tournament>
{
    Task<List<Tournament>> GetByNameAsync(string name, params Expression<Func<Tournament, object>>[] includes);
}