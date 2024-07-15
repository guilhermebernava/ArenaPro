using ArenaPro.Application.Models.MatchValidations;
using FluentValidation;

namespace ArenaPro.Application.Validations.TeamModels;
public class TeamModelValidator : AbstractValidator<TeamModel>
{
    public TeamModelValidator()
    {
        RuleFor(_ => _.Id).LessThan(1).When(_ => _.Id.HasValue).WithMessage("Id must be greater than 1");
    }
}
