namespace ArenaPro.Domain.Entities;
public class MatchResult
{
    protected MatchResult()
    {
        
    }
    public MatchResult(Team team, Match match, bool won)
    {
        TeamId = team.Id;
        MatchId = match.Id;
        Won = won;
    }

    public int TeamId { get; set; }
    public int MatchId { get; set; }
    public bool Won { get; set; }
    public virtual Team Team { get; set; }
    public virtual Match Match { get; set; }
}
