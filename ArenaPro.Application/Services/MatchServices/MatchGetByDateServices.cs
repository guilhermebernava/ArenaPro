using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Services.MatchServices;
public class MatchGetByDateServices : IMatchGetByDateServices
{
    private readonly IMatchRepository _matchRepository;

    public MatchGetByDateServices(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task<List<Match>> ExecuteAsync(DateTime date)
    {
        var matches = await _matchRepository.GetByDateAsync(date);
        return matches;
    }
}
