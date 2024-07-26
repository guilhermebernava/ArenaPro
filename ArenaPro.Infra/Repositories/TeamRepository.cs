using ArenaPro.Domain.Entities;
using ArenaPro.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace ArenaPro.Infra.Repositories;
public class TeamRepository : Repository<Team>, ITeamRepository
{
    public TeamRepository(AppDbContext dbContext, ILogger<Repository<Team>> logger) : base(dbContext, logger)
    {
    }

    public async Task<List<Team>> GetByNameAsync(string name, params Expression<Func<Team, object>>[] includes)
    {
        var query = AddIncludes(includes);
        var entities = await query.Where(_ => _.Name.Contains(name) && _.DeletedAt == null).ToListAsync();
        return entities;
    }
}
