using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.MatchServices;
public interface IMatchGetByTournamentIdServices : IServices<int,List<Match>>
{
}
