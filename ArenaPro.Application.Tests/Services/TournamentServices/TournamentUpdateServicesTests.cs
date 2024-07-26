using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models.TournamentModels;
using ArenaPro.Application.Services.TournamentServices;
using ArenaPro.Domain.Entities;
using Moq;
using Match = ArenaPro.Domain.Entities.Match;

namespace ArenaPro.Application.Tests.Services.TournamentServices;
public class TournamentUpdateServicesTests
{
    private readonly Mock<ITeamRepository> _mockTeamRepository;
    private readonly Mock<ITournamentRepository> _mockTournamentRepository;
    private readonly Mock<IMatchRepository> _mockMatchRepository;
    private readonly TournamentUpdateServices _service;

    public TournamentUpdateServicesTests()
    {
        _mockTeamRepository = new Mock<ITeamRepository>();
        _mockTournamentRepository = new Mock<ITournamentRepository>();
        _mockMatchRepository = new Mock<IMatchRepository>();
        _service = new TournamentUpdateServices(_mockTeamRepository.Object, _mockTournamentRepository.Object, _mockMatchRepository.Object);
    }

    [Fact]
    public async Task ItShouldThrowValidationExceptionIfIdIsInvalid()
    {
        var invalidModel = new TournamentUpdateModel { Id = 0 };
        await Assert.ThrowsAsync<ValidationException>(() => _service.ExecuteAsync(invalidModel));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionIfTournamentNotFound()
    {
        var model = new TournamentUpdateModel { Id = 1 };
        _mockTournamentRepository.Setup(r => r.GetByIdAsync(model.Id)).ReturnsAsync((Tournament)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldUpdateTournamentSuccessfully()
    {
        var tournament = new Tournament("Old Name", 500, new List<Team>(), new List<Match>());
        var model = new TournamentUpdateModel
        {
            Id = 1,
            Name = "New Name",
            Prize = 1000,
            Ended = true
        };

        _mockTournamentRepository.Setup(r => r.GetByIdAsync(model.Id)).ReturnsAsync(tournament);
        _mockTournamentRepository.Setup(r => r.UpdateAsync(tournament)).ReturnsAsync(true);

        var result = await _service.ExecuteAsync(model);

        Assert.True(result);
        Assert.Equal("New Name", tournament.Name);
        Assert.Equal(1000, tournament.Prize);
        Assert.True(tournament.Ended);
    }

    [Fact]
    public async Task ItShouldUpdateMatchesSuccessfully()
    {
        var tournament = new Tournament("Tournament", 500, new List<Team>(), new List<Match>());
        var model = new TournamentUpdateModel
        {
            Id = 1,
            MatchesToAdd = new List<int> { 1 },
            MatchesToRemove = new List<int> { 2 }
        };

        var matchToAdd = new Match(DateTime.Now, tournament, new List<Team>());
        var matchToRemove = new Match(DateTime.Now, tournament, new List<Team>());

        _mockTournamentRepository.Setup(r => r.GetByIdAsync(model.Id)).ReturnsAsync(tournament);
        _mockMatchRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(matchToAdd);
        _mockMatchRepository.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(matchToRemove);
        _mockTournamentRepository.Setup(r => r.UpdateAsync(tournament)).ReturnsAsync(true);

        tournament.AddMatch(matchToRemove);

        var result = await _service.ExecuteAsync(model);

        Assert.True(result);
        Assert.Contains(matchToAdd, tournament.Matches);
        Assert.DoesNotContain(matchToRemove, tournament.Matches);
    }

    [Fact]
    public async Task ItShouldUpdateTeamsSuccessfully()
    {
        // Arrange
        var tournament = new Tournament("Tournament", 500, new List<Team>(), new List<Match>());
        var model = new TournamentUpdateModel
        {
            Id = 1,
            TeamsToAdd = new List<int> { 1 },
            TeamsToRemove = new List<int> { 2 }
        };

        var teamToAdd = new Team("team123");
        var teamToRemove = new Team("team321");

        _mockTournamentRepository.Setup(r => r.GetByIdAsync(model.Id)).ReturnsAsync(tournament);
        _mockTeamRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(teamToAdd);
        _mockTeamRepository.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(teamToRemove);
        _mockTournamentRepository.Setup(r => r.UpdateAsync(tournament)).ReturnsAsync(true);

        tournament.AddTeam(teamToRemove);

        var result = await _service.ExecuteAsync(model);

        Assert.True(result);
        Assert.Contains(teamToAdd, tournament.Teams);
        Assert.DoesNotContain(teamToRemove, tournament.Teams);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionIfMatchToAddNotFound()
    {
        var tournament = new Tournament("Tournament", 500, new List<Team>(), new List<Match>());
        var model = new TournamentUpdateModel
        {
            Id = 1,
            MatchesToAdd = new List<int> { 1 }
        };

        _mockTournamentRepository.Setup(r => r.GetByIdAsync(model.Id)).ReturnsAsync(tournament);
        _mockMatchRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Match)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionIfTeamToAddNotFound()
    {
        var tournament = new Tournament("Tournament", 500, new List<Team>(), new List<Match>());
        var model = new TournamentUpdateModel
        {
            Id = 1,
            TeamsToAdd = new List<int> { 1 }
        };

        _mockTournamentRepository.Setup(r => r.GetByIdAsync(model.Id)).ReturnsAsync(tournament);
        _mockTeamRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Team)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }
}