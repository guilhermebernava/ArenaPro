using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models;
using ArenaPro.Application.Services.MatchServices;
using ArenaPro.Domain.Entities;
using Moq;
using Match = ArenaPro.Domain.Entities.Match;

namespace ArenaPro.Application.Tests.Services.MatchServices;
public class MatchRemoveMatchResultServicesTests
{
    private readonly Mock<IMatchRepository> _mockMatchRepository;
    private readonly MatchRemoveMatchResultServices _service;

    public MatchRemoveMatchResultServicesTests()
    {
        _mockMatchRepository = new Mock<IMatchRepository>();
        _service = new MatchRemoveMatchResultServices(_mockMatchRepository.Object);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenMatchNotFound()
    {
        var model = new List<MatchResultModel>
            {
                new MatchResultModel { MatchId = 1, TeamId = 1 }
            };

        _mockMatchRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Match)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldThrowValidationExceptionWhenMatchResultNotFound()
    {
        var team = new Team("Team1");
        var match = new Match(DateTime.Now, new Tournament("Tournament1"), new List<Team> { team });
        var model = new List<MatchResultModel>
            {
                new MatchResultModel { MatchId = -1, TeamId = 999 }
            };

        _mockMatchRepository.Setup(r => r.GetByIdAsync(match.Id)).ReturnsAsync(match);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldRemoveMatchResultWhenValidParametersProvided()
    {
        var team = new Team("Team1");
        var match = new Match(DateTime.Now, new Tournament("Tournament1"), new List<Team> { team });
        match.AddMatchResult(team, true);
        var model = new List<MatchResultModel>
            {
                new MatchResultModel { MatchId = match.Id, TeamId = team.Id }
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
        var team = new Team("Team1");
        var match = new Match(DateTime.Now, new Tournament("Tournament1"), new List<Team> { team });
        var matchResult = new MatchResult(team, match, true);
        match.AddMatchResult(team, true);
        var model = new List<MatchResultModel>
            {
                new MatchResultModel { MatchId = match.Id, TeamId = team.Id }
            };

        _mockMatchRepository.Setup(r => r.GetByIdAsync(match.Id)).ReturnsAsync(match);
        _mockMatchRepository.Setup(r => r.SaveAsync()).ReturnsAsync(false);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }
}