﻿using ArenaPro.Application.Abstractions.MatchServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models;

namespace ArenaPro.Application.Services.MatchServices;
public class MatchAddMatchResultServices : IMatchAddMatchResultServices
{
    private readonly IMatchRepository _matchRepository;
    private readonly ITeamRepository _teamRepository;

    public MatchAddMatchResultServices(IMatchRepository matchRepository, ITeamRepository teamRepository)
    {
        _matchRepository = matchRepository;
        _teamRepository = teamRepository;
    }

    public async Task<bool> ExecuteAsync(List<TeamMatchModel> parameter)
    {
        foreach (var teamMatchModel in parameter)
        {
            var team = await _teamRepository.GetByIdAsync(teamMatchModel.TeamId);
            if (team == null) throw new RepositoryException($"Not found Team with this ID - {teamMatchModel.TeamId}", "Team");

            var match = await _matchRepository.GetByIdAsync(teamMatchModel.MatchId);
            if (match == null) throw new RepositoryException($"Not found Match with this ID - {teamMatchModel.MatchId}", "Match");
            match.AddMatchResult(team, teamMatchModel.Won);           
        }

        var saved = await _matchRepository.SaveAsync();
        if (!saved) throw new RepositoryException($"Could not save", "Match");
        return saved;
    }
}
