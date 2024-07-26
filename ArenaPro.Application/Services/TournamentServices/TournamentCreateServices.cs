using ArenaPro.Application.Abstractions.TournamentServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models.TournamentModels;
using ArenaPro.Application.Utils;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Services.TournamentServices;
public class TournamentCreateServices : ITournamentCreateServices
{
    private readonly ITeamRepository _teamRepository;
    private readonly ITournamentRepository _tournamentRepository;
    private readonly IMatchRepository _matchRepository;

    public TournamentCreateServices(ITeamRepository teamRepository, ITournamentRepository tournamentRepository, IMatchRepository matchRepository)
    {
        _teamRepository = teamRepository;
        _tournamentRepository = tournamentRepository;
        _matchRepository = matchRepository;
    }


    public async Task<bool> ExecuteAsync(TournamentModel parameter)
    {
        var teams = new List<Team>();
        var matches = new List<Match>();

        if (parameter.Matches != null)
        {
            foreach (var match in parameter.Matches)
            {
                var existMatch = await _matchRepository.GetByIdAsync(match.Id ?? -1);
                if (existMatch == null) throw new RepositoryException(ExceptionUtils.GetError("Match", match.Id), "Match");
                matches.Add(existMatch);
            }
        }

        if (parameter.Teams != null)
        {
            foreach (var team in parameter.Teams)
            {
                var existTeam = await _teamRepository.GetByIdAsync(team.Id ?? -1);
                if (existTeam == null) throw new RepositoryException(ExceptionUtils.GetError("Team", team.Id), "Team");
                teams.Add(existTeam);
            }
        }

        if (parameter.Matches != null)
        {
            foreach (var match in parameter.Matches)
            {
                var existMatch = await _matchRepository.GetByIdAsync(match.Id ?? -1);
                if (existMatch == null) throw new RepositoryException(ExceptionUtils.GetError("Match", match.Id), "Match");
                matches.Add(existMatch);
            }
        }

        var tournament = new Tournament(parameter.Name, parameter.Prize, teams, matches);
        var saved = await _tournamentRepository.CreateAsync(tournament);
        if (!saved) throw new RepositoryException(ExceptionUtils.CreateError("Tournament"), "Tournament");
        return saved;
    }
}
