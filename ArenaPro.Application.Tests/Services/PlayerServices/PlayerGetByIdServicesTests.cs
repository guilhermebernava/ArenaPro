using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Services.PlayerServices;
using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace ArenaPro.Application.Tests.Services.PlayerServices;
public class PlayerGetByIdServicesTests
{
    private readonly Mock<IPlayerRepository> _mockPlayerRepository;
    private readonly PlayerGetByIdServices _service;

    public PlayerGetByIdServicesTests()
    {
        _mockPlayerRepository = new Mock<IPlayerRepository>();
        _service = new PlayerGetByIdServices(_mockPlayerRepository.Object);
    }

    [Fact]
    public async Task ItShouldReturnPlayerWhenIdIsValid()
    {
        var player = new Player("Nick", 1, "Name", 25, "Genre", "test@example.com");

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<Expression<Func<Player, object>>[]>()))
            .ReturnsAsync(player);

        var result = await _service.ExecuteAsync(1);

        Assert.NotNull(result);
        Assert.Equal(player, result);
        _mockPlayerRepository.Verify(r => r.GetByIdAsync(1, It.IsAny<Expression<Func<Player, object>>[]>()), Times.Once);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenPlayerNotFound()
    {
        _mockPlayerRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<Expression<Func<Player, object>>[]>()))
            .ReturnsAsync((Player)null);

        var exception = await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(1));
        Assert.Equal("Could not GET this Player with this ID - 1", exception.Message);
        _mockPlayerRepository.Verify(r => r.GetByIdAsync(1, It.IsAny<Expression<Func<Player, object>>[]>()), Times.Once);
    }

    [Fact]
    public async Task ItShouldReturnPlayerWithIncludesWhenIdIsValid()
    {
        var player = new Player("Nick", 1, "Name", 25, "Genre", "test@example.com");
        Expression<Func<Player, object>> includeExpression = p => p.Team;

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<Expression<Func<Player, object>>[]>()))
            .ReturnsAsync(player);

        var result = await _service.ExecuteAsync(1, includeExpression);

        Assert.NotNull(result);
        Assert.Equal(player, result);
        _mockPlayerRepository.Verify(r => r.GetByIdAsync(1, It.IsAny<Expression<Func<Player, object>>[]>()), Times.Once);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenIdIsInvalid()
    {
        var invalidId = -1;
        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(invalidId));
    }
}