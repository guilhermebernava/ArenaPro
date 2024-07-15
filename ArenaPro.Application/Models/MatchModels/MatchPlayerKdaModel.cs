namespace ArenaPro.Application.Models.MatchValidations;
public class MatchPlayerKdaModel
{
    public int PlayerId { get; set; }
    public int MatchId { get; set; }
    public int Kills { get; set; }
    public int Deaths { get; set; }
    public int Assists { get; set; }
}
