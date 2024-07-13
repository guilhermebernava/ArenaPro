using ArenaPro.Domain.Entities;
using ArenaPro.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ArenaPro.Infra.Repositories;
public class MatchRepository : Repository<Match>, IMatchRepository
{
    public MatchRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Match>> GetByDateAsync(DateTime date, bool? ended = null, params Expression<Func<Match, object>>[] includes)
    {
        var query = AddIncludes(includes);
        var entities = await query.Where(_ => _.MatchDate.Date == date.Date && (!ended.HasValue || _.Ended == ended.Value)).ToListAsync();
        return entities;
    }

    public async Task<List<Match>> GetByTournamentIdAsync(int tournamentId, bool? ended = null, params Expression<Func<Match, object>>[] includes)
    {
        var query = AddIncludes(includes);
        var entities = await query.Where(_ => _.TournamentId == tournamentId && (!ended.HasValue || _.Ended == ended.Value)).ToListAsync();
        return entities;
    }
}
