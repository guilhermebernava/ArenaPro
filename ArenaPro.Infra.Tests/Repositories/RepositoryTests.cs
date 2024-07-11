using ArenaPro.Domain.Entities;
using ArenaPro.Infra.Data;
using ArenaPro.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArenaPro.Infra.Tests.Repositories;
public class RepositoryTests
{
    [Fact]
    public async Task ItShouldAdd()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
        using var context = new AppDbContext(options); 
        var repository = new Repository<Tournament>(context);
        var tournament = new Tournament("Test");

        var result = await repository.CreateAsync(tournament);
        Assert.True(result);
    }
}
