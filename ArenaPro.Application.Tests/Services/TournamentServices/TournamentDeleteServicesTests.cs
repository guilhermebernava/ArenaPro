using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Services.TournamentServices;
using ArenaPro.Domain.Entities;
using Moq;

namespace ArenaPro.Application.Tests.Services.TournamentServices;
public class TournamentDeleteServicesTests
{
    private readonly Mock<ITournamentRepository> _mockTournamentRepository;
    private readonly TournamentDeleteServices _service;

    public TournamentDeleteServicesTests()
    {
        _mockTournamentRepository = new Mock<ITournamentRepository>();
        _service = new TournamentDeleteServices(_mockTournamentRepository.Object);
    }

    [Fact]
    public async Task ItShouldThrowValidationExceptionWhenIdIsInvalid()
    {
        await Assert.ThrowsAsync<ValidationException>(() => _service.ExecuteAsync(0));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenTournamentNotFound()
    {
        _mockTournamentRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync((Tournament)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(1));
    }

    [Fact]
    public async Task ItShouldDeleteTournamentSuccessfully()
    {
        var tournament = new Tournament("Tournament", 1000, new List<Team>(), new List<Domain.Entities.Match>());
        _mockTournamentRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(tournament);
        _mockTournamentRepository.Setup(r => r.DeleteAsync(tournament))
            .ReturnsAsync(true);

        var result = await _service.ExecuteAsync(1);

        Assert.True(result);
        _mockTournamentRepository.Verify(r => r.DeleteAsync(tournament), Times.Once);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenDeletingFails()
    {
        var tournament = new Tournament("Tournament", 1000, new List<Team>(), new List<Domain.Entities.Match>());
        _mockTournamentRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(tournament);
        _mockTournamentRepository.Setup(r => r.DeleteAsync(tournament))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(1));
    }
}