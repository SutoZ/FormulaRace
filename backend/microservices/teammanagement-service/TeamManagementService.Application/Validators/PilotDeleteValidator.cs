using FluentValidation;
using TeamManagementService.Application.Dtos.Pilots;

namespace TeamManagementService.Application.Validators;

public class PilotDeleteValidator : AbstractValidator<PilotDeleteDto>
{
    public PilotDeleteValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Pilot ID must not be empty.")
            .GreaterThan(0).WithMessage("Pilot ID must be greater than zero.");
    }
}