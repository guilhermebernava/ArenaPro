using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Domain.Entities;
using System.Linq.Expressions;

namespace ArenaPro.Application.Services.MatchServices;
public class MatchGetAllServices : IMatchGetAllServices
{
    private readonly IMatchRepository _matchRepository;

    public MatchGetAllServices(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task<List<Match>> ExecuteAsync(params Expression<Func<Match, object>>[] includes)
    {
        var matches = await _matchRepository.GetAllAsync(includes);
        return matches;
    }
}
