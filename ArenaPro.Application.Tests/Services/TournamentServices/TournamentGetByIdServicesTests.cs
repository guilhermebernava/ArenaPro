using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Services.TournamentServices;
using ArenaPro.Application.Utils;
using ArenaPro.Domain.Entities;
using Moq;
using System.Linq.Expressions;
using Match = ArenaPro.Domain.Entities.Match;

namespace ArenaPro.Application.Tests.Services.TournamentServices;
public class TournamentGetByIdServicesTests
{
    private readonly Mock<ITournamentRepository> _mockTournamentRepository;
    private readonly TournamentGetByIdServices _service;

    public TournamentGetByIdServicesTests()
    {
        _mockTournamentRepository = new Mock<ITournamentRepository>();
        _service = new TournamentGetByIdServices(_mockTournamentRepository.Object);
    }

    [Fact]
    public async Task ItShouldReturnTournamentById()
    {
        var tournament = new Tournament("Tournament1", 1000, new List<Team>(), new List<Match>());
        _mockTournamentRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<Expression<Func<Tournament, object>>[]>()))
            .ReturnsAsync(tournament);

        var result = await _service.ExecuteAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Tournament1", result.Name);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenTournamentNotFound()
    {
        _mockTournamentRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<Expression<Func<Tournament, object>>[]>()))
            .ReturnsAsync((Tournament)null);

        var exception = await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(1));
        Assert.Equal(ExceptionUtils.GetError("Tournament", 1), exception.Message);
    }

    [Fact]
    public async Task ItShouldIncludeSpecifiedProperties()
    {
        var tournament = new Tournament("Tournament1", 1000, new List<Team>(), new List<Match>());
        Expression<Func<Tournament, object>> includeTeams = t => t.Teams;
        Expression<Func<Tournament, object>> includeMatches = t => t.Matches;

        _mockTournamentRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>(), includeTeams, includeMatches))
            .ReturnsAsync(tournament);

        var result = await _service.ExecuteAsync(1, includeTeams, includeMatches);

        Assert.NotNull(result);
        Assert.Equal("Tournament1", result.Name);
        _mockTournamentRepository.Verify(r => r.GetByIdAsync(1, includeTeams, includeMatches), Times.Once);
    }
}