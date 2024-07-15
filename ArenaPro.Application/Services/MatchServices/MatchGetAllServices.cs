using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Services.MatchServices;
public class MatchGetAllServices : IMatchGetAllServices
{
    private readonly IMatchRepository _matchRepository;

    public MatchGetAllServices(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task<List<Match>> ExecuteAsync()
    {
        var matches = await _matchRepository.GetAllAsync();
        return matches;
    }
}
