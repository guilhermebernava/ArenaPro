using ArenaPro.Application.Models.MatchValidations;
using FluentValidation;

namespace ArenaPro.Application.Validations.MatchModels;
public class MatchModelValidator : AbstractValidator<MatchModel>
{
    public MatchModelValidator()
    {
        RuleFor(_ => _.Id).LessThan(1).When(_ => _.Id.HasValue).WithMessage("Id must be greater than 1");
        RuleFor(_ => _.Teams).Must(_ => _.Count == 2).WithMessage("Can olny have 2 Teams in a MATCH");
        RuleFor(_ => _.TournamentId).LessThan(1).WithMessage("TournamentId must be greater than 1");
    }
}
