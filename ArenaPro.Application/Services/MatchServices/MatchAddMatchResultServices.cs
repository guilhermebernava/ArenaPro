using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models.MatchValidations;

namespace ArenaPro.Application.Services.MatchServices;
public class MatchAddMatchResultServices : IMatchAddMatchResultServices
{
    private readonly IMatchRepository _matchRepository;
    private readonly ITeamRepository _teamRepository;

    public MatchAddMatchResultServices(IMatchRepository matchRepository, ITeamRepository teamRepository)
    {
        _matchRepository = matchRepository;
        _teamRepository = teamRepository;
    }

    public async Task<bool> ExecuteAsync(TeamMatchModel parameter)
    {
        var team = await _teamRepository.GetByIdAsync(parameter.TeamId);
        if (team == null) throw new RepositoryException($"Not found Team with this ID - {parameter.TeamId}", "Team");

        var match = await _matchRepository.GetByIdAsync(parameter.MatchId);
        if (match == null) throw new RepositoryException($"Not found Match with this ID - {parameter.MatchId}", "Match");
        match.AddMatchResult(team, parameter.Won);

        var saved = await _matchRepository.SaveAsync();
        if (!saved) throw new RepositoryException($"Could not save this TeamMatch","Match");

        return saved;
    }
}
