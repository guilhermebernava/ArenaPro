namespace ArenaPro.Application.Models.MatchValidations;

public class MatchUpdateModel
{
    public int Id { get;  set; }
    public DateTime? MatchDate { get;  set; }
    public bool? Ended { get;  set; }
    public List<int>? TeamsToAdd { get;  set; }
    public List<int>? TeamsToRemove { get;  set; }
    public int? TournamentId { get;  set; }
}


