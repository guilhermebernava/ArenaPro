using ArenaPro.Application.Services.TeamServices;
using ArenaPro.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace ArenaPro.Application.Tests.Services.TeamServices;
public class TeamGetAllServicesTests
{
    private readonly Mock<ITeamRepository> _mockTeamRepository;
    private readonly TeamGetAllServices _service;

    public TeamGetAllServicesTests()
    {
        _mockTeamRepository = new Mock<ITeamRepository>();
        _service = new TeamGetAllServices(_mockTeamRepository.Object);
    }

    [Fact]
    public async Task ItShouldReturnAllTeams()
    {
        var teams = new List<Team>
            {
                new Team("Team A"),
                new Team("Team B"),
                new Team("Team C")
            };

        _mockTeamRepository.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Team, object>>[]>()))
            .ReturnsAsync(teams);

        var result = await _service.ExecuteAsync();

        Assert.Equal(teams.Count, result.Count);
        Assert.Equal(teams, result);
    }

    [Fact]
    public async Task ItShouldReturnEmptyListWhenNoTeamsFound()
    {
        _mockTeamRepository.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Team, object>>[]>()))
            .ReturnsAsync(new List<Team>());

        var result = await _service.ExecuteAsync();

        Assert.Empty(result);
    }

    [Fact]
    public async Task ItShouldCallRepositoryWithIncludes()
    {
        Expression<Func<Team, object>>[] includes = {
                team => team.Players,
                team => team.Tournaments,
                team => team.Matches
            };

        _mockTeamRepository.Setup(r => r.GetAllAsync(includes))
            .ReturnsAsync(new List<Team>());

        var result = await _service.ExecuteAsync(includes);

        _mockTeamRepository.Verify(r => r.GetAllAsync(includes), Times.Once);
    }
}