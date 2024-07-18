using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models.MatchValidations;
using ArenaPro.Application.Utils;
using ArenaPro.Domain.Abstractions;

namespace ArenaPro.Application.Services.MatchServices;
public class MatchAddPlayerKdaServices : IMatchAddPlayerKdaServices
{
    private readonly IMatchRepository _matchRepository;
    private readonly IPlayerRepository _playerRepository;

    public MatchAddPlayerKdaServices(IMatchRepository matchRepository, IPlayerRepository playerRepository)
    {
        _matchRepository = matchRepository;
        _playerRepository = playerRepository;
    }

    public async Task<bool> ExecuteAsync(List<MatchPlayerKdaModel> parameter)
    {
        foreach (var matchPlayerKdaModel in parameter)
        {
            if (matchPlayerKdaModel.Kills < 0 || matchPlayerKdaModel.Deaths < 0 || matchPlayerKdaModel.Assists < 0) throw new ValidationException("Kills, Deaths or Assits must be greater than 0");

            var player = await _playerRepository.GetByIdAsync(matchPlayerKdaModel.PlayerId);
            if (player == null) throw new RepositoryException(ExceptionUtils.GetError("Player", matchPlayerKdaModel.PlayerId), "Player");

            var match = await _matchRepository.GetByIdAsync(matchPlayerKdaModel.MatchId);
            if (match == null) throw new RepositoryException(ExceptionUtils.GetError("Match", matchPlayerKdaModel.MatchId), "Match");

            match.AddMatchPlayerKda(player, matchPlayerKdaModel.Kills, matchPlayerKdaModel.Deaths, matchPlayerKdaModel.Assists);
        }

        var saved = await _matchRepository.SaveAsync();
        if (!saved) throw new RepositoryException(ExceptionUtils.CreateError("PlayerKda"), "Match");
        return saved;
    }
}

