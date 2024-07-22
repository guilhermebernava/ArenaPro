using ArenaPro.Application.Abstractions.TeamServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models.TeamModels;
using ArenaPro.Application.Utils;
using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Services.TeamServices;
public class TeamCreateServices : ITeamCreateServices
{
    private readonly ITeamRepository _teamRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly ITournamentRepository _tournamentRepository;
    private readonly IMatchRepository _matchRepository;

    public TeamCreateServices(IPlayerRepository playerRepository, ITournamentRepository tournamentRepository, IMatchRepository matchRepository, ITeamRepository teamRepository)
    {
        _playerRepository = playerRepository;
        _tournamentRepository = tournamentRepository;
        _matchRepository = matchRepository;
        _teamRepository = teamRepository;
    }


    public async Task<bool> ExecuteAsync(TeamModel parameter)
    {
        var players = new List<Player>();
        var tournaments = new List<Tournament>();
        var matches = new List<Match>();

        if (parameter.Tournaments != null)
        {
            foreach (var tournament in parameter.Tournaments)
            {
                var existTournament = await _tournamentRepository.GetByIdAsync(tournament.Id ?? -1);
                if (existTournament == null) throw new RepositoryException(ExceptionUtils.GetError("Tournament",tournament.Id), "Tournament");
                tournaments.Add(existTournament);
            }
        }

        if (parameter.Players != null)
        {
            foreach (var player in parameter.Players)
            {
                var existPlayer = await _playerRepository.GetByIdAsync(player.Id ?? -1);
                if (existPlayer == null) throw new RepositoryException(ExceptionUtils.GetError("Player", player.Id), "Player");
                players.Add(existPlayer);
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

        var team = new Team(parameter.Name, players, parameter.Logo, tournaments, matches);
        var saved = await _teamRepository.CreateAsync(team);
        if (!saved) throw new RepositoryException(ExceptionUtils.CreateError("Team"), "Team");
        return saved;
    }
}
