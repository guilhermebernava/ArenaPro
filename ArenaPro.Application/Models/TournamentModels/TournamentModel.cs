using ArenaPro.Application.Models.MatchValidations;
using ArenaPro.Application.Models.TeamModels;

namespace ArenaPro.Application.Models.TournamentModels;
public class TournamentModel
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public double? Prize { get; set; }
    public bool? Ended { get; set; }
    public virtual List<TeamModel>? Teams { get; set; }
    public virtual List<MatchModel>? Matches { get; set; }
}
