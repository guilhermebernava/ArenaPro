using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.PlayerServices;
public interface IPlayerGetByTeamIdServices : IGetServices<int, List<Player>, Player>
{
}
