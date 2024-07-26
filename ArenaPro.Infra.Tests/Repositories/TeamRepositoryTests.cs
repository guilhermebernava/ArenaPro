using ArenaPro.Domain.Entities;
using ArenaPro.Infra.Data;
using ArenaPro.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArenaPro.Infra.Tests.Repositories;
public class TeamRepositoryTests
{
    [Fact]
    public async Task ItShouldGetByName()
    {
        var repository = _getRepository();
        var Team = new Team("test");
        var saved = await repository.CreateAsync(Team);
        Assert.True(saved);

        var Teams = await repository.GetByNameAsync(Team.Name);
        Assert.True(Teams.Count == 1);
    }


    private static TeamRepository _getRepository()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
        var context = new AppDbContext(options);
        return new TeamRepository(context, LogGenerator.GetLogger<TeamRepository>());
    }
}
