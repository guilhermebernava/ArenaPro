using ArenaPro.Application.Abstractions.PlayerServices;
using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Utils;
using ArenaPro.Domain.Abstractions;

namespace ArenaPro.Application.Services.PlayerServices;
public class PlayerDeleteServices : IPlayerDeleteServices
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerDeleteServices(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }


    public async Task<bool> ExecuteAsync(int parameter)
    {
        if (parameter < 1) throw new ValidationException("Id must be greater than 0");

        var player = await _playerRepository.GetByIdAsync(parameter);
        if (player == null) throw new RepositoryException(ExceptionUtils.GetError("Player", parameter), "Player");

        var deleted = await _playerRepository.DeleteAsync(player);
        if (!deleted) throw new RepositoryException(ExceptionUtils.DeleteError("Player",parameter), "Player");
        return deleted;
    }
}

