using ArenaPro.Application.Abstractions.TeamServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Utils;
using ArenaPro.Domain.Entities;
using System.Linq.Expressions;

namespace ArenaPro.Application.Services.TeamServices;
public class TeamGetByIdServices : ITeamGetByIdServices
{
    private readonly ITeamRepository _teamRepository;

    public TeamGetByIdServices(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Team> ExecuteAsync(int id, params Expression<Func<Team, object>>[] includes)
    {
        var team = await _teamRepository.GetByIdAsync(id, includes);
        if (team == null) throw new RepositoryException(ExceptionUtils.GetError("Team", id), "Team");
        return team;
    }
}
