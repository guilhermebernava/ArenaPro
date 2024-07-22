using ArenaPro.Application.Services.PlayerServices;
using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace ArenaPro.Application.Tests.Services.PlayerServices;
public class PlayerGetAllServicesTests
{
    private readonly Mock<IPlayerRepository> _mockPlayerRepository;
    private readonly PlayerGetAllServices _service;

    public PlayerGetAllServicesTests()
    {
        _mockPlayerRepository = new Mock<IPlayerRepository>();
        _service = new PlayerGetAllServices(_mockPlayerRepository.Object);
    }

    [Fact]
    public async Task ItShouldReturnAllPlayers()
    {
        var players = new List<Player>
            {
                new Player("Nick1", 1, "Name1", 25, "Genre1", "test1@example.com"),
                new Player("Nick2", 2, "Name2", 26, "Genre2", "test2@example.com")
            };

        _mockPlayerRepository.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Player, object>>[]>())).ReturnsAsync(players);

        var result = await _service.ExecuteAsync();

        Assert.NotNull(result);
        Assert.Equal(players.Count, result.Count);
        _mockPlayerRepository.Verify(r => r.GetAllAsync(It.IsAny<Expression<Func<Player, object>>[]>()), Times.Once);
    }

    [Fact]
    public async Task ItShouldReturnEmptyListWhenNoPlayersFound()
    {
        var players = new List<Player>();

        _mockPlayerRepository.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Player, object>>[]>())).ReturnsAsync(players);

        var result = await _service.ExecuteAsync();

        Assert.NotNull(result);
        Assert.Empty(result);
        _mockPlayerRepository.Verify(r => r.GetAllAsync(It.IsAny<Expression<Func<Player, object>>[]>()), Times.Once);
    }

    [Fact]
    public async Task ItShouldReturnAllPlayersWithIncludes()
    {
        var players = new List<Player>
            {
                new Player("Nick1", 1, "Name1", 25, "Genre1", "test1@example.com"),
                new Player("Nick2", 2, "Name2", 26, "Genre2", "test2@example.com")
            };

        Expression<Func<Player, object>> includeExpression = p => p.Team;

        _mockPlayerRepository.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Player, object>>[]>())).ReturnsAsync(players);

        var result = await _service.ExecuteAsync(includeExpression);

        Assert.NotNull(result);
        Assert.Equal(players.Count, result.Count);
        _mockPlayerRepository.Verify(r => r.GetAllAsync(It.IsAny<Expression<Func<Player, object>>[]>()), Times.Once);
    }
}