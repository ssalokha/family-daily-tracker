using FamilyTracker.Application.Interfaces;
using MediatR;

namespace FamilyTracker.Application.Commands.Appointments;

public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, Unit>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public DeleteAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<Unit> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
    {
        await _appointmentRepository.DeleteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}
