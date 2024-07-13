using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using ArenaPro.Infra.Data;
using ArenaPro.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArenaPro.Infra.Tests.Repositories;
public class RepositoryTests
{
    [Fact]
    public async Task ItShouldSave()
    {
        var repository = _getRepository();
        var tournament = new Tournament("Test");

        repository.dbSet.Add(tournament);
        var result = await repository.SaveAsync();
        Assert.True(result);
    }

    [Fact]
    public async Task ItShouldCreate()
    {
        var repository = _getRepository();
        var tournament = new Tournament("Test");

        var result = await repository.CreateAsync(tournament);
        Assert.True(result);
    }

    [Fact]
    public async Task ItShouldDelete()
    {
        var repository = _getRepository();
        var tournament = new Tournament("Test");

        var result = await repository.CreateAsync(tournament);
        Assert.True(result);

        result = await repository.DeleteAsync(tournament);
        Assert.True(result);
    }

    [Fact]
    public async Task ItShouldGetById()
    {
        var repository = _getRepository();
        var tournament = new Tournament("Test");

        var result = await repository.CreateAsync(tournament);
        Assert.True(result);

        var entity = await repository.GetByIdAsync(tournament.Id);
        Assert.NotNull(entity);
        Assert.Equal("Test", entity.Name);
    }

    [Fact]
    public async Task ItShouldGetAll()
    {       
        var repository = _getRepository();
        var tournament = new Tournament("Test");

        var result = await repository.CreateAsync(tournament);
        Assert.True(result);

        var entities = await repository.GetAllAsync();
        Assert.NotEmpty(entities);
    }

    private static Repository<Tournament> _getRepository()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
        var context = new AppDbContext(options);
        return new Repository<Tournament>(context);
    }
}
