using ArenaPro.Domain.Entities;
using ArenaPro.Domain.Validations;

namespace ArenaPro.Domain.Tests.Entities;
public class PlayerTests
{
    #region CONSTRUCTOR
    [Fact]
    public void ItShouldCreate()
    {
        var team = new Team("Team 1", new List<Player>());
        var player = new Player("player 1", team);
    }

    [Fact]
    public void ItShouldThrowsDomainExceptionWhenNameLessThan4Caracters()
    {
        var team = new Team("Team 1", new List<Player>());
        var exception = Assert.Throws<DomainException>(() => new Player("pla", team));
        Assert.Equal("Nick must have at least 4 characters", exception.Message);
    }

    [Fact]
    public void ItShouldThrowsDomainExceptionWhenAgeLessThan18()
    {
        var team = new Team("Team 1", new List<Player>());
        var exception = Assert.Throws<DomainException>(() => new Player("player 1", team, age: 17));
        Assert.Equal("Age could not be less than 18 years", exception.Message);
    }
    #endregion

    #region CHANGE NAME
    [Fact]
    public void ItShouldThrowExceptionWhenNickHasLessThanFourCharacters()
    {
        var team = new Team("Team 1", new List<Player>());
        var player = new Player("player 1", team);

        var exception = Assert.Throws<DomainException>(() => player.ChangeNick("abc"));

        Assert.Equal("Nick must have at least 4 characters", exception.Message);
    }

    [Fact]
    public void ItShouldChangeNickCorrectly()
    {
        var team = new Team("Team 1", new List<Player>());
        var player = new Player("player 1", team);

        player.ChangeNick("newNick");

        Assert.Equal("newNick", player.Nick);
    }
    #endregion

    #region CHANGE AGE
    [Fact]
    public void ItShouldThrowExceptionWhenAgeIsLessThan18()
    {
        var team = new Team("Team 1", new List<Player>());
        var player = new Player("player 1", team);

        var exception = Assert.Throws<DomainException>(() => player.ChangeAge(17));

        Assert.Equal("Age could not be less than 18 years", exception.Message);
    }

    [Fact]
    public void ItShouldChangeAgeCorrectly()
    {
        var team = new Team("Team 1", new List<Player>());
        var player = new Player("player 1", team);

        player.ChangeAge(20);

        Assert.Equal(20, player.Age);
    }
    #endregion

    #region CHANGE TEAM
    [Fact]
    public void ItShouldThrowExceptionWhenTeamIsNull()
    {
        var team = new Team("Team 1", new List<Player>());
        var player = new Player("player 1", team);

        var exception = Assert.Throws<DomainException>(() => player.ChangeTeam(null));

        Assert.Equal("Team could not be NULL", exception.Message);
    }

    [Fact]
    public void ItShouldChangeTeamCorrectly()
    {
        var team1 = new Team("Team 1", new List<Player>());
        var team2 = new Team("Team 2", new List<Player>());
        var player = new Player("player 1", team1);

        player.ChangeTeam(team2);

        Assert.Equal(team2, player.Team);
        Assert.Equal(team2.Id, player.TeamId);
    }
    #endregion

    #region CHANGE EMAIL
    [Fact]
    public void ItShouldThrowExceptionWhenEmailIsInvalid()
    {
        var team = new Team("Team 1", new List<Player>());
        var player = new Player("player 1", team);

        var exception = Assert.Throws<DomainException>(() => player.ChangeEmail("invalid-email"));

        Assert.Equal("Email is invalid", exception.Message);
    }

    [Fact]
    public void ItShouldChangeEmailCorrectly()
    {
        var team = new Team("Team 1", new List<Player>());
        var player = new Player("player 1", team);

        player.ChangeEmail("valid.email@example.com");

        Assert.Equal("valid.email@example.com", player.Email);
    }
    #endregion
}
