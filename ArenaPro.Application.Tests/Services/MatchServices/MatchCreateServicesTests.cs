using ArenaPro.Application.Models.MatchValidations;
using ArenaPro.Application.Models.TeamModels;
using ArenaPro.Application.Services.MatchServices;
using ArenaPro.Domain.Entities;
using Moq;
using Match = ArenaPro.Domain.Entities.Match;


namespace ArenaPro.Application.Tests.Services.MatchServices;
public class MatchCreateServicesTests
{
    private readonly Mock<IMatchRepository> _mockMatchRepository;
    private readonly Mock<ITournamentRepository> _mockTournamentRepository;
    private readonly Mock<ITeamRepository> _mockTeamRepository;
    private readonly MatchCreateServices _service;

    public MatchCreateServicesTests()
    {
        _mockMatchRepository = new Mock<IMatchRepository>();
        _mockTournamentRepository = new Mock<ITournamentRepository>();
        _mockTeamRepository = new Mock<ITeamRepository>();

        _service = new MatchCreateServices(
            _mockMatchRepository.Object,
            _mockTournamentRepository.Object,
            _mockTeamRepository.Object
        );
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenTournamentNotFound()
    {
        var model = new MatchModel() { TournamentId = -1 };
        _mockTournamentRepository.Setup(r => r.GetByIdAsync(model.TournamentId)).ReturnsAsync(null as Tournament);

        await Assert.ThrowsAsync<Exceptions.RepositoryException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldCreateMatchWhenValidParametersProvided()
    {
        var model = new MatchModel
        {
            TournamentId = 1,
            MatchDate = DateTime.UtcNow,
            Teams = [new TeamModel { Id = 1 }]
        };
        var tournament = new Tournament("tournament");
        var team = new Team("team");
        _mockTournamentRepository.Setup(r => r.GetByIdAsync(model.TournamentId)).ReturnsAsync(tournament);
        _mockTeamRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(team);
        _mockMatchRepository.Setup(r => r.CreateAsync(It.IsAny<Match>())).ReturnsAsync(true);

        var result = await _service.ExecuteAsync(model);

        Assert.True(result);
        _mockMatchRepository.Verify(r => r.CreateAsync(It.IsAny<Match>()), Times.Once);
    }
}
