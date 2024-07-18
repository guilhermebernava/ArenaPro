using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models.MatchValidations;
using ArenaPro.Application.Utils;

namespace ArenaPro.Application.Services.MatchServices;
public class MatchRemovePlayerKdaServices : IMatchRemovePlayerKdaServices
{
    private readonly IMatchRepository _matchRepository;

    public MatchRemovePlayerKdaServices(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task<bool> ExecuteAsync(List<MatchPlayerKdaModel> parameter)
    {
        foreach (var playerKda in parameter)
        {
            var match = await _matchRepository.GetByIdAsync(playerKda.MatchId);
            if (match == null) throw new RepositoryException(ExceptionUtils.GetError("Match", playerKda.MatchId), "Match");
            var existPlayerKda = match.MatchPlayerKdas.FirstOrDefault(_ => _.PlayerId == playerKda.PlayerId);
            if (existPlayerKda == null) throw new ValidationException(ExceptionUtils.GetError("PlayerKda", playerKda.PlayerId));
            match.RemoveMatchPlayerKda(existPlayerKda);
        }

        var saved = await _matchRepository.SaveAsync();
        if (!saved) throw new RepositoryException(ExceptionUtils.DeleteError("PlayerKdas"), "Match");
        return saved;
    }
}
