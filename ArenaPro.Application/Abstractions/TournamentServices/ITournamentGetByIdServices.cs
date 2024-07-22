using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.TournamentServices;
public interface ITournamentGetByIdServices : IGetServices<int, Tournament, Tournament>
{
}
