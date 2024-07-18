using ArenaPro.Application.Models;
using ArenaPro.Application.Models.MatchModels;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.MatchServices;
public interface IMatchGetByDateServices : IGetServices<MatchGetModel<DateTime>,List<Match>, Match>
{
}
