using ArenaPro.Application.Abstractions.TournamentServices;
using ArenaPro.Domain.Entities;
using System.Linq.Expressions;

namespace ArenaPro.Application.Services.TournamentServices;
public class TournamentGetAllServices : ITournamentGetAllServices
{
    private readonly ITournamentRepository _tournamentRepository;

    public TournamentGetAllServices(ITournamentRepository tournamentRepository)
    {
        _tournamentRepository = tournamentRepository;
    }

    public async Task<List<Tournament>> ExecuteAsync(params Expression<Func<Tournament, object>>[] includes)
    {
        var tournaments = await _tournamentRepository.GetAllAsync(includes);
        return tournaments;
    }
}
