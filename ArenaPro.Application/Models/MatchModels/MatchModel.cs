using ArenaPro.Domain.Entities;

namespace ArenaPro.Application.Models.MatchValidations;
public class MatchModel
{
    public int? Id { get; private set; }
    public DateTime MatchDate { get;  private set; }
    public bool? Ended { get; private set; }
    public List<TeamModel> Teams { get; private set; }
    public int TournamentId { get; private set; }
    public virtual List<TeamMatchModel>? MatchesResults { get; private set; }
    public virtual List<MatchPlayerKda>? MatchPlayerKdas { get; private set; }
}


