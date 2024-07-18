using ArenaPro.Application.Services.MatchServices;
using ArenaPro.Domain.Entities;
using Moq;
using Match = ArenaPro.Domain.Entities.Match;


namespace ArenaPro.Application.Tests.Services.MatchServices;
public class MatchGetByIdServicesTests
{
    private readonly Mock<IMatchRepository> _mockMatchRepository;
    private readonly MatchGetByIdServices _service;

    public MatchGetByIdServicesTests()
    {
        _mockMatchRepository = new Mock<IMatchRepository>();
        _service = new MatchGetByIdServices(_mockMatchRepository.Object);
    }

    [Fact]
    public async Task ItShouldReturnMatchesForGivenId()
    {
        var match = new Match(DateTime.Now, new Tournament("Tournament2"), new List<Team> { new Team("Team3"), new Team("Team4") });

        _mockMatchRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(match);

        var result = await _service.ExecuteAsync(1);

        Assert.NotNull(result);
        _mockMatchRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task ItShouldReturnEmptyListWhenNoMatchesFoundForGivenId()
    {
        var match = new Match(DateTime.Now, new Tournament("Tournament2"), new List<Team> { new Team("Team3"), new Team("Team4") });

        _mockMatchRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(match);

        var result = await _service.ExecuteAsync(1);

        Assert.NotNull(result);
        _mockMatchRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
    }
}