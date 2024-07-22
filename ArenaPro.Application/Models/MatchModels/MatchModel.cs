using ArenaPro.Application.Models.TeamModels;
using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Models.MatchValidations;
public class MatchModel
{
    public int? Id { get;  set; }
    public DateTime MatchDate { get;   set; }
    public bool? Ended { get;  set; }
    public List<TeamModel> Teams { get;  set; }
    public int TournamentId { get;  set; }
    public virtual List<MatchResultModel>? MatchesResults { get;  set; }
    public virtual List<MatchPlayerKda>? MatchPlayerKdas { get;  set; }
}


