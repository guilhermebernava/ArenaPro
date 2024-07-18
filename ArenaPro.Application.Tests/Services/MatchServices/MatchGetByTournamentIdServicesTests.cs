using ArenaPro.Application.Models.MatchModels;
using ArenaPro.Application.Services.MatchServices;
using ArenaPro.Domain.Entities;
using Moq;
using Match = ArenaPro.Domain.Entities.Match;

namespace ArenaPro.Application.Tests.Services.MatchServices;
public class MatchGetByTournamentIdServicesTests
{
    private readonly Mock<IMatchRepository> _mockMatchRepository;
    private readonly MatchGetByTournamentIdServices _service;

    public MatchGetByTournamentIdServicesTests()
    {
        _mockMatchRepository = new Mock<IMatchRepository>();
        _service = new MatchGetByTournamentIdServices(_mockMatchRepository.Object);
    }

    [Fact]
    public async Task ItShouldReturnMatchesForGivenTournamentId()
    {
        var matches = new List<Match>
            {
                new Match(DateTime.Now, new Tournament("Tournament1"), new List<Team> { new Team("Team1"), new Team("Team2") }),
                new Match(DateTime.Now, new Tournament("Tournament2"), new List<Team> { new Team("Team3"), new Team("Team4") })
            };

        _mockMatchRepository.Setup(r => r.GetByTournamentIdAsync(1, false)).ReturnsAsync(matches);

        var result = await _service.ExecuteAsync(new MatchGetModel<int>() { Data = 1, Ended = false });

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        _mockMatchRepository.Verify(r => r.GetByTournamentIdAsync(1, false), Times.Once);
    }

    [Fact]
    public async Task ItShouldReturnEmptyListWhenNoMatchesFoundForGivenTournamentId()
    {
        var matches = new List<Match>();

        _mockMatchRepository.Setup(r => r.GetByTournamentIdAsync(1, false)).ReturnsAsync(matches);

        var result = await _service.ExecuteAsync(new MatchGetModel<int>() { Data = 1, Ended = false });

        Assert.NotNull(result);
        Assert.Empty(result);
        _mockMatchRepository.Verify(r => r.GetByTournamentIdAsync(1, false), Times.Once);
    }
}
