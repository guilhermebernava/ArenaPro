using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models.MatchValidations;
using ArenaPro.Application.Services.MatchServices;
using ArenaPro.Domain.Entities;
using Moq;
using Match = ArenaPro.Domain.Entities.Match;

namespace ArenaPro.Application.Tests.Services.MatchServices;
public class MatchUpdateServicesTests
{
    private readonly Mock<IMatchRepository> _mockMatchRepository;
    private readonly Mock<ITeamRepository> _mockTeamRepository;
    private readonly Mock<ITournamentRepository> _mockTournamentRepository;
    private readonly MatchUpdateServices _service;

    public MatchUpdateServicesTests()
    {
        _mockMatchRepository = new Mock<IMatchRepository>();
        _mockTeamRepository = new Mock<ITeamRepository>();
        _mockTournamentRepository = new Mock<ITournamentRepository>();
        _service = new MatchUpdateServices(
            _mockMatchRepository.Object,
            _mockTeamRepository.Object,
            _mockTournamentRepository.Object
        );
    }

    [Fact]
    public async Task ItShouldThrowValidationExceptionWhenIdIsLessThanOrEqualToZero()
    {
        var model = new MatchUpdateModel { Id = 0 };

        await Assert.ThrowsAsync<ValidationException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenMatchNotFound()
    {
        var model = new MatchUpdateModel { Id = 1 };
        _mockMatchRepository.Setup(r => r.GetByIdAsync(model.Id)).ReturnsAsync((Match)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldUpdateMatchWhenValidParametersProvided()
    {
        // Arrange
        var tournament = new Tournament("tournament");
        var teamToRemove = new Team("toRemove");
        var teamToAdd = new Team("toAdd");
        var match = new Match(DateTime.Now, tournament, new List<Team>() { teamToRemove });
        var model = new MatchUpdateModel
        {
            Id = 1,
            MatchDate = DateTime.Now,
            Ended = false,
            TeamsToAdd = new List<int> { teamToAdd.Id },
            TeamsToRemove = new List<int> { teamToRemove.Id },
            TournamentId = 1
        };

        _mockMatchRepository.Setup(r => r.GetByIdAsync(model.Id)).ReturnsAsync(match);
        _mockTeamRepository.Setup(r => r.GetByIdAsync(teamToAdd.Id)).ReturnsAsync(teamToAdd);
        _mockTeamRepository.Setup(r => r.GetByIdAsync(teamToRemove.Id)).ReturnsAsync(teamToRemove);
        _mockTournamentRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(tournament);
        _mockMatchRepository.Setup(r => r.SaveAsync()).ReturnsAsync(true);

        var result = await _service.ExecuteAsync(model);

        Assert.True(result);
        _mockMatchRepository.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task ItShouldThrowValidationExceptionWhenAddingMoreThanTwoTeams()
    {
        var match = new Match(DateTime.Now, new Tournament("tournament"), new List<Team>() { new Team("toAdd"), new Team("toAdd") });
        var model = new MatchUpdateModel
        {
            Id = 1,
            MatchDate = DateTime.Now,
            Ended = false,
            TeamsToAdd = new List<int> { 3 },
            TeamsToRemove = new List<int> { },
            TournamentId = 1
        };

        _mockMatchRepository.Setup(r => r.GetByIdAsync(model.Id)).ReturnsAsync(match);

        await Assert.ThrowsAsync<ValidationException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenTournamentNotFound()
    {
        var model = new MatchUpdateModel
        {
            Id = 1,
            TournamentId = 999
        };
        var match = new Match(DateTime.Now, new Tournament("tournament"), new List<Team>() { new Team("toAdd"), new Team("toAdd") });

        _mockMatchRepository.Setup(r => r.GetByIdAsync(model.Id)).ReturnsAsync(match);
        _mockTournamentRepository.Setup(r => r.GetByIdAsync(model.TournamentId.Value)).ReturnsAsync((Tournament)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldThrowValidationExceptionWhenMatchDateIsInvalid()
    {
        var model = new MatchUpdateModel
        {
            Id = 1,
            MatchDate = new DateTime(2000, 1, 1)
        };
        var match = new Match(DateTime.Now, new Tournament("tournament"), new List<Team>() { new Team("toAdd"), new Team("toAdd") });

        _mockMatchRepository.Setup(r => r.GetByIdAsync(model.Id)).ReturnsAsync(match);

        await Assert.ThrowsAsync<ValidationException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldThrowValidationException_WhenTeamToRemoveNotFoundInMatch()
    {
        var model = new MatchUpdateModel
        {
            Id = 1,
            TeamsToRemove = new List<int> { 999 }
        };
        var match = new Match(DateTime.Now, new Tournament("tournament"), new List<Team>() { new Team("toAdd"), new Team("toAdd") });

        _mockMatchRepository.Setup(r => r.GetByIdAsync(model.Id)).ReturnsAsync(match);

        await Assert.ThrowsAsync<ValidationException>(() => _service.ExecuteAsync(model));
    }
}