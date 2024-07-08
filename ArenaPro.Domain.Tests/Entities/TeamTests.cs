using ArenaPro.Domain.Entities;
using ArenaPro.Domain.Validations;

namespace ArenaPro.Domain.Tests.Entities;
public class TeamTests
{
    #region CONSTRUCTOR
    [Fact]
    public void ItShouldThrowExceptionWhenPlayersCountIsMoreThanFive()
    {
        var players = new List<Player>
        {
            new Player("Player 1", null),
            new Player("Player 2", null),
            new Player("Player 3", null),
            new Player("Player 4", null),
            new Player("Player 5", null),
            new Player("Player 6", null)
        };

        var exception = Assert.Throws<DomainException>(() => new Team("Team 1", players));
        Assert.Equal("Team must to have MAX 5 players", exception.Message);
    }

    [Fact]
    public void ItShouldThrowExceptionWhenNameHasLessThanFourCharacters()
    {
        var players = new List<Player> { };

        var exception = Assert.Throws<DomainException>(() => new Team("T 1", players));
        Assert.Equal("Name must have at least 4 characters", exception.Message);
    }

    [Fact]
    public void ItShouldInitializeTeamCorrectlyWithMandatoryParameters()
    {
        var players = new List<Player> { };
        var team = new Team("Team 1", players);

        Assert.Equal("Team1", team.Name);
        Assert.Equal(players, team.Players);
        Assert.Null(team.Logo);
        Assert.Null(team.Tournaments);
        Assert.Null(team.Matches);
    }

    [Fact]
    public void ItShouldInitializeTeamCorrectlyWithAllParameters()
    {
        var players = new List<Player> { new Player("Player 1", null) };
        var tournaments = new List<Tournament> { new Tournament("Tournament 1") };
        var logo = "team_logo.png";

        var team = new Team("Team 1", players, logo, tournaments);

        Assert.Equal("Team1", team.Name);
        Assert.Equal(players, team.Players);
        Assert.Equal(logo, team.Logo);
        Assert.Equal(tournaments, team.Tournaments);
    }

    [Fact]
    public void ItShouldInitializeTeamCorrectlyWithEmptyLists()
    {
        var players = new List<Player> { new Player("Player 1", null) };
        var tournaments = new List<Tournament>();
        var matches = new List<Match>();

        var team = new Team("Team 1", players, null, tournaments, matches);

        Assert.Equal("Team1", team.Name);
        Assert.Equal(players, team.Players);
        Assert.Null(team.Logo);
        Assert.Empty(team.Tournaments);
        Assert.Empty(team.Matches);
    }
    #endregion

    #region CHANGE NAME
    [Fact]
    public void ItShouldThrowExceptionWhenChangeNameHasLessThanFourCharacters()
    {
        var team = new Team("Valid Name", new List<Player>());

        var exception = Assert.Throws<DomainException>(() => team.ChangeName("abc"));

        Assert.Equal("Name must have at least 4 characters", exception.Message);
    }

    [Fact]
    public void ItShouldChangeNameCorrectly()
    {
        var team = new Team("Valid Name", new List<Player>());

        team.ChangeName("New Name");

        Assert.Equal("NewName", team.Name);
    }
    #endregion

    #region PLAYER ADD/REMOVE
    [Fact]
    public void ItShouldRemovePlayerCorrectly()
    {
        var player = new Player("Player 1", null);
        var team = new Team("Valid Name", new List<Player> { player });

        var result = team.RemovePlayer(player);

        Assert.True(result);
        Assert.Empty(team.Players);
    }

    [Fact]
    public void ItShouldReturnFalseWhenPlayerNotInTeam()
    {
        var player = new Player("Player 1", null);
        var team = new Team("Valid Name", new List<Player>());

        var result = team.RemovePlayer(player);

        Assert.False(result);
    }

    [Fact]
    public void ItShouldThrowExceptionWhenAddingMoreThanFivePlayers()
    {
        var players = new List<Player>
        {
            new Player("Player 1", null),
            new Player("Player 2", null),
            new Player("Player 3", null),
            new Player("Player 4", null),
            new Player("Player 5", null)
        };
        var team = new Team("Valid Name", players);

        var exception = Assert.Throws<DomainException>(() => team.AddPlayer(new Player("Player 6", null)));

        Assert.Equal("Team must to have MAX 5 players", exception.Message);
    }

    [Fact]
    public void ItShouldAddPlayerCorrectly()
    {
        var players = new List<Player>
        {
            new Player("Player 1", null)
        };
        var team = new Team("Valid Name", players);

        var newPlayer = new Player("Player 2", null);
        team.AddPlayer(newPlayer);

        Assert.Contains(newPlayer, team.Players);
    }
    #endregion

    #region TOURNAMENTS ADD/REMOVE
    [Fact]
    public void ItShouldAddTournamentCorrectly()
    {
        var team = new Team("Valid Name", new List<Player>());
        var tournament = new Tournament("Tournament 1");

        team.AddTournament(tournament);

        Assert.Contains(tournament, team.Tournaments);
    }

    [Fact]
    public void ItShouldRemoveTournamentCorrectly()
    {
        var tournament = new Tournament("Tournament 1");
        var team = new Team("Valid Name", new List<Player>(), tournaments: new List<Tournament> { tournament });

        team.RemoveTournament(tournament);

        Assert.DoesNotContain(tournament, team.Tournaments);
    }

    [Fact]
    public void ItShouldAddMatchCorrectly()
    {
        var team = new Team("Valid Name", new List<Player>());
        var team2 = new Team("Team 2", new List<Player>());
        var match = new Match(DateTime.Now, new Tournament("Tournament 1"), new List<Team>() { team, team2});

        team.AddMatch(match);

        Assert.Contains(match, team.Matches);
    }

    [Fact]
    public void ItShouldRemoveMatchCorrectly()
    {
        var team = new Team("Valid Name", new List<Player>());
        var team2 = new Team("Team 2", new List<Player>());
        var match = new Match(DateTime.Now, new Tournament("Tournament 1"), new List<Team>() { team, team2 });

        team.AddMatch(match);
        team.RemoveMatch(match);

        Assert.DoesNotContain(match, team.Matches);
    }
    #endregion
}
