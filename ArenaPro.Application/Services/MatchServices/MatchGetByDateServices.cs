using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Application.Models.MatchModels;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Services.MatchServices;
public class MatchGetByDateServices : IMatchGetByDateServices
{
    private readonly IMatchRepository _matchRepository;

    public MatchGetByDateServices(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task<List<Match>> ExecuteAsync(MatchGetModel<DateTime> paramenter)
    {
        var matches = await _matchRepository.GetByDateAsync(paramenter.Data, paramenter.Ended);
        return matches;
    }
}
