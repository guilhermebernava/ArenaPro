using ArenaPro.Domain.Validations;

namespace ArenaPro.Domain.Entities;
public class Match : Entity
{
    protected Match()
    {
        
    }
    public Match(DateTime matchDate, Tournament tournament, List<Team> teams)
    {
        DomainException.When(tournament == null, "Tournament could not be NULL");
        DomainException.When(matchDate.Date < new DateTime(2010, 12, 31), "MatchDate could not be smaller than 12/31/2010");
        DomainException.When(teams.Count > 2, "Must have olny 2 Teams in a Match");

        MatchDate = matchDate;
        Ended = false;
        Tournament = tournament!;
        TournamentId = Tournament.Id;
        Teams = teams;
    }

    public void AddMatchResult(Team team, bool win)
    {
        DomainException.When(team == null, "Team could not be NULL");
        if (MatchesResults != null)
        {
            DomainException.When(MatchesResults.Count > 1, "Could not add more Match results, can olny have 2 Teams results per Match");

            MatchesResults.Add(new TeamMatch()
            {
                Match = this,
                MatchId = Id,
                Team = team,
                TeamId = team.Id,
                Won = win
            });

        }
        else
        {
            MatchesResults = new List<TeamMatch>
            {
                new TeamMatch()
                {
                Match= this,
                MatchId = Id,
                Team = team,
                TeamId = team.Id,
                Won = win
                }
            };
        }

    }

    public void AddMatchPlayerKda(Player player, int kills, int deaths, int assists)
    {
        DomainException.When(player == null, "Player could not be NULL");
        DomainException.When(kills < 0 || deaths < 0 || assists < 0, "Kills,Deaths or Assists could not be less than 0");
        if (MatchPlayerKdas != null)
        {
            DomainException.When(MatchPlayerKdas.Count >= 10, "Could not add more MatchPlayerKda, can olny have 10 MatchPlayerKda per Match");

            MatchPlayerKdas.Add(new MatchPlayerKda()
            {
                Match = this,
                MatchId = Id,
                Player = player!,
                PlayerId = player!.Id,
                Deaths = deaths,
                Assists = assists,
                Kills = kills
            });

        }
        else
        {
            MatchPlayerKdas = new List<MatchPlayerKda>
            {
              new MatchPlayerKda()
              {
                Match = this,
                MatchId = this.Id,
                Player = player!,
                PlayerId = player!.Id,
                Deaths = deaths,
                Assists = assists,
                Kills = kills
              }
            };
        }
    }

    public void ChangeMatchDate(DateTime newDate)
    {
        DomainException.When(newDate.Date < new DateTime(2010, 12, 31), "MatchDate could not be smaller than 12/31/2010");
        MatchDate = newDate.Date;
    }

    public bool ChangeTeam(Team teamToRemove, Team teamToAdd)
    {
        var couldRemove = Teams.Remove(teamToRemove);
        if (couldRemove)Teams.Add(teamToAdd);
        return couldRemove;
    }

    public void EndMatch()
    {
        Ended = true;
    }

    public DateTime MatchDate { get; private set; }
    public bool Ended { get; private set; }
    public int TournamentId { get; private set; }
    public virtual Tournament Tournament { get; private set; }
    public virtual List<Team> Teams { get; private set; }
    public virtual List<TeamMatch> MatchesResults { get; private set; }
    public virtual List<MatchPlayerKda> MatchPlayerKdas { get; private set; }
}
