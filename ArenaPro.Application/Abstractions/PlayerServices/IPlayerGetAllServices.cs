using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.PlayerServices;
public interface IPlayerGetAllServices : IGetServices<List<Player>,Player>
{
}
