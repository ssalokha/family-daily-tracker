using FluentValidation;

namespace FamilyTracker.Application.Commands.Shopping;

public class CreateShoppingItemCommandValidator : AbstractValidator<CreateShoppingItemCommand>
{
    public CreateShoppingItemCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Item name is required")
            .MaximumLength(100).WithMessage("Item name cannot exceed 100 characters");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0");

        RuleFor(x => x.CreatedByUserId)
            .NotEmpty().WithMessage("Created by user ID is required");
    }
}
