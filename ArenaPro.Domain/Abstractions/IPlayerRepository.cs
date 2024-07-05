using ArenaPro.Domain.Entities;
using System.Linq.Expressions;

namespace ArenaPro.Domain.Abstractions;
public interface IPlayerRepository : IRepository<Player>
{
    Task<List<Player>> GetByNickAsync(string nick, params Expression<Func<Player, object>>[] includes);
    Task<List<Player>> GetByTeamIdAsync(int teamId, params Expression<Func<Player, object>>[] includes);
}
