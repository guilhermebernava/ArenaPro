namespace ArenaPro.Application.Models.MatchValidations;
public class TournamentModel
{
    public string Name { get; private set; }
    public double? Prize { get; private set; }
    public bool? Ended { get; private set; }
    public virtual List<TeamModel>? Teams { get; private set; }
    public virtual List<MatchModel>? Matches { get; private set; }
}
