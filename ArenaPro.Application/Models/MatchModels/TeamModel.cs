namespace ArenaPro.Application.Models.MatchValidations;
public class TeamModel
{
    public int? Id { get; private set; }
    public string Name { get; private set; }
    public string? Logo { get; private set; }
    public virtual List<PlayerModel>? Players { get; private set; }
    public virtual List<TournamentModel>? Tournaments { get; private set; }
    public virtual List<MatchModel>? Matches { get; private set; }
    public virtual List<TeamMatchModel>? MatchesResults { get; private set; }
}
