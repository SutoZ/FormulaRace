using FluentValidation;
using TeamManagementService.Application.CQRS.Pilots.Queries;

namespace TeamManagementService.Application.Validators;

public class PilotGetByIdValidator : AbstractValidator<GetPilotByIdQuery>
{
    public PilotGetByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Pilot ID must not be empty.")
            .GreaterThanOrEqualTo(1).WithMessage("Pilot ID must be greater than zero.");
    }
}