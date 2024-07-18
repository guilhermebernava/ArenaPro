using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.PlayerServices;
public interface IPlayerGetByIdServices : IGetServices<int, Player,Player>
{
}
