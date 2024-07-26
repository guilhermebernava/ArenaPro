using ArenaPro.Application.Services.TournamentServices;
using ArenaPro.Domain.Entities;
using Moq;
using System.Linq.Expressions;
using Match = ArenaPro.Domain.Entities.Match;

namespace ArenaPro.Application.Tests.Services.TournamentServices;
public class TournamentGetByNameServicesTests
{
    private readonly Mock<ITournamentRepository> _mockTournamentRepository;
    private readonly TournamentGetByNameServices _service;

    public TournamentGetByNameServicesTests()
    {
        _mockTournamentRepository = new Mock<ITournamentRepository>();
        _service = new TournamentGetByNameServices(_mockTournamentRepository.Object);
    }

    [Fact]
    public async Task ItShouldReturnTournamentsByName()
    {
        var tournaments = new List<Tournament>
            {
                new Tournament("Tournament1", 1000, new List<Team>(), new List<Match>()),
                new Tournament("Tournament1", 2000, new List<Team>(), new List<Match>())
            };
        _mockTournamentRepository.Setup(r => r.GetByNameAsync(It.IsAny<string>(), It.IsAny<Expression<Func<Tournament, object>>[]>()))
            .ReturnsAsync(tournaments);

        var result = await _service.ExecuteAsync("Tournament1");

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Tournament1", result[0].Name);
        Assert.Equal("Tournament1", result[1].Name);
    }

    [Fact]
    public async Task ItShouldReturnEmptyListWhenNoTournamentsFound()
    {
        _mockTournamentRepository.Setup(r => r.GetByNameAsync(It.IsAny<string>(), It.IsAny<Expression<Func<Tournament, object>>[]>()))
            .ReturnsAsync(new List<Tournament>());

        var result = await _service.ExecuteAsync("NonExistentTournament");

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task ItShouldIncludeSpecifiedProperties()
    {
        var tournaments = new List<Tournament>
            {
                new Tournament("Tournament1", 1000, new List<Team>(), new List<Match>())
            };
        Expression<Func<Tournament, object>> includeTeams = t => t.Teams;
        Expression<Func<Tournament, object>> includeMatches = t => t.Matches;

        _mockTournamentRepository.Setup(r => r.GetByNameAsync(It.IsAny<string>(), includeTeams, includeMatches))
            .ReturnsAsync(tournaments);

        var result = await _service.ExecuteAsync("Tournament1", includeTeams, includeMatches);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Tournament1", result[0].Name);
        _mockTournamentRepository.Verify(r => r.GetByNameAsync("Tournament1", includeTeams, includeMatches), Times.Once);
    }
}