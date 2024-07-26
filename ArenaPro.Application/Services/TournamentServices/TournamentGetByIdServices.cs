using ArenaPro.Application.Abstractions.TournamentServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Utils;
using ArenaPro.Domain.Entities;
using System.Linq.Expressions;

namespace ArenaPro.Application.Services.TournamentServices;
public class TournamentGetByIdServices : ITournamentGetByIdServices
{
    private readonly ITournamentRepository _tournamentRepository;

    public TournamentGetByIdServices(ITournamentRepository tournamentRepository)
    {
        _tournamentRepository = tournamentRepository;
    }

    public async Task<Tournament> ExecuteAsync(int id, params Expression<Func<Tournament, object>>[] includes)
    {
        var tournament = await _tournamentRepository.GetByIdAsync(id, includes);
        if (tournament == null) throw new RepositoryException(ExceptionUtils.GetError("Tournament", id), "Tournament");
        return tournament;
    }
}
