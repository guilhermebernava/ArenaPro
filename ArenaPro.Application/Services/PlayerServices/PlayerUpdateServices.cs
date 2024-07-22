using ArenaPro.Application.Abstractions.PlayerServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models;
using ArenaPro.Application.Utils;
using ArenaPro.Domain.Abstractions;

namespace ArenaPro.Application.Services.PlayerServices;
public class PlayerUpdateServices : IPlayerUpdateServices
{
    private readonly IPlayerRepository _playerRepository;
    private readonly ITeamRepository _teamRepository;

    public PlayerUpdateServices(IPlayerRepository playerRepository, ITeamRepository teamRepository)
    {
        _playerRepository = playerRepository;
        _teamRepository = teamRepository;
    }


    public async Task<bool> ExecuteAsync(PlayerModel parameter)
    {
        if (parameter.Id == null || parameter.Id < 1) throw new ValidationException("Player Id was invalid");

        var entity = await _playerRepository.GetByIdAsync(parameter.Id ?? -1);
        if (entity == null) throw new RepositoryException(ExceptionUtils.DeleteError("Player", parameter.Id), "Player");

        entity.Name = parameter.Name;
        if (parameter.Age != null) entity.ChangeAge(parameter.Age ?? -1);
        if (parameter.Email != null) entity.ChangeEmail(parameter.Email);
        if (parameter.Nick != null) entity.ChangeNick(parameter.Nick);
        if (parameter.TeamId != null)
        {
            var existTeam = await _teamRepository.GetByIdAsync(parameter.TeamId ?? -1);
            if (existTeam == null) throw new RepositoryException(ExceptionUtils.DeleteError("Team", parameter.TeamId), "Team");
            entity.ChangeTeam(existTeam);
        }

        var saved = await _playerRepository.UpdateAsync(entity);
        if (!saved) throw new RepositoryException(ExceptionUtils.UpdateError("Player", parameter.Id), "Player");
        return saved;
    }
}
