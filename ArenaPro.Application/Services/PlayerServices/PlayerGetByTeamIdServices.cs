using ArenaPro.Application.Abstractions.PlayerServices;
using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using System.Linq.Expressions;

namespace ArenaPro.Application.Services.PlayerServices;
public class PlayerGetByTeamIdServices : IPlayerGetByTeamIdServices
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerGetByTeamIdServices(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<List<Player>> ExecuteAsync(int teamId, params Expression<Func<Player, object>>[] includes)
    {
        var players = await _playerRepository.GetByTeamIdAsync(teamId, includes);
        return players;
    }
}
