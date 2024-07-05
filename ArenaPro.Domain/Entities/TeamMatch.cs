namespace ArenaPro.Domain.Entities;
public class TeamMatch
{
    public int TeamId { get; set; }
    public int MatchId { get; set; }
    public bool Won { get; set; }
    public virtual Team Team { get; set; }
    public virtual Match Match { get; set; }
}
