using ArenaPro.Application.Services.PlayerServices;
using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace ArenaPro.Application.Tests.Services.PlayerServices;
public class PlayerGetByNickServicesTests
{
    private readonly Mock<IPlayerRepository> _mockPlayerRepository;
    private readonly PlayerGetByNickServices _service;

    public PlayerGetByNickServicesTests()
    {
        _mockPlayerRepository = new Mock<IPlayerRepository>();
        _service = new PlayerGetByNickServices(_mockPlayerRepository.Object);
    }

    [Fact]
    public async Task ItShouldReturnPlayersWhenNickIsValid()
    {
        var players = new List<Player>
            {
                new Player("Nick1", 1, "Name1", 25, "Genre1", "test1@example.com"),
                new Player("Nick2", 2, "Name2", 26, "Genre2", "test2@example.com")
            };

        _mockPlayerRepository.Setup(r => r.GetByNickAsync(It.IsAny<string>(), It.IsAny<Expression<Func<Player, object>>[]>()))
            .ReturnsAsync(players);

        var result = await _service.ExecuteAsync("Nick");

        Assert.NotNull(result);
        Assert.Equal(players, result);
        _mockPlayerRepository.Verify(r => r.GetByNickAsync("Nick", It.IsAny<Expression<Func<Player, object>>[]>()), Times.Once);
    }

    [Fact]
    public async Task ItShouldReturnEmptyListWhenNoPlayersFound()
    {
        var players = new List<Player>();

        _mockPlayerRepository.Setup(r => r.GetByNickAsync(It.IsAny<string>(), It.IsAny<Expression<Func<Player, object>>[]>()))
            .ReturnsAsync(players);

        var result = await _service.ExecuteAsync("Nick");

        Assert.NotNull(result);
        Assert.Empty(result);
        _mockPlayerRepository.Verify(r => r.GetByNickAsync("Nick", It.IsAny<Expression<Func<Player, object>>[]>()), Times.Once);
    }

    [Fact]
    public async Task ItShouldReturnPlayersWithIncludesWhenNickIsValid()
    {
        var players = new List<Player>
            {
                new Player("Nick1", 1, "Name1", 25, "Genre1", "test1@example.com"),
                new Player("Nick2", 2, "Name2", 26, "Genre2", "test2@example.com")
            };
        Expression<Func<Player, object>> includeExpression = p => p.Team;

        _mockPlayerRepository.Setup(r => r.GetByNickAsync(It.IsAny<string>(), It.IsAny<Expression<Func<Player, object>>[]>()))
            .ReturnsAsync(players);

        var result = await _service.ExecuteAsync("Nick", includeExpression);

        Assert.NotNull(result);
        Assert.Equal(players, result);
        _mockPlayerRepository.Verify(r => r.GetByNickAsync("Nick", It.IsAny<Expression<Func<Player, object>>[]>()), Times.Once);
    }
}