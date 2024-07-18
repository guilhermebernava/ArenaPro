using ArenaPro.Application.Models;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.MatchServices;
public interface IMatchAddMatchResultServices : IServices<List<MatchResultModel>, bool>
{
}
