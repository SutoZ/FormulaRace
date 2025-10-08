using FluentValidation;
using TeamManagementService.Application.Dtos.Pilots;

namespace TeamManagementService.Application.Validators;

public class PilotGetAllValidator : AbstractValidator<PilotFilterDto>
{
    public PilotGetAllValidator()
    {
        // Remove Id validation for GetAll - it should be optional for filtering
        // When filtering by Id, it should be positive if provided
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Pilot ID must be greater than zero.")
            .When(x => x.Id.HasValue);
    }
}