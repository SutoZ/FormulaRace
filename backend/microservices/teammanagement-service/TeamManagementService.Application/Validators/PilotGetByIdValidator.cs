using FluentValidation;
using TeamManagementService.Application.Dtos.Pilots;

namespace TeamManagementService.Application.Validators;

public class PilotGetByIdValidator : AbstractValidator<PilotFilterDto>
{
    public PilotGetByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Pilot ID must not be empty.")
            .GreaterThanOrEqualTo(0).WithMessage("Pilot ID must be greater than or equal to zero.");
    }
}