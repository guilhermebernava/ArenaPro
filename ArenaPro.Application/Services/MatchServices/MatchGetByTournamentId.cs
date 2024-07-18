using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Application.Models.MatchModels;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Services.MatchServices;
public class MatchGetByTournamentIdServices : IMatchGetByTournamentIdServices
{
    private readonly IMatchRepository _matchRepository;

    public MatchGetByTournamentIdServices(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task<List<Match>> ExecuteAsync(MatchGetModel<int> parameter)
    {
        var matches = await _matchRepository.GetByTournamentIdAsync(parameter.Data, parameter.Ended);
        return matches;
    }
}
