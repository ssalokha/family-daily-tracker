using FamilyTracker.Application.DTOs;
using MediatR;

namespace FamilyTracker.Application.Commands.Appointments;

public class CreateAppointmentCommand : IRequest<DoctorAppointmentDto>
{
    public Guid CreatedByUserId { get; set; }
    public Guid AppointmentForUserId { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public string Street { get; set; } = string.Empty;
    public string BuildingNumber { get; set; } = string.Empty;
}
