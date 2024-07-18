using ArenaPro.Application.Models;
using ArenaPro.Application.Models.MatchModels;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.MatchServices;
public interface IMatchGetByDateServices : IServices<MatchGetModel<DateTime>,List<Match>>
{
}
