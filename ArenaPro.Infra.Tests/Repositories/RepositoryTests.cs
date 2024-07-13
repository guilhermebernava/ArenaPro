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
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
        using var context = new AppDbContext(options);
        var repository = new Repository<Tournament>(context);
        var tournament = new Tournament("Test");

        repository.dbSet.Add(tournament);
        var result = await repository.SaveAsync();
        Assert.True(result);
    }

    [Fact]
    public async Task ItShouldCreate()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
        using var context = new AppDbContext(options); 
        var repository = new Repository<Tournament>(context);
        var tournament = new Tournament("Test");

        var result = await repository.CreateAsync(tournament);
        Assert.True(result);
    }

    [Fact]
    public async Task ItShouldDelete()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
        using var context = new AppDbContext(options);
        var repository = new Repository<Tournament>(context);
        var tournament = new Tournament("Test");

        var result = await repository.CreateAsync(tournament);
        Assert.True(result);

        result = await repository.DeleteAsync(tournament);
        Assert.True(result);
    }

    [Fact]
    public async Task ItShouldGetById()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
        using var context = new AppDbContext(options);
        var repository = new Repository<Tournament>(context);
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
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
        using var context = new AppDbContext(options);
        var repository = new Repository<Tournament>(context);
        var tournament = new Tournament("Test");

        var result = await repository.CreateAsync(tournament);
        Assert.True(result);

        var entities = await repository.GetAllAsync();
        Assert.NotEmpty(entities);
    }
}
