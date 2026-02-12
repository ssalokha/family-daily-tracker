using FamilyTracker.Application.DTOs;
using FamilyTracker.Application.Interfaces;
using MediatR;

namespace FamilyTracker.Application.Queries.Appointments;

public class GetAppointmentHistoryQueryHandler : IRequestHandler<GetAppointmentHistoryQuery, IEnumerable<DoctorAppointmentDto>>
{
    private readonly IAppointmentRepository _repository;

    public GetAppointmentHistoryQueryHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DoctorAppointmentDto>> Handle(GetAppointmentHistoryQuery request, CancellationToken cancellationToken)
    {
        var appointments = await _repository.GetAllAsync();

        // Apply filters
        var query = appointments.AsQueryable();

        if (!string.IsNullOrEmpty(request.UserId))
        {
            var userId = Guid.Parse(request.UserId);
            query = query.Where(a => a.AppointmentForUserId == userId || a.CreatedByUserId == userId);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(a => a.AppointmentDateTime >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(a => a.AppointmentDateTime <= request.EndDate.Value);
        }

        if (request.IsCompleted.HasValue)
        {
            query = query.Where(a => a.IsCompleted == request.IsCompleted.Value);
        }

        // Order by date descending (most recent first)
        var filteredAppointments = query
            .OrderByDescending(a => a.AppointmentDateTime)
            .ToList();

        return filteredAppointments.Select(a => new DoctorAppointmentDto
        {
            Id = a.Id,
            CreatedByUserId = a.CreatedByUserId,
            CreatedByUserName = a.CreatedByUser?.UserName ?? "Unknown",
            AppointmentForUserId = a.AppointmentForUserId,
            AppointmentForUserName = a.AppointmentForUser?.UserName ?? "Unknown",
            AppointmentDateTime = a.AppointmentDateTime,
            Location = $"{a.Location.Street} {a.Location.BuildingNumber}",
            IsCompleted = a.IsCompleted,
            CreatedAt = a.CreatedAt,
            UpdatedAt = a.UpdatedAt
        });
    }
}
