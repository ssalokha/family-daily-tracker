using FamilyTracker.Application.DTOs;
using MediatR;

namespace FamilyTracker.Application.Queries.Appointments;

public class GetAppointmentHistoryQuery : IRequest<IEnumerable<DoctorAppointmentDto>>
{
    public string? UserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool? IsCompleted { get; set; }
}
