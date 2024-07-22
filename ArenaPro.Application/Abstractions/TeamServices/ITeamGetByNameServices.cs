using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.TeamServices;
public interface ITeamGetByNameServices : IGetServices<string, List<Team>, Team>
{
}
