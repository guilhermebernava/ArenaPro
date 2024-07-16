using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models;
using ArenaPro.Application.Services.MatchServices;
using ArenaPro.Domain.Entities;
using Moq;
using Match = ArenaPro.Domain.Entities.Match;

namespace ArenaPro.Application.Tests.Services.MatchServices;
public class MatchAddMatchResultServicesTests
{
    private readonly Mock<IMatchRepository> _mockMatchRepository;
    private readonly Mock<ITeamRepository> _mockTeamRepository;
    private readonly MatchAddMatchResultServices _service;

    public MatchAddMatchResultServicesTests()
    {
        _mockMatchRepository = new Mock<IMatchRepository>();
        _mockTeamRepository = new Mock<ITeamRepository>();
        _service = new MatchAddMatchResultServices(
            _mockMatchRepository.Object,
            _mockTeamRepository.Object
        );
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenTeamNotFound()
    {
        var model = new List<TeamMatchModel>
            {
                new TeamMatchModel { TeamId = 1, MatchId = 1, Won = true }
            };

        _mockTeamRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Team)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenMatchNotFound()
    {
        var team = new Team("Team1");
        var model = new List<TeamMatchModel>
            {
                new TeamMatchModel { TeamId = team.Id, MatchId = 1, Won = true }
            };

        _mockTeamRepository.Setup(r => r.GetByIdAsync(team.Id)).ReturnsAsync(team);
        _mockMatchRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Match)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldAddMatchResultWhenValidParametersProvided()
    {
        var team = new Team("Team1");
        var match = new Match(DateTime.Now, new Tournament("Tournament1"), new List<Team> { team });
        var model = new List<TeamMatchModel>
            {
                new TeamMatchModel { TeamId = team.Id, MatchId = match.Id, Won = true }
            };

        _mockTeamRepository.Setup(r => r.GetByIdAsync(team.Id)).ReturnsAsync(team);
        _mockMatchRepository.Setup(r => r.GetByIdAsync(match.Id)).ReturnsAsync(match);
        _mockMatchRepository.Setup(r => r.SaveAsync()).ReturnsAsync(true);

        var result = await _service.ExecuteAsync(model);

        Assert.True(result);
        Assert.Contains(match.MatchesResults, mr => mr.TeamId == team.Id && mr.Won == true);
        _mockMatchRepository.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenSaveFails()
    {
        var team = new Team("Team1");
        var match = new Match(System.DateTime.Now, new Tournament("Tournament1"), new List<Team> { team });
        var model = new List<TeamMatchModel>
            {
                new TeamMatchModel { TeamId = team.Id, MatchId = match.Id, Won = true }
            };

        _mockTeamRepository.Setup(r => r.GetByIdAsync(team.Id)).ReturnsAsync(team);
        _mockMatchRepository.Setup(r => r.GetByIdAsync(match.Id)).ReturnsAsync(match);
        _mockMatchRepository.Setup(r => r.SaveAsync()).ReturnsAsync(false);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }
}