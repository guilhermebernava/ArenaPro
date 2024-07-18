using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Utils;
using ArenaPro.Domain.Entities;
using System.Linq.Expressions;

namespace ArenaPro.Application.Services.MatchServices;
public class MatchGetByIdServices : IMatchGetByIdServices
{
    private readonly IMatchRepository _matchRepository;

    public MatchGetByIdServices(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task<Match> ExecuteAsync(int id, params Expression<Func<Match, object>>[] includes)
    {
        var match = await _matchRepository.GetByIdAsync(id,includes);
        if (match == null) throw new RepositoryException(ExceptionUtils.GetError("Match", id), "Match");
        return match;
    }
}
