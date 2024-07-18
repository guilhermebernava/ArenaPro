using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models.MatchValidations;
using ArenaPro.Application.Utils;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Services.MatchServices;

public class MatchUpdateServices : IMatchUpdateServices
{
    private readonly IMatchRepository _matchRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly ITournamentRepository _tournamentRepository;

    public MatchUpdateServices(IMatchRepository matchRepository, ITeamRepository teamRepository, ITournamentRepository tournamentRepository)
    {
        _matchRepository = matchRepository;
        _teamRepository = teamRepository;
        _tournamentRepository = tournamentRepository;
    }

    public async Task<bool> ExecuteAsync(MatchUpdateModel parameter)
    {
        if (parameter.Id <= 0) throw new ValidationException("Id must be greater than 0");
        var match = await _matchRepository.GetByIdAsync(parameter.Id);
        if (match == null) throw new RepositoryException(ExceptionUtils.GetError("Match", parameter.Id), "Match");

        _changeMatchDate(parameter, match);
        _endMatch(parameter, match);
        await _addTeams(parameter, match);
        _removeTeams(parameter, match);
        await _changeTournament(parameter, match);

        var saved = await _matchRepository.SaveAsync();
        if (!saved) throw new RepositoryException(ExceptionUtils.UpdateError("Match", parameter.Id), "Match");
        return saved;
    }

    private async Task _changeTournament(MatchUpdateModel parameter, Match match)
    {
        if (parameter.TournamentId != null)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(parameter.TournamentId ?? -1);
            if (tournament == null) throw new RepositoryException(ExceptionUtils.GetError("Tournament", parameter.TournamentId), "Tournament");
            match.TournamentId = tournament.Id;
            match.Tournament = tournament;
        }
    }

    private static void _endMatch(MatchUpdateModel parameter, Match? match)
    {
        if (parameter.Ended != null)
        {
            match.Ended = parameter.Ended ?? false;
        }
    }

    private static void _changeMatchDate(MatchUpdateModel parameter, Match? match)
    {
        if (parameter.MatchDate != null)
        {
            if (parameter.MatchDate?.Date <= new DateTime(2010, 12, 31).Date) throw new ValidationException("MatchDate must be greater than 2010/12/31");
            match.ChangeMatchDate(parameter.MatchDate ?? DateTime.Now);
        }
    }

    private async Task _addTeams(MatchUpdateModel parameter, Match match)
    {
        if (parameter.TeamsToAdd != null)
        {
            foreach (int teamToAddId in parameter.TeamsToAdd)
            {
                if (match.Teams.Count >= 2) throw new ValidationException("Can have max 2 TEAMS per MATCH");

                var team = await _teamRepository.GetByIdAsync(teamToAddId);
                if (team == null) throw new RepositoryException(ExceptionUtils.GetError("Team", teamToAddId), "Team");
                match.Teams.Add(team);
            }
        }
    }

    private static void _removeTeams(MatchUpdateModel parameter, Match match)
    {
        if (parameter.TeamsToRemove != null)
        {
            foreach (int teamToRemoveId in parameter.TeamsToRemove)
            {
                if (match.Teams.Count < 1) continue;
                var existTeam = match.Teams.FirstOrDefault(x => x.Id == teamToRemoveId);
                if (existTeam == null) throw new ValidationException(ExceptionUtils.GetError("Team", teamToRemoveId));
                match.Teams.Remove(existTeam);
            }
        }
    }
}
