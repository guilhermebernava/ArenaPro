using ArenaPro.Application.Abstractions.TeamServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models.TeamModels;
using ArenaPro.Application.Utils;
using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Services.TeamServices;
public class TeamUpdateServices : ITeamUpdateServices
{
    private readonly ITeamRepository _teamRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly ITournamentRepository _tournamentRepository;
    private readonly IMatchRepository _matchRepository;

    public TeamUpdateServices(ITeamRepository teamRepository, IPlayerRepository playerRepository, ITournamentRepository tournamentRepository, IMatchRepository matchRepository)
    {
        _teamRepository = teamRepository;
        _playerRepository = playerRepository;
        _tournamentRepository = tournamentRepository;
        _matchRepository = matchRepository;
    }

    public async Task<bool> ExecuteAsync(TeamUpdateModel parameter)
    {
        if (parameter.Id < 1) throw new ValidationException("Team Id was invalid");

        var entity = await _teamRepository.GetByIdAsync(parameter.Id);
        if (entity == null) throw new RepositoryException(ExceptionUtils.GetError("Team", parameter.Id), "Team");

        if (parameter.Name != null) entity.ChangeName(parameter.Name);
        await _updatePlayers(parameter, entity);
        await _updateTournaments(parameter, entity);
        await _updateMatches(parameter, entity);
        entity.Logo = parameter.Logo;

        var saved = await _teamRepository.UpdateAsync(entity);
        if (!saved) throw new RepositoryException(ExceptionUtils.UpdateError("Team", parameter.Id), "Team");
        return saved;
    }

    private async Task _updatePlayers(TeamUpdateModel parameter, Team entity)
    {
        if (parameter.PlayersToAdd != null)
        {
            foreach (var playerId in parameter.PlayersToAdd)
            {
                var existPlayer = await _playerRepository.GetByIdAsync(playerId);
                if (existPlayer == null) throw new RepositoryException(ExceptionUtils.GetError("Player", playerId), "Player");
                entity.AddPlayer(existPlayer);
            }
        }

        if (parameter.PlayersToRemove != null)
        {
            foreach (var playerId in parameter.PlayersToRemove)
            {
                var existPlayer = await _playerRepository.GetByIdAsync(playerId);
                if (existPlayer == null) throw new RepositoryException(ExceptionUtils.GetError("Player", playerId), "Player");
                entity.RemovePlayer(existPlayer);
            }
        }
    }

    private async Task _updateTournaments(TeamUpdateModel parameter, Team entity)
    {
        if (parameter.TournamentsToAdd != null)
        {
            foreach (var tournamentId in parameter.TournamentsToAdd)
            {
                var existTournament = await _tournamentRepository.GetByIdAsync(tournamentId);
                if (existTournament == null) throw new RepositoryException(ExceptionUtils.GetError("Tournament", tournamentId), "Tournament");
                entity.AddTournament(existTournament);
            }
        }

        if (parameter.TournamentsToRemove != null)
        {
            foreach (var tournamentId in parameter.TournamentsToRemove)
            {
                var existTournament = await _tournamentRepository.GetByIdAsync(tournamentId);
                if (existTournament == null) throw new RepositoryException(ExceptionUtils.GetError("Tournament", tournamentId), "Tournament");
                entity.RemoveTournament(existTournament);
            }
        }
    }

    private async Task _updateMatches(TeamUpdateModel parameter, Team entity)
    {
        if (parameter.MatchesToAdd != null)
        {
            foreach (var matchId in parameter.MatchesToAdd)
            {
                var existMatch = await _matchRepository.GetByIdAsync(matchId);
                if (existMatch == null) throw new RepositoryException(ExceptionUtils.GetError("Match", matchId), "Match");
                entity.AddMatch(existMatch);
            }
        }

        if (parameter.MatchesToRemove != null)
        {
            foreach (var matchId in parameter.MatchesToRemove)
            {
                var existMatch = await _matchRepository.GetByIdAsync(matchId);
                if (existMatch == null) throw new RepositoryException(ExceptionUtils.GetError("Match", matchId), "Match");
                entity.RemoveMatch(existMatch);
            }
        }
    }
}
