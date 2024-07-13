using ArenaPro.Domain.Entities;
using ArenaPro.Infra.Data;
using ArenaPro.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArenaPro.Infra.Tests.Repositories;
public class TournamentRepositoryTests
{
    [Fact]
    public async Task ItShouldGetByName()
    {
        var repository = _getRepository();
        var Tournament = new Tournament("test");
        var saved = await repository.CreateAsync(Tournament);
        Assert.True(saved);

        var Tournaments = await repository.GetByNameAsync(Tournament.Name);
        Assert.True(Tournaments.Count == 1);
    }


    private static TournamentRepository _getRepository()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
        var context = new AppDbContext(options);
        return new TournamentRepository(context);
    }
}
