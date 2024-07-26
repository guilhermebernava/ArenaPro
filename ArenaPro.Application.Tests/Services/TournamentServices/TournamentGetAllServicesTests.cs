using ArenaPro.Application.Services.TournamentServices;
using ArenaPro.Domain.Entities;
using Moq;
using Match = ArenaPro.Domain.Entities.Match;
using System.Linq.Expressions;

namespace ArenaPro.Application.Tests.Services.TournamentServices;
public class TournamentGetAllServicesTests
{
    private readonly Mock<ITournamentRepository> _mockTournamentRepository;
    private readonly TournamentGetAllServices _service;

    public TournamentGetAllServicesTests()
    {
        _mockTournamentRepository = new Mock<ITournamentRepository>();
        _service = new TournamentGetAllServices(_mockTournamentRepository.Object);
    }

    [Fact]
    public async Task ItShouldReturnListOfTournaments()
    {
        var tournaments = new List<Tournament>
            {
                new Tournament("Tournament1", 1000, new List<Team>(), new List<Match>()),
                new Tournament("Tournament2", 2000, new List<Team>(), new List<Match>())
            };

        _mockTournamentRepository.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Tournament, object>>[]>()))
            .ReturnsAsync(tournaments);

        var result = await _service.ExecuteAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Tournament1", result[0].Name);
        Assert.Equal("Tournament2", result[1].Name);
    }

    [Fact]
    public async Task ItShouldReturnEmptyListWhenNoTournamentsFound()
    {
        _mockTournamentRepository.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Tournament, object>>[]>()))
            .ReturnsAsync(new List<Tournament>());

        var result = await _service.ExecuteAsync();

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task ItShouldIncludeSpecifiedProperties()
    {
        var tournaments = new List<Tournament>
            {
                new Tournament("Tournament1", 1000, new List<Team>(), new List<Match>()),
                new Tournament("Tournament2", 2000, new List<Team>(), new List<Match>())
            };

        Expression<Func<Tournament, object>> includeTeams = t => t.Teams;
        Expression<Func<Tournament, object>> includeMatches = t => t.Matches;

        _mockTournamentRepository.Setup(r => r.GetAllAsync(includeTeams, includeMatches))
            .ReturnsAsync(tournaments);

        var result = await _service.ExecuteAsync(includeTeams, includeMatches);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        _mockTournamentRepository.Verify(r => r.GetAllAsync(includeTeams, includeMatches), Times.Once);
    }
}