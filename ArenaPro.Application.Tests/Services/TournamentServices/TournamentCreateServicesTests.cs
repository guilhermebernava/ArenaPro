using ArenaPro.Application.Exceptions;
using ArenaPro.Application.Models.MatchValidations;
using ArenaPro.Application.Models.TeamModels;
using ArenaPro.Application.Models.TournamentModels;
using ArenaPro.Application.Services.TournamentServices;
using ArenaPro.Domain.Entities;
using Moq;
using Match = ArenaPro.Domain.Entities.Match;

namespace ArenaPro.Application.Tests.Services.TournamentServices;
public class TournamentCreateServicesTests
{
    private readonly Mock<ITeamRepository> _mockTeamRepository;
    private readonly Mock<ITournamentRepository> _mockTournamentRepository;
    private readonly Mock<IMatchRepository> _mockMatchRepository;
    private readonly TournamentCreateServices _service;

    public TournamentCreateServicesTests()
    {
        _mockTeamRepository = new Mock<ITeamRepository>();
        _mockTournamentRepository = new Mock<ITournamentRepository>();
        _mockMatchRepository = new Mock<IMatchRepository>();
        _service = new TournamentCreateServices(
            _mockTeamRepository.Object,
            _mockTournamentRepository.Object,
            _mockMatchRepository.Object);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenMatchNotFound()
    {
        var parameter = new TournamentModel
        {
            Name = "Tournament",
            Matches = new List<MatchModel> { new MatchModel { Id = 1 } }
        };
        _mockMatchRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync((Match)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(parameter));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenTeamNotFound()
    {
        var parameter = new TournamentModel
        {
            Name = "Tournament",
            Teams = new List<TeamModel> { new TeamModel { Id = 1 } }
        };
        _mockTeamRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync((Team)null);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(parameter));
    }

    [Fact]
    public async Task ItShouldCreateTournamentSuccessfully()
    {
        var parameter = new TournamentModel
        {
            Name = "Tournament",
            Prize = 1000,
            Matches = new List<MatchModel> { },
            Teams = new List<TeamModel> { new TeamModel { Id = 1 } }
        };
        var team = new Team("TeamName");
        _mockTeamRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(team);
        _mockTournamentRepository.Setup(r => r.CreateAsync(It.IsAny<Tournament>()))
            .ReturnsAsync(true);

        var result = await _service.ExecuteAsync(parameter);

        Assert.True(result);
        _mockTournamentRepository.Verify(r => r.CreateAsync(It.IsAny<Tournament>()), Times.Once);
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenSavingFails()
    {
        var parameter = new TournamentModel
        {
            Name = "Tournament",
            Prize = 1000,
            Matches = new List<MatchModel> {  },
            Teams = new List<TeamModel> { new TeamModel { Id = 1 } }
        };
        var team = new Team("TeamName");
        _mockTeamRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(team);
        _mockTournamentRepository.Setup(r => r.CreateAsync(It.IsAny<Tournament>()))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<RepositoryException>(() => _service.ExecuteAsync(parameter));
    }

    [Fact]
    public async Task ItShouldHandleMultipleMatchesAndTeams()
    {
        var parameter = new TournamentModel
        {
            Name = "Tournament",
            Prize = 1000,
            Matches = new List<MatchModel>
                {
                    new MatchModel { Id = 1 },
                    new MatchModel { Id = 2 }
                },
            Teams = new List<TeamModel>
                {
                    new TeamModel { Id = 1 },
                    new TeamModel { Id = 2 }
                }
        };
        var match1 = new Match(DateTime.Now, new Tournament("12345"), new List<Team>());
        var match2 = new Match(DateTime.Now, new Tournament("12345"), new List<Team>());
        var team1 = new Team("Team1");
        var team2 = new Team("Team2");
        _mockMatchRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(match1);
        _mockMatchRepository.Setup(r => r.GetByIdAsync(2))
            .ReturnsAsync(match2);
        _mockTeamRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(team1);
        _mockTeamRepository.Setup(r => r.GetByIdAsync(2))
            .ReturnsAsync(team2);
        _mockTournamentRepository.Setup(r => r.CreateAsync(It.IsAny<Tournament>()))
            .ReturnsAsync(true);

        var result = await _service.ExecuteAsync(parameter);

        Assert.True(result);
        _mockTournamentRepository.Verify(r => r.CreateAsync(It.IsAny<Tournament>()), Times.Once);
    }
}