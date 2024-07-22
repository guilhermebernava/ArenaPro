using ArenaPro.Application.Models.MatchValidations;
using ArenaPro.Application.Models.TeamModels;
using ArenaPro.Application.Services.MatchServices;
using ArenaPro.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Match = ArenaPro.Domain.Entities.Match;


namespace ArenaPro.Application.Tests.Services.MatchServices;
public class MatchCreateServicesTests
{
    private readonly Mock<IMatchRepository> _mockMatchRepository;
    private readonly Mock<ITournamentRepository> _mockTournamentRepository;
    private readonly Mock<IValidator<MatchModel>> _mockValidator;
    private readonly Mock<ITeamRepository> _mockTeamRepository;
    private readonly MatchCreateServices _service;

    public MatchCreateServicesTests()
    {
        _mockMatchRepository = new Mock<IMatchRepository>();
        _mockTournamentRepository = new Mock<ITournamentRepository>();
        _mockValidator = new Mock<IValidator<MatchModel>>();
        _mockTeamRepository = new Mock<ITeamRepository>();

        _service = new MatchCreateServices(
            _mockMatchRepository.Object,
            _mockTournamentRepository.Object,
            _mockValidator.Object,
            _mockTeamRepository.Object
        );
    }

    [Fact]
    public async Task ItShouldThrowValidationExceptionWhenValidationFails()
    {
        var model = new MatchModel();
        var validationResult = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Test", "Validation error") });
        _mockValidator.Setup(v => v.Validate(model)).Returns(validationResult);

        await Assert.ThrowsAsync<ValidationException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldThrowRepositoryExceptionWhenTournamentNotFound()
    {
        var model = new MatchModel() { TournamentId = -1};
        var validationResult = new ValidationResult();
        _mockValidator.Setup(v => v.Validate(model)).Returns(validationResult);
        _mockTournamentRepository.Setup(r => r.GetByIdAsync(model.TournamentId)).ReturnsAsync((Tournament)null);

        await Assert.ThrowsAsync<Exceptions.RepositoryException>(() => _service.ExecuteAsync(model));
    }

    [Fact]
    public async Task ItShouldCreateMatchWhenValidParametersProvided()
    {
        var model = new MatchModel
        {
            TournamentId = 1,
            MatchDate = DateTime.UtcNow,
            Teams = new List<TeamModel> { new TeamModel { Id = 1 } }
        };
        var tournament = new Tournament("tournament");
        var team = new Team("team");
        var validationResult = new ValidationResult();
        _mockValidator.Setup(v => v.Validate(model)).Returns(validationResult);
        _mockTournamentRepository.Setup(r => r.GetByIdAsync(model.TournamentId)).ReturnsAsync(tournament);
        _mockTeamRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(team);
        _mockMatchRepository.Setup(r => r.CreateAsync(It.IsAny<Match>())).ReturnsAsync(true);

        var result = await _service.ExecuteAsync(model);

        Assert.True(result);
        _mockMatchRepository.Verify(r => r.CreateAsync(It.IsAny<Match>()), Times.Once);
    }
}
