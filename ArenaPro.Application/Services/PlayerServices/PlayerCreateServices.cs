using ArenaPro.Application.Abstractions.PlayerServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models.PlayerModels;
using ArenaPro.Application.Utils;
using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Services.PlayerServices;
public class PlayerCreateServices : IPlayerCreateServices
{
    //TODO criar tests para os player services
    private readonly IPlayerRepository _playerRepository;

    public PlayerCreateServices(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }


    public async Task<bool> ExecuteAsync(PlayerModel parameter)
    {
        var player = new Player(parameter.Nick, parameter.TeamId, parameter.Name, parameter.Age, parameter.Genre, parameter.Email);
        var saved = await _playerRepository.CreateAsync(player);
        if(!saved) throw new RepositoryException(ExceptionUtils.CreateError("Player"), "Player");
        return saved;
    }
}
