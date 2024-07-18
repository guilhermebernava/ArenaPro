using ArenaPro.Application.Models.MatchModels;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Abstractions.MatchServices;
public interface IMatchGetByIdServices : IServices<int, Match>
{
}
