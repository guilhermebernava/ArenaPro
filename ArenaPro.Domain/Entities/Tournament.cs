using ArenaPro.Domain.Validations;

namespace ArenaPro.Domain.Entities;
public class Tournament : Entity
{
    protected Tournament()
    {

    }
    public Tournament(string name, double? prize = null, List<Team>? teams = null, List<Match>? matches = null)
    {
        var cleanName = name.Replace(" ", "");
        DomainException.When(cleanName.Length < 4, "Name must have at least 4 characters");
        DomainException.When(prize < 0, "Prize must be greater than 0");

        Name = cleanName;
        Prize = prize;
        if (teams != null) Teams = teams;
        if (matches != null) Matches = matches;
    }

    public void ChangeName(string name)
    {
        if(string.IsNullOrEmpty(name)) DomainException.When(true, "String was empty");
        DomainException.When(name.Length < 4, "Name must have at least 4 characters");
        Name = name;
    }

    public void ChangePrize(double prize)
    {
        DomainException.When(prize < 0, "Prize must be greater than 0");
        Prize = prize;
    }

    public void AddTeam(Team team)
    {
        if (Teams == null) Teams = new List<Team>();
        Teams.Add(team);
    }
    public void RemoveTeam(Team team)
    {
        if (Teams == null) return;
        Teams.Remove(team);
    }

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

    public void AddTeams(List<Team> teams)
    {
        if (Teams == null) Teams = new List<Team>();
        Teams.AddRange(teams);
    }

    public void RemoveTeams(List<Team> teams)
    {
        if (Teams == null) return;
        foreach (Team team in teams)
        {
            var existTeam = Teams.FirstOrDefault(_ => _.Id == team.Id);
            if (existTeam != null) Teams.Remove(existTeam);
        }
    }

    public void AddMatches(List<Match> matches)
    {
        if (Matches == null) Matches = new List<Match>();
        Matches.AddRange(matches);
    }

    public void RemoveMatches(List<Match> matches)
    {
        if (Matches == null) return;
        foreach (Match match in matches)
        {
            var existMatch = Matches.FirstOrDefault(_ => _.Id == match.Id);
            if (existMatch != null) Matches.Remove(existMatch);
        }
    }

    public string Name { get; private set; }
    public double? Prize { get; private set; }
    public bool Ended { get; set; }
    public virtual List<Team> Teams { get; private set; }
    public virtual List<Match> Matches { get; private set; }
}
