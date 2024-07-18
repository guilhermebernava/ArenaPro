using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Utils;

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
        if (match == null) throw new RepositoryException(ExceptionUtils.GetError("Match", parameter), "Match");

        var deleted = await _matchRepository.DeleteAsync(match);
        if (!deleted) throw new RepositoryException(ExceptionUtils.DeleteError("Match", parameter), "Match");
        return deleted;
    }
}
