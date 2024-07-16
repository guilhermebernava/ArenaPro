using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Services.MatchServices;
using ArenaPro.Domain.Entities;
using Moq;
using Match = ArenaPro.Domain.Entities.Match;

namespace ArenaPro.Application.Tests.Services.MatchServices;
public class MatchDeleteServicesTests
{
    private readonly Mock<IMatchRepository> _mockMatchRepository;
    private readonly MatchDeleteServices _service;

    public MatchDeleteServicesTests()
    {
        _mockMatchRepository = new Mock<IMatchRepository>();
        _service = new MatchDeleteServices(_mockMatchRepository.Object);
    }

    [Fact]
    public async Task ItShouldThrowValidationExceptionWhenParameterIsLessThanOne()
    {
        var invalidParameter = 0;

        await Assert.ThrowsAsync<ValidationException>(() => _service.ExecuteAsync(invalidParameter));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenMatchNotFound()
    {
        var validParameter = 1;
        _mockMatchRepository.Setup(r => r.GetByIdAsync(validParameter)).ReturnsAsync((Match)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(validParameter));
    }

    [Fact]
    public async Task ItShouldDeleteMatchWhenValidParameterProvided()
    {
        var validParameter = 1;
        var match = new Match(DateTime.Now, new Tournament("tournament"), new List<Team>());
        _mockMatchRepository.Setup(r => r.GetByIdAsync(validParameter)).ReturnsAsync(match);
        _mockMatchRepository.Setup(r => r.DeleteAsync(match)).ReturnsAsync(true);

        var result = await _service.ExecuteAsync(validParameter);

        Assert.True(result);
        _mockMatchRepository.Verify(r => r.DeleteAsync(match), Times.Once);
    }
}