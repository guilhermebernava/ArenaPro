using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using System.Linq.Expressions;

public interface IMatchRepository : IRepository<Match>
{
    Task<List<Match>> GetByDateAsync(DateTime date ,bool? ended = null, params Expression<Func<Match, object>>[] includes);
    Task<List<Match>> GetByTournamentIdAsync(int tournamentId, bool? ended = null, params Expression<Func<Match, object>>[] includes);
}