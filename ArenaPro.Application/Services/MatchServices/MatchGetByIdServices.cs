using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Services.MatchServices;
public class MatchGetByIdServices : IMatchGetByIdServices
{
    private readonly IMatchRepository _matchRepository;

    public MatchGetByIdServices(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task<Match> ExecuteAsync(int id)
    {
        var match = await _matchRepository.GetByIdAsync(id);
        if (match == null)throw new RepositoryException($"Not found any MATCH with this ID - {id}", "Match");
        return match;
    }
}
