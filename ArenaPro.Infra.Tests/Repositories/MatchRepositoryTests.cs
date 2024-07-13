using ArenaPro.Domain.Entities;
using ArenaPro.Infra.Data;
using ArenaPro.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArenaPro.Infra.Tests.Repositories;
public class MatchRepositoryTests
{
    [Fact]
    public async Task ItShouldGetByDate()
    {
        var repository = _getRepository();
        var tournament = new Tournament("test");
        var match = new Match(DateTime.Now, tournament, new List<Team>());

        var saved = await repository.CreateAsync(match);
        Assert.True(saved);

        var match2 = new Match(DateTime.Now,tournament, new List<Team>());
        saved = await repository.CreateAsync(match2);
        Assert.True(saved);

        var matches = await repository.GetByDateAsync(match.MatchDate);
        Assert.True(matches.Count == 2);
    }

    [Fact]
    public async Task ItShouldGetByDateAndEndedMatch()
    {
        var repository = _getRepository();
        var tournament = new Tournament("test");
        var match = new Match(DateTime.Now, tournament, new List<Team>());

        var saved = await repository.CreateAsync(match);
        Assert.True(saved);

        var match2 = new Match(DateTime.Now, tournament, new List<Team>());
        match2.EndMatch();
        saved = await repository.CreateAsync(match2);
        Assert.True(saved);

        var matches = await repository.GetByDateAsync(match.MatchDate, true);
        Assert.True(matches.Count == 1);
    }

    [Fact]
    public async Task ItShouldGetByTournamentId()
    {
        var repository = _getRepository();
        var tournament = new Tournament("test");
        var match = new Match(DateTime.Now, tournament, new List<Team>());

        var saved = await repository.CreateAsync(match);
        Assert.True(saved);

        var match2 = new Match(DateTime.Now, tournament, new List<Team>());
        saved = await repository.CreateAsync(match2);
        Assert.True(saved);

        var matches = await repository.GetByTournamentIdAsync(tournament.Id);
        Assert.True(matches.Count == 2);
    }

    [Fact]
    public async Task ItShouldGetByTournamentIdAndEndedMatch()
    {
        var repository = _getRepository();
        var tournament = new Tournament("test");
        var match = new Match(DateTime.Now, tournament, new List<Team>());

        var saved = await repository.CreateAsync(match);
        Assert.True(saved);

        var match2 = new Match(DateTime.Now, tournament, new List<Team>());
        match2.EndMatch();
        saved = await repository.CreateAsync(match2);
        Assert.True(saved);

        var matches = await repository.GetByTournamentIdAsync(tournament.Id, true);
        Assert.True(matches.Count == 1);
    }

    private static MatchRepository _getRepository()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
        var context = new AppDbContext(options);
        return new MatchRepository(context);
    }
}
