using ArenaPro.Application.Models.MatchValidations;

namespace ArenaPro.Application.Models;
public class TournamentModel
{
    public string Name { get;  set; }
    public double? Prize { get;  set; }
    public bool? Ended { get;  set; }
    public virtual List<TeamModel>? Teams { get;  set; }
    public virtual List<MatchModel>? Matches { get;  set; }
}
