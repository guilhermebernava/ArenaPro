using ArenaPro.Application.Abstractions.TeamServices;
using ArenaPro.Domain.Entities;
using System.Linq.Expressions;

namespace ArenaPro.Application.Services.TeamServices;
public class TeamGetAllServices : ITeamGetAllServices
{
    private readonly ITeamRepository _teamRepository;

    public TeamGetAllServices(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<List<Team>> ExecuteAsync(params Expression<Func<Team, object>>[] includes)
    {
        var teams = await _teamRepository.GetAllAsync(includes);
        return teams;
    }
}
