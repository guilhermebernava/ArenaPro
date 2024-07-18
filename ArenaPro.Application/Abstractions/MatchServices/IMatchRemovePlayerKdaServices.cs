using ArenaPro.Application.Models.MatchValidations;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.MatchServices;
public interface IMatchRemovePlayerKdaServices : IServices<List<MatchPlayerKdaModel>, bool>
{
}
