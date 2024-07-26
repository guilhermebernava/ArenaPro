using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Application.Models.MatchValidations;
using ArenaPro.Application.Utils;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Services.MatchServices;
public class MatchCreateServices(IMatchRepository matchRepository, ITournamentRepository tournamentRepository, ITeamRepository teamRepository) : IMatchCreateServices
{
    private readonly IMatchRepository _matchRepository = matchRepository;
    private readonly ITournamentRepository _tournamentRepository = tournamentRepository;
    private readonly ITeamRepository _teamRepository = teamRepository;

    public async Task<bool> ExecuteAsync(MatchModel parameter)
    {
        var tournament = await _tournamentRepository.GetByIdAsync(parameter.TournamentId) ?? throw new Exceptions.RepositoryException(ExceptionUtils.GetError("Tournament", parameter.TournamentId), "Tournament");
        var teams = new List<Team>();
        if (parameter.Teams != null)
        {
            foreach (var teamModel in parameter.Teams)
            {
                var team = await _teamRepository.GetByIdAsync(teamModel.Id ?? -1);
                if (team == null) continue;
                teams.Add(team);
            }
        }

        var created = await _matchRepository.CreateAsync(new Match(parameter.MatchDate, tournament, teams));
        if (!created) throw new Exceptions.RepositoryException(ExceptionUtils.CreateError("Match"), "Match");
        return created;
    }
}
