using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Services.TeamServices;
using ArenaPro.Application.Utils;
using ArenaPro.Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace ArenaPro.Application.Tests.Services.TeamServices;
public class TeamGetByIdServicesTests
{
    private readonly Mock<ITeamRepository> _mockTeamRepository;
    private readonly TeamGetByIdServices _service;

    public TeamGetByIdServicesTests()
    {
        _mockTeamRepository = new Mock<ITeamRepository>();
        _service = new TeamGetByIdServices(_mockTeamRepository.Object);
    }

    [Fact]
    public async Task ItShouldReturnTeamWhenFound()
    {
        var teamId = 1;
        var team = new Team("Team A");
        _mockTeamRepository.Setup(r => r.GetByIdAsync(teamId, It.IsAny<Expression<Func<Team, object>>[]>()))
            .ReturnsAsync(team);

        var result = await _service.ExecuteAsync(teamId);

        Assert.NotNull(result);
        Assert.Equal(team, result);
    }

    [Fact]
    public async Task ItShouldThrowExceptionWhenTeamNotFound()
    {
        var teamId = 1;
        _mockTeamRepository.Setup(r => r.GetByIdAsync(teamId, It.IsAny<Expression<Func<Team, object>>[]>()))
            .ReturnsAsync((Team)null);

        var exception = await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(teamId));
        Assert.Equal(ExceptionUtils.GetError("Team", teamId), exception.Message);
        Assert.Equal("Team", exception.Repository);
    }

    [Fact]
    public async Task ItShouldCallRepositoryWithIncludes()
    {
        var teamId = 1;
        var team = new Team("Team A");
        Expression<Func<Team, object>>[] includes = {
                t => t.Players,
                t => t.Tournaments,
                t => t.Matches
            };

        _mockTeamRepository.Setup(r => r.GetByIdAsync(teamId, includes))
            .ReturnsAsync(team);

        var result = await _service.ExecuteAsync(teamId, includes);

        _mockTeamRepository.Verify(r => r.GetByIdAsync(teamId, includes), Times.Once);
        Assert.Equal(team, result);
    }
}