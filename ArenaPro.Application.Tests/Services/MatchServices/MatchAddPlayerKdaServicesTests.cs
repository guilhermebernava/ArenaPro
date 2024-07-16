using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models.MatchValidations;
using ArenaPro.Application.Services.MatchServices;
using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using Moq;
using Match = ArenaPro.Domain.Entities.Match;

namespace ArenaPro.Application.Tests.Services.MatchServices;
public class MatchAddPlayerKdaServicesTests
{
    private readonly Mock<IMatchRepository> _mockMatchRepository;
    private readonly Mock<IPlayerRepository> _mockPlayerRepository;
    private readonly MatchAddPlayerKdaServices _service;

    public MatchAddPlayerKdaServicesTests()
    {
        _mockMatchRepository = new Mock<IMatchRepository>();
        _mockPlayerRepository = new Mock<IPlayerRepository>();
        _service = new MatchAddPlayerKdaServices(
            _mockMatchRepository.Object,
            _mockPlayerRepository.Object
        );
    }

    [Fact]
    public async Task ItShouldThrowValidationExceptionWhenKdaIsInvalid()
    {
        var model = new List<MatchPlayerKdaModel>
            {
                new MatchPlayerKdaModel { PlayerId = 1, MatchId = 1, Kills = -1, Deaths = 0, Assists = 0 }
            };

        await Assert.ThrowsAsync<ValidationException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenPlayerNotFound()
    {
        var model = new List<MatchPlayerKdaModel>
            {
                new MatchPlayerKdaModel { PlayerId = 1, MatchId = 1, Kills = 1, Deaths = 0, Assists = 0 }
            };

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Player)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenMatchNotFound()
    {
        var player = new Player("Player1");
        var model = new List<MatchPlayerKdaModel>
            {
                new MatchPlayerKdaModel { PlayerId = player.Id, MatchId = 1, Kills = 1, Deaths = 0, Assists = 0 }
            };

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(player.Id)).ReturnsAsync(player);
        _mockMatchRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Match)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldAddPlayerKdaWhenValidParametersProvided()
    {
        var player = new Player("Player1");
        var match = new Match(System.DateTime.Now, new Tournament("Tournament1"), new List<Team>());
        var model = new List<MatchPlayerKdaModel>
            {
                new MatchPlayerKdaModel { PlayerId = player.Id, MatchId = match.Id, Kills = 5, Deaths = 3, Assists = 7 }
            };

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(player.Id)).ReturnsAsync(player);
        _mockMatchRepository.Setup(r => r.GetByIdAsync(match.Id)).ReturnsAsync(match);
        _mockMatchRepository.Setup(r => r.SaveAsync()).ReturnsAsync(true);

        var result = await _service.ExecuteAsync(model);

        Assert.True(result);
        Assert.Contains(match.MatchPlayerKdas, pk => pk.PlayerId == player.Id && pk.Kills == 5 && pk.Deaths == 3 && pk.Assists == 7);
        _mockMatchRepository.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenSaveFails()
    {
        var player = new Player("Player1");
        var match = new Match(System.DateTime.Now, new Tournament("Tournament1"), new List<Team>());
        var model = new List<MatchPlayerKdaModel>
            {
                new MatchPlayerKdaModel { PlayerId = player.Id, MatchId = match.Id, Kills = 5, Deaths = 3, Assists = 7 }
            };

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(player.Id)).ReturnsAsync(player);
        _mockMatchRepository.Setup(r => r.GetByIdAsync(match.Id)).ReturnsAsync(match);
        _mockMatchRepository.Setup(r => r.SaveAsync()).ReturnsAsync(false);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }
}
