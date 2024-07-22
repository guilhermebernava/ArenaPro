using ArenaPro.Application.Services.TeamServices;
using ArenaPro.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace ArenaPro.Application.Tests.Services.TeamServices;
public class TeamGetByNameServicesTests
{
    private readonly Mock<ITeamRepository> _mockTeamRepository;
    private readonly TeamGetByNameServices _service;

    public TeamGetByNameServicesTests()
    {
        _mockTeamRepository = new Mock<ITeamRepository>();
        _service = new TeamGetByNameServices(_mockTeamRepository.Object);
    }

    [Fact]
    public async Task ItShouldReturnTeamsWhenFound()
    {
        var teamName = "Team A";
        var teams = new List<Team>
            {
                new Team("Team A"),
                new Team("Team A")
            };
        _mockTeamRepository.Setup(r => r.GetByNameAsync(teamName, It.IsAny<Expression<Func<Team, object>>[]>()))
            .ReturnsAsync(teams);

        var result = await _service.ExecuteAsync(teamName);

        Assert.NotNull(result);
        Assert.Equal(teams.Count, result.Count);
    }

    [Fact]
    public async Task ItShouldReturnEmptyListWhenNoTeamsFound()
    {
        var teamName = "Team A";
        _mockTeamRepository.Setup(r => r.GetByNameAsync(teamName, It.IsAny<Expression<Func<Team, object>>[]>()))
            .ReturnsAsync(new List<Team>());

        var result = await _service.ExecuteAsync(teamName);

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task ItShouldCallRepositoryWithIncludes()
    {
        var teamName = "Team A";
        var teams = new List<Team> { new Team("Team A") };
        Expression<Func<Team, object>>[] includes = {
                t => t.Players,
                t => t.Tournaments,
                t => t.Matches
            };

        _mockTeamRepository.Setup(r => r.GetByNameAsync(teamName, includes))
            .ReturnsAsync(teams);

        var result = await _service.ExecuteAsync(teamName, includes);

        _mockTeamRepository.Verify(r => r.GetByNameAsync(teamName, includes), Times.Once);
        Assert.Equal(teams, result);
    }
}