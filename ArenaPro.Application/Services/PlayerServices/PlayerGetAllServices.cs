using ArenaPro.Application.Abstractions.PlayerServices;
using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using System.Linq.Expressions;

namespace ArenaPro.Application.Services.PlayerServices;
public class PlayerGetAllServices : IPlayerGetAllServices
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerGetAllServices(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<List<Player>> ExecuteAsync(params Expression<Func<Player, object>>[] includes)
    {
        var playeres = await _playerRepository.GetAllAsync(includes);
        return playeres;
    }
}
