using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.TeamServices;
public interface ITeamGetByIdServices : IGetServices<int,Team, Team>
{
}
