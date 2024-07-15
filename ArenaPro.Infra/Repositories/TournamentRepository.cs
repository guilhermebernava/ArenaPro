using ArenaPro.Domain.Entities;
using ArenaPro.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ArenaPro.Infra.Repositories;
public class TournamentRepository : Repository<Tournament>, ITournamentRepository
{
    public TournamentRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Tournament>> GetByNameAsync(string name, params Expression<Func<Tournament, object>>[] includes)
    {
        var query = AddIncludes(includes);
        var entities = await query.Where(_ => _.Name.Contains(name) && _.DeletedAt == null).ToListAsync();
        return entities;
    }
}
