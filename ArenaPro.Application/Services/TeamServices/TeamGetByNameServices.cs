using ArenaPro.Application.Abstractions.TeamServices;
using ArenaPro.Domain.Entities;
using System.Linq.Expressions;

namespace ArenaPro.Application.Services.TeamServices;
public class TeamGetByNameServices : ITeamGetByNameServices
{
    private readonly ITeamRepository _teamRepository;

    public TeamGetByNameServices(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<List<Team>> ExecuteAsync(string name, params Expression<Func<Team, object>>[] includes)
    {
        var teams = await _teamRepository.GetByNameAsync(name, includes);
        return teams;
    }
}
