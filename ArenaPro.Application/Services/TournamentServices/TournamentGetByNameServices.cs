using ArenaPro.Application.Abstractions.TournamentServices;
using ArenaPro.Domain.Entities;
using System.Linq.Expressions;

namespace ArenaPro.Application.Services.TournamentServices;
public class TournamentGetByNameServices : ITournamentGetByNameServices
{
    private readonly ITournamentRepository _tournamentRepository;

    public TournamentGetByNameServices(ITournamentRepository tournamentRepository)
    {
        _tournamentRepository = tournamentRepository;
    }

    public async Task<List<Tournament>> ExecuteAsync(string name, params Expression<Func<Tournament, object>>[] includes)
    {
        var tournaments = await _tournamentRepository.GetByNameAsync(name, includes);
        return tournaments;
    }
}
