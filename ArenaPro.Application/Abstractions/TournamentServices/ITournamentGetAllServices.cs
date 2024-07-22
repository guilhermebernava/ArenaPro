using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.TournamentServices;
public interface ITournamentGetAllServices : IGetServices<List<Tournament>, Tournament>
{
}
