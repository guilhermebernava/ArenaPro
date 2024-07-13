using ArenaPro.Domain.Entities;
using ArenaPro.Infra.Data;
using ArenaPro.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArenaPro.Infra.Tests.Repositories;
public class PlayerRepositoryTests
{
    [Fact]
    public async Task ItShouldGetByNick()
    {
        var repository = _getRepository();
        var player = new Player("test");
        var saved = await repository.CreateAsync(player);
        Assert.True(saved);

        var players = await repository.GetByNickAsync(player.Nick);
        Assert.True(players.Count == 1);
    }

    [Fact]
    public async Task ItShouldGetByTeamId()
    {
        var repository = _getRepository();
        var team = new Team("Team 1", new List<Player> { });
        var player = new Player("test", team);    
        var saved = await repository.CreateAsync(player);
        Assert.True(saved);

        var players = await repository.GetByTeamIdAsync(team.Id);
        Assert.True(players.Count == 1);
    }

    private static PlayerRepository _getRepository()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
        var context = new AppDbContext(options);
        return new PlayerRepository(context);
    }
}
