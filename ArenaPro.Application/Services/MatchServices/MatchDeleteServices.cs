using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Application.Exceptions;

namespace ArenaPro.Application.Services.MatchServices;
public class MatchDeleteServices : IMatchDeleteServices
{
    private readonly IMatchRepository _matchRepository;

    public MatchDeleteServices(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task<bool> ExecuteAsync(int parameter)
    {
        if (parameter < 1) throw new ValidationException("Id must be greater than 0");
        
        var match = await _matchRepository.GetByIdAsync(parameter);
        if (match == null) throw new RepositoryException("Not found any MATCH with this Id", "Match");
        
        var deleted = await _matchRepository.DeleteAsync(match);
        if (!deleted) throw new RepositoryException("Could not DELETE this MATCH", "Match");
        return deleted;
    }
}
