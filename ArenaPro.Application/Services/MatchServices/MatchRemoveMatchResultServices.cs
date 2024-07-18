using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models;
using ArenaPro.Application.Utils;

namespace ArenaPro.Application.Services.MatchServices;
public class MatchRemoveMatchResultServices : IMatchRemoveMatchResultServices
{
    private readonly IMatchRepository _matchRepository;

    public MatchRemoveMatchResultServices(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task<bool> ExecuteAsync(List<MatchResultModel> parameter)
    {
        foreach (var matchResultModel in parameter)
        {
            var match = await _matchRepository.GetByIdAsync(matchResultModel.MatchId);
            if (match == null) throw new RepositoryException(ExceptionUtils.GetError("Match", matchResultModel.MatchId), "Match");
            var existMatchResult = match.MatchesResults.FirstOrDefault(_ => _.TeamId == matchResultModel.TeamId);
            if (existMatchResult == null) throw new ValidationException(ExceptionUtils.GetError("MatchResult", matchResultModel.TeamId));
            match.RemoveMatchResult(existMatchResult!);
        }

        var saved = await _matchRepository.SaveAsync();
        if (!saved) throw new RepositoryException(ExceptionUtils.DeleteError("MatchResults"), "Match");
        return saved;
    }
}
