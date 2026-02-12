using FluentValidation;

namespace FamilyTracker.Application.Commands.Appointments;

public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(x => x.CreatedByUserId)
            .NotEmpty().WithMessage("Created by user ID is required");

        RuleFor(x => x.AppointmentForUserId)
            .NotEmpty().WithMessage("Appointment for user ID is required");

        RuleFor(x => x.AppointmentDateTime)
            .NotEmpty().WithMessage("Appointment date and time is required")
            .GreaterThan(DateTime.Now).WithMessage("Appointment must be in the future");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street is required")
            .MaximumLength(200).WithMessage("Street cannot exceed 200 characters");

        RuleFor(x => x.BuildingNumber)
            .NotEmpty().WithMessage("Building number is required")
            .MaximumLength(20).WithMessage("Building number cannot exceed 20 characters");
    }
}
