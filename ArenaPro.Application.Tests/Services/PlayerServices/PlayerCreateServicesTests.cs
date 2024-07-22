using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models;
using ArenaPro.Application.Services.PlayerServices;
using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using Moq;

namespace ArenaPro.Application.Tests.Services.PlayerServices;
public class PlayerCreateServicesTests
{
    private readonly Mock<IPlayerRepository> _mockPlayerRepository;
    private readonly PlayerCreateServices _service;

    public PlayerCreateServicesTests()
    {
        _mockPlayerRepository = new Mock<IPlayerRepository>();
        _service = new PlayerCreateServices(_mockPlayerRepository.Object);
    }

    [Fact]
    public async Task ItShouldCreatePlayerWhenValidParametersProvided()
    {
        var model = new PlayerModel
        {
            Nick = "TestNick",
            TeamId = 1,
            Name = "TestName",
            Age = 25,
            Genre = "TestGenre",
            Email = "test@example.com"
        };
        var player = new Player(model.Nick, model.TeamId, model.Name, model.Age, model.Genre, model.Email);

        _mockPlayerRepository.Setup(r => r.CreateAsync(It.IsAny<Player>())).ReturnsAsync(true);

        var result = await _service.ExecuteAsync(model);

        Assert.True(result);
        _mockPlayerRepository.Verify(r => r.CreateAsync(It.IsAny<Player>()), Times.Once);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenPlayerNotSaved()
    {
        var model = new PlayerModel
        {
            Nick = "TestNick",
            TeamId = 1,
            Name = "TestName",
            Age = 25,
            Genre = "TestGenre",
            Email = "test@example.com"
        };
        _mockPlayerRepository.Setup(r => r.CreateAsync(It.IsAny<Player>())).ReturnsAsync(false);

        var exception = await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
        Assert.Equal("Could not CREATE this Player", exception.Message);
    }
}
