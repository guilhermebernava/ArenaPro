using ArenaPro.Domain.Validations;

namespace ArenaPro.Domain.Entities;
public class Tournament : Entity
{
    public Tournament(string name,double? prize = null, List<Team>? teams = null)
    {
        var cleanName = name.Replace(" ", "");
        DomainException.When(cleanName.Length < 4, "Name must have at least 4 characters");
        DomainException.When(prize < 0, "Prize must be greater than 0");

        Name = cleanName;
        Prize = prize;
        if(teams != null) Teams = teams;
    }

    public void ChangeName(string name)
    {
        var cleanName = name.Replace(" ", "");
        DomainException.When(cleanName.Length < 4, "Name must have at least 4 characters");
        Name = cleanName;
    }

    public void EndTournament() => Ended = true;

    public void ChangePrize(double prize)
    {
        DomainException.When(prize < 0, "Prize must be greater than 0");
        Prize = prize;
    }

    public void AddTeams(List<Team> teams) => Teams.AddRange(teams);

    public void RemoveTeams(List<Team> teams) {
        foreach (Team team in teams)
        {
            Teams.Remove(team);
        }
    }

    public void AddMatches(List<Match> matches) => Matches.AddRange(matches);

    public void RemoveMatches(List<Match> matches)
    {
        foreach (Match match in matches)
        {
            Matches.Remove(match);
        }
    }

    public string Name { get; private set; }
    public double? Prize { get; private set; }
    public bool Ended { get; private set; }
    public virtual List<Team> Teams { get; private set; }
    public virtual List<Match> Matches { get; private set; }
}
