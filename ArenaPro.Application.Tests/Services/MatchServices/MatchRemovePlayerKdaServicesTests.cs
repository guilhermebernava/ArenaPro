using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models.MatchValidations;
using ArenaPro.Application.Services.MatchServices;
using ArenaPro.Domain.Entities;
using Moq;
using Match = ArenaPro.Domain.Entities.Match;

namespace ArenaPro.Application.Tests.Services.MatchServices;
public class MatchRemovePlayerKdaServicesTests
{
    private readonly Mock<IMatchRepository> _mockMatchRepository;
    private readonly MatchRemovePlayerKdaServices _service;

    public MatchRemovePlayerKdaServicesTests()
    {
        _mockMatchRepository = new Mock<IMatchRepository>();
        _service = new MatchRemovePlayerKdaServices(_mockMatchRepository.Object);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenMatchNotFound()
    {
        var model = new List<MatchPlayerKdaModel>
            {
                new MatchPlayerKdaModel { MatchId = 1, PlayerId = 1 }
            };

        _mockMatchRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Match)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldThrowValidationExceptionWhenPlayerKdaNotFound()
    {
        var match = new Match(DateTime.Now, new Tournament("Tournament1"), new List<Team>());
        match.AddMatchPlayerKda(new Player("teste"), 1, 1, 1);
        var model = new List<MatchPlayerKdaModel>
            {
                new MatchPlayerKdaModel { MatchId = match.Id, PlayerId = 999 }
            };

        _mockMatchRepository.Setup(r => r.GetByIdAsync(match.Id)).ReturnsAsync(match);

        await Assert.ThrowsAsync<ValidationException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldRemovePlayerKdaWhenValidParametersProvided()
    {
        var player = new Player("Player1");
        var match = new Match(DateTime.Now, new Tournament("Tournament1"), new List<Team>());
        match.AddMatchPlayerKda(player, 5,3,7);
        var model = new List<MatchPlayerKdaModel>
            {
                new MatchPlayerKdaModel { MatchId = match.Id, PlayerId = player.Id }
            };

        _mockMatchRepository.Setup(r => r.GetByIdAsync(match.Id)).ReturnsAsync(match);
        _mockMatchRepository.Setup(r => r.SaveAsync()).ReturnsAsync(true);

        var result = await _service.ExecuteAsync(model);

        Assert.True(result);
        _mockMatchRepository.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenSaveFails()
    {
        var player = new Player("Player1");
        var match = new Match(DateTime.Now, new Tournament("Tournament1"), new List<Team>());
        match.AddMatchPlayerKda(player, 5, 3, 7);
        var model = new List<MatchPlayerKdaModel>
            {
                new MatchPlayerKdaModel { MatchId = match.Id, PlayerId = player.Id }
            };

        _mockMatchRepository.Setup(r => r.GetByIdAsync(match.Id)).ReturnsAsync(match);
        _mockMatchRepository.Setup(r => r.SaveAsync()).ReturnsAsync(false);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }
}