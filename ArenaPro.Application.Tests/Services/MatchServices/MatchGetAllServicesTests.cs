using ArenaPro.Application.Services.MatchServices;
using ArenaPro.Domain.Entities;
using Moq;
using Match = ArenaPro.Domain.Entities.Match;

namespace ArenaPro.Application.Tests.Services.MatchServices;
public class MatchGetAllServicesTests
{
    private readonly Mock<IMatchRepository> _mockMatchRepository;
    private readonly MatchGetAllServices _service;

    public MatchGetAllServicesTests()
    {
        _mockMatchRepository = new Mock<IMatchRepository>();
        _service = new MatchGetAllServices(_mockMatchRepository.Object);
    }

    [Fact]
    public async Task ItShouldReturnAllMatches()
    {
        var matches = new List<Match>
            {
                new Match(DateTime.Now, new Tournament("tournament"), new List<Team>()),
                new Match(DateTime.Now, new Tournament("tournament"), new List<Team>())
            };
        _mockMatchRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(matches);

        var result = await _service.ExecuteAsync();

        Assert.NotNull(result);
        Assert.Equal(matches.Count, result.Count);
        Assert.Equal(matches, result);
        _mockMatchRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task ItShouldReturnEmptyListWhenNoMatchesFound()
    {
        var matches = new List<Match>();
        _mockMatchRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(matches);

        var result = await _service.ExecuteAsync();

        Assert.NotNull(result);
        Assert.Empty(result);
        _mockMatchRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }
}