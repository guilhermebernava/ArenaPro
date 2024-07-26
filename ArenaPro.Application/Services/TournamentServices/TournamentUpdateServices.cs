using ArenaPro.Application.Abstractions.TournamentServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models.TournamentModels;
using ArenaPro.Application.Utils;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Services.TournamentServices;
public class TournamentUpdateServices : ITournamentUpdateServices
{
    private readonly ITeamRepository _teamRepository;
    private readonly ITournamentRepository _tournamentRepository;
    private readonly IMatchRepository _matchRepository;

    public TournamentUpdateServices(ITeamRepository teamRepository, ITournamentRepository tournamentRepository, IMatchRepository matchRepository)
    {
        _teamRepository = teamRepository;
        _tournamentRepository = tournamentRepository;
        _matchRepository = matchRepository;
    }


    public async Task<bool> ExecuteAsync(TournamentUpdateModel parameter)
    {
        if (parameter.Id < 1) throw new ValidationException("Team Id was invalid");
        var entity = await _tournamentRepository.GetByIdAsync(parameter.Id);
        if (entity == null) throw new RepositoryException(ExceptionUtils.GetError("Tournament", parameter.Id), "Tournament");

        await _updateMatches(parameter, entity);
        await _updateTeams(parameter, entity);
        if (parameter.Prize != null) entity.ChangePrize(parameter.Prize ?? 0);
        if (parameter.Name != null) entity.ChangeName(parameter.Name);
        if (parameter.Ended != null) entity.Ended = parameter.Ended ?? false;

        var updated = await _tournamentRepository.UpdateAsync(entity);
        if (!updated) throw new RepositoryException(ExceptionUtils.UpdateError("Tournament"), "Tournament");
        return updated;
    }

    private async Task _updateMatches(TournamentUpdateModel parameter, Tournament entity)
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

    private async Task _updateTeams(TournamentUpdateModel parameter, Tournament entity)
    {
        if (parameter.TeamsToAdd != null)
        {
            foreach (var teamId in parameter.TeamsToAdd)
            {
                var existTeam = await _teamRepository.GetByIdAsync(teamId);
                if (existTeam == null) throw new RepositoryException(ExceptionUtils.GetError("Team", teamId), "Team");
                entity.AddTeam(existTeam);
            }
        }

        if (parameter.TeamsToRemove != null)
        {
            foreach (var teamId in parameter.TeamsToRemove)
            {
                var existTeam = await _teamRepository.GetByIdAsync(teamId);
                if (existTeam == null) throw new RepositoryException(ExceptionUtils.GetError("Team", teamId), "Team");
                entity.RemoveTeam(existTeam);
            }
        }
    }
}
