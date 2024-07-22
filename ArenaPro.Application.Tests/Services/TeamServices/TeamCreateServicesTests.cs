using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models;
using ArenaPro.Application.Models.MatchValidations;
using ArenaPro.Application.Models.TeamModels;
using ArenaPro.Application.Services.TeamServices;
using ArenaPro.Domain.Abstractions;
using ArenaPro.Domain.Entities;
using Moq;

namespace ArenaPro.Application.Tests.Services.TeamServices;
public class TeamCreateServicesTests
{
    private readonly Mock<IPlayerRepository> _mockPlayerRepository;
    private readonly Mock<ITournamentRepository> _mockTournamentRepository;
    private readonly Mock<IMatchRepository> _mockMatchRepository;
    private readonly Mock<ITeamRepository> _mockTeamRepository;
    private readonly TeamCreateServices _service;

    public TeamCreateServicesTests()
    {
        _mockPlayerRepository = new Mock<IPlayerRepository>();
        _mockTournamentRepository = new Mock<ITournamentRepository>();
        _mockMatchRepository = new Mock<IMatchRepository>();
        _mockTeamRepository = new Mock<ITeamRepository>();
        _service = new TeamCreateServices(
            _mockPlayerRepository.Object,
            _mockTournamentRepository.Object,
            _mockMatchRepository.Object,
            _mockTeamRepository.Object
        );
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenTournamentNotFound()
    {
        var model = new TeamModel { Tournaments = new List<TournamentModel> { new TournamentModel { Id = 1 } } };
        _mockTournamentRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Tournament)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenPlayerNotFound()
    {
        var model = new TeamModel { Players = new List<PlayerModel> { new PlayerModel { Id = 1 } } };
        _mockPlayerRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Player)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenMatchNotFound()
    {
        var model = new TeamModel { Matches = new List<MatchModel> { new MatchModel { Id = 1 } } };
        _mockMatchRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Domain.Entities.Match)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldCreateTeamWhenValidParametersProvided()
    {
        var player = new Player("Nick", 1, "Name", 25, "Genre", "test@example.com");
        var tournament = new Tournament("TournamentName");
        var match = new Domain.Entities.Match(DateTime.Now, tournament, new List<Team>());

        var model = new TeamModel
        {
            Name = "TeamName",
            Players = new List<PlayerModel> { new PlayerModel { Id = 1 } },
            Tournaments = new List<TournamentModel> { new TournamentModel { Id = 1 } },
            Matches = new List<MatchModel> { new MatchModel { Id = 1 } },
            Logo = "logo.png"
        };

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(player);
        _mockTournamentRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(tournament);
        _mockMatchRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(match);
        _mockTeamRepository.Setup(r => r.CreateAsync(It.IsAny<Team>())).ReturnsAsync(true);

        var result = await _service.ExecuteAsync(model);

        Assert.True(result);
        _mockTeamRepository.Verify(r => r.CreateAsync(It.Is<Team>(t =>
            t.Name == model.Name &&
            t.Logo == model.Logo &&
            t.Players.Contains(player) &&
            t.Tournaments.Contains(tournament) &&
            t.Matches.Contains(match)
        )), Times.Once);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenSaveFails()
    {
        var player = new Player("Nick", 1, "Name", 25, "Genre", "test@example.com");
        var tournament = new Tournament("TournamentName");
        var match = new Domain.Entities.Match(DateTime.Now, tournament, new List<Team>());

        var model = new TeamModel
        {
            Name = "TeamName",
            Players = new List<PlayerModel> { new PlayerModel { Id = 1 } },
            Tournaments = new List<TournamentModel> { new TournamentModel { Id = 1 } },
            Matches = new List<MatchModel> { new MatchModel { Id = 1 } },
            Logo = "logo.png"
        };

        _mockPlayerRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(player);
        _mockTournamentRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(tournament);
        _mockMatchRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(match);
        _mockTeamRepository.Setup(r => r.CreateAsync(It.IsAny<Team>())).ReturnsAsync(false);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(model));
    }
}