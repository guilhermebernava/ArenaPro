using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models;
using ArenaPro.Application.Utils;

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

    public async Task<bool> ExecuteAsync(List<MatchResultModel> parameter)
    {
        foreach (var matchResultModel in parameter)
        {
            var team = await _teamRepository.GetByIdAsync(matchResultModel.TeamId);
            if (team == null) throw new RepositoryException(ExceptionUtils.GetError("Team", matchResultModel.TeamId), "Team");

            var match = await _matchRepository.GetByIdAsync(matchResultModel.MatchId);
            if (match == null) throw new RepositoryException(ExceptionUtils.GetError("Match", matchResultModel.MatchId), "Match");
            match.AddMatchResult(team, matchResultModel.Won);
        }

        var saved = await _matchRepository.SaveAsync();
        if (!saved) throw new RepositoryException(ExceptionUtils.CreateError("Match"), "Match");
        return saved;
    }
}
