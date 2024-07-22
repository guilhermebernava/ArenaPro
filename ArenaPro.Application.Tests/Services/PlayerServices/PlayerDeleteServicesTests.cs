using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Services.PlayerServices;
using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using Moq;

namespace ArenaPro.Application.Tests.Services.PlayerServices;
public class PlayerDeleteServicesTests
{
    private readonly Mock<IPlayerRepository> _mockPlayerRepository;
    private readonly PlayerDeleteServices _service;

    public PlayerDeleteServicesTests()
    {
        _mockPlayerRepository = new Mock<IPlayerRepository>();
        _service = new PlayerDeleteServices(_mockPlayerRepository.Object);
    }

    [Fact]
    public async Task ItShouldThrowValidationExceptionWhenIdIsLessThanOne()
    {
        var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.ExecuteAsync(0));
        Assert.Equal("Id must be greater than 0", exception.Message);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenPlayerNotFound()
    {
        var playerId = 1;
        _mockPlayerRepository.Setup(r => r.GetByIdAsync(playerId)).ReturnsAsync((Player)null);

        var exception = await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(playerId));
        Assert.Equal($"Could not GET this Player with this ID - {playerId}", exception.Message);
    }

    [Fact]
    public async Task ItShouldDeletePlayerWhenValidIdProvided()
    {
        var playerId = 1;
        var player = new Player("Nick", 1, "Name", 25, "Genre", "test@example.com");

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(playerId)).ReturnsAsync(player);
        _mockPlayerRepository.Setup(r => r.DeleteAsync(player)).ReturnsAsync(true);

        var result = await _service.ExecuteAsync(playerId);

        Assert.True(result);
        _mockPlayerRepository.Verify(r => r.DeleteAsync(player), Times.Once);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenPlayerNotDeleted()
    {
        var playerId = 1;
        var player = new Player("Nick", 1, "Name", 25, "Genre", "test@example.com");

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(playerId)).ReturnsAsync(player);
        _mockPlayerRepository.Setup(r => r.DeleteAsync(player)).ReturnsAsync(false);

        var exception = await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(playerId));
        Assert.Equal($"Could not DELETE this Player with this ID - {playerId}", exception.Message);
    }
}