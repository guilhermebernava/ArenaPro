using ArenaPro.Domain.Entities;
using ArenaPro.Domain.Validations;

namespace ArenaPro.Domain.Tests.Entities;
public class TournamentTests
{
    #region CONSTRUCTOR
    [Fact]
    public void ItShouldThrowExceptionWhenNameHasLessThanFourCharacters()
    {
        var exception = Assert.Throws<DomainException>(() => new Tournament("abc"));

        Assert.Equal("Name must have at least 4 characters", exception.Message);
    }

    [Fact]
    public void ItShouldThrowExceptionWhenPrizeIsNegative()
    {
        var exception = Assert.Throws<DomainException>(() => new Tournament("Valid Name", -100));

        Assert.Equal("Prize must be greater than 0", exception.Message);
    }

    [Fact]
    public void ItShouldInitializeTournamentCorrectlyWithMandatoryParameters()
    {
        var tournament = new Tournament("Valid Name");

        Assert.Equal("ValidName", tournament.Name); // Name should be cleaned of spaces
        Assert.Null(tournament.Prize);
        Assert.Null(tournament.Teams);
    }

    [Fact]
    public void ItShouldInitializeTournamentCorrectlyWithAllParameters()
    {
        var teams = new List<Team> { new Team("Team 1", new List<Player>()) };
        var tournament = new Tournament("Valid Name", 1000, teams);

        Assert.Equal("ValidName", tournament.Name); // Name should be cleaned of spaces
        Assert.Equal(1000, tournament.Prize);
        Assert.Equal(teams, tournament.Teams);
    }

    [Fact]
    public void ItShouldInitializeTournamentCorrectlyWithEmptyTeams()
    {
        var teams = new List<Team>();
        var tournament = new Tournament("Valid Name", 1000, teams);

        Assert.Equal("ValidName", tournament.Name); // Name should be cleaned of spaces
        Assert.Equal(1000, tournament.Prize);
        Assert.Empty(tournament.Teams);
    }
    #endregion

    #region CHANGE NAME
    [Fact]
    public void ItShouldThrowExceptionWhenChangeNameHasLessThanFourCharacters()
    {
        var tournament = new Tournament("Valid Name");

        var exception = Assert.Throws<DomainException>(() => tournament.ChangeName("abc"));

        Assert.Equal("Name must have at least 4 characters", exception.Message);
    }

    [Fact]
    public void ItShouldChangeNameCorrectly()
    {
        var tournament = new Tournament("Valid Name");

        tournament.ChangeName("New Name");

        Assert.Equal("New Name", tournament.Name);
    }
    #endregion

    #region CHANGE PRIZE
    [Fact]
    public void ItShouldThrowExceptionWhenChangePrizeIsNegative()
    {
        var tournament = new Tournament("Valid Name");

        var exception = Assert.Throws<DomainException>(() => tournament.ChangePrize(-100));

        Assert.Equal("Prize must be greater than 0", exception.Message);
    }

    [Fact]
    public void ItShouldChangePrizeCorrectly()
    {
        var tournament = new Tournament("Valid Name");

        tournament.ChangePrize(500);

        Assert.Equal(500, tournament.Prize);
    }
    #endregion

    #region CHANGE TEAMS
    [Fact]
    public void ItShouldAddTeamsCorrectly()
    {
        var tournament = new Tournament("Valid Name");
        var teams = new List<Team> { new Team("Team 1", new List<Player>()) };

        tournament.AddTeams(teams);

        Assert.Contains(teams[0], tournament.Teams);
    }

    [Fact]
    public void ItShouldRemoveTeamsCorrectly()
    {
        var team = new Team("Team 1", new List<Player>());
        var tournament = new Tournament("Valid Name", teams: new List<Team> { team });

        tournament.RemoveTeams(new List<Team> { team });

        Assert.DoesNotContain(team, tournament.Teams);
    }
    #endregion

    #region CHANGE MATCHES
    [Fact]
    public void ItShouldAddMatchesCorrectly()
    {
        var team = new Team("Team 1", new List<Player>());
        var tournament = new Tournament("Valid Name");
        var match = new Match(DateTime.Now, new Tournament("Another Tournament"), new List<Team>() { team, team});

        tournament.AddMatches(new List<Match> { match });

        Assert.Contains(match, tournament.Matches);
    }

    [Fact]
    public void ItShouldRemoveMatchesCorrectly()
    {
        var team = new Team("Team 1", new List<Player>());
        var match = new Match(DateTime.Now, new Tournament("Another Tournament"), new List<Team>() { team, team });
        var tournament = new Tournament("Valid Name");
        tournament.AddMatches(new List<Match>() { match });

        tournament.RemoveMatches(new List<Match> { match });

        Assert.DoesNotContain(match, tournament.Matches);
    }
    #endregion
}
