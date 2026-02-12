using FamilyTracker.Application.DTOs;
using MediatR;

namespace FamilyTracker.Application.Queries.Appointments;

public class GetUpcomingAppointmentsQuery : IRequest<IEnumerable<DoctorAppointmentDto>>
{
    public int DaysAhead { get; set; } = 14;
}
