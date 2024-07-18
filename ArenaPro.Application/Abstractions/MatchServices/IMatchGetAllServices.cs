using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.MatchServices;
public interface IMatchGetAllServices : IGetServices<List<Match>, Match>
{
}
