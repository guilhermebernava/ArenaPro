namespace ArenaPro.Application.Models.TeamModels;
public class TeamUpdateModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Logo { get; set; }
    public virtual List<int>? PlayersToAdd { get; set; }
    public virtual List<int>? PlayersToRemove { get; set; }
    public virtual List<int>? TournamentsToAdd { get; set; }
    public virtual List<int>? TournamentsToRemove { get; set; }
    public virtual List<int>? MatchesToAdd { get; set; }
    public virtual List<int>? MatchesToRemove { get; set; }
}
