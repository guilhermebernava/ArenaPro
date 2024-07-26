using ArenaPro.Application.Models.MatchValidations;
using ArenaPro.Application.Models.TournamentModels;

namespace ArenaPro.Application.Models.TeamModels;
public class TeamModel
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string? Logo { get; set; }
    public virtual List<PlayerModel>? Players { get; set; }
    public virtual List<TournamentModel>? Tournaments { get; set; }
    public virtual List<MatchModel>? Matches { get; set; }
}
