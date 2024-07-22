using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models.TeamModels;
using ArenaPro.Application.Services.TeamServices;
using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using Moq;
using Match = ArenaPro.Domain.Entities.Match;

namespace ArenaPro.Application.Tests.Services.TeamServices;
public class TeamUpdateServicesTests
{
    private readonly Mock<ITeamRepository> _mockTeamRepository;
    private readonly Mock<IPlayerRepository> _mockPlayerRepository;
    private readonly Mock<ITournamentRepository> _mockTournamentRepository;
    private readonly Mock<IMatchRepository> _mockMatchRepository;
    private readonly TeamUpdateServices _service;

    public TeamUpdateServicesTests()
    {
        _mockTeamRepository = new Mock<ITeamRepository>();
        _mockPlayerRepository = new Mock<IPlayerRepository>();
        _mockTournamentRepository = new Mock<ITournamentRepository>();
        _mockMatchRepository = new Mock<IMatchRepository>();
        _service = new TeamUpdateServices(
            _mockTeamRepository.Object,
            _mockPlayerRepository.Object,
            _mockTournamentRepository.Object,
            _mockMatchRepository.Object);
    }

    [Fact]
    public async Task ItShouldThrowValidationExceptionWhenTeamIdIsInvalid()
    {
        var parameter = new TeamUpdateModel { Id = 0 };

        await Assert.ThrowsAsync<ValidationException>(() => _service.ExecuteAsync(parameter));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenTeamNotFound()
    {
        var parameter = new TeamUpdateModel { Id = 1 };
        _mockTeamRepository.Setup(r => r.GetByIdAsync(parameter.Id))
            .ReturnsAsync((Team)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(parameter));
    }

    [Fact]
    public async Task ItShouldUpdateTeamNameAndSave()
    {
        var team = new Team("OldName");
        var parameter = new TeamUpdateModel { Id = 1, Name = "NewName" };
        _mockTeamRepository.Setup(r => r.GetByIdAsync(parameter.Id))
            .ReturnsAsync(team);
        _mockTeamRepository.Setup(r => r.UpdateAsync(team))
            .ReturnsAsync(true);

        var result = await _service.ExecuteAsync(parameter);

        Assert.True(result);
        Assert.Equal("NewName", team.Name);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenPlayerToAddNotFound()
    {
        var team = new Team("TeamName");
        var parameter = new TeamUpdateModel { Id = 1, PlayersToAdd = new List<int> { 1 } };
        _mockTeamRepository.Setup(r => r.GetByIdAsync(parameter.Id))
            .ReturnsAsync(team);
        _mockPlayerRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync((Player)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(parameter));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenPlayerToRemoveNotFound()
    {
        var team = new Team("TeamName");
        var parameter = new TeamUpdateModel { Id = 1, PlayersToRemove = new List<int> { 1 } };
        _mockTeamRepository.Setup(r => r.GetByIdAsync(parameter.Id))
            .ReturnsAsync(team);
        _mockPlayerRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync((Player)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(parameter));
    }

    [Fact]
    public async Task ItShouldAddAndRemovePlayers()
    {
        var team = new Team("TeamName");
        var playerToAdd = new Player("PlayerToAdd");
        var playerToRemove = new Player("PlayerToRemove");
        team.AddPlayer(playerToRemove);
        var parameter = new TeamUpdateModel
        {
            Id = 1,
            PlayersToAdd = new List<int> { 1 },
            PlayersToRemove = new List<int> { 2 }
        };
        _mockTeamRepository.Setup(r => r.GetByIdAsync(parameter.Id))
            .ReturnsAsync(team);
        _mockPlayerRepository.Setup(r => r.GetByIdAsync(2))
            .ReturnsAsync(playerToRemove);
        _mockPlayerRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(playerToAdd);
        _mockTeamRepository.Setup(r => r.UpdateAsync(team))
            .ReturnsAsync(true);

        var result = await _service.ExecuteAsync(parameter);

        Assert.True(result);
        Assert.Contains(playerToAdd, team.Players);
        Assert.DoesNotContain(playerToRemove, team.Players);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenTournamentToAddNotFound()
    {
        var team = new Team("TeamName");
        var parameter = new TeamUpdateModel { Id = 1, TournamentsToAdd = new List<int> { 1 } };
        _mockTeamRepository.Setup(r => r.GetByIdAsync(parameter.Id))
            .ReturnsAsync(team);
        _mockTournamentRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync((Tournament)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(parameter));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenTournamentToRemoveNotFound()
    {
        var team = new Team("TeamName");
        var parameter = new TeamUpdateModel { Id = 1, TournamentsToRemove = new List<int> { 1 } };
        _mockTeamRepository.Setup(r => r.GetByIdAsync(parameter.Id))
            .ReturnsAsync(team);
        _mockTournamentRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync((Tournament)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(parameter));
    }

    [Fact]
    public async Task ItShouldAddAndRemoveTournaments()
    {
        var team = new Team("TeamName");
        var tournamentToAdd = new Tournament("TournamentToAdd");
        var tournamentToRemove = new Tournament("TournamentToRemove");
        team.AddTournament(tournamentToRemove);
        var parameter = new TeamUpdateModel
        {
            Id = 1,
            TournamentsToAdd = new List<int> { 1 },
            TournamentsToRemove = new List<int> { 2 }
        };
        _mockTeamRepository.Setup(r => r.GetByIdAsync(parameter.Id))
            .ReturnsAsync(team);
        _mockTournamentRepository.Setup(r => r.GetByIdAsync(2))
            .ReturnsAsync(tournamentToRemove);
        _mockTournamentRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(tournamentToAdd);
        _mockTeamRepository.Setup(r => r.UpdateAsync(team))
            .ReturnsAsync(true);

        var result = await _service.ExecuteAsync(parameter);

        Assert.True(result);
        Assert.Contains(tournamentToAdd, team.Tournaments);
        Assert.DoesNotContain(tournamentToRemove, team.Tournaments);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenMatchToAddNotFound()
    {
        var team = new Team("TeamName");
        var parameter = new TeamUpdateModel { Id = 1, MatchesToAdd = new List<int> { 1 } };
        _mockTeamRepository.Setup(r => r.GetByIdAsync(parameter.Id))
            .ReturnsAsync(team);
        _mockMatchRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync((Match)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(parameter));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenMatchToRemoveNotFound()
    {
        var team = new Team("TeamName");
        var parameter = new TeamUpdateModel { Id = 1, MatchesToRemove = new List<int> { 1 } };
        _mockTeamRepository.Setup(r => r.GetByIdAsync(parameter.Id))
            .ReturnsAsync(team);
        _mockMatchRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync((Match)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(parameter));
    }

    [Fact]
    public async Task ItShouldAddAndRemoveMatches()
    {
        var team = new Team("TeamName");
        var matchToAdd = new Match(DateTime.Now, new Tournament("12345"), new List<Team>());
        var matchToRemove = new Match(DateTime.Now, new Tournament("12345"), new List<Team>());
        team.AddMatch(matchToRemove);
        var parameter = new TeamUpdateModel
        {
            Id = 1,
            MatchesToAdd = new List<int> { 2 },
            MatchesToRemove = new List<int> { 1 }
        };
        _mockTeamRepository.Setup(r => r.GetByIdAsync(parameter.Id))
            .ReturnsAsync(team);
        _mockMatchRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(matchToRemove);
        _mockMatchRepository.Setup(r => r.GetByIdAsync(2))
            .ReturnsAsync(matchToAdd);
        _mockTeamRepository.Setup(r => r.UpdateAsync(team))
            .ReturnsAsync(true);

        var result = await _service.ExecuteAsync(parameter);

        Assert.True(result);
        Assert.Contains(matchToAdd, team.Matches);
        Assert.DoesNotContain(matchToRemove, team.Matches);
    }
}