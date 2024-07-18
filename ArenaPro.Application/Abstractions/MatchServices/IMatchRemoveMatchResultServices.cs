using ArenaPro.Application.Models;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.MatchServices;
public interface IMatchRemoveMatchResultServices : IServices<List<MatchResultModel>, bool>
{
}
