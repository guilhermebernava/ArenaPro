namespace ArenaPro.Application.Models.MatchValidations;
public class TeamMatchModel
{
    public int TeamId { get; private set; }
    public int MatchId { get; private set; }
    public bool Won { get; private set; }
}
