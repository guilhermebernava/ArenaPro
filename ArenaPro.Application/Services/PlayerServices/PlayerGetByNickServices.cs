using ArenaPro.Application.Abstractions.PlayerServices;
using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using System.Linq.Expressions;

namespace ArenaPro.Application.Services.PlayerServices;
public class PlayerGetByNickServices : IPlayerGetByNickServices
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerGetByNickServices(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<List<Player>> ExecuteAsync(string nick, params Expression<Func<Player, object>>[] includes)
    {
        var players = await _playerRepository.GetByNickAsync(nick, includes);
        return players;
    }
}
