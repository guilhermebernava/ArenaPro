using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Services.MatchServices;
public class MatchGetByTournamentIdServices : IMatchGetByTournamentIdServices
{
    private readonly IMatchRepository _matchRepository;

    public MatchGetByTournamentIdServices(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task<List<Match>> ExecuteAsync(int TournamentId)
    {
        var matches = await _matchRepository.GetByTournamentIdAsync(TournamentId);
        return matches;
    }
}
