using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.MatchServices;
public interface IMatchGetByIdServices : IGetServices<int, Match, Match>
{
}
