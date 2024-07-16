using ArenaPro.Application.Services.MatchServices;
using ArenaPro.Domain.Entities;
using Moq;
using Match = ArenaPro.Domain.Entities.Match;

namespace ArenaPro.Application.Tests.Services.MatchServices;
public class MatchGetByDateServicesTests
{
    private readonly Mock<IMatchRepository> _mockMatchRepository;
    private readonly MatchGetByDateServices _service;

    public MatchGetByDateServicesTests()
    {
        _mockMatchRepository = new Mock<IMatchRepository>();
        _service = new MatchGetByDateServices(_mockMatchRepository.Object);
    }
    //TODO resolver problemas com esses TESTES

    //[Fact]
    //public async Task ItShouldReturnMatchesForGivenDate()
    //{
    //    // Arrange
    //    var date = new DateTime(2023, 7, 16);
    //    var matches = new List<Match>
    //        {
    //            new Match(date, new Tournament("Tournament1"), new List<Team> { new Team("Team1"), new Team("Team2") }),
    //            new Match(date, new Tournament("Tournament2"), new List<Team> { new Team("Team3"), new Team("Team4") })
    //        };

    //    _mockMatchRepository.Setup(r => r.GetByDateAsync(date, false)).ReturnsAsync(matches);

    //    // Act
    //    var result = await _service.ExecuteAsync(date);

    //    // Assert
    //    Assert.NotNull(result);
    //    Assert.Equal(2, result.Count);
    //    _mockMatchRepository.Verify(r => r.GetByDateAsync(date, false), Times.Once);
    //}

    //[Fact]
    //public async Task ItShouldReturnEmptyListWhenNoMatchesFoundForGivenDate()
    //{
    //    // Arrange
    //    var date = new DateTime(2023, 7, 16);
    //    var matches = new List<Match>();

    //    _mockMatchRepository.Setup(r => r.GetByDateAsync(date, false)).ReturnsAsync(matches);

    //    // Act
    //    var result = await _service.ExecuteAsync(date);

    //    // Assert
    //    Assert.NotNull(result);
    //    Assert.Empty(result);
    //    _mockMatchRepository.Verify(r => r.GetByDateAsync(date, false), Times.Once);
    //}
}
