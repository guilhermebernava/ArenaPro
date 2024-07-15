using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.MatchServices;
public interface IMatchGetByDateServices : IServices<DateTime,List<Match>>
{
}
