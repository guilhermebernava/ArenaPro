namespace ArenaPro.Application.Models.TournamentModels;
public class TournamentUpdateModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double? Prize { get; set; }
    public bool? Ended { get; set; }
    public virtual List<int>? TeamsToAdd { get; set; }
    public virtual List<int>? TeamsToRemove { get; set; }
    public virtual List<int>? MatchesToAdd { get; set; }
    public virtual List<int>? MatchesToRemove { get; set; }
}
