using MediatR;

namespace FamilyTracker.Application.Commands.Appointments;

public class DeleteAppointmentCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
