using FamilyTracker.Application.DTOs;
using FamilyTracker.Application.Interfaces;
using MediatR;

namespace FamilyTracker.Application.Queries.Appointments;

public class GetUpcomingAppointmentsQueryHandler : IRequestHandler<GetUpcomingAppointmentsQuery, IEnumerable<DoctorAppointmentDto>>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUserRepository _userRepository;

    public GetUpcomingAppointmentsQueryHandler(
        IAppointmentRepository appointmentRepository,
        IUserRepository userRepository)
    {
        _appointmentRepository = appointmentRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<DoctorAppointmentDto>> Handle(GetUpcomingAppointmentsQuery request, CancellationToken cancellationToken)
    {
        var appointments = await _appointmentRepository.GetUpcomingAsync(request.DaysAhead, cancellationToken);
        var users = await _userRepository.GetAllAsync(cancellationToken);
        var userDict = users.ToDictionary(u => u.Id, u => u.UserName);

        return appointments.Select(a => new DoctorAppointmentDto
        {
            Id = a.Id,
            CreatedByUserId = a.CreatedByUserId,
            CreatedByUserName = userDict.GetValueOrDefault(a.CreatedByUserId) ?? "",
            AppointmentForUserId = a.AppointmentForUserId,
            AppointmentForUserName = userDict.GetValueOrDefault(a.AppointmentForUserId) ?? "",
            AppointmentDateTime = a.AppointmentDateTime,
            Location = a.Location.ToString(),
            IsCompleted = a.IsCompleted,
            CreatedAt = a.CreatedAt,
            UpdatedAt = a.UpdatedAt
        });
    }
}
