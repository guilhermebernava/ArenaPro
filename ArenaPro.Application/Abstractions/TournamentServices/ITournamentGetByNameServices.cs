using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.TournamentServices;
public interface ITournamentGetByNameServices : IGetServices<List<Tournament>, Tournament>
{
}
