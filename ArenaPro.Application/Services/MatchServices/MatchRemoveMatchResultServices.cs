using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models;

namespace ArenaPro.Application.Services.MatchServices;
public class MatchRemoveMatchResultServices : IMatchRemoveMatchResultServices
{
    private readonly IMatchRepository _matchRepository;

    public MatchRemoveMatchResultServices(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task<bool> ExecuteAsync(List<TeamMatchModel> parameter)
    {
        foreach (var teamMatchModel in parameter)
        {
            var match = await _matchRepository.GetByIdAsync(teamMatchModel.MatchId);
            if (match == null) throw new RepositoryException($"Not found Match with this ID - {teamMatchModel.MatchId}", "Match");
            var existMatchResult = match.MatchesResults.FirstOrDefault(_ => _.TeamId == teamMatchModel.TeamId);
            if (existMatchResult == null) throw new ValidationException($"Not found MatchResult of this TEAM - {teamMatchModel.TeamId}");
            match.RemoveMatchResult(existMatchResult!);
        }

        var saved = await _matchRepository.SaveAsync();
        if (!saved) throw new RepositoryException($"Could not save", "Match");
        return saved;
    }
}
