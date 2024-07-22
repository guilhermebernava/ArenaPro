using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.TeamServices;
public interface ITeamGetAllServices : IGetServices<List<Team>, Team>
{
}
