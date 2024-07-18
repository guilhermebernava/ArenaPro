using ArenaPro.Domain.Validations;

namespace ArenaPro.Domain.Entities;
public class Team : Entity
{
    protected Team()
    {
        
    }
    public Team(string name, List<Player>? players = null, string? logo = null, List<Tournament>? tournaments = null, List<Match>? matches = null)
    {
        var cleanName = name.Replace(" ", "");
        DomainException.When(players?.Count > 5, "Team must to have MAX 5 players");
        DomainException.When(cleanName.Length < 4, "Name must have at least 4 characters");

        Name = cleanName;
        Logo = logo;

        if(players != null) Players = players;
        if (tournaments != null) Tournaments = tournaments;
        if (matches != null) Matches = matches;
    }

    public void ChangeName(string name)
    {
        var cleanName = name.Replace(" ", "");
        DomainException.When(cleanName.Length < 4, "Name must have at least 4 characters");
        Name = cleanName;
    }

    public bool RemovePlayer(Player playerToRemove) => Players.Remove(playerToRemove);

    public void AddPlayer(Player player)
    {
        DomainException.When(Players.Count >= 5, "Team must to have MAX 5 players");
        Players.Add(player);
    }

    public void AddTournament(Tournament tournament)
    {
        if(Tournaments == null) Tournaments = new List<Tournament>();
        Tournaments.Add(tournament);
    }

    public void RemoveTournament(Tournament tournament) {
        if (Tournaments == null) return;
        Tournaments.Remove(tournament);}

    public void AddMatch(Match match)
    {
        if (Matches == null) Matches = new List<Match>();
        Matches.Add(match);
    }

    public void RemoveMatch(Match match)
    {
        if (Matches == null) return;
        Matches.Remove(match);
    }


    public string Name { get; private set; }
    public string? Logo { get; set; }
    public virtual List<Player> Players { get; private set; }
    public virtual List<Tournament> Tournaments { get; private set; }
    public virtual List<Match> Matches { get; private set; }
    public virtual List<MatchResult> MatchesResults { get; private set; }
}
