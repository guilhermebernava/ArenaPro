using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Services.TeamServices;
using ArenaPro.Domain.Entities;
using Moq;

namespace ArenaPro.Application.Tests.Services.TeamServices;
public class TeamDeleteServicesTests
{
    private readonly Mock<ITeamRepository> _mockTeamRepository;
    private readonly TeamDeleteServices _service;

    public TeamDeleteServicesTests()
    {
        _mockTeamRepository = new Mock<ITeamRepository>();
        _service = new TeamDeleteServices(_mockTeamRepository.Object);
    }

    [Fact]
    public async Task ItShouldThrowValidationExceptionWhenIdIsInvalid()
    {
        await Assert.ThrowsAsync<ValidationException>(() => _service.ExecuteAsync(0));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenTeamNotFound()
    {
        _mockTeamRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Team)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(1));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenDeleteFails()
    {
        var team = new Team("TeamName");
        _mockTeamRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(team);
        _mockTeamRepository.Setup(r => r.DeleteAsync(It.IsAny<Team>())).ReturnsAsync(false);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(1));
    }

    [Fact]
    public async Task ItShouldDeleteTeamWhenValidParametersProvided()
    {
        var team = new Team("TeamName");
        _mockTeamRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(team);
        _mockTeamRepository.Setup(r => r.DeleteAsync(It.IsAny<Team>())).ReturnsAsync(true);

        var result = await _service.ExecuteAsync(1);

        Assert.True(result);
        _mockTeamRepository.Verify(r => r.DeleteAsync(team), Times.Once);
    }
}