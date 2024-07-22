using ArenaPro.Application.Abstractions.TeamServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Utils;

namespace ArenaPro.Application.Services.TeamServices;
public class TeamDeleteServices : ITeamDeleteServices
{
    private readonly ITeamRepository _teamRepository;

    public TeamDeleteServices(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }


    public async Task<bool> ExecuteAsync(int parameter)
    {
        if (parameter < 1) throw new ValidationException("Id must be greater than 0");

        var team = await _teamRepository.GetByIdAsync(parameter);
        if (team == null) throw new RepositoryException(ExceptionUtils.GetError("Team", parameter), "Team");

        var deleted = await _teamRepository.DeleteAsync(team);
        if (!deleted) throw new RepositoryException(ExceptionUtils.DeleteError("Team", parameter), "Team");
        return deleted;
    }
}

