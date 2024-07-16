using ArenaPro.Domain.Entities;
using ArenaPro.Domain.Validations;

namespace ArenaPro.Domain.Tests.Entities;
public class MatchTests
{
    #region CONSTRUCTOR
    [Fact]
    public void ItShouldCraeteMatch()
    {
        var tournament = new Tournament("test");
        var teams = new List<Team>() { new Team("test 1", new List<Player>()), new Team("test 2", new List<Player>()) };
        var match = new Match(DateTime.Now, tournament, teams);
    }

    [Fact]
    public void ItShouldThrowsDomainExceptionDueToHaveMoreThan2Teams()
    {
        var tournament = new Tournament("test");
        var teams = new List<Team>() { new Team("test 1", new List<Player>()), new Team("test 1", new List<Player>()), new Team("test 1", new List<Player>()) };
        var exception = Assert.Throws<DomainException>(() => new Match(DateTime.Now, tournament, teams));
        Assert.Equal("Must have olny 2 Teams in a Match", exception.Message);
    }

    [Fact]
    public void ItShouldThrowsDomainExceptionDueToDateInvalid()
    {
        var tournament = new Tournament("test");
        var teams = new List<Team>() { new Team("test 1", new List<Player>()), new Team("test 2", new List<Player>()) };
        var exception = Assert.Throws<DomainException>(() => new Match(new DateTime(2000, 01, 01), tournament, teams));
        Assert.Equal("MatchDate could not be smaller than 12/31/2010", exception.Message);
    }

    [Fact]
    public void ItShouldThrowsDomainExceptionDueToNullTournament()
    {
        var teams = new List<Team>() { new Team("test 1", new List<Player>()), new Team("test 2", new List<Player>()) };
        var exception = Assert.Throws<DomainException>(() => new Match(new DateTime(2000, 01, 01), null, teams));
        Assert.Equal("Tournament could not be NULL", exception.Message);
    }
    #endregion

    #region MATCH RESULT
    [Fact]
    public void ItShouldThrowExceptionWhenTeamIsNullInAddMatchResult()
    {
        var tournament = new Tournament("test");
        var teams = new List<Team>() { new Team("test 1", new List<Player>()), new Team("test 2", new List<Player>()) };
        var match = new Match(DateTime.Now, tournament, teams);

        var exception = Assert.Throws<DomainException>(() => match.AddMatchResult(null, true));

        Assert.Equal("Team could not be NULL", exception.Message);
    }

    [Fact]
    public void ItShouldThrowExceptionWhenAddingMoreThanTwoMatchResults()
    {
        var team1 = new Team("Team 1", new List<Player>());
        var team2 = new Team("Team 2", new List<Player>());
        var team3 = new Team("Team 3", new List<Player>());
        var tournament = new Tournament("test");
        var teams = new List<Team>() { team1, team2 };
        var match = new Match(DateTime.Now, tournament, teams);


        match.AddMatchResult(team1, true);
        match.AddMatchResult(team2, false);

        var exception = Assert.Throws<DomainException>(() => match.AddMatchResult(team3, true));

        Assert.Equal("Could not add more Match results, can olny have 2 Teams results per Match", exception.Message);
    }

    [Fact]
    public void ItShouldAddMatchResultCorrectly()
    {
        var team1 = new Team("Team 1", new List<Player>());
        var team2 = new Team("Team 2", new List<Player>());
        var tournament = new Tournament("test");
        var teams = new List<Team>() { team1, team2 };
        var match = new Match(DateTime.Now, tournament, teams);

        match.AddMatchResult(team1, true);

        Assert.Single(match.MatchesResults);
    }

    [Fact]
    public void ItShouldRemoveMatchResultCorrectly()
    {
        var team1 = new Team("Team 1", new List<Player>());
        var tournament = new Tournament("test");
        var teams = new List<Team>() { team1 };
        var match = new Match(DateTime.Now, tournament, teams);

        match.AddMatchResult(team1, true);
        Assert.True(match.MatchesResults.Count > 0);

        var teamMatch = match.MatchesResults.First();
        match.RemoveMatchResult(teamMatch);
        Assert.True(match.MatchesResults.Count == 0);
    }
    #endregion

    #region MATCH PLAYER KDA
    [Fact]
    public void ItShouldThrowExceptionWhenPlayerIsNullInAddMatchPlayerKda()
    {
        var tournament = new Tournament("test");
        var teams = new List<Team>() { new Team("test 1", new List<Player>()), new Team("test 2", new List<Player>()) };
        var match = new Match(DateTime.Now, tournament, teams);

        var exception = Assert.Throws<DomainException>(() => match.AddMatchPlayerKda(null, 1, 1, 1));

        Assert.Equal("Player could not be NULL", exception.Message);
    }

    [Fact]
    public void ItShouldThrowExceptionWhenKillsDeathsOrAssistsAreNegative()
    {
        var tournament = new Tournament("test");
        var teams = new List<Team>() { new Team("test 1", new List<Player>()), new Team("test 2", new List<Player>()) };
        var match = new Match(DateTime.Now, tournament, teams);
        var player = new Player("Player 1", teams.First());

        var exception = Assert.Throws<DomainException>(() => match.AddMatchPlayerKda(player, -1, 0, 0));
        Assert.Equal("Kills,Deaths or Assists could not be less than 0", exception.Message);
    }

    [Fact]
    public void ItShouldThrowExceptionWhenAddingMoreThanTenMatchPlayerKda()
    {
        var tournament = new Tournament("test");
        var teams = new List<Team>() { new Team("test 1", new List<Player>()), new Team("test 2", new List<Player>()) };
        var match = new Match(DateTime.Now, tournament, teams);
        var player = new Player("Player 1", teams.First());

        for (int i = 0; i < 10; i++)
        {
            match.AddMatchPlayerKda(new Player("Player " + i, teams.First()), 1, 1, 1);
        }

        var exception = Assert.Throws<DomainException>(() => match.AddMatchPlayerKda(player, 1, 1, 1));

        Assert.Equal("Could not add more MatchPlayerKda, can olny have 10 MatchPlayerKda per Match", exception.Message);
    }

    [Fact]
    public void ItShouldAddMatchPlayerKdaCorrectly()
    {
        var tournament = new Tournament("test");
        var teams = new List<Team>() { new Team("test 1", new List<Player>()), new Team("test 2", new List<Player>()) };
        var match = new Match(DateTime.Now, tournament, teams);
        var player = new Player("Player 1", teams.First());

        match.AddMatchPlayerKda(player, 1, 1, 1);

        Assert.Single(match.MatchPlayerKdas);
        Assert.Equal(player, match.MatchPlayerKdas.First().Player);
    }

    [Fact]
    public void ItShouldRemoveMatchPlayerKdaCorrectly()
    {
        var tournament = new Tournament("test");
        var teams = new List<Team>() { new Team("test 1", new List<Player>()) };
        var match = new Match(DateTime.Now, tournament, teams);
        var player = new Player("Player 1", teams.First());

        match.AddMatchPlayerKda(player, 1, 1, 1);
        Assert.Single(match.MatchPlayerKdas);
        Assert.Equal(player, match.MatchPlayerKdas.First().Player);

        var playerKda = match.MatchPlayerKdas.First();
        match.RemoveMatchPlayerKda(playerKda);
        Assert.True(match.MatchPlayerKdas.Count == 0);
    }
    #endregion

    #region CHANGE DATE
    [Fact]
    public void ItShouldThrowExceptionWhenDateIsBeforeLimit()
    {
        var tournament = new Tournament("test");
        var teams = new List<Team>() { new Team("test 1", new List<Player>()), new Team("test 2", new List<Player>()) };
        var match = new Match(DateTime.Now, tournament, teams);

        var exception = Assert.Throws<DomainException>(() => match.ChangeMatchDate(new DateTime(2000, 01, 01)));

        Assert.Equal("MatchDate could not be smaller than 12/31/2010", exception.Message);
    }

    [Fact]
    public void ItShouldChangeMatchDateCorrectly()
    {
        var tournament = new Tournament("test");
        var teams = new List<Team>() { new Team("test 1", new List<Player>()), new Team("test 2", new List<Player>()) };
        var match = new Match(DateTime.Now, tournament, teams);
        var newDate = new DateTime(2021, 01, 01);

        match.ChangeMatchDate(newDate);

        Assert.Equal(newDate, match.MatchDate);
    }
    #endregion
}
