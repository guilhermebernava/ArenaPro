using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models.MatchValidations;

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
        foreach (var matchPlayerKdaModel in parameter)
        {
            var match = await _matchRepository.GetByIdAsync(matchPlayerKdaModel.MatchId);
            if (match == null) throw new RepositoryException($"Not found Match with this ID - {matchPlayerKdaModel.MatchId}", "Match");
            var existPlayerKda = match.MatchPlayerKdas.FirstOrDefault(_ => _.PlayerId == matchPlayerKdaModel.PlayerId);
            if (existPlayerKda == null) throw new ValidationException($"Not found PlayerKda of this Player - {matchPlayerKdaModel.PlayerId}");
            match.RemoveMatchPlayerKda(existPlayerKda);
        }

        var saved = await _matchRepository.SaveAsync();
        if (!saved) throw new RepositoryException($"Could not save", "Match");
        return saved;
    }
}
