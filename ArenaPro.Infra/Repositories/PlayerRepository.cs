using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using ArenaPro.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ArenaPro.Infra.Repositories;
public class PlayerRepository : Repository<Player>, IPlayerRepository
{
    public PlayerRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Player>> GetByNickAsync(string nick, params Expression<Func<Player, object>>[] includes)
    {
        var query = AddIncludes(includes);
        var entities = await query.Where(_ => _.Nick.Contains(nick)).ToListAsync();
        return entities;
    }

    public async Task<List<Player>> GetByTeamIdAsync(int teamId, params Expression<Func<Player, object>>[] includes)
    {
        var query = AddIncludes(includes);
        var entities = await query.Where(_ => _.TeamId == teamId).ToListAsync();
        return entities;
    }
}
