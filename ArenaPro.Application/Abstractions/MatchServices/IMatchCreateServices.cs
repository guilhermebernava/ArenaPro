using ArenaPro.Application.Models.MatchValidations;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.MatchServices;
public interface IMatchCreateServices : IServices<MatchModel, bool>
{
}
