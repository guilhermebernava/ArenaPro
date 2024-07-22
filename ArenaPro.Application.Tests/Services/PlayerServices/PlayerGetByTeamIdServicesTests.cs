using ArenaPro.Application.Services.PlayerServices;
using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace ArenaPro.Application.Tests.Services.PlayerServices;
public class PlayerGetByTeamIdServicesTests
{
    private readonly Mock<IPlayerRepository> _mockPlayerRepository;
    private readonly PlayerGetByTeamIdServices _service;

    public PlayerGetByTeamIdServicesTests()
    {
        _mockPlayerRepository = new Mock<IPlayerRepository>();
        _service = new PlayerGetByTeamIdServices(_mockPlayerRepository.Object);
    }

    [Fact]
    public async Task ItShouldReturnPlayersWhenTeamIdIsValid()
    {
        var players = new List<Player>
            {
                new Player("Nick1", 1, "Name1", 25, "Genre1", "test1@example.com"),
                new Player("Nick2", 1, "Name2", 26, "Genre2", "test2@example.com")
            };

        _mockPlayerRepository.Setup(r => r.GetByTeamIdAsync(It.IsAny<int>(), It.IsAny<Expression<Func<Player, object>>[]>()))
            .ReturnsAsync(players);

        var result = await _service.ExecuteAsync(1);

        Assert.NotNull(result);
        Assert.Equal(players, result);
        _mockPlayerRepository.Verify(r => r.GetByTeamIdAsync(1, It.IsAny<Expression<Func<Player, object>>[]>()), Times.Once);
    }

    [Fact]
    public async Task ItShouldReturnEmptyListWhenNoPlayersFoundForTeamId()
    {
        var players = new List<Player>();

        _mockPlayerRepository.Setup(r => r.GetByTeamIdAsync(It.IsAny<int>(), It.IsAny<Expression<Func<Player, object>>[]>()))
            .ReturnsAsync(players);

        var result = await _service.ExecuteAsync(1);

        Assert.NotNull(result);
        Assert.Empty(result);
        _mockPlayerRepository.Verify(r => r.GetByTeamIdAsync(1, It.IsAny<Expression<Func<Player, object>>[]>()), Times.Once);
    }

    [Fact]
    public async Task ItShouldReturnPlayersWithIncludesWhenTeamIdIsValid()
    {
        var players = new List<Player>
            {
                new Player("Nick1", 1, "Name1", 25, "Genre1", "test1@example.com"),
                new Player("Nick2", 1, "Name2", 26, "Genre2", "test2@example.com")
            };
        Expression<Func<Player, object>> includeExpression = p => p.Team;

        _mockPlayerRepository.Setup(r => r.GetByTeamIdAsync(It.IsAny<int>(), It.IsAny<Expression<Func<Player, object>>[]>()))
            .ReturnsAsync(players);

        var result = await _service.ExecuteAsync(1, includeExpression);

        Assert.NotNull(result);
        Assert.Equal(players, result);
        _mockPlayerRepository.Verify(r => r.GetByTeamIdAsync(1, It.IsAny<Expression<Func<Player, object>>[]>()), Times.Once);
    }
}
