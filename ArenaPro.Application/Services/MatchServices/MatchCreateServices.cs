using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Application.Models.MatchValidations;
using ArenaPro.Application.Utils;
using ArenaPro.Domain.Entities;
using FluentValidation;

namespace ArenaPro.Application.Services.MatchServices;
public class MatchCreateServices : IMatchCreateServices
{
    private readonly IMatchRepository _matchRepository;
    private readonly ITournamentRepository _tournamentRepository;
    private readonly IValidator<MatchModel> _validator;
    private readonly ITeamRepository _teamRepository;

    public MatchCreateServices(IMatchRepository matchRepository, ITournamentRepository tournamentRepository, IValidator<MatchModel> validator, ITeamRepository teamRepository)
    {
        _matchRepository = matchRepository;
        _tournamentRepository = tournamentRepository;
        _validator = validator;
        _teamRepository = teamRepository;
    }

    public async Task<bool> ExecuteAsync(MatchModel parameter)
    {
        var validation = _validator.Validate(parameter);
        if (!validation.IsValid) throw new ValidationException(validation.Errors);

        var tournament = await _tournamentRepository.GetByIdAsync(parameter.TournamentId);
        if (tournament == null) throw new Exceptions.RepositoryException(ExceptionUtils.GetError("Tournament", parameter.TournamentId), "Tournament");

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
