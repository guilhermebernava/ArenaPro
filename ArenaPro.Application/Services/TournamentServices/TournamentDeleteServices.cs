using ArenaPro.Application.Abstractions.TournamentServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Utils;

namespace ArenaPro.Application.Services.TournamentServices;
public class TournamentDeleteServices : ITournamentDeleteServices
{
    private readonly ITournamentRepository _tournamentRepository;

    public TournamentDeleteServices(ITournamentRepository tournamentRepository)
    {
        _tournamentRepository = tournamentRepository;
    }


    public async Task<bool> ExecuteAsync(int parameter)
    {
        if (parameter < 1) throw new ValidationException("Id must be greater than 0");

        var tournament = await _tournamentRepository.GetByIdAsync(parameter);
        if (tournament == null) throw new RepositoryException(ExceptionUtils.GetError("Tournament", parameter), "Tournament");

        var deleted = await _tournamentRepository.DeleteAsync(tournament);
        if (!deleted) throw new RepositoryException(ExceptionUtils.DeleteError("Tournament", parameter), "Tournament");
        return deleted;
    }
}

