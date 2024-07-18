using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.PlayerServices;
public interface IPlayerGetByNickServices : IGetServices<string, List<Player>, Player>
{
}
