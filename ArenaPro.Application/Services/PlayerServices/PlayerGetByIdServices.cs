using ArenaPro.Application.Abstractions.PlayerServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Utils;
using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using System.Linq.Expressions;

namespace ArenaPro.Application.Services.PlayerServices;
public class PlayerGetByIdServices : IPlayerGetByIdServices
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerGetByIdServices(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<Player> ExecuteAsync(int id, params Expression<Func<Player, object>>[] includes)
    {
        var player = await _playerRepository.GetByIdAsync(id, includes);
        if (player == null) throw new RepositoryException(ExceptionUtils.GetError("Player", id), "Player");
        return player;
    }
}
