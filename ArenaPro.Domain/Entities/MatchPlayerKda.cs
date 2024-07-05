namespace ArenaPro.Domain.Entities;
public class MatchPlayerKda
{
    public int PlayerId { get; set; }
    public int MatchId { get; set; }
    public int Kills { get; set; }
    public int Deaths { get; set; }
    public int Assists { get; set; }

    public int KDA
    {
        get
        {
            return (Kills + Assists) / Deaths;
        }
    }
    public virtual Player Player { get; set; }
    public virtual Match Match { get; set; }
}
