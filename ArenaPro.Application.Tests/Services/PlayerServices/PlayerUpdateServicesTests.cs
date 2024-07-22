using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models;
using ArenaPro.Application.Services.PlayerServices;
using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using Moq;

namespace ArenaPro.Application.Tests.Services.PlayerServices;
public class PlayerUpdateServicesTests
{
    private readonly Mock<IPlayerRepository> _mockPlayerRepository;
    private readonly Mock<ITeamRepository> _mockTeamRepository;
    private readonly PlayerUpdateServices _service;

    public PlayerUpdateServicesTests()
    {
        _mockPlayerRepository = new Mock<IPlayerRepository>();
        _mockTeamRepository = new Mock<ITeamRepository>();
        _service = new PlayerUpdateServices(
            _mockPlayerRepository.Object,
            _mockTeamRepository.Object
        );
    }

    [Fact]
    public async Task ItShouldThrowValidationExceptionWhenIdIsNullOrLessThanOne()
    {
        var model = new PlayerModel { Id = 0 };

        await Assert.ThrowsAsync<ValidationException>(() => _service.ExecuteAsync(model));

        model.Id = null;
        await Assert.ThrowsAsync<ValidationException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenPlayerNotFound()
    {
        var model = new PlayerModel { Id = 1 };
        _mockPlayerRepository.Setup(r => r.GetByIdAsync(model.Id.Value)).ReturnsAsync((Player)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenTeamNotFound()
    {
        var player = new Player("Nick", 1, "Name", 25, "Genre", "test@example.com");
        var model = new PlayerModel { Id = 1, TeamId = 2 };

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(model.Id.Value)).ReturnsAsync(player);
        _mockTeamRepository.Setup(r => r.GetByIdAsync(model.TeamId.Value)).ReturnsAsync((Team)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldUpdatePlayerWhenValidParametersProvided()
    {
        var existingPlayer = new Player("OldNick", 1, "OldName", 24, "OldGenre", "old@example.com");
        var newTeam = new Team("NewTeam");
        var model = new PlayerModel
        {
            Id = 1,
            Name = "NewName",
            Age = 26,
            Email = "new@example.com",
            Nick = "NewNick",
            TeamId = 2
        };

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(model.Id.Value)).ReturnsAsync(existingPlayer);
        _mockTeamRepository.Setup(r => r.GetByIdAsync(model.TeamId.Value)).ReturnsAsync(newTeam);
        _mockPlayerRepository.Setup(r => r.UpdateAsync(existingPlayer)).ReturnsAsync(true);

        var result = await _service.ExecuteAsync(model);

        Assert.True(result);
        _mockPlayerRepository.Verify(r => r.UpdateAsync(It.Is<Player>(p =>
            p.Name == model.Name &&
            p.Age == model.Age &&
            p.Email == model.Email &&
            p.Nick == model.Nick &&
            p.Team == newTeam
        )), Times.Once);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenUpdateFails()
    {
        var existingPlayer = new Player("Nick", 1, "Name", 25, "Genre", "test@example.com");
        var model = new PlayerModel { Id = 1 };

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(model.Id.Value)).ReturnsAsync(existingPlayer);
        _mockPlayerRepository.Setup(r => r.UpdateAsync(existingPlayer)).ReturnsAsync(false);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }
}