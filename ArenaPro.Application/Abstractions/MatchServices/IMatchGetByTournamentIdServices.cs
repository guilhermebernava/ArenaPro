using ArenaPro.Application.Models.MatchModels;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.MatchServices;
public interface IMatchGetByTournamentIdServices : IServices<MatchGetModel<int>,List<Match>>
{
}
